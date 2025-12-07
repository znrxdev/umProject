using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class ErrorSqlService : IErrorSqlService
    {
        private readonly IErrorSqlRepository _errorSqlRepository;

        public ErrorSqlService(IErrorSqlRepository errorSqlRepository)
        {
            _errorSqlRepository = errorSqlRepository;
        }

        public async Task<List<ErrorSql>> ListarErroresAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null, string? origenError = null)
        {
            return await _errorSqlRepository.ListarErroresAsync(idSesion, fechaInicio, fechaFin, origenError);
        }
    }
}

