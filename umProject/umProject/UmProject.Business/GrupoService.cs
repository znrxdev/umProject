using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class GrupoService : IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;

        public GrupoService(IGrupoRepository grupoRepository)
        {
            _grupoRepository = grupoRepository;
        }

        public async Task<List<Grupo>> ListarGruposAsync(int idSesion, int? idPeriodo = null)
        {
            var resultado = await _grupoRepository.ListarGruposAsync(idSesion, idPeriodo);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<Grupo>();
        }

        public async Task<Grupo?> ObtenerGrupoPorIdAsync(int idGrupo, int idSesion)
        {
            var resultado = await _grupoRepository.FiltrarGrupoPorIdAsync(idGrupo, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarGrupoAsync(Grupo grupo, int idSesion)
        {
            return await _grupoRepository.AgregarGrupoAsync(grupo, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarGrupoAsync(Grupo grupo, int idSesion)
        {
            return await _grupoRepository.ActualizarGrupoAsync(grupo, idSesion);
        }
    }
}

