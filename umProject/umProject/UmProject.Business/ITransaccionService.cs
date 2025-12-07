using UmProject.Entities;

namespace UmProject.Business
{
    public interface ITransaccionService
    {
        Task<List<Transaccion>> ListarAuditoriaAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
    }
}

