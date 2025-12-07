using UmProject.Entities;

namespace UmProject.Data
{
    public interface IEstudianteRepository
    {
        Task<List<Estudiante>> ListarEstudiantesAsync(int? idSesion);
        Task<List<Estudiante>> ListarEstudiantesSinInscripcionesAsync(int? idSesion);
        Task<EstudianteDetalle?> ObtenerEstudianteDetalleAsync(int idUsuario, int? idSesion);
        Task<List<EstudianteInscripcion>> ObtenerInscripcionesAsync(int idUsuario, int? idSesion);
        Task<List<EstudianteGrupo>> ObtenerGruposAsync(int idUsuario, int? idSesion);
        Task<List<EstudianteSeccion>> ObtenerSeccionesAsync(int idUsuario, int? idSesion);
        Task<List<EstudiantePeriodo>> ObtenerPeriodosAsync(int idUsuario, int? idSesion);
        Task<List<EstudianteEvaluacion>> ObtenerEvaluacionesAsync(int idUsuario, int? idSesion, bool? soloActuales = null);
        Task<List<EstudianteDesempeno>> ObtenerDesempenoPorPeriodoAsync(int idUsuario, int? idSesion);
        Task<List<EstudianteSancion>> ObtenerSancionesAsync(int idUsuario, int? idSesion, bool? soloActivas = null);
        Task<List<EstudianteSolicitudBeca>> ObtenerSolicitudesBecasAsync(int idUsuario, int? idSesion);
        Task<List<BecaPrograma>> ObtenerProgramasBecaDisponiblesAsync(int idSesion);
        Task<ResultadoOperacion> AplicarSolicitudBecaAsync(int idBecaPrograma, string? observaciones, int idSesion);
        Task<List<EstudianteSolicitudBecaHistorial>> ObtenerHistorialSolicitudesBecaAsync(int idUsuario, int? idSesion);
    }
}

