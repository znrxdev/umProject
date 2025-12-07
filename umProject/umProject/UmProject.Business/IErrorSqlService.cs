using UmProject.Entities;

namespace UmProject.Business
{
    public interface IErrorSqlService
    {
        Task<List<ErrorSql>> ListarErroresAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null, string? origenError = null);
    }
}

