using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;

        public TransaccionService(ITransaccionRepository transaccionRepository)
        {
            _transaccionRepository = transaccionRepository;
        }

        public async Task<List<Transaccion>> ListarAuditoriaAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _transaccionRepository.ListarAuditoriaAsync(idSesion, fechaInicio, fechaFin);
        }
    }
}

