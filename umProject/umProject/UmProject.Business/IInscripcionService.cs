using UmProject.Entities;

namespace UmProject.Business
{
    public interface IInscripcionService
    {
        Task<List<Inscripcion>> ListarInscripcionesAsync(int idSesion);
        Task<Inscripcion?> ObtenerInscripcionPorIdAsync(int idInscripcion, int idSesion);
        Task<ResultadoOperacion> AgregarInscripcionAsync(Inscripcion inscripcion, int idSesion);
        Task<ResultadoOperacion> ActualizarInscripcionAsync(Inscripcion inscripcion, int idSesion);
        Task<List<Inscripcion>> ListarInscripcionesDisponiblesAsync(int idSesion);
        Task<List<GrupoInscripcion>> ListarInscripcionesGrupoAsync(int idGrupo, int idSesion);
        Task<ResultadoOperacion> AgregarInscripcionGrupoAsync(int idGrupo, int idInscripcion, string? observaciones, int idSesion);
    }
}

