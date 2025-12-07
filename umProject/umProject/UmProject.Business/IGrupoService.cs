using UmProject.Entities;

namespace UmProject.Business
{
    public interface IGrupoService
    {
        Task<List<Grupo>> ListarGruposAsync(int idSesion, int? idPeriodo = null);
        Task<Grupo?> ObtenerGrupoPorIdAsync(int idGrupo, int idSesion);
        Task<ResultadoOperacion> AgregarGrupoAsync(Grupo grupo, int idSesion);
        Task<ResultadoOperacion> ActualizarGrupoAsync(Grupo grupo, int idSesion);
    }
}

