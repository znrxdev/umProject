using Microsoft.Data.SqlClient;
using System.Data;

namespace UmProject.Data
{
    /// <summary>
    /// Helper para operaciones comunes en repositorios
    /// </summary>
    public static class RepositorioHelper
    {
        /// <summary>
        /// Captura los parámetros de salida después de cerrar un SqlDataReader
        /// y verifica si hay errores controlados (o_Num = -1)
        /// </summary>
        /// <param name="cmd">Comando SQL ejecutado</param>
        /// <param name="oNum">Parámetro de salida o_Num</param>
        /// <param name="oMsg">Parámetro de salida o_Msg</param>
        /// <exception cref="Exception">Lanza excepción si o_Num = -1</exception>
        public static void VerificarResultado(SqlCommand cmd, out int oNum, out string oMsg)
        {
            oNum = cmd.Parameters["@o_Num"].Value != DBNull.Value 
                ? Convert.ToInt32(cmd.Parameters["@o_Num"].Value) 
                : 0;
            oMsg = cmd.Parameters["@o_Msg"].Value?.ToString() ?? string.Empty;
            
            // Si hay un error controlado (o_Num = -1), lanzar excepción
            if (oNum == -1)
            {
                throw new Exception(oMsg);
            }
        }

        /// <summary>
        /// Captura los parámetros de salida después de ExecuteNonQueryAsync
        /// </summary>
        /// <param name="cmd">Comando SQL ejecutado</param>
        /// <returns>ResultadoOperacion con código y mensaje</returns>
        public static Entities.ResultadoOperacion ObtenerResultado(SqlCommand cmd)
        {
            var resultado = new Entities.ResultadoOperacion();
            resultado.Codigo = cmd.Parameters["@o_Num"].Value != DBNull.Value 
                ? Convert.ToInt32(cmd.Parameters["@o_Num"].Value) 
                : 0;
            resultado.Mensaje = cmd.Parameters["@o_Msg"].Value?.ToString() ?? string.Empty;
            return resultado;
        }

        /// <summary>
        /// Captura los parámetros de salida después de ExecuteReaderAsync para consultas
        /// </summary>
        /// <typeparam name="T">Tipo de datos devueltos</typeparam>
        /// <param name="cmd">Comando SQL ejecutado</param>
        /// <param name="datos">Datos obtenidos del reader</param>
        /// <returns>ResultadoConsulta con datos, código y mensaje</returns>
        public static Entities.ResultadoConsulta<T> ObtenerResultadoConsulta<T>(SqlCommand cmd, T datos)
        {
            var resultado = new Entities.ResultadoConsulta<T>();
            resultado.Datos = datos;
            resultado.Codigo = cmd.Parameters["@o_Num"].Value != DBNull.Value 
                ? Convert.ToInt32(cmd.Parameters["@o_Num"].Value) 
                : 0;
            resultado.Mensaje = cmd.Parameters["@o_Msg"].Value?.ToString() ?? string.Empty;
            return resultado;
        }
    }
}

