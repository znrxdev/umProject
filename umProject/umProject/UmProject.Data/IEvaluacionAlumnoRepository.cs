using UmProject.Entities;

namespace UmProject.Data
{
    public interface IEvaluacionAlumnoRepository
    {
        Task<ResultadoConsulta<List<EvaluacionAlumno>>> ListarEvaluacionesAlumnoAsync(int idSesion);
        Task<ResultadoConsulta<List<EvaluacionAlumno>>> FiltrarEvaluacionAlumnoPorIdAsync(int idEvaluacionAlumno, int idSesion);
        Task<ResultadoOperacion> AgregarEvaluacionAlumnoAsync(EvaluacionAlumno evaluacionAlumno, int idSesion);
        Task<ResultadoOperacion> ActualizarEvaluacionAlumnoAsync(EvaluacionAlumno evaluacionAlumno, int idSesion);
    }
}

