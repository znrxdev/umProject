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
    public class CD_ROLES
    {
        CD_CONEXION con = new CD_CONEXION();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        public List<CE_ROLES> LISTAR_ROLES(out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            List<CE_ROLES> lts_roles = new List<CE_ROLES>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_roles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 34;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
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
                        CE_ROLES fila = new CE_ROLES();
                        fila.Id_Rol = dr["Id_Rol"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Rol"]);
                        fila.Nombre_Rol = dr["Nombre_Rol"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Rol"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"]);
                        lts_roles.Add(fila);
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
            return lts_roles;
        }
        public List<CE_ROLES> FILTRAR_ROLES_ID(CE_ROLES Obj,out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            List<CE_ROLES> lts_roles = new List<CE_ROLES>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_roles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 33;
            cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = Obj.Id_Rol;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
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
                        CE_ROLES fila = new CE_ROLES();
                        fila.Id_Rol = dr["Id_Rol"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Rol"]);
                        fila.Nombre_Rol = dr["Nombre_Rol"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Rol"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"]);
                        lts_roles.Add(fila);
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
            return lts_roles;
        }
    }
}
