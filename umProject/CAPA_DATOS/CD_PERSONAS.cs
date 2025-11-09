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
    public class CD_PERSONAS
    {
        CD_CONEXION con = new CD_CONEXION();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        public void AGREGAR_PERSONAS(CE_PERSONAS obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_personas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 15;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Primer_Nombre", SqlDbType.NVarChar, 100).Value = obj.Primer_Nombre;
            cmd.Parameters.Add("@Segundo_Nombre", SqlDbType.NVarChar, 100).Value = obj.Segundo_Nombre;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.NVarChar, 100).Value = obj.Primer_Apellido;
            cmd.Parameters.Add("@Segundo_Apellido", SqlDbType.NVarChar, 100).Value = obj.Segundo_Apellido;
            cmd.Parameters.Add("@Id_Tipo_Documento", SqlDbType.Int).Value = obj.Id_Tipo_Documento;
            cmd.Parameters.Add("@Fecha_Nacimiento", SqlDbType.Date).Value = obj.Fecha_Nacimiento;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = obj.Valor_Documento;
            cmd.Parameters.Add("@Id_Genero_Persona", SqlDbType.Int).Value = obj.Id_Genero_Persona;
            cmd.Parameters.Add("@Id_Nacionalidad", SqlDbType.Int).Value = obj.Id_Nacionalidad;
            cmd.Parameters.Add("@Id_Estado_Civil", SqlDbType.Int).Value = obj.Id_Estado_Civil;
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
        public void ACTUALIZAR_PERSONAS(CE_PERSONAS obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_personas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 16;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona;
            cmd.Parameters.Add("@Primer_Nombre", SqlDbType.NVarChar, 100).Value = obj.Primer_Nombre;
            cmd.Parameters.Add("@Segundo_Nombre", SqlDbType.NVarChar, 100).Value = obj.Segundo_Nombre;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.NVarChar, 100).Value = obj.Primer_Apellido;
            cmd.Parameters.Add("@Segundo_Apellido", SqlDbType.NVarChar, 100).Value = obj.Segundo_Apellido;
            cmd.Parameters.Add("@Id_Tipo_Documento", SqlDbType.Int).Value = obj.Id_Tipo_Documento;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = obj.Valor_Documento;
            cmd.Parameters.Add("@Fecha_Nacimiento", SqlDbType.Date).Value = obj.Fecha_Nacimiento;
            cmd.Parameters.Add("@Id_Genero_Persona", SqlDbType.Int).Value = obj.Id_Genero_Persona;
            cmd.Parameters.Add("@Id_Nacionalidad", SqlDbType.Int).Value = obj.Id_Nacionalidad;
            cmd.Parameters.Add("@Id_Estado_Civil", SqlDbType.Int).Value = obj.Id_Estado_Civil;
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
        public List<CE_PERSONAS> FILTRAR_PERSONAS_POR_NUMERO_DOCUMENTO(CE_PERSONAS obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            List<CE_PERSONAS> lts_personas = new List<CE_PERSONAS>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_personas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 18;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = obj.Valor_Documento;
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
                        CE_PERSONAS fila = new CE_PERSONAS();
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Primer_Nombre = dr["Primer_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Nombre"]);
                        fila.Segundo_Nombre = dr["Segundo_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Nombre"]);
                        fila.Primer_Apellido = dr["Primer_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Apellido"]);
                        fila.Segundo_Apellido = dr["Segundo_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Apellido"]);
                        fila.Id_Tipo_Documento = dr["Id_Tipo_Documento"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Documento"]);
                        fila.Valor_Documento = dr["Valor_Documento"] is DBNull ? string.Empty : Convert.ToString(dr["Valor_Documento"]);
                        fila.Id_Genero_Persona = dr["Id_Genero_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Genero_Persona"]);
                        fila.Id_Nacionalidad = dr["Id_Nacionalidad"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Nacionalidad"]);
                        fila.Fecha_Nacimiento = dr["Fecha_Nacimiento"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Nacimiento"]);
                        fila.Id_Estado_Civil = dr["Id_Estado_Civil"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado_Civil"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        lts_personas.Add(fila);
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
            return lts_personas;
        }
        public List<CE_PERSONAS> FILTRAR_PERSONAS_POR_ID(CE_PERSONAS obj, out int o_Num, out string o_Msg)
        {
            o_Num = 0;
            o_Msg = string.Empty;
            List<CE_PERSONAS> lts_personas = new List<CE_PERSONAS>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_personas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 17;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CE_SESION_USUARIO.Id_Sesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.Id_Persona;
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
                        CE_PERSONAS fila = new CE_PERSONAS();
                        fila.Id_Persona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        fila.Primer_Nombre = dr["Primer_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Nombre"]);
                        fila.Segundo_Nombre = dr["Segundo_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Nombre"]);
                        fila.Primer_Apellido = dr["Primer_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Apellido"]);
                        fila.Segundo_Apellido = dr["Segundo_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Apellido"]);
                        fila.Id_Tipo_Documento = dr["Id_Tipo_Documento"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Documento"]);
                        fila.Valor_Documento = dr["Valor_Documento"] is DBNull ? string.Empty : Convert.ToString(dr["Valor_Documento"]);
                        fila.Id_Genero_Persona = dr["Id_Genero_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Genero_Persona"]);
                        fila.Id_Nacionalidad = dr["Id_Nacionalidad"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Nacionalidad"]);
                        fila.Id_Estado_Civil = dr["Id_Estado_Civil"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado_Civil"]);
                        fila.Fecha_Nacimiento = dr["Fecha_Nacimiento"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Nacimiento"]);
                        fila.Fecha_Creacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]);
                        fila.Fecha_Modificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]);
                        fila.Id_Creador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]);
                        fila.Id_Modificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]);
                        fila.Id_Transaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]);
                        fila.Id_Estado = dr["Id_Estado"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado"]);
                        lts_personas.Add(fila);
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
            return lts_personas;
        }
    }
}

