using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;

namespace UmProject.Data
{
    public class RolRepository : IRolRepository
    {
        private readonly IConexionService _conexionService;

        public RolRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Rol>> ListarRolesAsync(int idSesion)
        {
            var roles = new List<Rol>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_roles", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 34; // Listar roles
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                roles.Add(new Rol
                {
                    IdRol = reader["Id_Rol"] as int? ?? 0,
                    NombreRol = reader["Nombre_Rol"] as string ?? string.Empty,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string,
                    IdCreador = reader["Id_Creador"] as int?,
                    IdModificador = reader["Id_Modificador"] as int?,
                    IdTransaccion = reader["Id_Transaccion"] as int?,
                    Activo = reader["Activo"] as bool? ?? false
                });
            }

            return roles;
        }

        public async Task<List<Rol>> FiltrarRolPorIdAsync(int idRol, int idSesion)
        {
            var roles = new List<Rol>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_roles", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 33; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = idRol;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                roles.Add(new Rol
                {
                    IdRol = reader["Id_Rol"] as int? ?? 0,
                    NombreRol = reader["Nombre_Rol"] as string ?? string.Empty,
                    FechaCreacion = reader["Fecha_Creacion"] as string,
                    FechaModificacion = reader["Fecha_Modificacion"] as string,
                    IdCreador = reader["Id_Creador"] as int?,
                    IdModificador = reader["Id_Modificador"] as int?,
                    IdTransaccion = reader["Id_Transaccion"] as int?,
                    Activo = reader["Activo"] as bool? ?? false
                });
            }

            return roles;
        }
    }
}

