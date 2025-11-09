using CAPA_ENTIDADES;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS
{
    public class CD_ESTADOS
    {
        CD_CONEXION con = new CD_CONEXION();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CE_ESTADOS> FILTRAR_ESTADOS_POR_ID(CE_ESTADOS Obj, out int o_Num, out string o_Msg)
        {
            DataTable tbl = new DataTable();
            List<CE_ESTADOS> lts_estados = new List<CE_ESTADOS>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_estados";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 3;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = Obj.Id_Estado;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tbl);
                o_Num = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                o_Msg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow dr in tbl.Rows)
                    {
                        CE_ESTADOS fila = new CE_ESTADOS();
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        fila.Nombre_Estado = dr["Nombre_Estado"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Estado"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"]);
                        lts_estados.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                o_Num = -1;
                o_Msg = ex.ToString();
            }
            finally
            {
                cmd.Connection = con.CerrarConexion();
                cmd.Parameters.Clear();
            }
            return lts_estados;
        }

        public List<CE_ESTADOS> FILTRAR_ESTADOS_POR_TIPO_TRANSACCION(CE_ESTADOS Obj, out int o_Num, out string o_Msg)
        {
            DataTable tbl = new DataTable();
            List<CE_ESTADOS> lts_estados = new List<CE_ESTADOS>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_estados";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 4;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = Obj.Id_Tipo_Transaccion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tbl);
                o_Num = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                o_Msg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow dr in tbl.Rows)
                    {
                        CE_ESTADOS fila = new CE_ESTADOS();
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        fila.Nombre_Estado = dr["Nombre_Estado"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Estado"]);
                        lts_estados.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                o_Num = -1;
                o_Msg = ex.ToString();
            }
            finally
            {
                cmd.Connection = con.CerrarConexion();
                cmd.Parameters.Clear();
            }
            return lts_estados;
        }
    }
}
