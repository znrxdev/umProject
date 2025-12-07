using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly IConexionService _conexionService;

        public ReporteRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<ReporteUsuario>> GenerarReporteUsuariosAsync(int idSesion, int tipoReporte, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteUsuario>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = tipoReporte; // 151 o 152
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteUsuario
                {
                    IdUsuario = reader["Id_Usuario"] as int?,
                    Usuario = reader["Usuario"] as string,
                    IdPersona = reader["Id_Persona"] as int?,
                    PrimerNombre = reader["Primer_Nombre"] as string,
                    SegundoNombre = reader["Segundo_Nombre"] as string,
                    PrimerApellido = reader["Primer_Apellido"] as string,
                    SegundoApellido = reader["Segundo_Apellido"] as string,
                    NombreCompleto = reader["Nombre_Completo"] as string,
                    ValorDocumento = reader["Valor_Documento"] as string,
                    TipoDocumento = reader["Tipo_Documento"] as string,
                    FechaNacimiento = reader["Fecha_Nacimiento"]?.ToString(),
                    Genero = reader["Genero"] as string,
                    Nacionalidad = reader["Nacionalidad"] as string,
                    EstadoCivil = reader["Estado_Civil"] as string,
                    FechaCreacionUsuario = reader["Fecha_Creacion_Usuario"] as string,
                    FechaModificacionUsuario = reader["Fecha_Modificacion_Usuario"] as string,
                    UltimaSesion = reader["Ultima_Sesion"] as string,
                    UltimoCambioContrasena = reader["Ultimo_Cambio_Contrasena"] as string,
                    EstadoUsuario = reader["Estado_Usuario"] as string,
                    FechaCreacionPersona = reader["Fecha_Creacion_Persona"] as string,
                    FechaModificacionPersona = reader["Fecha_Modificacion_Persona"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReportePersona>> GenerarReportePersonasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReportePersona>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 155;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            var paramFechaInicio = new SqlParameter("@Fecha_Inicio", SqlDbType.DateTime);
            paramFechaInicio.Value = fechaInicio.HasValue ? (object)fechaInicio.Value : DBNull.Value;
            cmd.Parameters.Add(paramFechaInicio);
            
            var paramFechaFin = new SqlParameter("@Fecha_Fin", SqlDbType.DateTime);
            paramFechaFin.Value = fechaFin.HasValue ? (object)fechaFin.Value : DBNull.Value;
            cmd.Parameters.Add(paramFechaFin);
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var fechaNacimiento = reader["Fecha_Nacimiento"];
                reportes.Add(new ReportePersona
                {
                    IdPersona = reader["Id_Persona"] as int?,
                    PrimerNombre = reader["Primer_Nombre"] as string,
                    SegundoNombre = reader["Segundo_Nombre"] as string,
                    PrimerApellido = reader["Primer_Apellido"] as string,
                    SegundoApellido = reader["Segundo_Apellido"] as string,
                    NombreCompleto = reader["Nombre_Completo"] as string,
                    ValorDocumento = reader["Valor_Documento"] as string,
                    TipoDocumento = reader["Tipo_Documento"] as string,
                    FechaNacimiento = fechaNacimiento == DBNull.Value ? null : fechaNacimiento.ToString(),
                    Genero = reader["Genero"] as string,
                    Nacionalidad = reader["Nacionalidad"] as string,
                    EstadoCivil = reader["Estado_Civil"] as string,
                    Estado = reader["Estado"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteMateria>> GenerarReporteMateriasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteMateria>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 156;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteMateria
                {
                    IdMateria = reader["Id_Materia"] as int?,
                    CodigoMateria = reader["Codigo_Materia"] as string,
                    NombreMateria = reader["Nombre_Materia"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string,
                    Estado = reader["Estado"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReportePeriodo>> GenerarReportePeriodosAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReportePeriodo>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 157;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReportePeriodo
                {
                    IdPeriodo = reader["Id_Periodo"] as int?,
                    CodigoPeriodo = reader["Codigo_Periodo"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    TipoPeriodo = reader["Tipo_Periodo"] as string,
                    FechaInicio = reader["Fecha_Inicio"] as string,
                    FechaFin = reader["Fecha_Fin"] as string,
                    FechaCierreCalificaciones = reader["Fecha_Cierre_Calificaciones"] as string,
                    EsPeriodoActual = reader["Es_Periodo_Actual"] as string,
                    Estado = reader["Estado"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteSeccion>> GenerarReporteSeccionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteSeccion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 158;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteSeccion
                {
                    IdSeccion = reader["Id_Seccion"] as int?,
                    CodigoSeccion = reader["Codigo_Seccion"] as string,
                    NombreMateria = reader["Nombre_Materia"] as string,
                    CodigoPlan = reader["Codigo_Plan"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    Docente = reader["Docente"] as string,
                    TipoSeccion = reader["Tipo_Seccion"] as string,
                    Aula = reader["Aula"] as string,
                    HorarioDescripcion = reader["Horario_Descripcion"] as string,
                    Modalidad = reader["Modalidad"] as string,
                    CupoMaximo = reader["Cupo_Maximo"] as int?,
                    Estado = reader["Estado"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteGrupo>> GenerarReporteGruposAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteGrupo>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 159;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteGrupo
                {
                    IdGrupo = reader["Id_Grupo"] as int?,
                    CodigoGrupo = reader["Codigo_Grupo"] as string,
                    NombreGrupo = reader["Nombre_Grupo"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    TipoGrupo = reader["Tipo_Grupo"] as string,
                    Coordinador = reader["Coordinador"] as string,
                    Jornada = reader["Jornada"] as string,
                    Estado = reader["Estado"] as string,
                    FechaCierre = reader["Fecha_Cierre"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteInscripcion>> GenerarReporteInscripcionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteInscripcion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 160;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteInscripcion
                {
                    IdInscripcion = reader["Id_Inscripcion"] as int?,
                    CodigoInscripcion = reader["Codigo_Inscripcion"] as string,
                    Estudiante = reader["Estudiante"] as string,
                    NombreEstudiante = reader["Nombre_Estudiante"] as string,
                    TipoInscripcion = reader["Tipo_Inscripcion"] as string,
                    Estado = reader["Estado"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaValidacion = reader["Fecha_Validacion"] as string,
                    FechaRetiro = reader["Fecha_Retiro"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteEvaluacion>> GenerarReporteEvaluacionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteEvaluacion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 161;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteEvaluacion
                {
                    IdEvaluacionAlumno = reader["Id_Evaluacion_Alumno"] as int?,
                    CodigoRegistro = reader["Codigo_Registro"] as string,
                    CodigoInstancia = reader["Codigo_Instancia"] as string,
                    NombreEvaluacion = reader["Nombre_Evaluacion"] as string,
                    NombreMateria = reader["Nombre_Materia"] as string,
                    Estudiante = reader["Estudiante"] as string,
                    NombreEstudiante = reader["Nombre_Estudiante"] as string,
                    PuntajeObtenido = reader["Puntaje_Obtenido"] as decimal?,
                    PorcentajeLogrado = reader["Porcentaje_Logrado"] as decimal?,
                    Estado = reader["Estado"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaValidacion = reader["Fecha_Validacion"] as string,
                    FechaPublicacion = reader["Fecha_Publicacion"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteBecaPrograma>> GenerarReporteBecasProgramasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteBecaPrograma>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 162;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteBecaPrograma
                {
                    IdBecaPrograma = reader["Id_Beca_Programa"] as int?,
                    CodigoPrograma = reader["Codigo_Programa"] as string,
                    NombrePrograma = reader["Nombre_Programa"] as string,
                    Descripcion = reader["Descripcion"] as string,
                    TipoPrograma = reader["Tipo_Programa"] as string,
                    ModalidadPrograma = reader["Modalidad_Programa"] as string,
                    PromedioMinimo = reader["Promedio_Minimo"] as decimal?,
                    EstadoPrograma = reader["Estado_Programa"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteBecaConvocatoria>> GenerarReporteBecasConvocatoriasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteBecaConvocatoria>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 163;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteBecaConvocatoria
                {
                    IdConvocatoria = reader["Id_Convocatoria"] as int?,
                    CodigoConvocatoria = reader["Codigo_Convocatoria"] as string,
                    NombreConvocatoria = reader["Nombre_Convocatoria"] as string,
                    NombrePrograma = reader["Nombre_Programa"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    CupoTotal = reader["Cupo_Total"] as int?,
                    CupoReservado = reader["Cupo_Reservado"] as int?,
                    CupoAsignado = reader["Cupo_Asignado"] as int?,
                    FechaInicio = reader["Fecha_Inicio"] as string,
                    FechaPublicacion = reader["Fecha_Publicacion"] as string,
                    FechaFin = reader["Fecha_Fin"] as string,
                    Estado = reader["Estado"] as string,
                    EstadoPublicacion = reader["Estado_Publicacion"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteBecaSolicitud>> GenerarReporteBecasSolicitudesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteBecaSolicitud>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 164;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteBecaSolicitud
                {
                    IdSolicitudBeca = reader["Id_Solicitud_Beca"] as int?,
                    CodigoSeguimiento = reader["Codigo_Seguimiento"] as string,
                    NombrePrograma = reader["Nombre_Programa"] as string,
                    Estudiante = reader["Estudiante"] as string,
                    NombreEstudiante = reader["Nombre_Estudiante"] as string,
                    PromedioVigente = reader["Promedio_Vigente"] as decimal?,
                    TotalSancionesActivas = reader["Total_Sanciones_Activas"] as int?,
                    CumpleCriterios = reader["Cumple_Criterios"] as string,
                    Estado = reader["Estado"] as string,
                    FechaSolicitud = reader["Fecha_Solicitud"] as string,
                    FechaUltimaDecision = reader["Fecha_Ultima_Decision"] as string,
                    FechaCierre = reader["Fecha_Cierre"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteSancion>> GenerarReporteSancionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteSancion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 165;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteSancion
                {
                    IdSancion = reader["Id_Sancion"] as int?,
                    CodigoSancion = reader["Codigo_Sancion"] as string,
                    Estudiante = reader["Estudiante"] as string,
                    NombreEstudiante = reader["Nombre_Estudiante"] as string,
                    TipoSancion = reader["Tipo_Sancion"] as string,
                    TipoFalta = reader["Tipo_Falta"] as string,
                    Severidad = reader["Severidad"] as string,
                    Estado = reader["Estado"] as string,
                    FechaRegistro = reader["Fecha_Registro"] as string,
                    FechaFin = reader["Fecha_Fin"] as string,
                    Motivo = reader["Motivo"] as string,
                    EsApelable = reader["Es_Apelable"] as string,
                    FechaApelacion = reader["Fecha_Apelacion"] as string,
                    ResultadoApelacion = reader["Resultado_Apelacion"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string
                });
            }

            return reportes;
        }

        public async Task<List<ReporteTransaccion>> GenerarReporteTransaccionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var reportes = new List<ReporteTransaccion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_reportes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 166;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            if (fechaFin.HasValue)
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reportes.Add(new ReporteTransaccion
                {
                    IdTransaccion = reader["Id_Transaccion"] as int?,
                    NombreTipoTransaccion = reader["Nombre_Tipo_Transaccion"] as string,
                    Concepto = reader["Concepto"] as string,
                    TipoEntidad = reader["Tipo_Entidad"] as string,
                    Autor = reader["Autor"] as string,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    Estado = reader["Estado"] as string
                });
            }

            return reportes;
        }
    }
}

