using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<List<Persona>> ListarPersonasAsync(int idSesion)
        {
            return await _personaRepository.ListarPersonasAsync(idSesion);
        }

        public async Task<List<Persona>> FiltrarPersonaPorIdAsync(int idPersona, int idSesion)
        {
            return await _personaRepository.FiltrarPersonaPorIdAsync(idPersona, idSesion);
        }

        public async Task<List<Persona>> FiltrarPersonaPorDocumentoAsync(string valorDocumento, int idSesion)
        {
            return await _personaRepository.FiltrarPersonaPorDocumentoAsync(valorDocumento, idSesion);
        }

        public async Task<ResultadoOperacion> AgregarPersonaAsync(Persona persona, int idSesion)
        {
            return await _personaRepository.AgregarPersonaAsync(persona, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarPersonaAsync(Persona persona, int idSesion)
        {
            return await _personaRepository.ActualizarPersonaAsync(persona, idSesion);
        }
    }
}

