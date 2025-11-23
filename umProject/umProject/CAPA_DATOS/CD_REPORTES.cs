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
    public class CdReportes
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeReporteUsuario> ObtenerReporteUsuariosActivos(DateTime? fechaInicio, DateTime? fechaFin, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeReporteUsuario> lista = new List<CeReporteUsuario>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_reportes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 151;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.HasValue ? (object)fechaInicio.Value : DBNull.Value;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.HasValue ? (object)fechaFin.Value : DBNull.Value;
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
                        lista.Add(LlenarModeloUsuario(dr));
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
            return lista;
        }

        public List<CeReporteUsuario> ObtenerReporteUsuariosInactivos(DateTime? fechaInicio, DateTime? fechaFin, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeReporteUsuario> lista = new List<CeReporteUsuario>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_reportes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 152;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.HasValue ? (object)fechaInicio.Value : DBNull.Value;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.HasValue ? (object)fechaFin.Value : DBNull.Value;
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
                        lista.Add(LlenarModeloUsuario(dr));
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
            return lista;
        }

        public List<CeReporteAuditoria> ObtenerReporteAuditoria(DateTime? fechaInicio, DateTime? fechaFin, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeReporteAuditoria> lista = new List<CeReporteAuditoria>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_transacciones";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 153;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.HasValue ? (object)fechaInicio.Value : DBNull.Value;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.HasValue ? (object)fechaFin.Value : DBNull.Value;
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
                        lista.Add(LlenarModeloAuditoria(dr));
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
            return lista;
        }

        private CeReporteUsuario LlenarModeloUsuario(DataRow dr)
        {
            CeReporteUsuario obj = new CeReporteUsuario();
            obj.IdUsuario = dr["Id_Usuario"] is DBNull ? null : Convert.ToInt32(dr["Id_Usuario"]);
            obj.Usuario = dr["Usuario"] is DBNull ? null : Convert.ToString(dr["Usuario"]);
            obj.IdPersona = dr["Id_Persona"] is DBNull ? null : Convert.ToInt32(dr["Id_Persona"]);
            obj.PrimerNombre = dr["Primer_Nombre"] is DBNull ? null : Convert.ToString(dr["Primer_Nombre"]);
            obj.SegundoNombre = dr["Segundo_Nombre"] is DBNull ? null : Convert.ToString(dr["Segundo_Nombre"]);
            obj.PrimerApellido = dr["Primer_Apellido"] is DBNull ? null : Convert.ToString(dr["Primer_Apellido"]);
            obj.SegundoApellido = dr["Segundo_Apellido"] is DBNull ? null : Convert.ToString(dr["Segundo_Apellido"]);
            obj.NombreCompleto = dr["Nombre_Completo"] is DBNull ? null : Convert.ToString(dr["Nombre_Completo"]);
            obj.ValorDocumento = dr["Valor_Documento"] is DBNull ? null : Convert.ToString(dr["Valor_Documento"]);
            obj.TipoDocumento = dr["Tipo_Documento"] is DBNull ? null : Convert.ToString(dr["Tipo_Documento"]);
            obj.FechaNacimiento = dr["Fecha_Nacimiento"] is DBNull ? null : Convert.ToDateTime(dr["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
            obj.Genero = dr["Genero"] is DBNull ? null : Convert.ToString(dr["Genero"]);
            obj.Nacionalidad = dr["Nacionalidad"] is DBNull ? null : Convert.ToString(dr["Nacionalidad"]);
            obj.EstadoCivil = dr["Estado_Civil"] is DBNull ? null : Convert.ToString(dr["Estado_Civil"]);
            obj.FechaCreacionUsuario = dr["Fecha_Creacion_Usuario"] is DBNull ? null : Convert.ToString(dr["Fecha_Creacion_Usuario"]);
            obj.FechaModificacionUsuario = dr["Fecha_Modificacion_Usuario"] is DBNull ? null : Convert.ToString(dr["Fecha_Modificacion_Usuario"]);
            obj.UltimaSesion = dr["Ultima_Sesion"] is DBNull ? null : Convert.ToString(dr["Ultima_Sesion"]);
            obj.UltimoCambioContrasena = dr["Ultimo_Cambio_Contrasena"] is DBNull ? null : Convert.ToString(dr["Ultimo_Cambio_Contrasena"]);
            obj.EstadoUsuario = dr["Estado_Usuario"] is DBNull ? null : Convert.ToString(dr["Estado_Usuario"]);
            obj.FechaCreacionPersona = dr["Fecha_Creacion_Persona"] is DBNull ? null : Convert.ToString(dr["Fecha_Creacion_Persona"]);
            obj.FechaModificacionPersona = dr["Fecha_Modificacion_Persona"] is DBNull ? null : Convert.ToString(dr["Fecha_Modificacion_Persona"]);
            return obj;
        }

        private CeReporteAuditoria LlenarModeloAuditoria(DataRow dr)
        {
            CeReporteAuditoria obj = new CeReporteAuditoria();
            obj.IdTransaccion = dr["Id_Transaccion"] is DBNull ? null : Convert.ToInt32(dr["Id_Transaccion"]);
            obj.IdTipoTransaccion = dr["Id_Tipo_Transaccion"] is DBNull ? null : Convert.ToInt32(dr["Id_Tipo_Transaccion"]);
            obj.NombreTipoTransaccion = dr["Nombre_Tipo_Transaccion"] is DBNull ? null : Convert.ToString(dr["Nombre_Tipo_Transaccion"]);
            obj.Concepto = dr["Concepto"] is DBNull ? null : Convert.ToString(dr["Concepto"]);
            obj.IdPersona = dr["Id_Persona"] is DBNull ? null : Convert.ToInt32(dr["Id_Persona"]);
            obj.NombrePersona = dr["Nombre_Persona"] is DBNull ? null : Convert.ToString(dr["Nombre_Persona"]);
            obj.IdUsuario = dr["Id_Usuario"] is DBNull ? null : Convert.ToInt32(dr["Id_Usuario"]);
            obj.NombreUsuario = dr["Nombre_Usuario"] is DBNull ? null : Convert.ToString(dr["Nombre_Usuario"]);
            obj.IdContacto = dr["Id_Contacto"] is DBNull ? null : Convert.ToInt32(dr["Id_Contacto"]);
            obj.IdEvaluacion = dr["Id_Evaluacion"] is DBNull ? null : Convert.ToInt32(dr["Id_Evaluacion"]);
            obj.IdSolicitudBeca = dr["Id_Solicitud_Beca"] is DBNull ? null : Convert.ToInt32(dr["Id_Solicitud_Beca"]);
            obj.IdInscripcion = dr["Id_Inscripcion"] is DBNull ? null : Convert.ToInt32(dr["Id_Inscripcion"]);
            obj.IdAutor = dr["Id_Autor"] is DBNull ? null : Convert.ToInt32(dr["Id_Autor"]);
            obj.NombreAutor = dr["Nombre_Autor"] is DBNull ? null : Convert.ToString(dr["Nombre_Autor"]);
            obj.FechaCreacion = dr["Fecha_Creacion"] is DBNull ? null : Convert.ToString(dr["Fecha_Creacion"]);
            obj.Completado = dr["Completado"] is DBNull ? null : Convert.ToBoolean(dr["Completado"]);
            obj.TipoEntidad = dr["Tipo_Entidad"] is DBNull ? null : Convert.ToString(dr["Tipo_Entidad"]);
            return obj;
        }
    }
}

