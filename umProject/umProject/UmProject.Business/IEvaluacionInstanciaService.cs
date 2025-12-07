using UmProject.Entities;

namespace UmProject.Business
{
    public interface IEvaluacionInstanciaService
    {
        Task<List<EvaluacionInstancia>> ListarEvaluacionesInstanciasAsync(int idSesion);
        Task<EvaluacionInstancia?> ObtenerEvaluacionInstanciaPorIdAsync(int idEvaluacionInstancia, int idSesion);
        Task<ResultadoOperacion> AgregarEvaluacionInstanciaAsync(EvaluacionInstancia evaluacionInstancia, int idSesion);
        Task<ResultadoOperacion> ActualizarEvaluacionInstanciaAsync(EvaluacionInstancia evaluacionInstancia, int idSesion);
    }
}

