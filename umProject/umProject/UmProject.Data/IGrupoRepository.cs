using UmProject.Entities;

namespace UmProject.Data
{
    public interface IGrupoRepository
    {
        Task<ResultadoConsulta<List<Grupo>>> ListarGruposAsync(int idSesion, int? idPeriodo = null);
        Task<ResultadoConsulta<List<Grupo>>> FiltrarGrupoPorIdAsync(int idGrupo, int idSesion);
        Task<ResultadoOperacion> AgregarGrupoAsync(Grupo grupo, int idSesion);
        Task<ResultadoOperacion> ActualizarGrupoAsync(Grupo grupo, int idSesion);
    }
}

