using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class UsuarioRolService : IUsuarioRolService
    {
        private readonly IUsuarioRolRepository _usuarioRolRepository;

        public UsuarioRolService(IUsuarioRolRepository usuarioRolRepository)
        {
            _usuarioRolRepository = usuarioRolRepository;
        }

        public async Task<ResultadoOperacion> AgregarUsuarioRolAsync(UsuarioRol usuarioRol, int idSesion)
        {
            return await _usuarioRolRepository.AgregarUsuarioRolAsync(usuarioRol, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarUsuarioRolAsync(UsuarioRol usuarioRol, int idSesion)
        {
            return await _usuarioRolRepository.ActualizarUsuarioRolAsync(usuarioRol, idSesion);
        }

        public async Task<List<UsuarioRol>> ListarRolesPorUsuarioAsync(int idUsuario, int idSesion)
        {
            return await _usuarioRolRepository.ListarRolesPorUsuarioAsync(idUsuario, idSesion);
        }

        public async Task<List<UsuarioRol>> FiltrarUsuarioRolPorIdAsync(int idUsuarioRol, int idSesion)
        {
            return await _usuarioRolRepository.FiltrarUsuarioRolPorIdAsync(idUsuarioRol, idSesion);
        }
    }
}

