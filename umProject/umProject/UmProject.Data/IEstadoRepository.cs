using UmProject.Entities;

namespace UmProject.Data
{
    public interface IEstadoRepository
    {
        Task<List<Estado>> FiltrarEstadosPorTipoTransaccionAsync(int idTipoTransaccion, int idSesion);
    }
}

