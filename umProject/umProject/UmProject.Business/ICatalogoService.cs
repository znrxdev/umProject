using UmProject.Entities;

namespace UmProject.Business
{
    public interface ICatalogoService
    {
        Task<List<Catalogo>> ListarCatalogosPorTipoAsync(int idTipoCatalogo, int idSesion);
    }
}

