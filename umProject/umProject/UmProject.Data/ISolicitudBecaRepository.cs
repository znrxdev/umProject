using UmProject.Entities;

namespace UmProject.Data
{
    public interface ISolicitudBecaRepository
    {
        Task<ResultadoConsulta<List<SolicitudBeca>>> ListarSolicitudesBecaAsync(int idSesion);
        Task<ResultadoConsulta<List<SolicitudBeca>>> FiltrarSolicitudBecaPorIdAsync(int idSolicitudBeca, int idSesion);
        Task<ResultadoOperacion> AgregarSolicitudBecaAsync(SolicitudBeca solicitudBeca, int idSesion);
        Task<ResultadoOperacion> ActualizarSolicitudBecaAsync(SolicitudBeca solicitudBeca, int idSesion);
    }
}

