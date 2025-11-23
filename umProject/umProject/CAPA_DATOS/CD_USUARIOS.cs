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
    public class CdUsuarios
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();


        public List<CeUsuarios> InicioSesion(CeUsuarios obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeUsuarios> ltsUsuarios = new List<CeUsuarios>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = obj.Usuario;
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
                        CeUsuarios fila = new CeUsuarios();
                        fila.IdUsuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        fila.IdPersona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        fila.Contrasena = dr["Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Contrasena"]);
                        if (CdUtilidades.VerificarContrasena(obj.Contrasena, fila.Contrasena))
                        {
                            fila.Contrasena = string.Empty;
                            ltsUsuarios.Add(fila);
                            oNum = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                            oMsg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                        }
                        else
                        {
                            oNum = -1;
                            oMsg = "¡Usuario o Contraseña incorrecta!";
                        }

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
            return ltsUsuarios;
        }

        public List<CeUsuarios> ListarUsuarios(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeUsuarios> ltsUsuarios = new List<CeUsuarios>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 22;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
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
                        ltsUsuarios.Add(LlenarModelo(dr));
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
            return ltsUsuarios;
        }

        public List<CeUsuarios> FiltrarUsuariosPorId(CeUsuarios obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeUsuarios> ltsUsuarios = new List<CeUsuarios>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 23;
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
                        ltsUsuarios.Add(LlenarModelo(dr));
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
            return ltsUsuarios;
        }

        public List<CeUsuarios> FiltrarUsuariosPorIdPersona(CeUsuarios obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeUsuarios> ltsUsuarios = new List<CeUsuarios>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 25;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.IdPersona;
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
                        ltsUsuarios.Add(LlenarModelo(dr));
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
            return ltsUsuarios;
        }
        public List<CeUsuarios> FiltrarUsuarioPorUsuario(CeUsuarios obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeUsuarios> ltsUsuarios = new List<CeUsuarios>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 24;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar,255).Value = obj.Usuario;
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
                        ltsUsuarios.Add(LlenarModelo(dr));
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
            return ltsUsuarios;
        }

        public List<CeUsuarios> FiltrarUsuariosPorRol(int idRol, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeUsuarios> ltsUsuarios = new List<CeUsuarios>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 143;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = idRol; // Reutilizamos @Id_Estado para pasar el Id_Rol
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
                        ltsUsuarios.Add(LlenarModelo(dr));
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
            return ltsUsuarios;
        }

        public List<CeMenus> ListarMenuPorRol(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            List<CeMenus> ltsMenu = new List<CeMenus>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 26;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
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
                        CeMenus fila = new CeMenus();
                        fila.IdMenu = dr["Id_Menu"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Menu"]);
                        fila.Menu = dr["Menu"] is DBNull ? string.Empty : Convert.ToString(dr["Menu"]);
                        fila.NombreBoton = dr["Nombre_Boton"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Boton"]);
                        ltsMenu.Add(fila);
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
            return ltsMenu;
        }

        public void AgregarUsuarios(CeUsuarios obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 20;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.IdPersona;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = obj.Usuario;
            cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 100).Value = CdUtilidades.HashearContrasena(obj.Contrasena);
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.IdEstado;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
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
        public void ActualizarUsuarios(CeUsuarios obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 21;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = obj.IdUsuario;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.IdPersona;
            cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar, 255).Value = obj.Usuario;
            cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 100).Value = obj.Contrasena != null ? CdUtilidades.HashearContrasena(obj.Contrasena) : obj.Contrasena;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.IdEstado;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
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

        public List<CeUsuarios> ObtenerEstudiantePorDocumento(CeUsuarios obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeUsuarios> ltsUsuarios = new List<CeUsuarios>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_usuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 133;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.VarChar, 50).Value = obj.ValorDocumento;
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
                        CeUsuarios usuario = new CeUsuarios();
                        usuario.IdUsuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        usuario.IdPersona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        usuario.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        usuario.ValorDocumento = dr["Valor_Documento"] is DBNull ? string.Empty : Convert.ToString(dr["Valor_Documento"]);
                        usuario.NombreCompleto = dr["Nombre_Completo"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Completo"]);
                        usuario.UltimaSesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]);
                        usuario.UltimoCambioContrasena = dr["Ultimo_Cambio_Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Ultimo_Cambio_Contrasena"]);
                        usuario.FechaCreacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        usuario.FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        usuario.IdCreador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        usuario.IdModificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        usuario.IdTransaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        usuario.IdEstado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        ltsUsuarios.Add(usuario);
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
            return ltsUsuarios;
        }

        private CeUsuarios LlenarModelo(DataRow dr)
        {
            return new CeUsuarios
            {
                IdUsuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]),
                IdPersona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]),
                Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]),
                UltimaSesion = dr["Ultima_Sesion"] is DBNull ? string.Empty : Convert.ToString(dr["Ultima_Sesion"]),
                UltimoCambioContrasena = dr["Ultimo_Cambio_Contrasena"] is DBNull ? string.Empty : Convert.ToString(dr["Ultimo_Cambio_Contrasena"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]),
                IdEstado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"])
            };
        }
    }

}
