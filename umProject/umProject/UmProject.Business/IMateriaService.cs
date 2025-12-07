using UmProject.Entities;

namespace UmProject.Business
{
    public interface IMateriaService
    {
        Task<List<Materia>> ListarMateriasAsync(int idSesion);
        Task<List<Materia>> FiltrarMateriaPorIdAsync(int idMateria, int idSesion);
        Task<List<Materia>> FiltrarMateriaPorCodigoAsync(string codigoMateria, int idSesion);
        Task<List<Materia>> FiltrarMateriaPorNombreAsync(string nombreMateria, int idSesion);
        Task<ResultadoOperacion> AgregarMateriaAsync(Materia materia, int idSesion);
        Task<ResultadoOperacion> ActualizarMateriaAsync(Materia materia, int idSesion);
    }
}

