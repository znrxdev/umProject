using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> InicioSesionAsync(string usuario, string contrasena)
        {
            return await _usuarioRepository.InicioSesionAsync(usuario, contrasena);
        }

        public async Task<ResultadoOperacion> ActualizarUltimaSesionAsync(int idUsuario)
        {
            return await _usuarioRepository.ActualizarUltimaSesionAsync(idUsuario);
        }

        public async Task<List<Usuario>> ListarUsuariosAsync(int idSesion)
        {
            return await _usuarioRepository.ListarUsuariosAsync(idSesion);
        }

        public async Task<List<Usuario>> FiltrarUsuarioPorUsuarioAsync(string usuario, int idSesion)
        {
            return await _usuarioRepository.FiltrarUsuarioPorUsuarioAsync(usuario, idSesion);
        }

        public async Task<List<Usuario>> FiltrarUsuariosPorIdAsync(int idUsuario, int idSesion)
        {
            return await _usuarioRepository.FiltrarUsuariosPorIdAsync(idUsuario, idSesion);
        }

        public async Task<List<Usuario>> FiltrarUsuariosPorIdPersonaAsync(int idPersona, int idSesion)
        {
            return await _usuarioRepository.FiltrarUsuariosPorIdPersonaAsync(idPersona, idSesion);
        }

        public async Task<List<Usuario>> FiltrarUsuariosPorRolAsync(int idRol, int idSesion)
        {
            return await _usuarioRepository.FiltrarUsuariosPorRolAsync(idRol, idSesion);
        }

        public async Task<List<Menu>> ListarMenuPorRolAsync(int idSesion)
        {
            return await _usuarioRepository.ListarMenuPorRolAsync(idSesion);
        }

        public async Task<ResultadoOperacion> AgregarUsuariosAsync(Usuario usuario, int idSesion)
        {
            return await _usuarioRepository.AgregarUsuariosAsync(usuario, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarUsuariosAsync(Usuario usuario, int idSesion)
        {
            return await _usuarioRepository.ActualizarUsuariosAsync(usuario, idSesion);
        }

        public async Task<List<Usuario>> ObtenerEstudiantePorDocumentoAsync(string valorDocumento, int idSesion)
        {
            return await _usuarioRepository.ObtenerEstudiantePorDocumentoAsync(valorDocumento, idSesion);
        }
    }
}

