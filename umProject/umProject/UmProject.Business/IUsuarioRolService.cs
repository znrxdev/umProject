using UmProject.Entities;

namespace UmProject.Business
{
    public interface IUsuarioRolService
    {
        Task<ResultadoOperacion> AgregarUsuarioRolAsync(UsuarioRol usuarioRol, int idSesion);
        Task<ResultadoOperacion> ActualizarUsuarioRolAsync(UsuarioRol usuarioRol, int idSesion);
        Task<List<UsuarioRol>> ListarRolesPorUsuarioAsync(int idUsuario, int idSesion);
        Task<List<UsuarioRol>> FiltrarUsuarioRolPorIdAsync(int idUsuarioRol, int idSesion);
    }
}

