using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;

namespace UmProject.Data
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly IConexionService _conexionService;

        public EstadoRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Estado>> FiltrarEstadosPorTipoTransaccionAsync(int idTipoTransaccion, int idSesion)
        {
            var estados = new List<Estado>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_estados", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 4; // Filtrar estados por tipo transacción
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = idTipoTransaccion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                estados.Add(new Estado
                {
                    IdEstado = reader["Id_Estado"] as int? ?? 0,
                    NombreEstado = reader["Nombre_Estado"] as string ?? string.Empty
                });
            }

            return estados;
        }
    }
}

