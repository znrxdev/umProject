using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;
using UmProject.Web.Helpers;
using UmProject.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class PeriodosAcademicosController : Controller
    {
        private readonly IPeriodoAcademicoService _periodoAcademicoService;
        private readonly ICatalogoService _catalogoService;
        private readonly IEstadoService _estadoService;
        private readonly IMateriaService _materiaService;
        private readonly IConexionService _conexionService;

        public PeriodosAcademicosController(
            IPeriodoAcademicoService periodoAcademicoService,
            ICatalogoService catalogoService,
            IEstadoService estadoService,
            IMateriaService materiaService,
            IConexionService conexionService)
        {
            _periodoAcademicoService = periodoAcademicoService;
            _catalogoService = catalogoService;
            _estadoService = estadoService;
            _materiaService = materiaService;
            _conexionService = conexionService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Períodos Académicos";
            ViewData["Subtitle"] = "Administración de períodos académicos";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var periodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion.Value);
                return View(periodos ?? new List<PeriodoAcademico>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<PeriodoAcademico>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Período Académico";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var periodos = await _periodoAcademicoService.FiltrarPeriodoPorIdAsync(id, idSesion);
            if (periodos == null || periodos.Count == 0)
            {
                return NotFound();
            }
            return View(periodos.First());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nuevo Período Académico";
            ViewData["Subtitle"] = "Registrar nuevo período académico";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 78); // 78 = AGREGAR PERIODO ACADEMICO
            
            // Generar código de período automáticamente
            var siguienteCodigo = await GenerarSiguienteCodigoPeriodoAsync(idSesion);
            ViewBag.CodigoPeriodo = siguienteCodigo;
            ViewBag.EstadoRevision = 4; // EN REVISION
            
            // Obtener el período más reciente para validación de fecha de inicio
            var periodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion);
            var periodoMasReciente = periodos?.OrderByDescending(p => p.IdPeriodo).FirstOrDefault();
            
            if (periodoMasReciente != null && !string.IsNullOrEmpty(periodoMasReciente.FechaCierreCalificaciones))
            {
                // La fecha de inicio debe ser después de la fecha de cierre de calificaciones del período más reciente
                var fechaCierreMasReciente = DateTime.Parse(periodoMasReciente.FechaCierreCalificaciones);
                ViewBag.FechaMinimaInicio = fechaCierreMasReciente.AddDays(1).ToString("yyyy-MM-dd");
            }
            else
            {
                // Si no hay período con fecha de cierre, usar la fecha actual
                ViewBag.FechaMinimaInicio = DateTime.Now.ToString("yyyy-MM-dd");
            }
            
            return View();
        }

        private async Task<string> GenerarSiguienteCodigoPeriodoAsync(int idSesion)
        {
            try
            {
                var periodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion);
                
                if (periodos == null || periodos.Count == 0)
                {
                    // Si no hay períodos, empezar con el año actual y I
                    var anioActual = DateTime.Now.Year;
                    return $"{anioActual}-I";
                }

                // Ordenar por Id_Periodo descendente para obtener el último creado
                // Esto asegura que obtenemos el período más reciente
                var ultimoPeriodo = periodos
                    .Where(p => !string.IsNullOrEmpty(p.CodigoPeriodo))
                    .OrderByDescending(p => p.IdPeriodo)
                    .FirstOrDefault();

                if (ultimoPeriodo == null || string.IsNullOrEmpty(ultimoPeriodo.CodigoPeriodo))
                {
                    var anioActual = DateTime.Now.Year;
                    return $"{anioActual}-I";
                }

                // Extraer año y número romano del último período
                var codigo = ultimoPeriodo.CodigoPeriodo.Trim().ToUpper();
                var partes = codigo.Split('-');
                
                if (partes.Length != 2)
                {
                    // Si el formato no es correcto, empezar con el año actual
                    var anioActual = DateTime.Now.Year;
                    return $"{anioActual}-I";
                }

                var anio = partes[0];
                var numeroRomano = partes[1];
                int anioInt;

                if (!int.TryParse(anio, out anioInt))
                {
                    anioInt = DateTime.Now.Year;
                }

                // Determinar el siguiente número romano
                string siguienteRomano;
                int siguienteAnio = anioInt;

                switch (numeroRomano)
                {
                    case "I":
                        siguienteRomano = "II";
                        siguienteAnio = anioInt;
                        break;
                    case "II":
                        siguienteRomano = "III";
                        siguienteAnio = anioInt;
                        break;
                    case "III":
                        siguienteRomano = "I";
                        siguienteAnio = anioInt + 1;
                        break;
                    default:
                        // Si no es I, II o III, empezar con I del mismo año
                        siguienteRomano = "I";
                        siguienteAnio = anioInt;
                        break;
                }

                return $"{siguienteAnio}-{siguienteRomano}";
            }
            catch
            {
                // En caso de error, devolver año actual con I
                var anioActual = DateTime.Now.Year;
                return $"{anioActual}-I";
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeriodoAcademico periodo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            // Forzar estado EN REVISION (4) al crear
            periodo.IdEstado = 4;
            periodo.EsPeriodoActual = false; // Siempre false al crear
            
            // Generar código si no viene o está vacío
            if (string.IsNullOrWhiteSpace(periodo.CodigoPeriodo))
            {
                periodo.CodigoPeriodo = await GenerarSiguienteCodigoPeriodoAsync(idSesion);
            }
            
            // Validaciones de fechas en C# (formato DD/MM/YYYY)
            if (!string.IsNullOrWhiteSpace(periodo.FechaInicio) && !string.IsNullOrWhiteSpace(periodo.FechaFin))
            {
                if (FechaHelper.TryParseFecha(periodo.FechaInicio, out DateTime fechaInicio) && 
                    FechaHelper.TryParseFecha(periodo.FechaFin, out DateTime fechaFin))
                {
                    // Validar que la fecha de fin sea mayor que la fecha de inicio
                    if (fechaFin <= fechaInicio)
                    {
                        ModelState.AddModelError("FechaFin", "La fecha de fin debe ser mayor que la fecha de inicio.");
                    }
                    
                    // Validar que la fecha de cierre de calificaciones sea mayor o igual a la fecha de fin
                    if (!string.IsNullOrWhiteSpace(periodo.FechaCierreCalificaciones) && 
                        FechaHelper.TryParseFecha(periodo.FechaCierreCalificaciones, out DateTime fechaCierre))
                    {
                        if (fechaCierre < fechaFin)
                        {
                            ModelState.AddModelError("FechaCierreCalificaciones", "La fecha de cierre de calificaciones no puede ser menor a la fecha de fin.");
                        }
                    }
                    
                    // Validar que la fecha de inicio no sea menor a la fecha de cierre de calificaciones del período más reciente
                    var periodosExistentes = await _periodoAcademicoService.ListarPeriodosAsync(idSesion);
                    var periodoMasReciente = periodosExistentes?
                        .Where(p => !string.IsNullOrWhiteSpace(p.FechaCierreCalificaciones) && 
                                   FechaHelper.TryParseFecha(p.FechaCierreCalificaciones, out _))
                        .OrderByDescending(p => p.IdPeriodo)
                        .FirstOrDefault();
                    
                    if (periodoMasReciente != null && 
                        !string.IsNullOrWhiteSpace(periodoMasReciente.FechaCierreCalificaciones) &&
                        FechaHelper.TryParseFecha(periodoMasReciente.FechaCierreCalificaciones, out DateTime fechaCierreMasReciente))
                    {
                        if (fechaInicio < fechaCierreMasReciente)
                        {
                            ModelState.AddModelError("FechaInicio", 
                                $"La fecha de inicio no puede ser menor a la fecha de cierre de calificaciones del período más reciente ({FechaHelper.ToFechaString(fechaCierreMasReciente)}).");
                        }
                    }
                    else
                    {
   
                    }
                    
                    // Validar que la fecha de inicio no esté en el transcurso de otro período
                    var periodoSolapado = periodosExistentes?
                        .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.FechaInicio) && 
                                            !string.IsNullOrWhiteSpace(p.FechaFin) &&
                                            FechaHelper.TryParseFecha(p.FechaInicio, out DateTime pFechaInicio) &&
                                            FechaHelper.TryParseFecha(p.FechaFin, out DateTime pFechaFin) &&
                                            fechaInicio >= pFechaInicio && 
                                            fechaInicio <= pFechaFin &&
                                            p.IdPeriodo != periodo.IdPeriodo);
                    
                    if (periodoSolapado != null)
                    {
                        ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede estar en el transcurso de otro período académico.");
                    }
                }
            }
            
            if (ModelState.IsValid)
            {
                var resultado = await _periodoAcademicoService.AgregarPeriodoAsync(periodo, idSesion);
                
                if (resultado.Codigo != -1)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    // Guardar el ID del período creado para mostrar la notificación de agregar materias
                    TempData["PeriodoCreadoId"] = resultado.Codigo;
                    TempData["PeriodoCreadoNombre"] = periodo.NombrePeriodo;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", resultado.Mensaje);
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }
            
            ViewData["Title"] = "Nuevo Período Académico";
            ViewData["Subtitle"] = "Registrar nuevo período académico";
            await CargarCatalogos(idSesion, 78);
            
            return View(periodo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string? tab = null)
        {
            ViewData["Title"] = "Editar Período Académico";
            ViewData["Subtitle"] = "Modificar información de período académico";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var periodos = await _periodoAcademicoService.FiltrarPeriodoPorIdAsync(id, idSesion);
            if (periodos == null || periodos.Count == 0)
            {
                return NotFound();
            }
            
            var periodo = periodos.First();
            periodo.EsPeriodoActual = periodo.EsPeriodoActual ?? false;
            
            // Convertir fechas de DD/MM/YYYY a YYYY-MM-DD para campos type="date"
            if (!string.IsNullOrWhiteSpace(periodo.FechaInicio) && FechaHelper.TryParseFecha(periodo.FechaInicio, out DateTime fechaInicio))
            {
                periodo.FechaInicio = fechaInicio.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(periodo.FechaFin) && FechaHelper.TryParseFecha(periodo.FechaFin, out DateTime fechaFin))
            {
                periodo.FechaFin = fechaFin.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(periodo.FechaCierreCalificaciones) && FechaHelper.TryParseFecha(periodo.FechaCierreCalificaciones, out DateTime fechaCierre))
            {
                periodo.FechaCierreCalificaciones = fechaCierre.ToString("yyyy-MM-dd");
            }
            
            await CargarCatalogos(idSesion, 79); // 79 = ACTUALIZAR PERIODO ACADEMICO
            
            // Pasar el tab activo a la vista
            ViewBag.ActiveTab = tab ?? "informacion";
            
            return View(periodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PeriodoAcademico periodo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            if (id != periodo.IdPeriodo)
            {
                return NotFound();
            }

            // Obtener el período actual para comparar el estado
            var periodosActuales = await _periodoAcademicoService.FiltrarPeriodoPorIdAsync(id, idSesion);
            var periodoActual = periodosActuales?.FirstOrDefault();
            
            // Si se está intentando cambiar de EN REVISION (4) a ACTIVO (1), validar ANTES de procesar
            if (periodoActual != null && periodoActual.IdEstado == 4 && periodo.IdEstado == 1)
            {
                var validacion = await ValidarActivacionPeriodoAsync(id, idSesion);
                if (!validacion.EsValido)
                {
                    ModelState.AddModelError("IdEstado", validacion.Mensaje);
                    TempData["ErrorMessage"] = validacion.Mensaje;
                    
                    ViewData["Title"] = "Editar Período Académico";
                    ViewData["Subtitle"] = "Modificar información de período académico";
                    await CargarCatalogos(idSesion, 79);
                    
                    // Convertir fechas de DD/MM/YYYY a YYYY-MM-DD para campos type="date"
                    if (periodoActual != null)
                    {
                        if (!string.IsNullOrWhiteSpace(periodoActual.FechaInicio) && FechaHelper.TryParseFecha(periodoActual.FechaInicio, out DateTime fechaInicio))
                        {
                            periodo.FechaInicio = fechaInicio.ToString("yyyy-MM-dd");
                        }
                        if (!string.IsNullOrWhiteSpace(periodoActual.FechaFin) && FechaHelper.TryParseFecha(periodoActual.FechaFin, out DateTime fechaFin))
                        {
                            periodo.FechaFin = fechaFin.ToString("yyyy-MM-dd");
                        }
                        if (!string.IsNullOrWhiteSpace(periodoActual.FechaCierreCalificaciones) && FechaHelper.TryParseFecha(periodoActual.FechaCierreCalificaciones, out DateTime fechaCierre))
                        {
                            periodo.FechaCierreCalificaciones = fechaCierre.ToString("yyyy-MM-dd");
                        }
                    }
                    
                    // Restaurar el estado original del período
                    periodo.IdEstado = periodoActual.IdEstado;
                    
                    return View(periodo);
                }
            }

            if (ModelState.IsValid)
            {
                periodo.EsPeriodoActual = periodo.EsPeriodoActual ?? false;
                
                // No permitir modificar las fechas - usar las fechas del período actual
                if (periodoActual != null)
                {
                    periodo.FechaInicio = periodoActual.FechaInicio;
                    periodo.FechaFin = periodoActual.FechaFin;
                    periodo.FechaCierreCalificaciones = periodoActual.FechaCierreCalificaciones;
                }
                
                var resultado = await _periodoAcademicoService.ActualizarPeriodoAsync(periodo, idSesion);
                
                if (resultado.Codigo != -1)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", resultado.Mensaje);
                    TempData["ErrorMessage"] = resultado.Mensaje;
                }
            }
            
            ViewData["Title"] = "Editar Período Académico";
            ViewData["Subtitle"] = "Modificar información de período académico";
            await CargarCatalogos(idSesion, 79);
            
            // Convertir fechas de DD/MM/YYYY a YYYY-MM-DD para campos type="date" si vienen del modelo
            if (periodoActual != null)
            {
                if (!string.IsNullOrWhiteSpace(periodoActual.FechaInicio) && FechaHelper.TryParseFecha(periodoActual.FechaInicio, out DateTime fechaInicio))
                {
                    periodo.FechaInicio = fechaInicio.ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(periodoActual.FechaFin) && FechaHelper.TryParseFecha(periodoActual.FechaFin, out DateTime fechaFin))
                {
                    periodo.FechaFin = fechaFin.ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(periodoActual.FechaCierreCalificaciones) && FechaHelper.TryParseFecha(periodoActual.FechaCierreCalificaciones, out DateTime fechaCierre))
                {
                    periodo.FechaCierreCalificaciones = fechaCierre.ToString("yyyy-MM-dd");
                }
            }
            
            return View(periodo);
        }

        private async Task<(bool EsValido, string Mensaje)> ValidarActivacionPeriodoAsync(int idPeriodo, int idSesion)
        {
            try
            {
                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_periodos_academicos", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 185; // VALIDAR ACTIVACION PERIODO ACADEMICO
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = idPeriodo;
                cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output; // -1 = MAX para mensajes largos
                
                await conexion.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                await conexion.CloseAsync();
                
                var codigoResultadoParam = cmd.Parameters["@o_Num"].Value;
                var codigoResultado = codigoResultadoParam == DBNull.Value ? -1 : Convert.ToInt32(codigoResultadoParam);
                var mensaje = cmd.Parameters["@o_Msg"].Value?.ToString() ?? "Error desconocido";
                
                if (codigoResultado == -1)
                {
                    return (false, mensaje);
                }
                
                return (true, mensaje);
            }
            catch (Exception ex)
            {
                return (false, $"Error al validar la activación del período: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMateriasDisponibles(int? idPeriodo = null)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                var todasLasMaterias = await _materiaService.ListarMateriasAsync(idSesion);
                
                // Si se proporciona un idPeriodo, filtrar las materias que ya están asignadas
                if (idPeriodo.HasValue && idPeriodo.Value > 0)
                {
                    var materiasPeriodo = new List<int>();
                    
                    using var conexion = _conexionService.ObtenerConexion();
                    using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 95; // Filtrar por período
                    cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                    cmd.Parameters.Add("@Id_Periodo_Academico", SqlDbType.Int).Value = idPeriodo.Value;
                    cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                    await conexion.OpenAsync();
                    
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var idMateria = reader["Id_Materia"] as int? ?? 0;
                            if (idMateria > 0)
                            {
                                materiasPeriodo.Add(idMateria);
                            }
                        }
                    }
                    
                    // Filtrar las materias que ya están asignadas
                    var materiasDisponibles = todasLasMaterias
                        .Where(m => m.IdMateria.HasValue && !materiasPeriodo.Contains(m.IdMateria.Value))
                        .Select(m => new { 
                            idMateria = m.IdMateria, 
                            codigoMateria = m.CodigoMateria, 
                            nombreMateria = m.NombreMateria 
                        })
                        .ToList();
                    
                    return Json(new { success = true, materias = materiasDisponibles });
                }
                else
                {
                    // Si no se proporciona idPeriodo, retornar todas las materias
                    var materiasJson = todasLasMaterias.Select(m => new { 
                        idMateria = m.IdMateria, 
                        codigoMateria = m.CodigoMateria, 
                        nombreMateria = m.NombreMateria 
                    }).ToList();
                    return Json(new { success = true, materias = materiasJson });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMateriasPeriodo(int idPeriodo)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                var materiasPeriodo = new List<Dictionary<string, object>>();
                
                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 95; // Filtrar por período
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@Id_Periodo_Academico", SqlDbType.Int).Value = idPeriodo;
                cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                await conexion.OpenAsync();
                
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        materiasPeriodo.Add(new Dictionary<string, object>
                        {
                            { "idMateriaPeriodo", reader["Id_Materia_Periodo"] as int? ?? 0 },
                            { "idMateria", reader["Id_Materia"] as int? ?? 0 },
                            { "idPeriodoAcademico", reader["Id_Periodo_Academico"] as int? ?? 0 },
                            { "codigoPlan", reader["Codigo_Plan"] as string ?? string.Empty },
                            { "idJornada", reader["Id_Jornada"] as int? },
                            { "modalidad", reader["Modalidad"] as string },
                            { "horasTeoricas", reader["Horas_Teoricas"] as int? ?? 0 },
                            { "horasPracticas", reader["Horas_Practicas"] as int? ?? 0 },
                            { "porcentajeAsistenciaMinima", reader["Porcentaje_Asistencia_Minima"] as decimal? },
                            { "activo", reader["Activo"] as bool? ?? false }
                        });
                    }
                }
                
                // Obtener nombres de materias y jornadas
                var materias = await _materiaService.ListarMateriasAsync(idSesion);
                var jornadas = await _catalogoService.ListarCatalogosPorTipoAsync(14, idSesion);
                
                var materiasCompletas = materiasPeriodo.Cast<Dictionary<string, object>>().Select(mp => {
                    var idMateria = (int)mp["idMateria"];
                    var idJornada = mp["idJornada"] as int?;
                    
                    var materia = materias.FirstOrDefault(m => m.IdMateria == idMateria);
                    var jornada = jornadas?.FirstOrDefault(j => j.IdCatalogo == idJornada);
                    
                    return new {
                        idMateriaPeriodo = mp["idMateriaPeriodo"],
                        idMateria = idMateria,
                        codigoMateria = materia?.CodigoMateria ?? "N/A",
                        nombreMateria = materia?.NombreMateria ?? "N/A",
                        codigoPlan = mp["codigoPlan"],
                        nombreJornada = jornada?.NombreCatalogo ?? "N/A",
                        modalidad = mp["modalidad"]?.ToString() ?? "N/A",
                        horasTeoricas = mp["horasTeoricas"],
                        horasPracticas = mp["horasPracticas"],
                        porcentajeAsistenciaMinima = mp["porcentajeAsistenciaMinima"],
                        activo = mp["activo"]
                    };
                }).ToList();
                
                return Json(new { success = true, materias = materiasCompletas });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarMateriaPeriodo([FromBody] System.Text.Json.JsonElement jsonElement)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                
                // Extraer valores del JsonElement
                var idMateria = jsonElement.GetProperty("IdMateria").GetInt32();
                var idPeriodoAcademico = jsonElement.GetProperty("IdPeriodoAcademico").GetInt32();
                
                // Codigo_Plan siempre será "PLAN-AÑO ACTUAL"
                var anioActual = DateTime.Now.Year;
                var codigoPlan = $"PLAN-{anioActual}";
                
                // Id_Jornada siempre será NULL (no se envía)
                
                // Modalidad siempre será "Hibrida"
                var modalidad = jsonElement.TryGetProperty("Modalidad", out var modalidadElement) && modalidadElement.ValueKind != System.Text.Json.JsonValueKind.Null 
                    ? modalidadElement.GetString() : "Hibrida";
                
                // Si viene vacío o null, usar "Hibrida" por defecto
                if (string.IsNullOrWhiteSpace(modalidad))
                {
                    modalidad = "Hibrida";
                }
                
                // Horas Teóricas y Horas Prácticas siempre serán 0
                var horasTeoricas = 0;
                var horasPracticas = 0;
                
                // Porcentaje_Asistencia_Minima por defecto 75% si no se proporciona
                var porcentajeAsistenciaMinima = jsonElement.TryGetProperty("PorcentajeAsistenciaMinima", out var porcentajeElement) && porcentajeElement.ValueKind != System.Text.Json.JsonValueKind.Null 
                    ? porcentajeElement.GetDecimal() : 75.00m;
                
                // Validar que el porcentaje de asistencia mínima sea >= 50%
                if (porcentajeAsistenciaMinima < 50.00m)
                {
                    return Json(new { success = false, message = "El porcentaje de asistencia mínima debe ser al menos 50%" });
                }
                
                var observaciones = jsonElement.TryGetProperty("Observaciones", out var observacionesElement) && observacionesElement.ValueKind != System.Text.Json.JsonValueKind.Null 
                    ? observacionesElement.GetString() : null;
                
                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 91; // Agregar (91 = AGREGAR, 92 = ACTUALIZAR)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@Id_Materia", SqlDbType.Int).Value = idMateria;
                cmd.Parameters.Add("@Id_Periodo_Academico", SqlDbType.Int).Value = idPeriodoAcademico;
                cmd.Parameters.Add("@Codigo_Plan", SqlDbType.VarChar, 30).Value = codigoPlan;
                cmd.Parameters.Add("@Id_Jornada", SqlDbType.Int).Value = DBNull.Value; // Siempre NULL
                cmd.Parameters.Add("@Modalidad", SqlDbType.NVarChar, 50).Value = (object)modalidad; // Siempre "Hibrida"
                cmd.Parameters.Add("@Horas_Teoricas", SqlDbType.Int).Value = horasTeoricas;
                cmd.Parameters.Add("@Horas_Practicas", SqlDbType.Int).Value = horasPracticas;
                cmd.Parameters.Add("@Porcentaje_Asistencia_Minima", SqlDbType.Decimal).Value = porcentajeAsistenciaMinima;
                cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = 1; // ACTIVO por defecto
                cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = observaciones != null ? (object)observaciones : DBNull.Value;
                cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                await conexion.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                var resultado = RepositorioHelper.ObtenerResultado(cmd);
                
                if (resultado.Codigo != -1)
                {
                    return Json(new { success = true, message = resultado.Mensaje });
                }
                else
                {
                    return Json(new { success = false, message = resultado.Mensaje });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEstadoMateriaPeriodo([FromBody] System.Text.Json.JsonElement jsonElement)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                
                // Extraer valores del JsonElement
                var idMateriaPeriodo = jsonElement.GetProperty("IdMateriaPeriodo").GetInt32();
                var activo = jsonElement.GetProperty("Activo").GetBoolean();
                
                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 92; // Actualizar (92 = ACTUALIZAR)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = idMateriaPeriodo;
                cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = activo;
                cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                await conexion.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                var resultado = RepositorioHelper.ObtenerResultado(cmd);
                
                if (resultado.Codigo != -1)
                {
                    return Json(new { success = true, message = resultado.Mensaje });
                }
                else
                {
                    return Json(new { success = false, message = resultado.Mensaje });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // Tipo de Período (Id_Tipo_Catalogo = 10 según umDbData.sql)
            ViewBag.TiposPeriodo = await _catalogoService.ListarCatalogosPorTipoAsync(10, idSesion);
            
            // Estados según la transacción (AGREGAR = 78, ACTUALIZAR = 79)
            ViewBag.Estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            
            // Jornadas (Id_Tipo_Catalogo = 14)
            ViewBag.Jornadas = await _catalogoService.ListarCatalogosPorTipoAsync(14, idSesion);
        }
    }
}

