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
    public class CdSancionesAcademicas
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeSancionesAcademicas> FiltrarSancionesPorId(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeSancionesAcademicas> ltsSanciones = new List<CeSancionesAcademicas>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 88;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Sancion", SqlDbType.Int).Value = obj.IdSancion;
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
                        ltsSanciones.Add(LlenarModelo(dr));
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
            return ltsSanciones;
        }

        public List<CeSancionesAcademicas> FiltrarSancionesPorEstudiante(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeSancionesAcademicas> ltsSanciones = new List<CeSancionesAcademicas>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 89;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = obj.IdEstudiante;
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
                        ltsSanciones.Add(LlenarModelo(dr));
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
            return ltsSanciones;
        }

        public List<CeSancionesAcademicas> FiltrarSancionesPorEstudianteYEstado(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeSancionesAcademicas> ltsSanciones = new List<CeSancionesAcademicas>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 135;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = obj.IdEstudiante;
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
                        ltsSanciones.Add(LlenarModelo(dr));
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
            return ltsSanciones;
        }

        public List<CeSancionesAcademicas> ObtenerMisSancionesAcademicas(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeSancionesAcademicas> ltsSanciones = new List<CeSancionesAcademicas>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 136;
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
                        ltsSanciones.Add(LlenarModelo(dr));
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
            return ltsSanciones;
        }

        public void ApelarSancionAcademica(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 139;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Sancion", SqlDbType.Int).Value = obj.IdSancion;
            cmd.Parameters.Add("@Observaciones_Apelacion", SqlDbType.NVarChar, 500).Value = obj.ObservacionesApelacion ?? string.Empty;
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
                oMsg = $"Error al procesar la apelación: {ex.Message}";
            }
            finally
            {
                cmd.Connection = con.CerrarConexion();
                cmd.Parameters.Clear();
            }
        }

        public void AgregarSancionAcademica(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 87;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Codigo_Sancion", SqlDbType.VarChar, 30).Value = (object)obj.CodigoSancion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = obj.IdEstudiante;
            cmd.Parameters.Add("@Id_Tipo_Sancion", SqlDbType.Int).Value = obj.IdTipoSancion;
            cmd.Parameters.Add("@Id_Tipo_Falta", SqlDbType.Int).Value = (object)obj.IdTipoFalta ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Severidad", SqlDbType.Int).Value = obj.IdSeveridad;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.IdEstado;
            cmd.Parameters.Add("@Fecha_Registro", SqlDbType.DateTime).Value = obj.FechaRegistro;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = (object)obj.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo", SqlDbType.NVarChar, 300).Value = (object)obj.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Apelable", SqlDbType.Bit).Value = obj.EsApelable ?? false;
            cmd.Parameters.Add("@Fecha_Apelacion", SqlDbType.DateTime).Value = (object)obj.FechaApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Resultado_Apelacion", SqlDbType.NVarChar, 200).Value = (object)obj.ResultadoApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones_Apelacion", SqlDbType.NVarChar, 500).Value = (object)obj.ObservacionesApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Documento_Resolucion", SqlDbType.NVarChar, 255).Value = (object)obj.DocumentoResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Resolucion", SqlDbType.Int).Value = (object)obj.IdUsuarioResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Resolucion", SqlDbType.DateTime).Value = (object)obj.FechaResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Sancion_Origen", SqlDbType.Int).Value = (object)obj.IdSancionOrigen ?? DBNull.Value;
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

        public void ActualizarSancionAcademica(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 90;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Sancion", SqlDbType.Int).Value = obj.IdSancion;
            cmd.Parameters.Add("@Codigo_Sancion", SqlDbType.VarChar, 30).Value = (object)obj.CodigoSancion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = (object)obj.IdEstudiante ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Sancion", SqlDbType.Int).Value = (object)obj.IdTipoSancion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Falta", SqlDbType.Int).Value = (object)obj.IdTipoFalta ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Severidad", SqlDbType.Int).Value = (object)obj.IdSeveridad ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object)obj.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Registro", SqlDbType.DateTime).Value = (object)obj.FechaRegistro ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = (object)obj.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo", SqlDbType.NVarChar, 300).Value = (object)obj.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Apelable", SqlDbType.Bit).Value = (object)obj.EsApelable ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Apelacion", SqlDbType.DateTime).Value = (object)obj.FechaApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Resultado_Apelacion", SqlDbType.NVarChar, 200).Value = (object)obj.ResultadoApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones_Apelacion", SqlDbType.NVarChar, 500).Value = (object)obj.ObservacionesApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Documento_Resolucion", SqlDbType.NVarChar, 255).Value = (object)obj.DocumentoResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Resolucion", SqlDbType.Int).Value = (object)obj.IdUsuarioResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Resolucion", SqlDbType.DateTime).Value = (object)obj.FechaResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Sancion_Origen", SqlDbType.Int).Value = (object)obj.IdSancionOrigen ?? DBNull.Value;
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

        public List<CeUsuarios> ListarEstudiantesConSanciones(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeUsuarios> ltsEstudiantes = new List<CeUsuarios>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 134;
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
                        CeUsuarios estudiante = new CeUsuarios();
                        estudiante.IdUsuario = dr["Id_Usuario"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Usuario"]);
                        estudiante.IdPersona = dr["Id_Persona"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Persona"]);
                        estudiante.Usuario = dr["Usuario"] is DBNull ? string.Empty : Convert.ToString(dr["Usuario"]);
                        estudiante.ValorDocumento = dr["Valor_Documento"] is DBNull ? string.Empty : Convert.ToString(dr["Valor_Documento"]);
                        estudiante.NombreCompleto = dr["Nombre_Completo"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Completo"]);
                        ltsEstudiantes.Add(estudiante);
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
            return ltsEstudiantes;
        }

        public List<CeSancionesAcademicas> ObtenerSancionesEnEsperaDeApelacion(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeSancionesAcademicas> ltsSanciones = new List<CeSancionesAcademicas>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 140;
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
                        ltsSanciones.Add(LlenarModelo(dr));
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
            return ltsSanciones;
        }

        public void ResponderApelacion(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_sanciones_academicas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 141;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Sancion", SqlDbType.Int).Value = obj.IdSancion;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.IdEstado;
            cmd.Parameters.Add("@Resultado_Apelacion", SqlDbType.NVarChar, 200).Value = (object)obj.ResultadoApelacion ?? DBNull.Value;
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

        private CeSancionesAcademicas LlenarModelo(DataRow dr)
        {
            return new CeSancionesAcademicas
            {
                IdSancion = dr["Id_Sancion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Sancion"]),
                CodigoSancion = dr["Codigo_Sancion"] is DBNull ? string.Empty : Convert.ToString(dr["Codigo_Sancion"]),
                IdEstudiante = dr["Id_Estudiante"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Estudiante"]),
                IdTipoSancion = dr["Id_Tipo_Sancion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Tipo_Sancion"]),
                IdTipoFalta = dr["Id_Tipo_Falta"] is DBNull ? null : Convert.ToInt32(dr["Id_Tipo_Falta"]),
                IdSeveridad = dr["Id_Severidad"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Severidad"]),
                IdEstado = dr["Id_Estado"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estado"]),
                FechaRegistro = dr["Fecha_Registro"] is DBNull ? null : Convert.ToDateTime(dr["Fecha_Registro"]),
                FechaFin = dr["Fecha_Fin"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Fin"]),
                Motivo = dr["Motivo"] is DBNull ? string.Empty : Convert.ToString(dr["Motivo"]),
                EsApelable = dr["Es_Apelable"] is DBNull ? false : Convert.ToBoolean(dr["Es_Apelable"]),
                FechaApelacion = dr["Fecha_Apelacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Apelacion"]),
                ResultadoApelacion = dr["Resultado_Apelacion"] is DBNull ? null : Convert.ToString(dr["Resultado_Apelacion"]),
                ObservacionesApelacion = dr["Observaciones_Apelacion"] is DBNull ? null : Convert.ToString(dr["Observaciones_Apelacion"]),
                DocumentoResolucion = dr["Documento_Resolucion"] is DBNull ? string.Empty : Convert.ToString(dr["Documento_Resolucion"]),
                IdUsuarioResolucion = dr["Id_Usuario_Resolucion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Usuario_Resolucion"]),
                FechaResolucion = dr["Fecha_Resolucion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Resolucion"]),
                IdSancionOrigen = dr["Id_Sancion_Origen"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Sancion_Origen"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"])
            };
        }
    }
}

