using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace UmProject.Data
{
    public class ConexionService : IConexionService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ConexionService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

