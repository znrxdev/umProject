using UmProject.Entities;

namespace UmProject.Data
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> InicioSesionAsync(string usuario, string contrasena);
        Task<ResultadoOperacion> ActualizarUltimaSesionAsync(int idUsuario);
        Task<List<Usuario>> ListarUsuariosAsync(int idSesion);
        Task<List<Usuario>> FiltrarUsuarioPorUsuarioAsync(string usuario, int idSesion);
        Task<List<Usuario>> FiltrarUsuariosPorIdAsync(int idUsuario, int idSesion);
        Task<List<Usuario>> FiltrarUsuariosPorIdPersonaAsync(int idPersona, int idSesion);
        Task<List<Usuario>> FiltrarUsuariosPorRolAsync(int idRol, int idSesion);
        Task<List<Menu>> ListarMenuPorRolAsync(int idSesion);
        Task<ResultadoOperacion> AgregarUsuariosAsync(Usuario usuario, int idSesion);
        Task<ResultadoOperacion> ActualizarUsuariosAsync(Usuario usuario, int idSesion);
        Task<List<Usuario>> ObtenerEstudiantePorDocumentoAsync(string valorDocumento, int idSesion);
    }
}

