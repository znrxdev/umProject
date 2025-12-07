using UmProject.Entities;

namespace UmProject.Data
{
    public interface IErrorSqlRepository
    {
        Task<List<ErrorSql>> ListarErroresAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null, string? origenError = null);
    }
}

