using UmProject.Entities;

namespace UmProject.Business
{
    public interface IEvaluacionModeloService
    {
        Task<List<EvaluacionModelo>> ListarEvaluacionesModelosAsync(int idSesion);
        Task<EvaluacionModelo?> ObtenerEvaluacionModeloPorIdAsync(int idEvaluacionModelo, int idSesion);
        Task<ResultadoOperacion> AgregarEvaluacionModeloAsync(EvaluacionModelo evaluacionModelo, int idSesion);
        Task<ResultadoOperacion> ActualizarEvaluacionModeloAsync(EvaluacionModelo evaluacionModelo, int idSesion);
    }
}

