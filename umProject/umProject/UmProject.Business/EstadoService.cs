using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task<List<Estado>> FiltrarEstadosPorTipoTransaccionAsync(int idTipoTransaccion, int idSesion)
        {
            return await _estadoRepository.FiltrarEstadosPorTipoTransaccionAsync(idTipoTransaccion, idSesion);
        }
    }
}

