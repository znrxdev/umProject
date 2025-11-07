using CAPA_ENTIDADES;
using CD_DATOS;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_DATOS
{
    public class CD_USUARIOS
    {
        private CD_CONEXION con = new CD_CONEXION();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable tbl = new DataTable();

        public List<CE_USUARIOS> INICIO_SESION(CE_USUARIOS Obj, out int o_Num,out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            List<CE_USUARIOS> lts_usuarios = new List<CE_USUARIOS>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = Obj.Usuario;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 19;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar,255).Direction = ParameterDirection.Output;
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
                        CE_USUARIOS fila = new CE_USUARIOS();
                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Contrasena = dr["Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Contrasena"]);
                        if (CD_UTILIDADES.VERIFICAR_CONTRASENA(Obj.Contrasena, fila.Contrasena))
                        {
                            fila.Contrasena = string.Empty;
                            lts_usuarios.Add(fila); // Lista con los datos en caso de que la verificación de contraseña devuelva verdadero.
                            o_Msg = "¡Bienvenido!";
                        }
                        else
                        {
                            lts_usuarios.Add(new CE_USUARIOS()); // Lista vacía en caso de que la verificación de contraseña devuelva falso.
                        }

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
            return lts_usuarios;
        }

        public List<CE_MENUS> LISTAR_MENU_POR_ROL(out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            List<CE_MENUS> lts_menu = new List<CE_MENUS>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 26;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
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
                        CE_MENUS fila = new CE_MENUS();
                        fila.Id_Menu = dr["Id_Menu"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Menu"]);
                        fila.Menu = dr["Menu"] is DBNull ? string.Empty : Convert.ToString(dr["Menu"]);
                        fila.Nombre_Boton = dr["Nombre_Boton"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Boton"]);
                        lts_menu.Add(fila);
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
            return lts_menu;
        }
    }
}
