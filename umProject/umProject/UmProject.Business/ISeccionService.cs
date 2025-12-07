using UmProject.Entities;

namespace UmProject.Business
{
    public interface ISeccionService
    {
        Task<List<Seccion>> ListarSeccionesAsync(int idSesion, int? idPeriodoAcademico = null);
        Task<Seccion?> ObtenerSeccionPorIdAsync(int idSeccion, int idSesion);
        Task<ResultadoOperacion> AgregarSeccionAsync(Seccion seccion, int idSesion);
        Task<ResultadoOperacion> ActualizarSeccionAsync(Seccion seccion, int idSesion);
    }
}

