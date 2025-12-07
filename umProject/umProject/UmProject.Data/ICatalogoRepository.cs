using UmProject.Entities;

namespace UmProject.Data
{
    public interface ICatalogoRepository
    {
        Task<List<Catalogo>> ListarCatalogosPorTipoAsync(int idTipoCatalogo, int idSesion);
    }
}

