using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class EvaluacionModeloService : IEvaluacionModeloService
    {
        private readonly IEvaluacionModeloRepository _evaluacionModeloRepository;

        public EvaluacionModeloService(IEvaluacionModeloRepository evaluacionModeloRepository)
        {
            _evaluacionModeloRepository = evaluacionModeloRepository;
        }

        public async Task<List<EvaluacionModelo>> ListarEvaluacionesModelosAsync(int idSesion)
        {
            var resultado = await _evaluacionModeloRepository.ListarEvaluacionesModelosAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<EvaluacionModelo>();
        }

        public async Task<EvaluacionModelo?> ObtenerEvaluacionModeloPorIdAsync(int idEvaluacionModelo, int idSesion)
        {
            var resultado = await _evaluacionModeloRepository.FiltrarEvaluacionModeloPorIdAsync(idEvaluacionModelo, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarEvaluacionModeloAsync(EvaluacionModelo evaluacionModelo, int idSesion)
        {
            return await _evaluacionModeloRepository.AgregarEvaluacionModeloAsync(evaluacionModelo, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarEvaluacionModeloAsync(EvaluacionModelo evaluacionModelo, int idSesion)
        {
            return await _evaluacionModeloRepository.ActualizarEvaluacionModeloAsync(evaluacionModelo, idSesion);
        }
    }
}

