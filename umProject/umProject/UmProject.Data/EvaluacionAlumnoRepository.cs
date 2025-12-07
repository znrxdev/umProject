using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class EvaluacionAlumnoRepository : IEvaluacionAlumnoRepository
    {
        private readonly IConexionService _conexionService;

        public EvaluacionAlumnoRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<EvaluacionAlumno>>> ListarEvaluacionesAlumnoAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<EvaluacionAlumno>>();
            var evaluaciones = new List<EvaluacionAlumno>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_alumnos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 147; // Listar todas las calificaciones
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    evaluaciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<EvaluacionAlumno>>(cmd, evaluaciones);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<EvaluacionAlumno>>> FiltrarEvaluacionAlumnoPorIdAsync(int idEvaluacionAlumno, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<EvaluacionAlumno>>();
            var evaluaciones = new List<EvaluacionAlumno>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_alumnos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 130; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Alumno", SqlDbType.Int).Value = idEvaluacionAlumno;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    evaluaciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<EvaluacionAlumno>>(cmd, evaluaciones);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarEvaluacionAlumnoAsync(EvaluacionAlumno evaluacionAlumno, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_alumnos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 128; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Registro", SqlDbType.VarChar, 30).Value = (object?)evaluacionAlumno.CodigoRegistro ?? DBNull.Value; // Puede ser NULL, el SP lo genera
            cmd.Parameters.Add("@Id_Evaluacion_Instancia", SqlDbType.Int).Value = evaluacionAlumno.IdEvaluacionInstancia ?? 0;
            cmd.Parameters.Add("@Id_Inscripcion", SqlDbType.Int).Value = evaluacionAlumno.IdInscripcion ?? 0;
            cmd.Parameters.Add("@Puntaje_Obtenido", SqlDbType.Decimal).Value = evaluacionAlumno.PuntajeObtenido;
            cmd.Parameters.Add("@Porcentaje_Logrado", SqlDbType.Decimal).Value = (object?)evaluacionAlumno.PorcentajeLogrado ?? DBNull.Value;
            cmd.Parameters.Add("@Puntaje_Normalizado", SqlDbType.Decimal).Value = (object?)evaluacionAlumno.PuntajeNormalizado ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Recalculo", SqlDbType.Bit).Value = evaluacionAlumno.EsRecalculo;
            cmd.Parameters.Add("@Numero_Recalculo", SqlDbType.Int).Value = evaluacionAlumno.NumeroRecalculo;
            cmd.Parameters.Add("@Motivo_Ajuste", SqlDbType.NVarChar, 500).Value = (object?)evaluacionAlumno.MotivoAjuste ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object?)evaluacionAlumno.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Evaluador", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdUsuarioEvaluador ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Validador", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdUsuarioValidador ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Validacion", SqlDbType.DateTime2).Value = (object?)evaluacionAlumno.FechaValidacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = evaluacionAlumno.IdEstado ?? 1;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Reemplazada", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdEvaluacionReemplazada ?? DBNull.Value;
            cmd.Parameters.Add("@Firmado_Por_Estudiante", SqlDbType.Bit).Value = evaluacionAlumno.FirmadoPorEstudiante;
            cmd.Parameters.Add("@Firma_Digital", SqlDbType.NVarChar, 255).Value = (object?)evaluacionAlumno.FirmaDigital ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Notificacion", SqlDbType.DateTime2).Value = (object?)evaluacionAlumno.FechaNotificacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime2).Value = (object?)evaluacionAlumno.FechaPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarEvaluacionAlumnoAsync(EvaluacionAlumno evaluacionAlumno, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_alumnos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 129; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Alumno", SqlDbType.Int).Value = evaluacionAlumno.IdEvaluacionAlumno ?? 0;
            cmd.Parameters.Add("@Codigo_Registro", SqlDbType.VarChar, 30).Value = (object?)evaluacionAlumno.CodigoRegistro ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Instancia", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdEvaluacionInstancia ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Inscripcion", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdInscripcion ?? DBNull.Value;
            cmd.Parameters.Add("@Puntaje_Obtenido", SqlDbType.Decimal).Value = (object?)evaluacionAlumno.PuntajeObtenido ?? DBNull.Value;
            cmd.Parameters.Add("@Porcentaje_Logrado", SqlDbType.Decimal).Value = (object?)evaluacionAlumno.PorcentajeLogrado ?? DBNull.Value;
            cmd.Parameters.Add("@Puntaje_Normalizado", SqlDbType.Decimal).Value = (object?)evaluacionAlumno.PuntajeNormalizado ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Recalculo", SqlDbType.Bit).Value = (object?)evaluacionAlumno.EsRecalculo ?? DBNull.Value;
            cmd.Parameters.Add("@Numero_Recalculo", SqlDbType.Int).Value = (object?)evaluacionAlumno.NumeroRecalculo ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Ajuste", SqlDbType.NVarChar, 500).Value = (object?)evaluacionAlumno.MotivoAjuste ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object?)evaluacionAlumno.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Evaluador", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdUsuarioEvaluador ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Validador", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdUsuarioValidador ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Validacion", SqlDbType.DateTime2).Value = (object?)evaluacionAlumno.FechaValidacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Reemplazada", SqlDbType.Int).Value = (object?)evaluacionAlumno.IdEvaluacionReemplazada ?? DBNull.Value;
            cmd.Parameters.Add("@Firmado_Por_Estudiante", SqlDbType.Bit).Value = (object?)evaluacionAlumno.FirmadoPorEstudiante ?? DBNull.Value;
            cmd.Parameters.Add("@Firma_Digital", SqlDbType.NVarChar, 255).Value = (object?)evaluacionAlumno.FirmaDigital ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Notificacion", SqlDbType.DateTime2).Value = (object?)evaluacionAlumno.FechaNotificacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime2).Value = (object?)evaluacionAlumno.FechaPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private EvaluacionAlumno LlenarModelo(SqlDataReader reader)
        {
            // Helper para leer columnas opcionales de forma segura
            string? LeerColumnaString(string nombreColumna)
            {
                try
                {
                    return reader[nombreColumna] is DBNull ? null : reader[nombreColumna] as string;
                }
                catch
                {
                    return null;
                }
            }

            return new EvaluacionAlumno
            {
                IdEvaluacionAlumno = reader["Id_Evaluacion_Alumno"] as int? ?? 0,
                CodigoRegistro = reader["Codigo_Registro"] as string ?? string.Empty,
                IdEvaluacionInstancia = reader["Id_Evaluacion_Instancia"] as int? ?? 0,
                IdInscripcion = reader["Id_Inscripcion"] as int? ?? 0,
                PuntajeObtenido = reader["Puntaje_Obtenido"] is DBNull ? 0 : Convert.ToDecimal(reader["Puntaje_Obtenido"]),
                PorcentajeLogrado = reader["Porcentaje_Logrado"] is DBNull ? null : (decimal?)Convert.ToDecimal(reader["Porcentaje_Logrado"]),
                PuntajeNormalizado = reader["Puntaje_Normalizado"] is DBNull ? null : (decimal?)Convert.ToDecimal(reader["Puntaje_Normalizado"]),
                EsRecalculo = reader["Es_Recalculo"] as bool? ?? false,
                NumeroRecalculo = reader["Numero_Recalculo"] as int? ?? 0,
                MotivoAjuste = reader["Motivo_Ajuste"] as string,
                Observaciones = reader["Observaciones"] as string,
                IdUsuarioEvaluador = reader["Id_Usuario_Evaluador"] as int?,
                IdUsuarioValidador = reader["Id_Usuario_Validador"] as int?,
                FechaValidacion = reader["Fecha_Validacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Validacion"]),
                IdEstado = reader["Id_Estado"] as int? ?? 0,
                IdEstadoPublicacion = reader["Id_Estado_Publicacion"] as int?,
                IdEvaluacionReemplazada = reader["Id_Evaluacion_Reemplazada"] as int?,
                FirmadoPorEstudiante = reader["Firmado_Por_Estudiante"] as bool? ?? false,
                FirmaDigital = reader["Firma_Digital"] as string,
                FechaNotificacion = reader["Fecha_Notificacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Notificacion"]),
                FechaPublicacion = reader["Fecha_Publicacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Publicacion"]),
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI (pueden ser NULL si no vienen del SP)
                CodigoInstancia = LeerColumnaString("Codigo_Instancia"),
                NombreModeloEvaluacion = LeerColumnaString("Nombre_Modelo_Evaluacion"),
                CodigoInscripcion = LeerColumnaString("Codigo_Inscripcion"),
                EstudianteUsuario = LeerColumnaString("Usuario_Estudiante"),
                EstudianteNombre = LeerColumnaString("Nombre_Estudiante"),
                NombreMateria = LeerColumnaString("Nombre_Materia"),
                CodigoMateria = LeerColumnaString("Codigo_Materia"),
                NombrePeriodo = LeerColumnaString("Nombre_Periodo"),
                CodigoPeriodo = LeerColumnaString("Codigo_Periodo"),
                CodigoSeccion = LeerColumnaString("Codigo_Seccion"),
                EstadoNombre = LeerColumnaString("Estado_Nombre") ?? LeerColumnaString("Nombre_Estado"),
                EstadoPublicacionNombre = LeerColumnaString("Estado_Publicacion_Nombre"),
                EvaluadorUsuario = LeerColumnaString("Evaluador_Usuario"),
                ValidadorUsuario = LeerColumnaString("Validador_Usuario")
            };
        }
    }
}

