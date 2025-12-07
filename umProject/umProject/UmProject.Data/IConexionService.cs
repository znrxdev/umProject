using Microsoft.Data.SqlClient;

namespace UmProject.Data
{
    public interface IConexionService
    {
        SqlConnection ObtenerConexion();
    }
}

