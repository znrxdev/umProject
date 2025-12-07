using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly IConexionService _conexionService;

        public EstudianteRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Estudiante>> ListarEstudiantesAsync(int? idSesion)
        {
            var estudiantes = new List<Estudiante>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 167; // LISTAR ESTUDIANTES
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    estudiantes.Add(new Estudiante
                    {
                        IdUsuario = reader["Id_Usuario"] as int? ?? 0,
                        Usuario = reader["Usuario"] as string,
                        IdPersona = reader["Id_Persona"] as int?,
                        NombreCompleto = reader["Nombre_Completo"] as string,
                        ValorDocumento = reader["Valor_Documento"] as string,
                        FechaNacimiento = reader["Fecha_Nacimiento"] as DateTime?,
                        EstadoUsuario = reader["Estado_Usuario"] as string,
                        UltimaSesion = reader["Ultima_Sesion"] as DateTime?,
                        FechaCreacionUsuario = reader["Fecha_Creacion_Usuario"] as DateTime?
                    });
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return estudiantes;
        }

        public async Task<List<Estudiante>> ListarEstudiantesSinInscripcionesAsync(int? idSesion)
        {
            var estudiantes = new List<Estudiante>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 177; // LISTAR ESTUDIANTES SIN INSCRIPCIONES
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    estudiantes.Add(new Estudiante
                    {
                        IdUsuario = reader["Id_Usuario"] as int? ?? 0,
                        Usuario = reader["Usuario"] as string,
                        IdPersona = reader["Id_Persona"] as int?,
                        NombreCompleto = reader["Nombre_Completo"] as string,
                        ValorDocumento = reader["Valor_Documento"] as string,
                        FechaNacimiento = reader["Fecha_Nacimiento"] as DateTime?,
                        EstadoUsuario = reader["Estado_Usuario"] as string,
                        UltimaSesion = reader["Ultima_Sesion"] as DateTime?,
                        FechaCreacionUsuario = reader["Fecha_Creacion_Usuario"] as DateTime?
                    });
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return estudiantes;
        }

        public async Task<EstudianteDetalle?> ObtenerEstudianteDetalleAsync(int idUsuario, int? idSesion)
        {
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 168; // OBTENER DETALLE ESTUDIANTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            EstudianteDetalle? detalle = null;
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    detalle = new EstudianteDetalle
                    {
                        IdUsuario = reader["Id_Usuario"] as int? ?? 0,
                        Usuario = reader["Usuario"] as string,
                        IdPersona = reader["Id_Persona"] as int?,
                        NombreCompleto = reader["Nombre_Completo"] as string,
                        ValorDocumento = reader["Valor_Documento"] as string,
                        FechaNacimiento = reader["Fecha_Nacimiento"] as DateTime?,
                        EstadoUsuario = reader["Estado_Usuario"] as string,
                        UltimaSesion = reader["Ultima_Sesion"] as DateTime?,
                        FechaCreacionUsuario = reader["Fecha_Creacion_Usuario"] as DateTime?,
                        TotalInscripcionesActivas = reader["Total_Inscripciones_Activas"] as int? ?? 0,
                        TotalGrupos = reader["Total_Grupos"] as int? ?? 0,
                        TotalEvaluaciones = reader["Total_Evaluaciones"] as int? ?? 0,
                        PromedioGeneral = reader["Promedio_General"] as decimal?,
                        TotalSancionesActivas = reader["Total_Sanciones_Activas"] as int? ?? 0,
                        PeriodoActual = reader["Periodo_Actual"] as string
                    };
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return detalle;
        }

        public async Task<List<EstudianteInscripcion>> ObtenerInscripcionesAsync(int idUsuario, int? idSesion)
        {
            var inscripciones = new List<EstudianteInscripcion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 169; // OBTENER INSCRIPCIONES ESTUDIANTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                inscripciones.Add(new EstudianteInscripcion
                {
                    IdInscripcion = reader["Id_Inscripcion"] as int? ?? 0,
                    CodigoInscripcion = reader["Codigo_Inscripcion"] as string,
                    TipoInscripcion = reader["Tipo_Inscripcion"] as string,
                    EstadoInscripcion = reader["Estado_Inscripcion"] as string,
                    FechaInscripcion = reader["Fecha_Inscripcion"] as DateTime?,
                    FechaValidacion = reader["Fecha_Validacion"] as DateTime?,
                    FechaRetiro = reader["Fecha_Retiro"] as DateTime?,
                    MotivoRetiro = reader["Motivo_Retiro"] as string
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return inscripciones;
        }

        public async Task<List<EstudianteGrupo>> ObtenerGruposAsync(int idUsuario, int? idSesion)
        {
            var grupos = new List<EstudianteGrupo>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 170; // OBTENER GRUPOS ESTUDIANTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                grupos.Add(new EstudianteGrupo
                {
                    IdGrupo = reader["Id_Grupo"] as int? ?? 0,
                    CodigoGrupo = reader["Codigo_Grupo"] as string,
                    NombreGrupo = reader["Nombre_Grupo"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    CodigoPeriodo = reader["Codigo_Periodo"] as string,
                    TipoGrupo = reader["Tipo_Grupo"] as string,
                    Jornada = reader["Jornada"] as string,
                    Coordinador = reader["Coordinador"] as string,
                    EstadoGrupo = reader["Estado_Grupo"] as string,
                    RolEnGrupo = reader["Rol_En_Grupo"] as string,
                    EsDelegado = reader["Es_Delegado"] as bool? ?? false,
                    FechaAsignacion = reader["Fecha_Asignacion"] as DateTime?,
                    FechaBaja = reader["Fecha_Baja"] as DateTime?,
                    MotivoBaja = reader["Motivo_Baja"] as string
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return grupos;
        }

        public async Task<List<EstudianteSeccion>> ObtenerSeccionesAsync(int idUsuario, int? idSesion)
        {
            var secciones = new List<EstudianteSeccion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 171; // OBTENER SECCIONES ESTUDIANTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                secciones.Add(new EstudianteSeccion
                {
                    IdSeccion = reader["Id_Seccion"] as int? ?? 0,
                    CodigoSeccion = reader["Codigo_Seccion"] as string,
                    NombreMateria = reader["Nombre_Materia"] as string,
                    CodigoMateria = reader["Codigo_Materia"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    CodigoPeriodo = reader["Codigo_Periodo"] as string,
                    Docente = reader["Docente"] as string,
                    TipoSeccion = reader["Tipo_Seccion"] as string,
                    Aula = reader["Aula"] as string,
                    Modalidad = reader["Modalidad"] as string,
                    CupoMaximo = reader["Cupo_Maximo"] as int?,
                    PorcentajeAsistenciaMinima = reader["Porcentaje_Asistencia_Minima"] as decimal?,
                    EstadoSeccion = reader["Estado_Seccion"] as string,
                    FechaPublicacion = reader["Fecha_Publicacion"] as DateTime?,
                    FechaCierre = reader["Fecha_Cierre"] as DateTime?
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return secciones;
        }

        public async Task<List<EstudiantePeriodo>> ObtenerPeriodosAsync(int idUsuario, int? idSesion)
        {
            var periodos = new List<EstudiantePeriodo>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 172; // OBTENER PERÍODOS ESTUDIANTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                periodos.Add(new EstudiantePeriodo
                {
                    IdPeriodo = reader["Id_Periodo"] as int? ?? 0,
                    CodigoPeriodo = reader["Codigo_Periodo"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    TipoPeriodo = reader["Tipo_Periodo"] as string,
                    FechaInicio = reader["Fecha_Inicio"] as DateTime?,
                    FechaFin = reader["Fecha_Fin"] as DateTime?,
                    EsPeriodoActual = reader["Es_Periodo_Actual"] as bool? ?? false,
                    EstadoPeriodo = reader["Estado_Periodo"] as string,
                    TotalInscripciones = reader["Total_Inscripciones"] as int? ?? 0
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return periodos;
        }

        public async Task<List<EstudianteEvaluacion>> ObtenerEvaluacionesAsync(int idUsuario, int? idSesion, bool? soloActuales = null)
        {
            var evaluaciones = new List<EstudianteEvaluacion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 173; // OBTENER EVALUACIONES ESTUDIANTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            if (soloActuales.HasValue)
                cmd.Parameters.Add("@Solo_Actuales", SqlDbType.Bit).Value = soloActuales.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                evaluaciones.Add(new EstudianteEvaluacion
                {
                    IdEvaluacionAlumno = reader["Id_Evaluacion_Alumno"] as int? ?? 0,
                    CodigoRegistro = reader["Codigo_Registro"] as string,
                    IdEvaluacionInstancia = reader["Id_Evaluacion_Instancia"] as int?,
                    CodigoInstancia = reader["Codigo_Instancia"] as string,
                    NombreEvaluacion = reader["Nombre_Evaluacion"] as string,
                    CodigoModelo = reader["Codigo_Modelo"] as string,
                    TipoEvaluacion = reader["Tipo_Evaluacion"] as string,
                    NombreMateria = reader["Nombre_Materia"] as string,
                    CodigoMateria = reader["Codigo_Materia"] as string,
                    CodigoSeccion = reader["Codigo_Seccion"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    CodigoPeriodo = reader["Codigo_Periodo"] as string,
                    PuntajeObtenido = reader["Puntaje_Obtenido"] as decimal? ?? 0,
                    PorcentajeLogrado = reader["Porcentaje_Logrado"] as decimal?,
                    CalificacionMaxima = reader["Calificacion_Maxima"] as decimal? ?? 0,
                    EstadoEvaluacion = reader["Estado_Evaluacion"] as string,
                    EsRecalculo = reader["Es_Recalculo"] as bool? ?? false,
                    NumeroRecalculo = reader["Numero_Recalculo"] as int? ?? 0,
                    FechaEvaluacion = reader["Fecha_Evaluacion"] as DateTime?,
                    FechaPublicacion = reader["Fecha_Publicacion"] as DateTime?,
                    FechaValidacion = reader["Fecha_Validacion"] as DateTime?,
                    UsuarioEvaluador = reader["Usuario_Evaluador"] as string,
                    UsuarioValidador = reader["Usuario_Validador"] as string
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return evaluaciones;
        }

        public async Task<List<EstudianteDesempeno>> ObtenerDesempenoPorPeriodoAsync(int idUsuario, int? idSesion)
        {
            var desempenos = new List<EstudianteDesempeno>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 174; // OBTENER DESEMPEÑO POR PERÍODO ESTUDIANTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                desempenos.Add(new EstudianteDesempeno
                {
                    IdPeriodo = reader["Id_Periodo"] as int? ?? 0,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    CodigoPeriodo = reader["Codigo_Periodo"] as string,
                    TotalMaterias = reader["Total_Materias"] as int? ?? 0,
                    TotalEvaluaciones = reader["Total_Evaluaciones"] as int? ?? 0,
                    PromedioGeneral = reader["Promedio_General"] as decimal?,
                    MateriasAprobadas = reader["Materias_Aprobadas"] as int? ?? 0,
                    MateriasReprobadas = reader["Materias_Reprobadas"] as int? ?? 0
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return desempenos;
        }

        public async Task<List<EstudianteSancion>> ObtenerSancionesAsync(int idUsuario, int? idSesion, bool? soloActivas = null)
        {
            var sanciones = new List<EstudianteSancion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 175; // OBTENER SANCIONES ESTUDIANTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            if (soloActivas.HasValue)
                cmd.Parameters.Add("@Solo_Activas", SqlDbType.Bit).Value = soloActivas.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                sanciones.Add(new EstudianteSancion
                {
                    IdSancion = reader["Id_Sancion"] as int? ?? 0,
                    CodigoSancion = reader["Codigo_Sancion"] as string,
                    TipoSancion = reader["Tipo_Sancion"] as string,
                    TipoFalta = reader["Tipo_Falta"] as string,
                    Severidad = reader["Severidad"] as string,
                    EstadoSancion = reader["Estado_Sancion"] as string,
                    FechaRegistro = reader["Fecha_Registro"] as DateTime?,
                    FechaFin = reader["Fecha_Fin"] as DateTime?,
                    Motivo = reader["Motivo"] as string,
                    EsApelable = reader["Es_Apelable"] as bool? ?? false,
                    FechaApelacion = reader["Fecha_Apelacion"] as DateTime?,
                    ResultadoApelacion = reader["Resultado_Apelacion"] as string,
                    ObservacionesApelacion = reader["Observaciones_Apelacion"] as string,
                    UsuarioResolucion = reader["Usuario_Resolucion"] as string,
                    FechaResolucion = reader["Fecha_Resolucion"] as DateTime?
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return sanciones;
        }

        public async Task<List<EstudianteSolicitudBeca>> ObtenerSolicitudesBecasAsync(int idUsuario, int? idSesion)
        {
            var solicitudes = new List<EstudianteSolicitudBeca>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_solicitudes_becas_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 190; // MIS SOLICITUDES
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion ?? idUsuario;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    solicitudes.Add(new EstudianteSolicitudBeca
                    {
                        IdSolicitudBeca = reader["Id_Solicitud_Beca"] as int? ?? 0,
                        CodigoSeguimiento = reader["Codigo_Seguimiento"] as string,
                        NombrePrograma = reader["Nombre_Programa"] as string,
                        CodigoPrograma = reader["Codigo_Programa"] as string,
                        PromedioVigente = reader["Promedio_Vigente"] as decimal?,
                        TotalSancionesActivas = reader["Total_Sanciones_Activas"] as int? ?? 0,
                        CumpleCriterios = reader["Cumple_Criterios"] as bool? ?? false,
                        EstadoSolicitud = reader["Estado_Solicitud"] as string,
                        FechaSolicitud = reader["Fecha_Solicitud"] as DateTime?,
                        FechaUltimaDecision = reader["Fecha_Ultima_Decision"] as DateTime?,
                        FechaCierre = reader["Fecha_Cierre"] as DateTime?,
                        MotivoUltimaDecision = reader["Motivo_Ultima_Decision"] as string,
                        Observaciones = reader["Observaciones"] as string
                    });
                }
            }
            
            RepositorioHelper.VerificarResultado(cmd, out _, out _);
            return solicitudes;
        }

        public async Task<List<BecaPrograma>> ObtenerProgramasBecaDisponiblesAsync(int idSesion)
        {
            var programas = new List<BecaPrograma>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_solicitudes_becas_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 188; // LISTAR PROGRAMAS DISPONIBLES
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    programas.Add(new BecaPrograma
                    {
                        IdBecaPrograma = reader["Id_Beca_Programa"] as int? ?? 0,
                        CodigoPrograma = reader["Codigo_Programa"] as string,
                        NombrePrograma = reader["Nombre_Programa"] as string,
                        PromedioMinimo = reader["Promedio_Minimo"] as decimal?,
                        RequiereSinSanciones = reader["Requiere_Sin_Sanciones"] as bool? ?? false,
                        NombreEstadoPrograma = reader["Nombre_Estado_Programa"] as string,
                        CriteriosResumen = reader["Criterios_Resumen"] as string
                    });
                }
            }

            RepositorioHelper.VerificarResultado(cmd, out _, out _);
            return programas;
        }

        public async Task<ResultadoOperacion> AplicarSolicitudBecaAsync(int idBecaPrograma, string? observaciones, int idSesion)
        {
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_solicitudes_becas_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 189; // APLICAR
            cmd.Parameters.Add("@Id_Beca_Programa", SqlDbType.Int).Value = idBecaPrograma;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 1000).Value = (object?)observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return RepositorioHelper.ObtenerResultado(cmd);
        }

        public async Task<List<EstudianteSolicitudBecaHistorial>> ObtenerHistorialSolicitudesBecaAsync(int idUsuario, int? idSesion)
        {
            var historial = new List<EstudianteSolicitudBecaHistorial>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_solicitudes_becas_estudiantes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 191; // HISTORIAL
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion ?? idUsuario;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    historial.Add(new EstudianteSolicitudBecaHistorial
                    {
                        IdHistorialSolicitud = reader["Id_Historial_Solicitud"] as int? ?? 0,
                        IdSolicitudBeca = reader["Id_Solicitud_Beca"] as int? ?? 0,
                        IdEstadoAnterior = reader["Id_Estado_Anterior"] as int?,
                        IdEstadoNuevo = reader["Id_Estado_Nuevo"] as int? ?? 0,
                        EstadoNuevoNombre = reader["Estado_Nuevo_Nombre"] as string,
                        IdUsuarioRevisor = reader["Id_Usuario_Revisor"] as int? ?? 0,
                        UsuarioRevisor = reader["Usuario_Revisor"] as string,
                        FechaDecision = reader["Fecha_Decision"] as DateTime? ?? DateTime.MinValue,
                        MotivoDecision = reader["Motivo_Decision"] as string,
                        Observaciones = reader["Observaciones"] as string
                    });
                }
            }

            RepositorioHelper.VerificarResultado(cmd, out _, out _);
            return historial;
        }
    }
}
