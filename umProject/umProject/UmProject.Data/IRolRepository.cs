using UmProject.Entities;

namespace UmProject.Data
{
    public interface IRolRepository
    {
        Task<List<Rol>> ListarRolesAsync(int idSesion);
        Task<List<Rol>> FiltrarRolPorIdAsync(int idRol, int idSesion);
    }
}

