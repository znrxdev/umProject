using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConexionService _conexionService;

        public UsuarioRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Usuario>> InicioSesionAsync(string usuario, string contrasena)
        {
            var usuarios = new List<Usuario>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = usuario;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 19;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var usuarioObj = new Usuario
                    {
                        IdUsuario = reader["Id_Usuario"] as int? ?? 0,
                        IdPersona = reader["Id_Persona"] as int? ?? 0,
                        UsuarioNombre = reader["Usuario"] as string ?? string.Empty,
                        Contrasena = reader["Contrasena"] as string ?? string.Empty
                    };

                    if (Utilidades.VerificarContrasena(contrasena, usuarioObj.Contrasena))
                    {
                        usuarioObj.Contrasena = string.Empty;
                        usuarios.Add(usuarioObj);
                    }
                }
            }
            
            // Verificar resultado (InicioSesion puede retornar o_Num = 0 si credenciales incorrectas, eso es válido)
            var oNum = cmd.Parameters["@o_Num"].Value != DBNull.Value ? Convert.ToInt32(cmd.Parameters["@o_Num"].Value) : 0;
            var oMsg = cmd.Parameters["@o_Msg"].Value?.ToString() ?? string.Empty;
            
            // Solo lanzar excepción si o_Num = -1 (error real)
            if (oNum == -1)
            {
                throw new Exception(oMsg);
            }

            return usuarios;
        }

        public async Task<ResultadoOperacion> ActualizarUltimaSesionAsync(int idUsuario)
        {
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@EstadoLogin", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return RepositorioHelper.ObtenerResultado(cmd);
        }

        public async Task<List<Usuario>> ListarUsuariosAsync(int idSesion)
        {
            var usuarios = new List<Usuario>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 22;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    usuarios.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return usuarios;
        }

        public async Task<List<Usuario>> FiltrarUsuarioPorUsuarioAsync(string usuario, int idSesion)
        {
            var usuarios = new List<Usuario>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 24;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = usuario;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    usuarios.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return usuarios;
        }

        public async Task<List<Usuario>> FiltrarUsuariosPorIdAsync(int idUsuario, int idSesion)
        {
            var usuarios = new List<Usuario>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 23;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    usuarios.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return usuarios;
        }

        public async Task<List<Usuario>> FiltrarUsuariosPorIdPersonaAsync(int idPersona, int idSesion)
        {
            var usuarios = new List<Usuario>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 25;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = idPersona;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    usuarios.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return usuarios;
        }

        public async Task<List<Usuario>> FiltrarUsuariosPorRolAsync(int idRol, int idSesion)
        {
            var usuarios = new List<Usuario>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 143;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = idRol;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    usuarios.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return usuarios;
        }

        public async Task<List<Menu>> ListarMenuPorRolAsync(int idSesion)
        {
            var menus = new List<Menu>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 26;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                menus.Add(new Menu
                {
                    IdMenu = reader["Id_Menu"] as int? ?? 0,
                    MenuNombre = reader["Menu"] as string ?? string.Empty, // Campo Menu (ej: "Usuarios")
                    NombreBoton = reader["Nombre_Boton"] as string ?? string.Empty // Campo Nombre_Boton (ej: "btn_UsuarioMenu")
                });
            }

            return menus;
        }

        public async Task<ResultadoOperacion> AgregarUsuariosAsync(Usuario usuario, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 20;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = usuario.IdPersona;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = usuario.UsuarioNombre;
            cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 100).Value = Utilidades.HashearContrasena(usuario.Contrasena ?? string.Empty);
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = usuario.IdEstado;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarUsuariosAsync(Usuario usuario, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 21;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = usuario.IdUsuario;
            cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 100).Value = usuario.Contrasena != null ? Utilidades.HashearContrasena(usuario.Contrasena) : null;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = usuario.IdEstado;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<List<Usuario>> ObtenerEstudiantePorDocumentoAsync(string valorDocumento, int idSesion)
        {
            var usuarios = new List<Usuario>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_usuarios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 133;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.VarChar, 50).Value = valorDocumento;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    usuarios.Add(new Usuario
                    {
                        IdUsuario = reader["Id_Usuario"] as int? ?? 0,
                        IdPersona = reader["Id_Persona"] as int? ?? 0,
                        UsuarioNombre = reader["Usuario"] as string ?? string.Empty,
                        ValorDocumento = reader["Valor_Documento"] as string ?? string.Empty,
                        NombreCompleto = reader["Nombre_Completo"] as string ?? string.Empty,
                        UltimaSesion = reader["Ultima_Sesion"] as string ?? string.Empty,
                        UltimoCambioContrasena = reader["Ultimo_Cambio_Contrasena"] as string ?? string.Empty,
                        FechaCreacion = reader["Fecha_Creacion"] as string ?? string.Empty,
                        FechaModificacion = reader["Fecha_Modificacion"] as string ?? string.Empty,
                        IdCreador = reader["Id_Creador"] as int? ?? 0,
                        IdModificador = reader["Id_Modificador"] as int? ?? 0,
                        IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                        IdEstado = reader["Id_Estado"] as int? ?? 0
                    });
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return usuarios;
        }

        private Usuario LlenarModelo(SqlDataReader reader)
        {
            return new Usuario
            {
                IdUsuario = reader["Id_Usuario"] as int? ?? 0,
                IdPersona = reader["Id_Persona"] as int? ?? 0,
                UsuarioNombre = reader["Usuario"] as string ?? string.Empty,
                UltimaSesion = reader["Ultima_Sesion"] as string ?? string.Empty,
                UltimoCambioContrasena = reader["Ultimo_Cambio_Contrasena"] as string ?? string.Empty,
                FechaCreacion = reader["Fecha_Creacion"] as string ?? string.Empty,
                FechaModificacion = reader["Fecha_Modificacion"] as string ?? string.Empty,
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                IdEstado = reader["Id_Estado"] as int? ?? 0
            };
        }
    }
}

