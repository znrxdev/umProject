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
    public class CdPersonas
    {
        CdConexion con = new CdConexion();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        public void AgregarPersonas(CePersonas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_personas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 15;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Primer_Nombre", SqlDbType.NVarChar, 100).Value = obj.PrimerNombre;
            cmd.Parameters.Add("@Segundo_Nombre", SqlDbType.NVarChar, 100).Value = obj.SegundoNombre;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.NVarChar, 100).Value = obj.PrimerApellido;
            cmd.Parameters.Add("@Segundo_Apellido", SqlDbType.NVarChar, 100).Value = obj.SegundoApellido;
            cmd.Parameters.Add("@Id_Tipo_Documento", SqlDbType.Int).Value = obj.IdTipoDocumento;
            cmd.Parameters.Add("@Fecha_Nacimiento", SqlDbType.Date).Value = obj.FechaNacimiento;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = obj.ValorDocumento;
            cmd.Parameters.Add("@Id_Genero_Persona", SqlDbType.Int).Value = obj.IdGeneroPersona;
            cmd.Parameters.Add("@Id_Nacionalidad", SqlDbType.Int).Value = obj.IdNacionalidad;
            cmd.Parameters.Add("@Id_Estado_Civil", SqlDbType.Int).Value = obj.IdEstadoCivil;
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
        public void ActualizarPersonas(CePersonas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_personas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 16;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.IdPersona;
            cmd.Parameters.Add("@Primer_Nombre", SqlDbType.NVarChar, 100).Value = obj.PrimerNombre;
            cmd.Parameters.Add("@Segundo_Nombre", SqlDbType.NVarChar, 100).Value = obj.SegundoNombre;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.NVarChar, 100).Value = obj.PrimerApellido;
            cmd.Parameters.Add("@Segundo_Apellido", SqlDbType.NVarChar, 100).Value = obj.SegundoApellido;
            cmd.Parameters.Add("@Id_Tipo_Documento", SqlDbType.Int).Value = obj.IdTipoDocumento;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = obj.ValorDocumento;
            cmd.Parameters.Add("@Fecha_Nacimiento", SqlDbType.Date).Value = obj.FechaNacimiento;
            cmd.Parameters.Add("@Id_Genero_Persona", SqlDbType.Int).Value = obj.IdGeneroPersona;
            cmd.Parameters.Add("@Id_Nacionalidad", SqlDbType.Int).Value = obj.IdNacionalidad;
            cmd.Parameters.Add("@Id_Estado_Civil", SqlDbType.Int).Value = obj.IdEstadoCivil;
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
        public List<CePersonas> FiltrarPersonasPorNumeroDocumento(CePersonas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            List<CePersonas> ltsPersonas = new List<CePersonas>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_personas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 18;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = obj.ValorDocumento;
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
                        ltsPersonas.Add(LlenarObjeto(dr));
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
            return ltsPersonas;
        }
        public List<CePersonas> FiltrarPersonasPorId(CePersonas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            List<CePersonas> ltsPersonas = new List<CePersonas>();
            DataTable tbl = new DataTable();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_personas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 17;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = obj.IdPersona;
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
                        ltsPersonas.Add(LlenarObjeto(dr));
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
            return ltsPersonas;
        }

        private CePersonas LlenarObjeto(DataRow dr)
        {
            return new CePersonas
            {
                IdPersona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]),
                PrimerNombre = dr["Primer_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Nombre"]),
                SegundoNombre = dr["Segundo_Nombre"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Nombre"]),
                PrimerApellido = dr["Primer_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Primer_Apellido"]),
                SegundoApellido = dr["Segundo_Apellido"] is DBNull ? string.Empty : Convert.ToString(dr["Segundo_Apellido"]),
                IdTipoDocumento = dr["Id_Tipo_Documento"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Tipo_Documento"]),
                ValorDocumento = dr["Valor_Documento"] is DBNull ? string.Empty : Convert.ToString(dr["Valor_Documento"]),
                IdGeneroPersona = dr["Id_Genero_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Genero_Persona"]),
                IdNacionalidad = dr["Id_Nacionalidad"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Nacionalidad"]),
                IdEstadoCivil = dr["Id_Estado_Civil"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estado_Civil"]),
                FechaNacimiento = dr["Fecha_Nacimiento"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Nacimiento"]),
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
