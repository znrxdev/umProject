using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class PeriodoAcademicoService : IPeriodoAcademicoService
    {
        private readonly IPeriodoAcademicoRepository _periodoAcademicoRepository;

        public PeriodoAcademicoService(IPeriodoAcademicoRepository periodoAcademicoRepository)
        {
            _periodoAcademicoRepository = periodoAcademicoRepository;
        }

        public async Task<List<PeriodoAcademico>> ListarPeriodosAsync(int idSesion)
        {
            return await _periodoAcademicoRepository.ListarPeriodosAsync(idSesion);
        }

        public async Task<List<PeriodoAcademico>> FiltrarPeriodoPorIdAsync(int idPeriodo, int idSesion)
        {
            return await _periodoAcademicoRepository.FiltrarPeriodoPorIdAsync(idPeriodo, idSesion);
        }

        public async Task<List<PeriodoAcademico>> FiltrarPeriodoPorCodigoAsync(string codigoPeriodo, int idSesion)
        {
            return await _periodoAcademicoRepository.FiltrarPeriodoPorCodigoAsync(codigoPeriodo, idSesion);
        }

        public async Task<ResultadoOperacion> AgregarPeriodoAsync(PeriodoAcademico periodo, int idSesion)
        {
            return await _periodoAcademicoRepository.AgregarPeriodoAsync(periodo, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarPeriodoAsync(PeriodoAcademico periodo, int idSesion)
        {
            return await _periodoAcademicoRepository.ActualizarPeriodoAsync(periodo, idSesion);
        }
    }
}

