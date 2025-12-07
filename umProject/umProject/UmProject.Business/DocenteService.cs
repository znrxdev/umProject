using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class DocenteService : IDocenteService
    {
        private readonly IDocenteRepository _docenteRepository;

        public DocenteService(IDocenteRepository docenteRepository)
        {
            _docenteRepository = docenteRepository;
        }

        public async Task<List<Docente>> ListarDocentesAsync(int? idSesion)
        {
            return await _docenteRepository.ListarDocentesAsync(idSesion);
        }

        public async Task<DocenteDetalle?> ObtenerDocenteDetalleAsync(int idUsuario, int? idSesion)
        {
            return await _docenteRepository.ObtenerDocenteDetalleAsync(idUsuario, idSesion);
        }

        public async Task<List<DocenteEvaluacion>> ObtenerEvaluacionesRealizadasAsync(int idUsuario, int? idSesion, int? idPeriodo = null)
        {
            return await _docenteRepository.ObtenerEvaluacionesRealizadasAsync(idUsuario, idSesion, idPeriodo);
        }

        public async Task<DocenteEvaluacionDetalle?> ObtenerDetalleEvaluacionAsync(int idEvaluacionAlumno, int? idSesion)
        {
            return await _docenteRepository.ObtenerDetalleEvaluacionAsync(idEvaluacionAlumno, idSesion);
        }

        public async Task<List<DocenteSeccion>> ObtenerSeccionesAsignadasAsync(int idUsuario, int? idSesion)
        {
            return await _docenteRepository.ObtenerSeccionesAsignadasAsync(idUsuario, idSesion);
        }

        public async Task<List<DocenteSeccionEstudiante>> ObtenerEstudiantesSeccionAsync(int idSeccion, int? idSesion)
        {
            return await _docenteRepository.ObtenerEstudiantesSeccionAsync(idSeccion, idSesion);
        }
    }
}

