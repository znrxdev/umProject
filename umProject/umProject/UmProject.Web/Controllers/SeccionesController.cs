using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;
using UmProject.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class SeccionesController : Controller
    {
        private readonly ISeccionService _seccionService;
        private readonly ICatalogoService _catalogoService;
        private readonly IEstadoService _estadoService;
        private readonly IDocenteService _docenteService;
        private readonly IConexionService _conexionService;
        private readonly IPeriodoAcademicoService _periodoAcademicoService;
        private readonly IMateriaService _materiaService;
        private readonly IGrupoService _grupoService;
        private readonly IInscripcionService _inscripcionService;

        public SeccionesController(
            ISeccionService seccionService,
            ICatalogoService catalogoService,
            IEstadoService estadoService,
            IDocenteService docenteService,
            IConexionService conexionService,
            IPeriodoAcademicoService periodoAcademicoService,
            IMateriaService materiaService,
            IGrupoService grupoService,
            IInscripcionService inscripcionService)
        {
            _seccionService = seccionService;
            _catalogoService = catalogoService;
            _estadoService = estadoService;
            _docenteService = docenteService;
            _conexionService = conexionService;
            _periodoAcademicoService = periodoAcademicoService;
            _materiaService = materiaService;
            _grupoService = grupoService;
            _inscripcionService = inscripcionService;
        }

        public async Task<IActionResult> Index(int? idPeriodoAcademico = null)
        {
            ViewData["Title"] = "Gestión de Secciones";
            ViewData["Subtitle"] = "Administración de secciones académicas";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Cargar períodos académicos para el filtro
                var periodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion.Value);
                ViewBag.PeriodosAcademicos = periodos ?? new List<PeriodoAcademico>();

                var secciones = await _seccionService.ListarSeccionesAsync(idSesion.Value, idPeriodoAcademico);
                ViewBag.IdPeriodoSeleccionado = idPeriodoAcademico;
                
                return View(secciones ?? new List<Seccion>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<Seccion>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Sección";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var seccion = await _seccionService.ObtenerSeccionPorIdAsync(id, idSesion);
            if (seccion == null)
            {
                TempData["ErrorMessage"] = "Sección no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(seccion);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nueva Sección";
            ViewData["Subtitle"] = "Registrar nueva sección académica";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 96); // 96 = AGREGAR SECCIÓN
            
            // Establecer estado EN REVISION (4) por defecto
            ViewBag.EstadoRevision = 4;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seccion seccion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            // Remover validación de CodigoSeccion ya que se autogenera en la base de datos
            ModelState.Remove(nameof(seccion.CodigoSeccion));
            // Remover validación de IdEstado ya que se establece automáticamente como EN REVISION (4)
            ModelState.Remove(nameof(seccion.IdEstado));
            
            if (!ModelState.IsValid)
            {
                await CargarCatalogos(idSesion, 96);
                ViewBag.EstadoRevision = 4;
                return View(seccion);
            }
            
            // Asegurar que CodigoSeccion sea null para que la BD lo autogenere
            seccion.CodigoSeccion = null;
            // Asegurar que IdEstado sea 4 (EN REVISION) - aunque el SP lo fuerza, lo establecemos aquí también
            seccion.IdEstado = 4;

            try
            {
                var resultado = await _seccionService.AgregarSeccionAsync(seccion, idSesion);
                if (resultado.Exitoso)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                    await CargarCatalogos(idSesion, 96);
                    ViewBag.EstadoRevision = 4;
                    return View(seccion);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                await CargarCatalogos(idSesion, 96);
                ViewBag.EstadoRevision = 4;
                return View(seccion);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Sección";
            ViewData["Subtitle"] = "Modificar información de sección";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var seccion = await _seccionService.ObtenerSeccionPorIdAsync(id, idSesion);
            if (seccion == null)
            {
                TempData["ErrorMessage"] = "Sección no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            // Asegurar que Activo tenga un valor para el checkbox
            if (seccion.Activo == null)
            {
                seccion.Activo = true;
            }

            await CargarCatalogos(idSesion, 97); // 97 = ACTUALIZAR SECCIÓN
            
            return View(seccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Seccion seccion, int? IdModalidadCatalogo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            
            // Si se seleccionó una modalidad desde catálogo, obtener su texto
            if (IdModalidadCatalogo.HasValue && IdModalidadCatalogo.Value > 0)
            {
                var modalidades = await _catalogoService.ListarCatalogosPorTipoAsync(11, idSesion);
                var modalidadSeleccionada = modalidades?.FirstOrDefault(m => m.IdCatalogo == IdModalidadCatalogo.Value);
                if (modalidadSeleccionada != null)
                {
                    seccion.Modalidad = modalidadSeleccionada.NombreCatalogo;
                }
            }
            
            // Obtener la sección actual para comparar y preservar datos inmutables
            var seccionActual = await _seccionService.ObtenerSeccionPorIdAsync((int)seccion.IdSeccion, idSesion);
            if (seccionActual == null)
            {
                TempData["ErrorMessage"] = "Sección no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            // Bloquear cambio de Materia-Período en edición
            seccion.IdMateriaPeriodo = seccionActual.IdMateriaPeriodo;
            
            // Si se está intentando cambiar de EN REVISION (4) a ACTIVO (1), validar ANTES de procesar
            if (seccionActual != null && seccionActual.IdEstado == 4 && seccion.IdEstado == 1)
            {
                var validacion = await ValidarActivacionSeccionAsync((int)seccion.IdSeccion, idSesion);
                if (!validacion.EsValido)
                {
                    ModelState.AddModelError("IdEstado", validacion.Mensaje);
                    TempData["ErrorMessage"] = validacion.Mensaje;
                    
                    await CargarCatalogos(idSesion, 97);
                    
                    // Restaurar el estado original de la sección
                    seccion.IdEstado = seccionActual.IdEstado;
                    
                    return View(seccion);
                }
            }
            
            if (!ModelState.IsValid)
            {
                await CargarCatalogos(idSesion, 97);
                return View(seccion);
            }

            try
            {
                var resultado = await _seccionService.ActualizarSeccionAsync(seccion, idSesion);
                if (resultado.Exitoso)
                {
                    TempData["SuccessMessage"] = resultado.Mensaje;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = resultado.Mensaje;
                    await CargarCatalogos(idSesion, 97);
                    return View(seccion);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                await CargarCatalogos(idSesion, 97);
                return View(seccion);
            }
        }

        private async Task<(bool EsValido, string Mensaje)> ValidarActivacionSeccionAsync(int idSeccion, int idSesion)
        {
            try
            {
                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_secciones", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 186; // VALIDAR ACTIVACION SECCION
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = idSeccion;
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
                return (false, $"Error al validar la activación de la sección: {ex.Message}");
            }
        }

        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // Cargar tipos de sección (Tipo Catálogo = 15 según umDbData.sql)
            var tiposSeccion = await _catalogoService.ListarCatalogosPorTipoAsync(15, idSesion);
            ViewBag.TiposSeccion = tiposSeccion ?? new List<Catalogo>();

            // Cargar aulas (Tipo Catálogo = 16 según umDbData.sql)
            var aulas = await _catalogoService.ListarCatalogosPorTipoAsync(16, idSesion);
            ViewBag.Aulas = aulas ?? new List<Catalogo>();

            // Cargar modalidades (Tipo Catálogo = 11 según umDbData.sql)
            var modalidades = await _catalogoService.ListarCatalogosPorTipoAsync(11, idSesion);
            ViewBag.Modalidades = modalidades ?? new List<Catalogo>();

            // Cargar estados según el tipo de transacción
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            ViewBag.Estados = estados ?? new List<Estado>();

            // Cargar docentes
            var docentes = await _docenteService.ListarDocentesAsync(idSesion);
            ViewBag.Docentes = docentes ?? new List<Docente>();

            // Cargar materias-períodos de períodos en estado PENDIENTE (3) o EN REVISION (4)
            ViewBag.MateriasPeriodos = await ObtenerMateriasPeriodosDisponiblesAsync(idSesion);
        }

        private async Task<List<object>> ObtenerMateriasPeriodosDisponiblesAsync(int idSesion)
        {
            try
            {
                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Usar transacción 96 para listar materias-períodos activas
                // El procedimiento ya filtra por períodos en estado PENDIENTE (3) o EN REVISION (4)
                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 96; // Listar todas
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                await conexion.OpenAsync();
                
                var materiasCompletas = new List<object>();
                
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // El procedimiento ya trae Codigo_Materia, Nombre_Materia, Nombre_Periodo y Codigo_Periodo
                        var codigoMateria = reader["Codigo_Materia"] as string ?? "N/A";
                        var nombreMateria = reader["Nombre_Materia"] as string ?? "N/A";
                        var nombrePeriodo = reader["Nombre_Periodo"] as string ?? "N/A";
                        var codigoPeriodo = reader["Codigo_Periodo"] as string ?? "N/A";
                        var codigoPlan = reader["Codigo_Plan"] as string ?? string.Empty;
                        
                        materiasCompletas.Add(new {
                            IdMateriaPeriodo = reader["Id_Materia_Periodo"] as int? ?? 0,
                            DisplayText = $"{codigoMateria} - {nombreMateria} ({codigoPeriodo} - {nombrePeriodo})",
                            CodigoPlan = codigoPlan
                        });
                    }
                }
                
                return materiasCompletas;
            }
            catch (Exception ex)
            {
                // En caso de error, retornar lista vacía
                return new List<object>();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPorcentajeAsistenciaMateriaPeriodo(int idMateriaPeriodo)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                
                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Usar transacción 93 para filtrar por ID de materia-período
                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 93; // FILTRAR POR ID MATERIA PERIODO
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = idMateriaPeriodo;
                cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                await conexion.OpenAsync();
                
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var porcentajeAsistenciaMinima = reader["Porcentaje_Asistencia_Minima"] as decimal?;
                        
                        return Json(new { 
                            success = true, 
                            porcentajeAsistenciaMinima = porcentajeAsistenciaMinima.HasValue ? porcentajeAsistenciaMinima.Value : 75.00m
                        });
                    }
                }
                
                return Json(new { success = false, message = "Materia-período no encontrada" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGruposPorMateriaPeriodo(int idMateriaPeriodo, int idSeccion)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                
                // Obtener Id_Periodo_Academico de la materia-período
                int? idPeriodoAcademico = null;
                using (var conexion = _conexionService.ObtenerConexion())
                {
                    using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 93; // FILTRAR POR ID MATERIA PERIODO
                    cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                    cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = idMateriaPeriodo;
                    cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                    await conexion.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            idPeriodoAcademico = reader["Id_Periodo_Academico"] as int?;
                        }
                    }
                }

                if (!idPeriodoAcademico.HasValue)
                {
                    return Json(new { success = false, message = "No se pudo obtener el período académico de la materia-período" });
                }

                // Obtener grupos del período académico
                var grupos = new List<object>();
                var gruposAsignados = new List<object>();

                using (var conexion = _conexionService.ObtenerConexion())
                {
                    // Obtener grupos del período
                    using var cmdGrupos = new SqlCommand("usp_grupos", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdGrupos.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 104; // FILTRAR POR PERIODO
                    cmdGrupos.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                    cmdGrupos.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = idPeriodoAcademico.Value;
                    cmdGrupos.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmdGrupos.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                    await conexion.OpenAsync();
                    using (var reader = await cmdGrupos.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var idGrupo = reader["Id_Grupo"] as int? ?? 0;
                            
                            // Contar inscripciones del grupo
                            int cantidadInscripciones = 0;
                            using (var conexion2 = _conexionService.ObtenerConexion())
                            {
                                using var cmdInscripciones = new SqlCommand("usp_grupos_inscripciones", conexion2)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };
                                cmdInscripciones.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 118; // FILTRAR POR GRUPO
                                cmdInscripciones.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                                cmdInscripciones.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = idGrupo;
                                cmdInscripciones.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                                cmdInscripciones.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                                await conexion2.OpenAsync();
                                using (var readerInscripciones = await cmdInscripciones.ExecuteReaderAsync())
                                {
                                    while (await readerInscripciones.ReadAsync())
                                    {
                                        cantidadInscripciones++;
                                    }
                                }
                            }

                            grupos.Add(new
                            {
                                IdGrupo = idGrupo,
                                CodigoGrupo = reader["Codigo_Grupo"] as string ?? "",
                                NombreGrupo = reader["Nombre_Grupo"] as string ?? "",
                                CodigoPeriodo = reader["Codigo_Periodo"] as string ?? "",
                                CantidadInscripciones = cantidadInscripciones
                            });
                        }
                    }

                    // Obtener grupos ya asignados a esta sección con información completa
                    using var cmdAsignados = new SqlCommand("usp_grupos_secciones", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdAsignados.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 109; // FILTRAR POR SECCION
                    cmdAsignados.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                    cmdAsignados.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = idSeccion;
                    cmdAsignados.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmdAsignados.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                    var gruposAsignadosInfo = new List<(int IdGrupoSeccion, int IdGrupo)>();
                    using (var reader = await cmdAsignados.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var idGrupoSeccion = reader["Id_Grupo_Seccion"] as int? ?? 0;
                            var idGrupoAsignado = reader["Id_Grupo"] as int? ?? 0;
                            gruposAsignadosInfo.Add((idGrupoSeccion, idGrupoAsignado));
                        }
                    }

                    // Obtener información completa de los grupos asignados
                    foreach (var (idGrupoSeccion, idGrupoAsignado) in gruposAsignadosInfo)
                    {
                        using var cmdGrupoAsignado = new SqlCommand("usp_grupos", conexion)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmdGrupoAsignado.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 103; // FILTRAR POR ID GRUPO
                        cmdGrupoAsignado.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                        cmdGrupoAsignado.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = idGrupoAsignado;
                        cmdGrupoAsignado.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmdGrupoAsignado.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                        using (var readerGrupo = await cmdGrupoAsignado.ExecuteReaderAsync())
                        {
                            if (await readerGrupo.ReadAsync())
                            {
                                // Contar inscripciones del grupo asignado
                                int cantidadInscripcionesAsignado = 0;
                                using (var conexion3 = _conexionService.ObtenerConexion())
                                {
                                    using var cmdInscripcionesAsignado = new SqlCommand("usp_grupos_inscripciones", conexion3)
                                    {
                                        CommandType = CommandType.StoredProcedure
                                    };
                                    cmdInscripcionesAsignado.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 118; // FILTRAR POR GRUPO
                                    cmdInscripcionesAsignado.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                                    cmdInscripcionesAsignado.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = idGrupoAsignado;
                                    cmdInscripcionesAsignado.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                                    cmdInscripcionesAsignado.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                                    await conexion3.OpenAsync();
                                    using (var readerInscripcionesAsignado = await cmdInscripcionesAsignado.ExecuteReaderAsync())
                                    {
                                        while (await readerInscripcionesAsignado.ReadAsync())
                                        {
                                            cantidadInscripcionesAsignado++;
                                        }
                                    }
                                }

                                gruposAsignados.Add(new
                                {
                                    IdGrupoSeccion = idGrupoSeccion,
                                    IdGrupo = idGrupoAsignado,
                                    CodigoGrupo = readerGrupo["Codigo_Grupo"] as string ?? "",
                                    NombreGrupo = readerGrupo["Nombre_Grupo"] as string ?? "",
                                    CodigoPeriodo = readerGrupo["Codigo_Periodo"] as string ?? "",
                                    CantidadInscripciones = cantidadInscripcionesAsignado
                                });
                            }
                        }
                    }
                }

                return Json(new { success = true, grupos = grupos, gruposAsignados = gruposAsignados });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AsignarGrupoSeccion(int idSeccion, int idGrupo)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                
                // Obtener Id_Tipo_Vinculo (Tipo Catálogo = 20, usar "PRINCIPAL" como default)
                int idTipoVinculo = 1; // Valor por defecto
                var tipoVinculos = await _catalogoService.ListarCatalogosPorTipoAsync(20, idSesion);
                var tipoVinculoPrincipal = tipoVinculos?.FirstOrDefault(tv => tv.NombreCatalogo?.ToUpper() == "PRINCIPAL");
                if (tipoVinculoPrincipal != null && tipoVinculoPrincipal.IdCatalogo.HasValue)
                {
                    idTipoVinculo = tipoVinculoPrincipal.IdCatalogo.Value;
                }

                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_grupos_secciones", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 105; // AGREGAR GRUPO SECCION
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = idGrupo;
                cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = idSeccion;
                cmd.Parameters.Add("@Id_Tipo_Vinculo", SqlDbType.Int).Value = idTipoVinculo;
                cmd.Parameters.Add("@Prioridad", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@Fecha_Asignacion", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                await conexion.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                var oNum = cmd.Parameters["@o_Num"].Value as int? ?? -1;
                var oMsg = cmd.Parameters["@o_Msg"].Value as string;
                
                // Log para depuración
                System.Diagnostics.Debug.WriteLine($"AsignarGrupoSeccion - oNum: {oNum}, oMsg: {oMsg}");
                
                // Si oMsg es null o vacío, intentar obtener un mensaje más descriptivo
                if (string.IsNullOrWhiteSpace(oMsg))
                {
                    if (oNum > 0)
                    {
                        oMsg = "Grupo asignado exitosamente.";
                    }
                    else if (oNum == -1)
                    {
                        oMsg = "Error al asignar el grupo. Verifique los datos e intente nuevamente.";
                    }
                    else
                    {
                        oMsg = "Error desconocido al asignar el grupo.";
                    }
                }

                if (oNum > 0)
                {
                    return Json(new { success = true, message = oMsg });
                }
                else
                {
                    return Json(new { success = false, message = oMsg });
                }
            }
            catch (Exception ex)
            {
                // Log del error para depuración
                System.Diagnostics.Debug.WriteLine($"Error en AsignarGrupoSeccion: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCupoSeccion(int idSeccion, int cupoMaximo)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                
                // Obtener la sección actual
                var seccion = await _seccionService.ObtenerSeccionPorIdAsync(idSeccion, idSesion);
                if (seccion == null)
                {
                    return Json(new { success = false, message = "Sección no encontrada" });
                }

                // Actualizar solo el cupo máximo
                seccion.CupoMaximo = cupoMaximo;

                // Actualizar la sección usando el servicio existente
                var resultado = await _seccionService.ActualizarSeccionAsync(seccion, idSesion);
                
                if (resultado.Exitoso)
                {
                    return Json(new { success = true, message = "Cupo máximo actualizado exitosamente" });
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

        [HttpGet]
        public async Task<IActionResult> ValidarPeriodoActivo(int idSeccion)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
                
                // Obtener la sección
                var seccion = await _seccionService.ObtenerSeccionPorIdAsync(idSeccion, idSesion);
                if (seccion == null)
                {
                    return Json(new { success = false, message = "Sección no encontrada" });
                }

                // Obtener el período académico de la materia-período
                int? idPeriodoAcademico = null;
                bool? esPeriodoActual = null;
                int? idEstadoPeriodo = null;

                using (var conexion = _conexionService.ObtenerConexion())
                {
                    using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 93; // FILTRAR POR ID MATERIA PERIODO
                    cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                    cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = seccion.IdMateriaPeriodo;
                    cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                    await conexion.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            idPeriodoAcademico = reader["Id_Periodo_Academico"] as int?;
                        }
                    }
                }

                if (idPeriodoAcademico.HasValue)
                {
                    using (var conexion = _conexionService.ObtenerConexion())
                    {
                        using var cmd = new SqlCommand("usp_periodos_academicos", conexion)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 80; // FILTRAR POR ID
                        cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                        cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = idPeriodoAcademico.Value;
                        cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                        await conexion.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                esPeriodoActual = reader["Es_Periodo_Actual"] as bool?;
                                idEstadoPeriodo = reader["Id_Estado"] as int?;
                            }
                        }
                    }
                }

                bool puedeDesasignar = true;
                string mensaje = "";

                if (esPeriodoActual == true || idEstadoPeriodo == 1) // ACTIVO
                {
                    puedeDesasignar = false;
                    mensaje = "No se puede desasignar el grupo porque el período académico está en curso (ACTIVO).";
                }

                return Json(new { 
                    success = true, 
                    puedeDesasignar = puedeDesasignar, 
                    mensaje = mensaje,
                    esPeriodoActual = esPeriodoActual,
                    idEstadoPeriodo = idEstadoPeriodo
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DesasignarGrupoSeccion(int idGrupoSeccion, string motivo)
        {
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

                // Obtener información del grupo-sección para validar
                int? idSeccion = null;
                using (var conexion = _conexionService.ObtenerConexion())
                {
                    using var cmd = new SqlCommand("usp_grupos_secciones", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 107; // FILTRAR POR ID GRUPO SECCION
                    cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                    cmd.Parameters.Add("@Id_Grupo_Seccion", SqlDbType.Int).Value = idGrupoSeccion;
                    cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                    await conexion.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            idSeccion = reader["Id_Seccion"] as int?;
                        }
                    }
                }

                if (!idSeccion.HasValue)
                {
                    return Json(new { success = false, message = "No se pudo encontrar la sección asociada" });
                }

                // Validar que el período académico no esté ACTIVO
                var seccion = await _seccionService.ObtenerSeccionPorIdAsync(idSeccion.Value, idSesion);
                if (seccion == null)
                {
                    return Json(new { success = false, message = "Sección no encontrada" });
                }

                // Obtener el período académico de la materia-período
                int? idPeriodoAcademico = null;
                bool? esPeriodoActual = null;
                int? idEstadoPeriodo = null;

                using (var conexion = _conexionService.ObtenerConexion())
                {
                    using var cmd = new SqlCommand("usp_materias_periodos", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 93; // FILTRAR POR ID MATERIA PERIODO
                    cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                    cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = seccion.IdMateriaPeriodo;
                    cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                    await conexion.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            idPeriodoAcademico = reader["Id_Periodo_Academico"] as int?;
                        }
                    }
                }

                if (idPeriodoAcademico.HasValue)
                {
                    using (var conexion = _conexionService.ObtenerConexion())
                    {
                        using var cmd = new SqlCommand("usp_periodos_academicos", conexion)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 80; // FILTRAR POR ID
                        cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                        cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = idPeriodoAcademico.Value;
                        cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                        await conexion.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                esPeriodoActual = reader["Es_Periodo_Actual"] as bool?;
                                idEstadoPeriodo = reader["Id_Estado"] as int?;
                            }
                        }
                    }
                }

                // Validar que el período académico no esté ACTIVO
                if (esPeriodoActual == true || idEstadoPeriodo == 1) // ACTIVO
                {
                    return Json(new { 
                        success = false, 
                        message = "No se puede desasignar el grupo porque el período académico está en curso (ACTIVO)." 
                    });
                }

                // Desasignar el grupo-sección
                using var conexion2 = _conexionService.ObtenerConexion();
                using var cmd2 = new SqlCommand("usp_grupos_secciones", conexion2)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd2.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 106; // ACTUALIZAR GRUPO SECCION
                cmd2.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
                cmd2.Parameters.Add("@Id_Grupo_Seccion", SqlDbType.Int).Value = idGrupoSeccion;
                cmd2.Parameters.Add("@Activo", SqlDbType.Bit).Value = false;
                cmd2.Parameters.Add("@Fecha_Desasignacion", SqlDbType.DateTime).Value = DateTime.Now;
                cmd2.Parameters.Add("@Motivo_Desasignacion", SqlDbType.NVarChar, 255).Value = string.IsNullOrEmpty(motivo) ? DBNull.Value : motivo;
                cmd2.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd2.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                await conexion2.OpenAsync();
                await cmd2.ExecuteNonQueryAsync();

                var oNum = cmd2.Parameters["@o_Num"].Value as int? ?? -1;
                var oMsg = cmd2.Parameters["@o_Msg"].Value as string ?? "Error desconocido";

                if (oNum >= 0)
                {
                    // Cambiar el estado de la sección a INACTIVO (2)
                    seccion.IdEstado = 2; // INACTIVO
                    await _seccionService.ActualizarSeccionAsync(seccion, idSesion);

                    return Json(new { success = true, message = oMsg });
                }
                else
                {
                    return Json(new { success = false, message = oMsg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Secciones/GetGruposAsignadosConInscripciones/5
        [HttpGet]
        public async Task<IActionResult> GetGruposAsignadosConInscripciones(int idSeccion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null)
            {
                return Json(new { success = false, message = "Sesión no válida" });
            }

            try
            {
                // Obtener grupos asignados a la sección usando stored procedure
                using var conexion = _conexionService.ObtenerConexion();
                using var cmd = new SqlCommand("usp_grupos_secciones", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 109; // FILTRAR POR ID SECCION
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
                cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = idSeccion;
                cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                await conexion.OpenAsync();
                
                var idsGrupos = new List<int>();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var idGrupo = reader["Id_Grupo"] as int? ?? 0;
                        if (idGrupo > 0)
                        {
                            idsGrupos.Add(idGrupo);
                        }
                    }
                }

                // Para cada grupo, obtener sus inscripciones
                var gruposConInscripciones = new List<object>();
                
                foreach (var idGrupo in idsGrupos)
                {
                    var grupo = await _grupoService.ObtenerGrupoPorIdAsync(idGrupo, idSesion.Value);
                    
                    if (grupo != null)
                    {
                        var inscripciones = await _inscripcionService.ListarInscripcionesGrupoAsync(idGrupo, idSesion.Value);
                        
                        gruposConInscripciones.Add(new
                        {
                            idGrupo = grupo.IdGrupo,
                            codigoGrupo = grupo.CodigoGrupo,
                            nombreGrupo = grupo.NombreGrupo,
                            inscripciones = inscripciones ?? new List<GrupoInscripcion>()
                        });
                    }
                }
                
                return PartialView("_GruposAsignadosConInscripciones", gruposConInscripciones);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}

