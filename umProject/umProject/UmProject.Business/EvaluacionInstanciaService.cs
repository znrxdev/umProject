using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class EvaluacionInstanciaService : IEvaluacionInstanciaService
    {
        private readonly IEvaluacionInstanciaRepository _evaluacionInstanciaRepository;

        public EvaluacionInstanciaService(IEvaluacionInstanciaRepository evaluacionInstanciaRepository)
        {
            _evaluacionInstanciaRepository = evaluacionInstanciaRepository;
        }

        public async Task<List<EvaluacionInstancia>> ListarEvaluacionesInstanciasAsync(int idSesion)
        {
            var resultado = await _evaluacionInstanciaRepository.ListarEvaluacionesInstanciasAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<EvaluacionInstancia>();
        }

        public async Task<EvaluacionInstancia?> ObtenerEvaluacionInstanciaPorIdAsync(int idEvaluacionInstancia, int idSesion)
        {
            var resultado = await _evaluacionInstanciaRepository.FiltrarEvaluacionInstanciaPorIdAsync(idEvaluacionInstancia, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarEvaluacionInstanciaAsync(EvaluacionInstancia evaluacionInstancia, int idSesion)
        {
            return await _evaluacionInstanciaRepository.AgregarEvaluacionInstanciaAsync(evaluacionInstancia, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarEvaluacionInstanciaAsync(EvaluacionInstancia evaluacionInstancia, int idSesion)
        {
            return await _evaluacionInstanciaRepository.ActualizarEvaluacionInstanciaAsync(evaluacionInstancia, idSesion);
        }
    }
}

