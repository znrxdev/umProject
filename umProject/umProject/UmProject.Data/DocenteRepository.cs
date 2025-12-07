using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class DocenteRepository : IDocenteRepository
    {
        private readonly IConexionService _conexionService;

        public DocenteRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Docente>> ListarDocentesAsync(int? idSesion)
        {
            var docentes = new List<Docente>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_docentes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 177; // LISTAR DOCENTES
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                docentes.Add(new Docente
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

            return docentes;
        }

        public async Task<DocenteDetalle?> ObtenerDocenteDetalleAsync(int idUsuario, int? idSesion)
        {
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_docentes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 178; // OBTENER DETALLE DOCENTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            DocenteDetalle? detalle = null;
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    detalle = new DocenteDetalle
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
                    TotalSeccionesActivas = reader["Total_Secciones_Activas"] as int? ?? 0,
                    TotalEvaluacionesRealizadas = reader["Total_Evaluaciones_Realizadas"] as int? ?? 0,
                    TotalEstudiantesActivos = reader["Total_Estudiantes_Activos"] as int? ?? 0,
                    PeriodoActual = reader["Periodo_Actual"] as string
                };
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return detalle;
        }

        public async Task<List<DocenteEvaluacion>> ObtenerEvaluacionesRealizadasAsync(int idUsuario, int? idSesion, int? idPeriodo = null)
        {
            var evaluaciones = new List<DocenteEvaluacion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_docentes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 179; // OBTENER EVALUACIONES REALIZADAS DOCENTE
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            if (idPeriodo.HasValue)
                cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = idPeriodo.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                evaluaciones.Add(new DocenteEvaluacion
                {
                    IdEvaluacionAlumno = reader["Id_Evaluacion_Alumno"] as int? ?? 0,
                    CodigoRegistro = reader["Codigo_Registro"] as string,
                    NombreEstudiante = reader["Nombre_Estudiante"] as string,
                    UsuarioEstudiante = reader["Usuario_Estudiante"] as string,
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
                    EstadoPublicacion = reader["Estado_Publicacion"] as string,
                    FechaEvaluacion = reader["Fecha_Evaluacion"] as DateTime?,
                    FechaPublicacion = reader["Fecha_Publicacion"] as DateTime?
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return evaluaciones;
        }

        public async Task<DocenteEvaluacionDetalle?> ObtenerDetalleEvaluacionAsync(int idEvaluacionAlumno, int? idSesion)
        {
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_docentes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 180; // OBTENER DETALLE EVALUACION
            cmd.Parameters.Add("@Id_Evaluacion_Alumno", SqlDbType.Int).Value = idEvaluacionAlumno;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            DocenteEvaluacionDetalle? detalle = null;
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    detalle = new DocenteEvaluacionDetalle
                    {
                        // Datos del Alumno
                        IdEvaluacionAlumno = reader["Id_Evaluacion_Alumno"] as int? ?? 0,
                    CodigoRegistro = reader["Codigo_Registro"] as string,
                    NombreEstudiante = reader["Nombre_Estudiante"] as string,
                    UsuarioEstudiante = reader["Usuario_Estudiante"] as string,
                    ValorDocumentoEstudiante = reader["Valor_Documento_Estudiante"] as string,

                    // Datos de la Instancia de Evaluación
                    IdEvaluacionInstancia = reader["Id_Evaluacion_Instancia"] as int?,
                    CodigoInstancia = reader["Codigo_Instancia"] as string,
                    FechaProgramada = reader["Fecha_Programada"] as DateTime?,
                    FechaLimite = reader["Fecha_Limite"] as DateTime?,

                    // Datos del Modelo de Evaluación
                    IdEvaluacionModelo = reader["Id_Evaluacion_Modelo"] as int?,
                    CodigoModelo = reader["Codigo_Modelo"] as string,
                    NombreEvaluacion = reader["Nombre_Evaluacion"] as string,
                    Concepto = reader["Concepto"] as string,
                    TipoEvaluacion = reader["Tipo_Evaluacion"] as string,
                    CalificacionMaxima = reader["Calificacion_Maxima"] as decimal? ?? 0,

                    // Datos de la Materia y Sección
                    NombreMateria = reader["Nombre_Materia"] as string,
                    CodigoMateria = reader["Codigo_Materia"] as string,
                    CodigoSeccion = reader["Codigo_Seccion"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    CodigoPeriodo = reader["Codigo_Periodo"] as string,

                    // Resultado del Alumno
                    PuntajeObtenido = reader["Puntaje_Obtenido"] as decimal? ?? 0,
                    PorcentajeLogrado = reader["Porcentaje_Logrado"] as decimal?,
                    PuntajeNormalizado = reader["Puntaje_Normalizado"] as decimal?,
                    EsRecalculo = reader["Es_Recalculo"] as bool? ?? false,
                    NumeroRecalculo = reader["Numero_Recalculo"] as int? ?? 0,
                    MotivoAjuste = reader["Motivo_Ajuste"] as string,
                    Observaciones = reader["Observaciones"] as string,

                    // Usuarios involucrados
                    IdUsuarioEvaluador = reader["Id_Usuario_Evaluador"] as int?,
                    UsuarioEvaluador = reader["Usuario_Evaluador"] as string,
                    NombreEvaluador = reader["Nombre_Evaluador"] as string,
                    IdUsuarioValidador = reader["Id_Usuario_Validador"] as int?,
                    UsuarioValidador = reader["Usuario_Validador"] as string,
                    NombreValidador = reader["Nombre_Validador"] as string,
                    FechaValidacion = reader["Fecha_Validacion"] as DateTime?,

                    // Estados
                    EstadoEvaluacion = reader["Estado_Evaluacion"] as string,
                    EstadoPublicacion = reader["Estado_Publicacion"] as string,

                    // Firma
                    FirmadoPorEstudiante = reader["Firmado_Por_Estudiante"] as bool? ?? false,
                    FirmaDigital = reader["Firma_Digital"] as string,
                    FechaNotificacion = reader["Fecha_Notificacion"] as DateTime?,
                    FechaPublicacionResultado = reader["Fecha_Publicacion_Resultado"] as DateTime?,

                    // Fechas de auditoría
                    FechaCreacion = reader["Fecha_Creacion"] as DateTime?,
                    FechaModificacion = reader["Fecha_Modificacion"] as DateTime?
                };
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return detalle;
        }

        public async Task<List<DocenteSeccion>> ObtenerSeccionesAsignadasAsync(int idUsuario, int? idSesion)
        {
            var secciones = new List<DocenteSeccion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_docentes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 181; // OBTENER SECCIONES ASIGNADAS DOCENTE
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
                secciones.Add(new DocenteSeccion
                {
                    IdSeccion = reader["Id_Seccion"] as int? ?? 0,
                    CodigoSeccion = reader["Codigo_Seccion"] as string,
                    NombreMateria = reader["Nombre_Materia"] as string,
                    CodigoMateria = reader["Codigo_Materia"] as string,
                    NombrePeriodo = reader["Nombre_Periodo"] as string,
                    CodigoPeriodo = reader["Codigo_Periodo"] as string,
                    TipoSeccion = reader["Tipo_Seccion"] as string,
                    Aula = reader["Aula"] as string,
                    HorarioDescripcion = reader["Horario_Descripcion"] as string,
                    Modalidad = reader["Modalidad"] as string,
                    CupoMaximo = reader["Cupo_Maximo"] as int?,
                    TotalEstudiantes = reader["Total_Estudiantes"] as int? ?? 0,
                    RequiereAsistencia = reader["Requiere_Asistencia"] as bool? ?? false,
                    PorcentajeAsistenciaMinima = reader["Porcentaje_Asistencia_Minima"] as decimal?,
                    EstadoSeccion = reader["Estado_Seccion"] as string,
                    EstadoPublicacion = reader["Estado_Publicacion"] as string,
                    FechaPublicacion = reader["Fecha_Publicacion"] as DateTime?,
                    FechaCierre = reader["Fecha_Cierre"] as DateTime?
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return secciones;
        }

        public async Task<List<DocenteSeccionEstudiante>> ObtenerEstudiantesSeccionAsync(int idSeccion, int? idSesion)
        {
            var estudiantes = new List<DocenteSeccionEstudiante>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_docentes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 182; // OBTENER ESTUDIANTES DE SECCION
            cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = idSeccion;
            if (idSesion.HasValue)
                cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
            {
                estudiantes.Add(new DocenteSeccionEstudiante
                {
                    IdInscripcion = reader["Id_Inscripcion"] as int? ?? 0,
                    CodigoInscripcion = reader["Codigo_Inscripcion"] as string,
                    IdEstudiante = reader["Id_Estudiante"] as int? ?? 0,
                    NombreEstudiante = reader["Nombre_Estudiante"] as string,
                    UsuarioEstudiante = reader["Usuario_Estudiante"] as string,
                    ValorDocumento = reader["Valor_Documento"] as string,
                    TipoInscripcion = reader["Tipo_Inscripcion"] as string,
                    EstadoInscripcion = reader["Estado_Inscripcion"] as string,
                    FechaInscripcion = reader["Fecha_Inscripcion"] as DateTime?,
                    FechaValidacion = reader["Fecha_Validacion"] as DateTime?
                });
            }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return estudiantes;
        }
    }
}

