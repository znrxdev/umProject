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
    public class CD_USUARIOS
    {
        private CD_CONEXION con = new CD_CONEXION();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();


        public List<CE_USUARIOS> INICIO_SESION(CE_USUARIOS Obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            DataTable tbl = new DataTable();
            List<CE_USUARIOS> lts_usuarios = new List<CE_USUARIOS>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = Obj.Usuario;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 19;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tbl);

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
                            o_Num = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                            o_Msg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                        }
                        else
                        {
                            o_Num = -1;
                            o_Msg = "¡Usuario o Contraseña incorrecta!";
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

        public List<CE_USUARIOS> LISTAR_USUARIOS(out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            DataTable tbl = new DataTable();
            List<CE_USUARIOS> lts_usuarios = new List<CE_USUARIOS>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 22;
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
                        CE_USUARIOS fila = new CE_USUARIOS();
                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Ultima_Sesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]);
                        fila.Ultimo_Cambio_Contrasena = dr["Ultimo_Cambio_Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Ultimo_Cambio_Contrasena"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        lts_usuarios.Add(fila);
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

        public List<CE_USUARIOS> FILTRAR_USUARIOS_POR_ID(CE_USUARIOS Obj,out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            DataTable tbl = new DataTable();
            List<CE_USUARIOS> lts_usuarios = new List<CE_USUARIOS>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 23;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = Obj.Id_Usuario;
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
                        CE_USUARIOS fila = new CE_USUARIOS();
                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Ultima_Sesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]);
                        fila.Ultimo_Cambio_Contrasena = dr["Ultimo_Cambio_Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Ultimo_Cambio_Contrasena"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        lts_usuarios.Add(fila);
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

        public List<CE_USUARIOS> FILTRAR_USUARIOS_POR_ID_PERSONA(CE_USUARIOS Obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            DataTable tbl = new DataTable();
            List<CE_USUARIOS> lts_usuarios = new List<CE_USUARIOS>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 25;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = Obj.Id_Persona;
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
                        CE_USUARIOS fila = new CE_USUARIOS();
                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Ultima_Sesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]);
                        fila.Ultimo_Cambio_Contrasena = dr["Ultimo_Cambio_Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Ultimo_Cambio_Contrasena"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        lts_usuarios.Add(fila);
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
        public List<CE_USUARIOS> FILTRAR_USUARIO_POR_USUARIO(CE_USUARIOS Obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            DataTable tbl = new DataTable();
            List<CE_USUARIOS> lts_usuarios = new List<CE_USUARIOS>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 24;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar,255).Value = Obj.Usuario;
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
                        CE_USUARIOS fila = new CE_USUARIOS();
                        fila.Id_Usuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Ultima_Sesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]);
                        fila.Ultimo_Cambio_Contrasena = dr["Ultimo_Cambio_Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Ultimo_Cambio_Contrasena"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        lts_usuarios.Add(fila);
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

        public void AGREGAR_USUARIOS(CE_USUARIOS obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 20;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = obj.Usuario;
            cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 100).Value = CD_UTILIDADES.HASHEAR_CONTRASENA(obj.Contrasena);
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
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
        public void ACTUALIZAR_USUARIOS(CE_USUARIOS obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 21;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.Id_Usuario;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = obj.Usuario;
            cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 100).Value = obj.Contrasena != null ? CD_UTILIDADES.HASHEAR_CONTRASENA(obj.Contrasena) : obj.Contrasena;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.Id_Estado;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
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
    }

}
