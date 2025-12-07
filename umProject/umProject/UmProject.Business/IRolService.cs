using UmProject.Entities;

namespace UmProject.Business
{
    public interface IRolService
    {
        Task<List<Rol>> ListarRolesAsync(int idSesion);
        Task<List<Rol>> FiltrarRolPorIdAsync(int idRol, int idSesion);
    }
}

