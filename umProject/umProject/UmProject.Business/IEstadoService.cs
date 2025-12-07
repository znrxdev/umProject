using UmProject.Entities;

namespace UmProject.Business
{
    public interface IEstadoService
    {
        Task<List<Estado>> FiltrarEstadosPorTipoTransaccionAsync(int idTipoTransaccion, int idSesion);
    }
}

