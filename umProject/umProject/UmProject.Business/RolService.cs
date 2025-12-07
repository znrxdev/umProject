using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<List<Rol>> ListarRolesAsync(int idSesion)
        {
            return await _rolRepository.ListarRolesAsync(idSesion);
        }

        public async Task<List<Rol>> FiltrarRolPorIdAsync(int idRol, int idSesion)
        {
            return await _rolRepository.FiltrarRolPorIdAsync(idRol, idSesion);
        }
    }
}

