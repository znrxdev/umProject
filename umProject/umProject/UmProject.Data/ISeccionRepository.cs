using UmProject.Entities;

namespace UmProject.Data
{
    public interface ISeccionRepository
    {
        Task<ResultadoConsulta<List<Seccion>>> ListarSeccionesAsync(int idSesion, int? idPeriodoAcademico = null);
        Task<ResultadoConsulta<List<Seccion>>> FiltrarSeccionPorIdAsync(int idSeccion, int idSesion);
        Task<ResultadoConsulta<List<Seccion>>> FiltrarSeccionPorDocenteAsync(int idDocente, int idSesion);
        Task<ResultadoConsulta<List<Seccion>>> FiltrarSeccionPorMateriaPeriodoAsync(int idMateriaPeriodo, int idSesion);
        Task<ResultadoOperacion> AgregarSeccionAsync(Seccion seccion, int idSesion);
        Task<ResultadoOperacion> ActualizarSeccionAsync(Seccion seccion, int idSesion);
    }
}

