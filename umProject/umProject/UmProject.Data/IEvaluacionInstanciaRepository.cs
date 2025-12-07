using UmProject.Entities;

namespace UmProject.Data
{
    public interface IEvaluacionInstanciaRepository
    {
        Task<ResultadoConsulta<List<EvaluacionInstancia>>> ListarEvaluacionesInstanciasAsync(int idSesion);
        Task<ResultadoConsulta<List<EvaluacionInstancia>>> FiltrarEvaluacionInstanciaPorIdAsync(int idEvaluacionInstancia, int idSesion);
        Task<ResultadoOperacion> AgregarEvaluacionInstanciaAsync(EvaluacionInstancia evaluacionInstancia, int idSesion);
        Task<ResultadoOperacion> ActualizarEvaluacionInstanciaAsync(EvaluacionInstancia evaluacionInstancia, int idSesion);
    }
}

