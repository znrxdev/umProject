using System;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data; 

namespace CAPA_DATOS
{
    public class CdConexion
    {
        static string conex = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        private SqlConnection conexion = new SqlConnection(conex);

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
            return conexion;
        }
    }
}