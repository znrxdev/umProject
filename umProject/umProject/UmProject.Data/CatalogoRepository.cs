using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;

namespace UmProject.Data
{
    public class CatalogoRepository : ICatalogoRepository
    {
        private readonly IConexionService _conexionService;

        public CatalogoRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Catalogo>> ListarCatalogosPorTipoAsync(int idTipoCatalogo, int idSesion)
        {
            var catalogos = new List<Catalogo>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_catalogos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 12; // Filtrar por tipo
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Tipo_Catalogo", SqlDbType.Int).Value = idTipoCatalogo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                catalogos.Add(new Catalogo
                {
                    IdCatalogo = reader["Id_Catalogo"] as int? ?? 0,
                    IdTipoCatalogo = reader["Id_Tipo_Catalogo"] as int? ?? 0,
                    NombreCatalogo = reader["Nombre_Catalogo"] as string ?? string.Empty,
                    Activo = reader["Activo"] as bool? ?? false
                });
            }

            return catalogos;
        }
    }
}

