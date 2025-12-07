using Microsoft.AspNetCore.Mvc;
using UmProject.Business;
using UmProject.Entities;
using UmProject.Web.Filters;
using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Data;

namespace UmProject.Web.Controllers
{
    [RequireSession]
    public class EvaluacionesController : Controller
    {
        private readonly IEvaluacionAlumnoService _evaluacionAlumnoService;
        private readonly IInscripcionService _inscripcionService;
        private readonly IEstadoService _estadoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IEvaluacionInstanciaService _evaluacionInstanciaService;
        private readonly IPeriodoAcademicoService _periodoAcademicoService;
        private readonly ISeccionService _seccionService;
        private readonly IGrupoService _grupoService;
        private readonly IEvaluacionModeloService _evaluacionModeloService;
        private readonly IUsuarioRolService _usuarioRolService;
        private readonly IConexionService _conexionService;
        private readonly IMateriaService _materiaService;

        public EvaluacionesController(
            IEvaluacionAlumnoService evaluacionAlumnoService,
            IInscripcionService inscripcionService,
            IEstadoService estadoService,
            IUsuarioService usuarioService,
            IEvaluacionInstanciaService evaluacionInstanciaService,
            IPeriodoAcademicoService periodoAcademicoService,
            ISeccionService seccionService,
            IGrupoService grupoService,
            IEvaluacionModeloService evaluacionModeloService,
            IUsuarioRolService usuarioRolService,
            IConexionService conexionService,
            IMateriaService materiaService)
        {
            _evaluacionAlumnoService = evaluacionAlumnoService;
            _inscripcionService = inscripcionService;
            _estadoService = estadoService;
            _usuarioService = usuarioService;
            _evaluacionInstanciaService = evaluacionInstanciaService;
            _periodoAcademicoService = periodoAcademicoService;
            _seccionService = seccionService;
            _grupoService = grupoService;
            _evaluacionModeloService = evaluacionModeloService;
            _usuarioRolService = usuarioRolService;
            _conexionService = conexionService;
            _materiaService = materiaService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Gestión de Evaluaciones";
            ViewData["Subtitle"] = "Administración de evaluaciones académicas";
            
            try
            {
                var idSesion = HttpContext.Session.GetInt32("IdSesion");
                if (idSesion == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var evaluaciones = await _evaluacionAlumnoService.ListarEvaluacionesAlumnoAsync(idSesion.Value);
                return View(evaluaciones ?? new List<EvaluacionAlumno>());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<EvaluacionAlumno>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalles de Evaluación";
            ViewData["Subtitle"] = "Información detallada";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            var evaluacion = await _evaluacionAlumnoService.ObtenerEvaluacionAlumnoPorIdAsync(id, idSesion);
            if (evaluacion == null)
            {
                TempData["ErrorMessage"] = "Evaluación no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(evaluacion);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nueva Evaluación";
            ViewData["Subtitle"] = "Registrar nueva evaluación académica";
            
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;
            await CargarCatalogos(idSesion, 128); // 128 = AGREGAR EVALUACION ALUMNO
            
            // Obtener períodos en curso
            var periodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion);
            ViewBag.PeriodosEnCurso = periodos?.Where(p => p.EsPeriodoActual == true).ToList() ?? new List<PeriodoAcademico>();
            
            // Obtener usuario actual y sus roles (IdSesion = IdUsuario según AccountController)
            var idUsuarioActual = idSesion;
            var rolesUsuario = await _usuarioRolService.ListarRolesPorUsuarioAsync(idUsuarioActual, idSesion);
            ViewBag.RolesUsuario = rolesUsuario?.Select(r => r.IdRol).Where(r => r.HasValue).Select(r => r.Value).ToList() ?? new List<int>();
            
            var usuarioActual = await _usuarioService.FiltrarUsuariosPorIdAsync(idUsuarioActual, idSesion);
            ViewBag.UsuarioActual = usuarioActual?.FirstOrDefault();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EvaluacionAlumno evaluacionAlumno)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion")!.Value;

            // Campos autogenerados/ocultos
            ModelState.Remove(nameof(evaluacionAlumno.CodigoRegistro)); // Se genera en BD
            evaluacionAlumno.CodigoRegistro = null;
            // IdEstado lo fijaremos nosotros según reglas; quitar validación previa
            ModelState.Remove(nameof(evaluacionAlumno.IdEstado));

            if (evaluacionAlumno.IdEvaluacionInstancia.HasValue)
            {
                // Obtener información de la instancia de evaluación
                var instancia = await _evaluacionInstanciaService.ObtenerEvaluacionInstanciaPorIdAsync(evaluacionAlumno.IdEvaluacionInstancia.Value, idSesion);
                
                bool requiereRevisionInterna = false;
                decimal puntajeMaximo = 0;
                
                if (instancia != null)
                {
                    requiereRevisionInterna = instancia.RequiereRevisionInterna;
                    puntajeMaximo = instancia.CalificacionMaxima;
                    
                    // Validar que el puntaje obtenido no sea mayor al máximo
                    if (puntajeMaximo > 0 && evaluacionAlumno.PuntajeObtenido > puntajeMaximo)
                    {
                        ModelState.AddModelError("PuntajeObtenido", $"El puntaje obtenido no puede ser mayor al puntaje máximo ({puntajeMaximo})");
                    }
                    
                    // Calcular porcentaje logrado automáticamente
                    if (puntajeMaximo > 0)
                    {
                        evaluacionAlumno.PorcentajeLogrado = (evaluacionAlumno.PuntajeObtenido / puntajeMaximo) * 100;
                    }
                    
                    // Lógica de estado según RequiereRevisionInterna (estado fijo EN REVISION = 4)
                    if (requiereRevisionInterna)
                    {
                        evaluacionAlumno.IdEstado = 4;
                    }
                    
                    // Lógica de fechas según estado
                    var estadoSeleccionado = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(128, idSesion);
                    var estadoNombre = estadoSeleccionado?.FirstOrDefault(e => e.IdEstado == evaluacionAlumno.IdEstado)?.NombreEstado?.ToUpper() ?? "";
                    
                    if (estadoNombre == "EN REVISION" || requiereRevisionInterna)
                    {
                        // Si está EN REVISIÓN: FechaValidacion = GETDATE(), Validador bloqueado
                        evaluacionAlumno.FechaValidacion = DateTime.Now;
                        
                        // Si no hay validador seleccionado, fecha de publicación = NULL
                        if (!evaluacionAlumno.IdUsuarioValidador.HasValue || evaluacionAlumno.IdUsuarioValidador.Value == 0)
                        {
                            evaluacionAlumno.FechaPublicacion = null;
                        }
                    }
                    else if (estadoNombre == "ACTIVO")
                    {
                        // Si está ACTIVO: Fecha de publicación = hoy
                        evaluacionAlumno.FechaPublicacion = DateTime.Now;
                    }
                    else if (estadoNombre == "PENDIENTE")
                    {
                        // Si está PENDIENTE: Fecha de publicación debe ser mayor al día actual
                        if (evaluacionAlumno.FechaPublicacion.HasValue && evaluacionAlumno.FechaPublicacion.Value.Date <= DateTime.Now.Date)
                        {
                            ModelState.AddModelError("FechaPublicacion", "La fecha de publicación debe ser mayor al día actual");
                        }
                    }
                }

                // Establecer evaluador siempre como el usuario en sesión
                evaluacionAlumno.IdUsuarioEvaluador = idSesion;

                // Revalidar después de setear valores calculados
                ModelState.Clear();
                TryValidateModel(evaluacionAlumno);

                if (ModelState.IsValid)
                {
                    var resultado = await _evaluacionAlumnoService.AgregarEvaluacionAlumnoAsync(evaluacionAlumno, idSesion);

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
            }

            ViewData["Title"] = "Nueva Evaluación";
            ViewData["Subtitle"] = "Registrar nueva evaluación académica";
            await CargarCatalogos(idSesion, 128);
            
            // Recargar datos para la vista
            var periodos = await _periodoAcademicoService.ListarPeriodosAsync(idSesion);
            ViewBag.PeriodosEnCurso = periodos?.Where(p => p.EsPeriodoActual == true).ToList() ?? new List<PeriodoAcademico>();
            
            var idUsuarioActualVista = idSesion;
            var rolesUsuarioVista = await _usuarioRolService.ListarRolesPorUsuarioAsync(idUsuarioActualVista, idSesion);
            ViewBag.RolesUsuario = rolesUsuarioVista?.Select(r => r.IdRol).Where(r => r.HasValue).Select(r => r.Value).ToList() ?? new List<int>();
            
            var usuarioActualVista = await _usuarioService.FiltrarUsuariosPorIdAsync(idUsuarioActualVista, idSesion);
            ViewBag.UsuarioActual = usuarioActualVista?.FirstOrDefault();
            
            // Registrar errores de ModelState para depuración en la vista
            var errores = ModelState.Where(x => x.Value?.Errors.Count > 0)
                .Select(x => $"{x.Key}: {string.Join(", ", x.Value!.Errors.Select(e => e.ErrorMessage))}")
                .ToList();
            if (errores.Any())
            {
                TempData["ErrorMessage"] = string.Join(" | ", errores);
            }

            return View(evaluacionAlumno);
        }


        // Endpoints AJAX para carga cascada
        [HttpGet]
        public async Task<IActionResult> ObtenerMateriasPorPeriodo(int idPeriodo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null) return Json(new { success = false, message = "Sesión no válida" });

            try
            {
                // Usar stored procedure directamente para obtener materias-períodos
                var materiasPeriodos = await ObtenerMateriasPeriodosPorPeriodoAsync(idPeriodo, idSesion.Value);
                return Json(new { success = true, data = materiasPeriodos });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSeccionesPorMateriaPeriodo(int idMateriaPeriodo)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null) return Json(new { success = false, message = "Sesión no válida" });

            try
            {
                var secciones = await _seccionService.ListarSeccionesAsync(idSesion.Value);
                var seccionesFiltradas = secciones?.Where(s => s.IdMateriaPeriodo == idMateriaPeriodo).ToList() ?? new List<Seccion>();
                
                var resultado = seccionesFiltradas.Select(s => new {
                    idSeccion = s.IdSeccion,
                    codigoSeccion = s.CodigoSeccion,
                    nombreMateria = s.NombreMateria,
                    idMateriaPeriodo = s.IdMateriaPeriodo
                }).ToList();
                
                return Json(new { success = true, data = resultado });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerGruposPorSeccion(int idSeccion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null) return Json(new { success = false, message = "Sesión no válida" });

            try
            {
                // Usar stored procedure usp_grupos_secciones con transacción 109
                var grupos = await ObtenerGruposPorSeccionAsync(idSeccion, idSesion.Value);
                return Json(new { success = true, data = grupos });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEstudiantesPorGrupo(int idGrupo, int idSeccion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null) return Json(new { success = false, message = "Sesión no válida" });

            try
            {
                // Obtener inscripciones del grupo que pertenecen a la sección seleccionada
                var estudiantes = await ObtenerEstudiantesPorGrupoYSeccionAsync(idGrupo, idSeccion, idSesion.Value);
                return Json(new { success = true, data = estudiantes });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerInstanciasEvaluacionPorSeccion(int idSeccion)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null) return Json(new { success = false, message = "Sesión no válida" });

            try
            {
            var instancias = await _evaluacionInstanciaService.ListarEvaluacionesInstanciasAsync(idSesion.Value);
            // Filtrar: instancias ACTIVAS (Id_Estado = 1) de la sección seleccionada
            var instanciasFiltradas = instancias?
                .Where(i => i.IdSeccion == idSeccion &&
                            i.IdEstado == 1) // ACTIVO
                .Select(i => new {
                    idEvaluacionInstancia = i.IdEvaluacionInstancia,
                    codigoInstancia = i.CodigoInstancia,
                    nombreModeloEvaluacion = i.NombreModeloEvaluacion,
                    codigoModelo = i.CodigoModelo,
                    requiereRevisionInterna = i.RequiereRevisionInterna,
                    calificacionMaxima = i.CalificacionMaxima
                })
                .ToList<object>();
                    
                return Json(new { success = true, data = instanciasFiltradas ?? new List<object>() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerModeloEvaluacion(int idEvaluacionInstancia)
        {
            var idSesion = HttpContext.Session.GetInt32("IdSesion");
            if (idSesion == null) return Json(new { success = false, message = "Sesión no válida" });

            try
            {
                var instancia = await _evaluacionInstanciaService.ObtenerEvaluacionInstanciaPorIdAsync(idEvaluacionInstancia, idSesion.Value);
                if (instancia == null)
                {
                    return Json(new { success = false, message = "Instancia no encontrada" });
                }

                var modelo = instancia.IdEvaluacionModelo.HasValue
                    ? await _evaluacionModeloService.ObtenerEvaluacionModeloPorIdAsync(instancia.IdEvaluacionModelo.Value, idSesion.Value)
                    : null;
                return Json(new { 
                    success = true, 
                    data = new {
                        idEvaluacionModelo = modelo?.IdEvaluacionModelo,
                        calificacionMaxima = instancia.CalificacionMaxima,
                        nombreEvaluacion = modelo?.NombreEvaluacion ?? instancia.NombreModeloEvaluacion
                    },
                    requiereRevisionInterna = instancia.RequiereRevisionInterna 
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Métodos auxiliares privados para obtener datos de stored procedures
        private async Task<List<object>> ObtenerMateriasPeriodosPorPeriodoAsync(int idPeriodo, int idSesion)
        {
            var materias = new List<object>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_materias_periodos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 95; // FILTRAR POR ID PERIODO
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Periodo_Academico", SqlDbType.Int).Value = idPeriodo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            var materiasPeriodosLeidas = new List<(int IdMateriaPeriodo, int IdMateria, string? CodigoPlan)>();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    materiasPeriodosLeidas.Add((
                        reader["Id_Materia_Periodo"] as int? ?? 0,
                        reader["Id_Materia"] as int? ?? 0,
                        reader["Codigo_Plan"] as string
                    ));
                }
            }
            
            await conexion.CloseAsync();

            // Obtener información de las materias
            foreach (var mp in materiasPeriodosLeidas)
            {
                var nombreMateria = await ObtenerNombreMateriaAsync(mp.IdMateria, idSesion);
                
                materias.Add(new {
                    idMateriaPeriodo = mp.IdMateriaPeriodo,
                    idMateria = mp.IdMateria,
                    nombreMateria = nombreMateria,
                    codigoPlan = mp.CodigoPlan
                });
            }

            return materias;
        }

        private async Task<List<object>> ObtenerGruposPorSeccionAsync(int idSeccion, int idSesion)
        {
            var grupos = new List<object>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_grupos_secciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 109; // FILTRAR POR ID SECCION
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
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
                    idsGrupos.Add(idGrupo);
                }
            }

            // Obtener información completa de los grupos
            foreach (var idGrupo in idsGrupos)
            {
                var grupo = await _grupoService.ObtenerGrupoPorIdAsync(idGrupo, idSesion);
                if (grupo != null)
                {
                    grupos.Add(new {
                        idGrupo = grupo.IdGrupo,
                        codigoGrupo = grupo.CodigoGrupo,
                        nombreGrupo = grupo.NombreGrupo
                    });
                }
            }

            return grupos;
        }

        private async Task<List<object>> ObtenerEstudiantesPorGrupoYSeccionAsync(int idGrupo, int idSeccion, int idSesion)
        {
            var estudiantes = new List<object>();
            var estudiantesUnicos = new HashSet<int>(); // Para evitar duplicados por Id_Estudiante
            
            // Paso 1: Obtener grupos-inscripciones por grupo
            var idsInscripciones = new List<int>();
            using var conexion1 = _conexionService.ObtenerConexion();
            using var cmd1 = new SqlCommand("usp_grupos_inscripciones", conexion1)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd1.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 118; // FILTRAR POR ID GRUPO
            cmd1.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd1.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = idGrupo;
            cmd1.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd1.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion1.OpenAsync();
            
            using (var reader = await cmd1.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var idInscripcion = reader["Id_Inscripcion"] as int? ?? 0;
                    if (idInscripcion > 0)
                    {
                        idsInscripciones.Add(idInscripcion);
                    }
                }
            }
            await conexion1.CloseAsync();

            // Paso 2: Obtener información completa de las inscripciones
            // Nota: Ya no filtramos por IdSeccion directamente, las inscripciones se relacionan con secciones a través de grupos
            foreach (var idInscripcion in idsInscripciones)
            {
                var inscripcion = await _inscripcionService.ObtenerInscripcionPorIdAsync(idInscripcion, idSesion);
                if (inscripcion != null)
                {
                    // Evitar duplicados por estudiante (si un estudiante tiene múltiples inscripciones en la misma sección)
                    if (!estudiantesUnicos.Contains(inscripcion.IdEstudiante ?? 0))
                    {
                        estudiantesUnicos.Add(inscripcion.IdEstudiante ?? 0);
                        estudiantes.Add(new {
                            idInscripcion = inscripcion.IdInscripcion,
                            codigoInscripcion = inscripcion.CodigoInscripcion,
                            estudianteNombre = inscripcion.EstudianteNombre,
                            estudianteUsuario = inscripcion.EstudianteUsuario
                        });
                    }
                }
            }

            return estudiantes;
        }

        private async Task<string> ObtenerNombreMateriaAsync(int idMateria, int idSesion)
        {
            try
            {
                var materias = await _materiaService.FiltrarMateriaPorIdAsync(idMateria, idSesion);
                return materias?.FirstOrDefault()?.NombreMateria ?? "";
            }
            catch
            {
                return "";
            }
        }


        private async Task CargarCatalogos(int idSesion, int idTipoTransaccion)
        {
            // Cargar Estados según el tipo de transacción
            var estados = await _estadoService.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
            ViewBag.Estados = estados ?? new List<Estado>();

            // Cargar Usuarios Evaluadores y Validadores
            var usuarios = await _usuarioService.ListarUsuariosAsync(idSesion);
            ViewBag.Evaluadores = usuarios ?? new List<Usuario>();
            ViewBag.Validadores = usuarios ?? new List<Usuario>();
        }
    }
}

