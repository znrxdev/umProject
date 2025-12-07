using UmProject.Entities;

namespace UmProject.Data
{
    public interface IEvaluacionModeloRepository
    {
        Task<ResultadoConsulta<List<EvaluacionModelo>>> ListarEvaluacionesModelosAsync(int idSesion);
        Task<ResultadoConsulta<List<EvaluacionModelo>>> FiltrarEvaluacionModeloPorIdAsync(int idEvaluacionModelo, int idSesion);
        Task<ResultadoConsulta<List<EvaluacionModelo>>> FiltrarEvaluacionModeloPorMateriaAsync(int idMateria, int idSesion);
        Task<ResultadoOperacion> AgregarEvaluacionModeloAsync(EvaluacionModelo evaluacionModelo, int idSesion);
        Task<ResultadoOperacion> ActualizarEvaluacionModeloAsync(EvaluacionModelo evaluacionModelo, int idSesion);
    }
}

