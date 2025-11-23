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
    public class CdUsuariosRoles
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        CdConexion con = new CdConexion();
        public void AgregarUsuariosRoles(CeUsuariosRoles obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios_roles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 47;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.IdUsuario;
            cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = obj.IdRol;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = obj.Activo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
                oNum = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                oMsg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
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
        }
        public List<CeUsuariosRoles> ActualizarRolesUsuario(CeUsuariosRoles obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            List<CeUsuariosRoles> ltsUsuariosRoles = new List<CeUsuariosRoles>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios_roles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 48;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Usuario_Rol", SqlDbType.Int).Value = obj.IdUsuarioRol;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.IdUsuario;
            cmd.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = obj.IdRol;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = obj.Activo;
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
                        ltsUsuariosRoles.Add(LlenarObjeto(dr));
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
            return ltsUsuariosRoles;
        }
        public List<CeUsuariosRoles> ListarRolesDeUsuario(CeUsuariosRoles obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            List<CeUsuariosRoles> ltsUsuariosRoles = new List<CeUsuariosRoles>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios_roles";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 50;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.IdUsuario;
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
                        ltsUsuariosRoles.Add(LlenarObjeto(dr));
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
            return ltsUsuariosRoles;
        }

        private CeUsuariosRoles LlenarObjeto(DataRow dr)
        {
            return new CeUsuariosRoles
            {
                IdUsuarioRol = dr["Id_Usuario_Rol"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario_Rol"]),
                IdUsuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]),
                IdRol = dr["Id_Rol"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Rol"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]),
                Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"])
            };
        }
    }
}
