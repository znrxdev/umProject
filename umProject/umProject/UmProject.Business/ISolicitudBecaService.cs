using UmProject.Entities;

namespace UmProject.Business
{
    public interface ISolicitudBecaService
    {
        Task<List<SolicitudBeca>> ListarSolicitudesBecaAsync(int idSesion);
        Task<SolicitudBeca?> ObtenerSolicitudBecaPorIdAsync(int idSolicitudBeca, int idSesion);
        Task<ResultadoOperacion> AgregarSolicitudBecaAsync(SolicitudBeca solicitudBeca, int idSesion);
        Task<ResultadoOperacion> ActualizarSolicitudBecaAsync(SolicitudBeca solicitudBeca, int idSesion);
    }
}

