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
    public class CD_USUARIOS_ROLES
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        CD_CONEXION con = new CD_CONEXION();
        public void AGREGAR_USUARIOS_ROLES(CE_USUARIOS_ROLES obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios_roles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 47;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.Id_Usuario;
            cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = obj.Id_Rol;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = obj.Activo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
                o_Num = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                o_Msg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
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
        }
        public List<CE_USUARIOS_ROLES> ACTUALIZAR_ROLES_USUARIO(CE_USUARIOS_ROLES obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            List<CE_USUARIOS_ROLES> lts_usuarios_roles = new List<CE_USUARIOS_ROLES>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios_roles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 48;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Usuario_Rol", SqlDbType.Int).Value = obj.Id_Usuario_Rol;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.Id_Usuario;
            cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = obj.Id_Rol;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = obj.Activo;
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
                        CE_USUARIOS_ROLES fila = new CE_USUARIOS_ROLES();
                        fila.Id_Usuario_Rol = dr["Id_Usuario_Rol"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario_Rol"]);
                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Rol = dr["Id_Rol"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Rol"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"]);
                        lts_usuarios_roles.Add(fila);
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
            return lts_usuarios_roles;
        }
        public List<CE_USUARIOS_ROLES> LISTAR_ROLES_DE_USUARIO(CE_USUARIOS_ROLES obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            List<CE_USUARIOS_ROLES> lts_usuarios_roles = new List<CE_USUARIOS_ROLES>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios_roles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 50;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.Id_Usuario;
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
                        CE_USUARIOS_ROLES fila = new CE_USUARIOS_ROLES();
                        fila.Id_Usuario_Rol = dr["Id_Usuario_Rol"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario_Rol"]);
                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Rol = dr["Id_Rol"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Rol"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"]);
                        lts_usuarios_roles.Add(fila);
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
            return lts_usuarios_roles;
        }
    }
}
