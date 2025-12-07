using UmProject.Entities;

namespace UmProject.Data
{
    public interface ISancionAcademicaRepository
    {
        Task<ResultadoConsulta<List<SancionAcademica>>> ListarSancionesAcademicasAsync(int idSesion);
        Task<ResultadoConsulta<List<SancionAcademica>>> FiltrarSancionAcademicaPorIdAsync(int idSancion, int idSesion);
        Task<ResultadoConsulta<List<SancionAcademica>>> ObtenerMisSancionesAcademicasAsync(int idSesion);
        Task<ResultadoOperacion> AgregarSancionAcademicaAsync(SancionAcademica sancionAcademica, int idSesion);
        Task<ResultadoOperacion> ActualizarSancionAcademicaAsync(SancionAcademica sancionAcademica, int idSesion);
        Task<ResultadoOperacion> ApelarSancionAcademicaAsync(int idSancion, string observacionesApelacion, int idSesion);
    }
}

