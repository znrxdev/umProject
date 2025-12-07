using UmProject.Entities;

namespace UmProject.Data
{
    public interface IUsuarioRolRepository
    {
        Task<ResultadoOperacion> AgregarUsuarioRolAsync(UsuarioRol usuarioRol, int idSesion);
        Task<ResultadoOperacion> ActualizarUsuarioRolAsync(UsuarioRol usuarioRol, int idSesion);
        Task<List<UsuarioRol>> ListarRolesPorUsuarioAsync(int idUsuario, int idSesion);
        Task<List<UsuarioRol>> FiltrarUsuarioRolPorIdAsync(int idUsuarioRol, int idSesion);
    }
}

