using UmProject.Entities;

namespace UmProject.Business
{
    public interface IBecaCriterioService
    {
        Task<List<BecaCriterio>> ListarPorProgramaAsync(int idPrograma, int idSesion);
        Task<BecaCriterio?> ObtenerPorIdAsync(int idBecaCriterio, int idSesion);
        Task<ResultadoOperacion> AgregarAsync(BecaCriterio criterio, int idSesion);
        Task<ResultadoOperacion> ActualizarAsync(BecaCriterio criterio, int idSesion);
    }
}

