using UmProject.Entities;

namespace UmProject.Data
{
    public interface IInscripcionRepository
    {
        Task<ResultadoConsulta<List<Inscripcion>>> ListarInscripcionesAsync(int idSesion);
        Task<ResultadoConsulta<List<Inscripcion>>> FiltrarInscripcionPorIdAsync(int idInscripcion, int idSesion);
        Task<ResultadoOperacion> AgregarInscripcionAsync(Inscripcion inscripcion, int idSesion);
        Task<ResultadoOperacion> ActualizarInscripcionAsync(Inscripcion inscripcion, int idSesion);
        Task<ResultadoConsulta<List<Inscripcion>>> ListarInscripcionesDisponiblesAsync(int idSesion);
        Task<ResultadoConsulta<List<GrupoInscripcion>>> ListarInscripcionesGrupoAsync(int idGrupo, int idSesion);
        Task<ResultadoOperacion> AgregarInscripcionGrupoAsync(int idGrupo, int idInscripcion, string? observaciones, int idSesion);
    }
}

