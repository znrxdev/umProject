using UmProject.Entities;

namespace UmProject.Data
{
    public interface IBecaProgramaRepository
    {
        Task<ResultadoConsulta<List<BecaPrograma>>> ListarBecaProgramasAsync(int idSesion);
        Task<ResultadoConsulta<List<BecaPrograma>>> FiltrarBecaProgramaPorIdAsync(int idBecaPrograma, int idSesion);
        Task<ResultadoOperacion> AgregarBecaProgramaAsync(BecaPrograma becaPrograma, int idSesion);
        Task<ResultadoOperacion> ActualizarBecaProgramaAsync(BecaPrograma becaPrograma, int idSesion);
    }
}

