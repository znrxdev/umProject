using UmProject.Entities;

namespace UmProject.Business
{
    public interface IEvaluacionAlumnoService
    {
        Task<List<EvaluacionAlumno>> ListarEvaluacionesAlumnoAsync(int idSesion);
        Task<EvaluacionAlumno?> ObtenerEvaluacionAlumnoPorIdAsync(int idEvaluacionAlumno, int idSesion);
        Task<ResultadoOperacion> AgregarEvaluacionAlumnoAsync(EvaluacionAlumno evaluacionAlumno, int idSesion);
        Task<ResultadoOperacion> ActualizarEvaluacionAlumnoAsync(EvaluacionAlumno evaluacionAlumno, int idSesion);
    }
}

