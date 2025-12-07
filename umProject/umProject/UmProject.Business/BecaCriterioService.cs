using System.Linq;
using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class BecaCriterioService : IBecaCriterioService
    {
        private readonly IBecaCriterioRepository _repositorio;

        public BecaCriterioService(IBecaCriterioRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<BecaCriterio>> ListarPorProgramaAsync(int idPrograma, int idSesion)
        {
            var resultado = await _repositorio.ListarPorProgramaAsync(idPrograma, idSesion);
            return resultado.Datos ?? new List<BecaCriterio>();
        }

        public async Task<BecaCriterio?> ObtenerPorIdAsync(int idBecaCriterio, int idSesion)
        {
            var resultado = await _repositorio.ObtenerPorIdAsync(idBecaCriterio, idSesion);
            return resultado.Datos?.FirstOrDefault();
        }

        public Task<ResultadoOperacion> AgregarAsync(BecaCriterio criterio, int idSesion)
        {
            return _repositorio.AgregarAsync(criterio, idSesion);
        }

        public Task<ResultadoOperacion> ActualizarAsync(BecaCriterio criterio, int idSesion)
        {
            return _repositorio.ActualizarAsync(criterio, idSesion);
        }
    }
}

