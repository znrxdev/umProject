using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class CatalogoService : ICatalogoService
    {
        private readonly ICatalogoRepository _catalogoRepository;

        public CatalogoService(ICatalogoRepository catalogoRepository)
        {
            _catalogoRepository = catalogoRepository;
        }

        public async Task<List<Catalogo>> ListarCatalogosPorTipoAsync(int idTipoCatalogo, int idSesion)
        {
            return await _catalogoRepository.ListarCatalogosPorTipoAsync(idTipoCatalogo, idSesion);
        }
    }
}

