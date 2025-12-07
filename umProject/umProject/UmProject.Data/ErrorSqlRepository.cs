using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class ErrorSqlRepository : IErrorSqlRepository
    {
        private readonly IConexionService _conexionService;

        public ErrorSqlRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<ErrorSql>> ListarErroresAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null, string? origenError = null)
        {
            var errores = new List<ErrorSql>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_logs_errores_sql", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 154;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
            {
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            }
            
            if (fechaFin.HasValue)
            {
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            }

            if (!string.IsNullOrWhiteSpace(origenError))
            {
                cmd.Parameters.Add("@Origen_Error", SqlDbType.NVarChar, 50).Value = origenError;
            }
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                errores.Add(LlenarModelo(reader));
            }

            return errores;
        }

        private ErrorSql LlenarModelo(SqlDataReader reader)
        {
            return new ErrorSql
            {
                IdError = reader["Id_Error"] as int? ?? 0,
                OrigenError = reader["Origen_Error"] as string,
                LineaError = reader["Linea_Error"] as int?,
                NumeroError = reader["Numero_Error"] as int?,
                MensajeError = reader["Mensaje_Error"] as string,
                FechaError = reader["Fecha_Error"] as string
            };
        }
    }
}

