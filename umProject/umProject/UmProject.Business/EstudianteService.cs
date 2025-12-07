using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepository;

        public EstudianteService(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        public async Task<List<Estudiante>> ListarEstudiantesAsync(int? idSesion)
        {
            return await _estudianteRepository.ListarEstudiantesAsync(idSesion);
        }

        public async Task<List<Estudiante>> ListarEstudiantesSinInscripcionesAsync(int? idSesion)
        {
            return await _estudianteRepository.ListarEstudiantesSinInscripcionesAsync(idSesion);
        }

        public async Task<EstudianteDetalle?> ObtenerEstudianteDetalleAsync(int idUsuario, int? idSesion)
        {
            return await _estudianteRepository.ObtenerEstudianteDetalleAsync(idUsuario, idSesion);
        }

        public async Task<List<EstudianteInscripcion>> ObtenerInscripcionesAsync(int idUsuario, int? idSesion)
        {
            return await _estudianteRepository.ObtenerInscripcionesAsync(idUsuario, idSesion);
        }

        public async Task<List<EstudianteGrupo>> ObtenerGruposAsync(int idUsuario, int? idSesion)
        {
            return await _estudianteRepository.ObtenerGruposAsync(idUsuario, idSesion);
        }

        public async Task<List<EstudianteSeccion>> ObtenerSeccionesAsync(int idUsuario, int? idSesion)
        {
            return await _estudianteRepository.ObtenerSeccionesAsync(idUsuario, idSesion);
        }

        public async Task<List<EstudiantePeriodo>> ObtenerPeriodosAsync(int idUsuario, int? idSesion)
        {
            return await _estudianteRepository.ObtenerPeriodosAsync(idUsuario, idSesion);
        }

        public async Task<List<EstudianteEvaluacion>> ObtenerEvaluacionesAsync(int idUsuario, int? idSesion, bool? soloActuales = null)
        {
            return await _estudianteRepository.ObtenerEvaluacionesAsync(idUsuario, idSesion, soloActuales);
        }

        public async Task<List<EstudianteDesempeno>> ObtenerDesempenoPorPeriodoAsync(int idUsuario, int? idSesion)
        {
            return await _estudianteRepository.ObtenerDesempenoPorPeriodoAsync(idUsuario, idSesion);
        }

        public async Task<List<EstudianteSancion>> ObtenerSancionesAsync(int idUsuario, int? idSesion, bool? soloActivas = null)
        {
            return await _estudianteRepository.ObtenerSancionesAsync(idUsuario, idSesion, soloActivas);
        }

        public async Task<List<EstudianteSolicitudBeca>> ObtenerSolicitudesBecasAsync(int idUsuario, int? idSesion)
        {
            return await _estudianteRepository.ObtenerSolicitudesBecasAsync(idUsuario, idSesion);
        }

        public async Task<List<BecaPrograma>> ObtenerProgramasBecaDisponiblesAsync(int idSesion)
        {
            return await _estudianteRepository.ObtenerProgramasBecaDisponiblesAsync(idSesion);
        }

        public async Task<ResultadoOperacion> AplicarSolicitudBecaAsync(int idBecaPrograma, string? observaciones, int idSesion)
        {
            return await _estudianteRepository.AplicarSolicitudBecaAsync(idBecaPrograma, observaciones, idSesion);
        }

        public async Task<List<EstudianteSolicitudBecaHistorial>> ObtenerHistorialSolicitudesBecaAsync(int idUsuario, int? idSesion)
        {
            return await _estudianteRepository.ObtenerHistorialSolicitudesBecaAsync(idUsuario, idSesion);
        }
    }
}

