using UmProject.Entities;

namespace UmProject.Business
{
    public interface IBecaProgramaService
    {
        Task<List<BecaPrograma>> ListarBecaProgramasAsync(int idSesion);
        Task<BecaPrograma?> ObtenerBecaProgramaPorIdAsync(int idBecaPrograma, int idSesion);
        Task<ResultadoOperacion> AgregarBecaProgramaAsync(BecaPrograma becaPrograma, int idSesion);
        Task<ResultadoOperacion> ActualizarBecaProgramaAsync(BecaPrograma becaPrograma, int idSesion);
    }
}

