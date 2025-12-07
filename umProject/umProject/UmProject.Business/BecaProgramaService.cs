using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class BecaProgramaService : IBecaProgramaService
    {
        private readonly IBecaProgramaRepository _becaProgramaRepository;

        public BecaProgramaService(IBecaProgramaRepository becaProgramaRepository)
        {
            _becaProgramaRepository = becaProgramaRepository;
        }

        public async Task<List<BecaPrograma>> ListarBecaProgramasAsync(int idSesion)
        {
            var resultado = await _becaProgramaRepository.ListarBecaProgramasAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<BecaPrograma>();
        }

        public async Task<BecaPrograma?> ObtenerBecaProgramaPorIdAsync(int idBecaPrograma, int idSesion)
        {
            var resultado = await _becaProgramaRepository.FiltrarBecaProgramaPorIdAsync(idBecaPrograma, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarBecaProgramaAsync(BecaPrograma becaPrograma, int idSesion)
        {
            return await _becaProgramaRepository.AgregarBecaProgramaAsync(becaPrograma, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarBecaProgramaAsync(BecaPrograma becaPrograma, int idSesion)
        {
            return await _becaProgramaRepository.ActualizarBecaProgramaAsync(becaPrograma, idSesion);
        }
    }
}

