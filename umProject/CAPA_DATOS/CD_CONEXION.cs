using System;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data; 

namespace CD_DATOS
{
    public class CD_CONEXION
    {
        static string Conex = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        private SqlConnection Conexion = new SqlConnection(Conex);

        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }
}