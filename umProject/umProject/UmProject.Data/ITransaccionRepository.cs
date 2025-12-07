using UmProject.Entities;

namespace UmProject.Data
{
    public interface ITransaccionRepository
    {
        Task<List<Transaccion>> ListarAuditoriaAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
    }
}

