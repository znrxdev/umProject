using UmProject.Entities;

namespace UmProject.Business
{
    public interface ISancionAcademicaService
    {
        Task<List<SancionAcademica>> ListarSancionesAcademicasAsync(int idSesion);
        Task<SancionAcademica?> ObtenerSancionAcademicaPorIdAsync(int idSancion, int idSesion);
        Task<List<SancionAcademica>> ObtenerMisSancionesAcademicasAsync(int idSesion);
        Task<ResultadoOperacion> AgregarSancionAcademicaAsync(SancionAcademica sancionAcademica, int idSesion);
        Task<ResultadoOperacion> ActualizarSancionAcademicaAsync(SancionAcademica sancionAcademica, int idSesion);
        Task<ResultadoOperacion> ApelarSancionAcademicaAsync(int idSancion, string observacionesApelacion, int idSesion);
    }
}

