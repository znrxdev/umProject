using UmProject.Entities;

namespace UmProject.Data
{
    public interface IPeriodoAcademicoRepository
    {
        Task<List<PeriodoAcademico>> ListarPeriodosAsync(int idSesion);
        Task<List<PeriodoAcademico>> FiltrarPeriodoPorIdAsync(int idPeriodo, int idSesion);
        Task<List<PeriodoAcademico>> FiltrarPeriodoPorCodigoAsync(string codigoPeriodo, int idSesion);
        Task<ResultadoOperacion> AgregarPeriodoAsync(PeriodoAcademico periodo, int idSesion);
        Task<ResultadoOperacion> ActualizarPeriodoAsync(PeriodoAcademico periodo, int idSesion);
    }
}

