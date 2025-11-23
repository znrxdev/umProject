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
    public class CdEstados
    {
        CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeEstados> FiltrarEstadosPorId(CeEstados obj, out int oNum, out string oMsg)
        {
            DataTable tbl = new DataTable();
            List<CeEstados> ltsEstados = new List<CeEstados>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_estados";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 3;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.IdEstado;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tbl);
                oNum = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                oMsg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow dr in tbl.Rows)
                    {
                        ltsEstados.Add(LlenarObjeto(dr));
                    }
                }
            }
            catch (Exception ex)
            {
                oNum = -1;
                oMsg = ex.Message;
            }
            finally
            {
                cmd.Connection = con.CerrarConexion();
                cmd.Parameters.Clear();
            }
            return ltsEstados;
        }

        public List<CeEstados> FiltrarEstadosPorTipoTransaccion(CeEstados obj, out int oNum, out string oMsg)
        {
            DataTable tbl = new DataTable();
            List<CeEstados> ltsEstados = new List<CeEstados>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_estados";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 4;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = obj.IdTipoTransaccion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tbl);
                oNum = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                oMsg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow dr in tbl.Rows)
                    {
                        CeEstados fila = new CeEstados();
                        fila.IdEstado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        fila.NombreEstado = dr["Nombre_Estado"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Estado"]);
                        ltsEstados.Add(fila);
                    }
                }
            }
            catch (Exception ex)
            {
                oNum = -1;
                oMsg = ex.Message;
            }
            finally
            {
                cmd.Connection = con.CerrarConexion();
                cmd.Parameters.Clear();
            }
            return ltsEstados;
        }

        private CeEstados LlenarObjeto(DataRow dr)
        {
            return new CeEstados
            {
                IdEstado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]),
                NombreEstado = dr["Nombre_Estado"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Estado"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]),
                Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"]),
            };
        }
    }
}
