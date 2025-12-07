using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class EvaluacionAlumnoService : IEvaluacionAlumnoService
    {
        private readonly IEvaluacionAlumnoRepository _evaluacionAlumnoRepository;

        public EvaluacionAlumnoService(IEvaluacionAlumnoRepository evaluacionAlumnoRepository)
        {
            _evaluacionAlumnoRepository = evaluacionAlumnoRepository;
        }

        public async Task<List<EvaluacionAlumno>> ListarEvaluacionesAlumnoAsync(int idSesion)
        {
            var resultado = await _evaluacionAlumnoRepository.ListarEvaluacionesAlumnoAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<EvaluacionAlumno>();
        }

        public async Task<EvaluacionAlumno?> ObtenerEvaluacionAlumnoPorIdAsync(int idEvaluacionAlumno, int idSesion)
        {
            var resultado = await _evaluacionAlumnoRepository.FiltrarEvaluacionAlumnoPorIdAsync(idEvaluacionAlumno, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarEvaluacionAlumnoAsync(EvaluacionAlumno evaluacionAlumno, int idSesion)
        {
            return await _evaluacionAlumnoRepository.AgregarEvaluacionAlumnoAsync(evaluacionAlumno, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarEvaluacionAlumnoAsync(EvaluacionAlumno evaluacionAlumno, int idSesion)
        {
            return await _evaluacionAlumnoRepository.ActualizarEvaluacionAlumnoAsync(evaluacionAlumno, idSesion);
        }
    }
}

