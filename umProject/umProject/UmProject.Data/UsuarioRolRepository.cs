using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;

namespace UmProject.Data
{
    public class UsuarioRolRepository : IUsuarioRolRepository
    {
        private readonly IConexionService _conexionService;

        public UsuarioRolRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoOperacion> AgregarUsuarioRolAsync(UsuarioRol usuarioRol, int idSesion)
        {
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios_roles", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 47; // Agregar usuario rol
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = usuarioRol.IdUsuario ?? 0;
            cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = usuarioRol.IdRol ?? 0;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = usuarioRol.Activo ?? true;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            var oNum = (int)(cmd.Parameters["@o_Num"].Value ?? -1);
            var oMsg = cmd.Parameters["@o_Msg"].Value?.ToString() ?? "Error desconocido";

            return new ResultadoOperacion
            {
                Mensaje = oMsg,
                Codigo = oNum
            };
        }

        public async Task<ResultadoOperacion> ActualizarUsuarioRolAsync(UsuarioRol usuarioRol, int idSesion)
        {
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios_roles", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 48; // Actualizar usuario rol
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Usuario_Rol", SqlDbType.Int).Value = usuarioRol.IdUsuarioRol ?? 0;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = usuarioRol.IdUsuario ?? 0;
            cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = usuarioRol.IdRol ?? 0;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = usuarioRol.Activo ?? true;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            var oNum = (int)(cmd.Parameters["@o_Num"].Value ?? -1);
            var oMsg = cmd.Parameters["@o_Msg"].Value?.ToString() ?? "Error desconocido";

            return new ResultadoOperacion
            {
                Mensaje = oMsg,
                Codigo = oNum
            };
        }

        public async Task<List<UsuarioRol>> ListarRolesPorUsuarioAsync(int idUsuario, int idSesion)
        {
            var usuarioRoles = new List<UsuarioRol>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios_roles", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 50; // Listar roles de usuario
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                usuarioRoles.Add(new UsuarioRol
                {
                    IdUsuarioRol = reader["Id_Usuario_Rol"] as int? ?? 0,
                    IdUsuario = reader["Id_Usuario"] as int? ?? 0,
                    IdRol = reader["Id_Rol"] as int? ?? 0,
                    FechaCreacion = reader["Fecha_Creacion"]?.ToString(),
                    FechaModificacion = reader["Fecha_Modificacion"]?.ToString(),
                    IdCreador = reader["Id_Creador"] as int?,
                    IdModificador = reader["Id_Modificador"] as int?,
                    IdTransaccion = reader["Id_Transaccion"] as int?,
                    Activo = reader["Activo"] as bool? ?? false
                });
            }

            return usuarioRoles;
        }

        public async Task<List<UsuarioRol>> FiltrarUsuarioRolPorIdAsync(int idUsuarioRol, int idSesion)
        {
            var usuarioRoles = new List<UsuarioRol>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios_roles", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 49; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Usuario_Rol", SqlDbType.Int).Value = idUsuarioRol;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                usuarioRoles.Add(new UsuarioRol
                {
                    IdUsuarioRol = reader["Id_Usuario_Rol"] as int? ?? 0,
                    IdUsuario = reader["Id_Usuario"] as int? ?? 0,
                    IdRol = reader["Id_Rol"] as int? ?? 0,
                    FechaCreacion = reader["Fecha_Creacion"]?.ToString(),
                    FechaModificacion = reader["Fecha_Modificacion"]?.ToString(),
                    IdCreador = reader["Id_Creador"] as int?,
                    IdModificador = reader["Id_Modificador"] as int?,
                    IdTransaccion = reader["Id_Transaccion"] as int?,
                    Activo = reader["Activo"] as bool? ?? false
                });
            }

            return usuarioRoles;
        }
    }
}

