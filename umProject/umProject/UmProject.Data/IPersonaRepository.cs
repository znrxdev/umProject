using UmProject.Entities;

namespace UmProject.Data
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> ListarPersonasAsync(int idSesion);
        Task<List<Persona>> FiltrarPersonaPorIdAsync(int idPersona, int idSesion);
        Task<List<Persona>> FiltrarPersonaPorDocumentoAsync(string valorDocumento, int idSesion);
        Task<ResultadoOperacion> AgregarPersonaAsync(Persona persona, int idSesion);
        Task<ResultadoOperacion> ActualizarPersonaAsync(Persona persona, int idSesion);
    }
}

