using UmProject.Entities;

namespace UmProject.Data
{
    public interface IBecaCriterioRepository
    {
        Task<ResultadoConsulta<List<BecaCriterio>>> ListarPorProgramaAsync(int idPrograma, int idSesion);
        Task<ResultadoConsulta<List<BecaCriterio>>> ObtenerPorIdAsync(int idBecaCriterio, int idSesion);
        Task<ResultadoOperacion> AgregarAsync(BecaCriterio criterio, int idSesion);
        Task<ResultadoOperacion> ActualizarAsync(BecaCriterio criterio, int idSesion);
    }
}

