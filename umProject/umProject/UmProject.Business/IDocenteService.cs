using UmProject.Entities;

namespace UmProject.Business
{
    public interface IDocenteService
    {
        Task<List<Docente>> ListarDocentesAsync(int? idSesion);
        Task<DocenteDetalle?> ObtenerDocenteDetalleAsync(int idUsuario, int? idSesion);
        Task<List<DocenteEvaluacion>> ObtenerEvaluacionesRealizadasAsync(int idUsuario, int? idSesion, int? idPeriodo = null);
        Task<DocenteEvaluacionDetalle?> ObtenerDetalleEvaluacionAsync(int idEvaluacionAlumno, int? idSesion);
        Task<List<DocenteSeccion>> ObtenerSeccionesAsignadasAsync(int idUsuario, int? idSesion);
        Task<List<DocenteSeccionEstudiante>> ObtenerEstudiantesSeccionAsync(int idSeccion, int? idSesion);
    }
}

