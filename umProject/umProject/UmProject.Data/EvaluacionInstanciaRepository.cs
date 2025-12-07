using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class EvaluacionInstanciaRepository : IEvaluacionInstanciaRepository
    {
        private readonly IConexionService _conexionService;

        public EvaluacionInstanciaRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<EvaluacionInstancia>>> ListarEvaluacionesInstanciasAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<EvaluacionInstancia>>();
            var instancias = new List<EvaluacionInstancia>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_instancias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 146; // Listar todas las instancias
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    instancias.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<EvaluacionInstancia>>(cmd, instancias);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<EvaluacionInstancia>>> FiltrarEvaluacionInstanciaPorIdAsync(int idEvaluacionInstancia, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<EvaluacionInstancia>>();
            var instancias = new List<EvaluacionInstancia>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_instancias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 126; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Instancia", SqlDbType.Int).Value = idEvaluacionInstancia;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    instancias.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<EvaluacionInstancia>>(cmd, instancias);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarEvaluacionInstanciaAsync(EvaluacionInstancia evaluacionInstancia, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_instancias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 124; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Instancia", SqlDbType.VarChar, 30).Value = (object?)evaluacionInstancia.CodigoInstancia ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = evaluacionInstancia.IdSeccion ?? 0;
            cmd.Parameters.Add("@Id_Evaluacion_Modelo", SqlDbType.Int).Value = evaluacionInstancia.IdEvaluacionModelo ?? 0;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = evaluacionInstancia.IdPeriodo ?? 0;
            cmd.Parameters.Add("@Fecha_Programada", SqlDbType.DateTime2).Value = (object?)evaluacionInstancia.FechaProgramada ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Limite", SqlDbType.DateTime2).Value = (object?)evaluacionInstancia.FechaLimite ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Revision_Interna", SqlDbType.Bit).Value = evaluacionInstancia.RequiereRevisionInterna;
            cmd.Parameters.Add("@Numero_Version", SqlDbType.Int).Value = evaluacionInstancia.NumeroVersion;
            cmd.Parameters.Add("@Nivel_Aprobacion_Actual", SqlDbType.TinyInt).Value = evaluacionInstancia.NivelAprobacionActual;
            cmd.Parameters.Add("@Calificacion_Maxima", SqlDbType.Decimal).Value = evaluacionInstancia.CalificacionMaxima;
            cmd.Parameters["@Calificacion_Maxima"].Precision = 6;
            cmd.Parameters["@Calificacion_Maxima"].Scale = 2;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = evaluacionInstancia.IdEstado ?? 0;
            cmd.Parameters.Add("@Id_Responsable_Revision", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdResponsableRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Revision", SqlDbType.DateTime2).Value = (object?)evaluacionInstancia.FechaRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Responsable_Publicacion", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdResponsablePublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime2).Value = (object?)evaluacionInstancia.FechaPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Padre", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdEvaluacionPadre ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones_Revision", SqlDbType.NVarChar, 500).Value = (object?)evaluacionInstancia.ObservacionesRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Rechazo", SqlDbType.NVarChar, 500).Value = (object?)evaluacionInstancia.MotivoRechazo ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarEvaluacionInstanciaAsync(EvaluacionInstancia evaluacionInstancia, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_instancias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 125; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Instancia", SqlDbType.Int).Value = evaluacionInstancia.IdEvaluacionInstancia ?? 0;
            cmd.Parameters.Add("@Codigo_Instancia", SqlDbType.VarChar, 30).Value = (object?)evaluacionInstancia.CodigoInstancia ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdSeccion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Modelo", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdEvaluacionModelo ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdPeriodo ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Programada", SqlDbType.DateTime2).Value = (object?)evaluacionInstancia.FechaProgramada ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Limite", SqlDbType.DateTime2).Value = (object?)evaluacionInstancia.FechaLimite ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Revision_Interna", SqlDbType.Bit).Value = (object?)evaluacionInstancia.RequiereRevisionInterna ?? DBNull.Value;
            cmd.Parameters.Add("@Numero_Version", SqlDbType.Int).Value = (object?)evaluacionInstancia.NumeroVersion ?? DBNull.Value;
            cmd.Parameters.Add("@Nivel_Aprobacion_Actual", SqlDbType.TinyInt).Value = (object?)evaluacionInstancia.NivelAprobacionActual ?? DBNull.Value;
            cmd.Parameters.Add("@Calificacion_Maxima", SqlDbType.Decimal).Value = (object?)evaluacionInstancia.CalificacionMaxima ?? DBNull.Value;
            cmd.Parameters["@Calificacion_Maxima"].Precision = 6;
            cmd.Parameters["@Calificacion_Maxima"].Scale = 2;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Responsable_Revision", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdResponsableRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Revision", SqlDbType.DateTime2).Value = (object?)evaluacionInstancia.FechaRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Responsable_Publicacion", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdResponsablePublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime2).Value = (object?)evaluacionInstancia.FechaPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Padre", SqlDbType.Int).Value = (object?)evaluacionInstancia.IdEvaluacionPadre ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones_Revision", SqlDbType.NVarChar, 500).Value = (object?)evaluacionInstancia.ObservacionesRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Rechazo", SqlDbType.NVarChar, 500).Value = (object?)evaluacionInstancia.MotivoRechazo ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private EvaluacionInstancia LlenarModelo(SqlDataReader reader)
        {
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

            return new EvaluacionInstancia
            {
                IdEvaluacionInstancia = reader["Id_Evaluacion_Instancia"] as int? ?? 0,
                CodigoInstancia = reader["Codigo_Instancia"] as string ?? string.Empty,
                IdSeccion = reader["Id_Seccion"] as int? ?? 0,
                IdEvaluacionModelo = reader["Id_Evaluacion_Modelo"] as int? ?? 0,
                IdPeriodo = reader["Id_Periodo"] as int? ?? 0,
                FechaProgramada = reader["Fecha_Programada"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Programada"]),
                FechaLimite = reader["Fecha_Limite"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Limite"]),
                RequiereRevisionInterna = reader["Requiere_Revision_Interna"] as bool? ?? false,
                NumeroVersion = reader["Numero_Version"] as int? ?? 1,
                NivelAprobacionActual = reader["Nivel_Aprobacion_Actual"] as byte? ?? 1,
                CalificacionMaxima = reader["Calificacion_Maxima"] is DBNull ? 0 : Convert.ToDecimal(reader["Calificacion_Maxima"]),
                IdEstado = reader["Id_Estado"] as int? ?? 0,
                IdResponsableRevision = reader["Id_Responsable_Revision"] as int?,
                FechaRevision = reader["Fecha_Revision"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Revision"]),
                IdResponsablePublicacion = reader["Id_Responsable_Publicacion"] as int?,
                FechaPublicacion = reader["Fecha_Publicacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Publicacion"]),
                IdEvaluacionPadre = reader["Id_Evaluacion_Padre"] as int?,
                ObservacionesRevision = reader["Observaciones_Revision"] as string,
                MotivoRechazo = reader["Motivo_Rechazo"] as string,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI
                CodigoSeccion = LeerColumnaString("Codigo_Seccion"),
                NombreMateria = LeerColumnaString("Nombre_Materia"),
                CodigoModelo = LeerColumnaString("Codigo_Modelo"),
                NombreModeloEvaluacion = LeerColumnaString("Nombre_Modelo_Evaluacion"),
                NombrePeriodo = LeerColumnaString("Nombre_Periodo"),
                CodigoPeriodo = LeerColumnaString("Codigo_Periodo"),
                NombreEstado = LeerColumnaString("Nombre_Estado")
            };
        }
    }
}

