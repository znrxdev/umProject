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
    public class CdEvaluacionesInstancias
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeEvaluacionesInstancias> FiltrarEvaluacionInstanciaPorId(CeEvaluacionesInstancias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesInstancias> ltsInstancias = new List<CeEvaluacionesInstancias>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_instancias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 126;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Instancia", SqlDbType.Int).Value = obj.IdEvaluacionInstancia;
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
                        ltsInstancias.Add(LlenarModelo(dr));
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
            return ltsInstancias;
        }

        public List<CeEvaluacionesInstancias> FiltrarEvaluacionInstanciaPorSeccion(CeEvaluacionesInstancias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesInstancias> ltsInstancias = new List<CeEvaluacionesInstancias>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_instancias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 127;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = obj.IdSeccion;
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
                        ltsInstancias.Add(LlenarModelo(dr));
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
            return ltsInstancias;
        }

        public void AgregarEvaluacionInstancia(CeEvaluacionesInstancias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_instancias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 124;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Codigo_Instancia", SqlDbType.VarChar, 30).Value = (object)obj.CodigoInstancia ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = obj.IdSeccion;
            cmd.Parameters.Add("@Id_Evaluacion_Modelo", SqlDbType.Int).Value = obj.IdEvaluacionModelo;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = obj.IdPeriodo;
            cmd.Parameters.Add("@Fecha_Programada", SqlDbType.DateTime).Value = (object)obj.FechaProgramada ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Limite", SqlDbType.DateTime).Value = (object)obj.FechaLimite ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Revision_Interna", SqlDbType.Bit).Value = (object)obj.RequiereRevisionInterna ?? false;
            cmd.Parameters.Add("@Numero_Version", SqlDbType.Int).Value = (object)obj.NumeroVersion ?? 1;
            cmd.Parameters.Add("@Nivel_Aprobacion_Actual", SqlDbType.TinyInt).Value = (object)obj.NivelAprobacionActual ?? 1;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.IdEstado;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = obj.IdEstadoPublicacion;
            cmd.Parameters.Add("@Id_Responsable_Revision", SqlDbType.Int).Value = (object)obj.IdResponsableRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Revision", SqlDbType.DateTime).Value = (object)obj.FechaRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Responsable_Publicacion", SqlDbType.Int).Value = (object)obj.IdResponsablePublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime).Value = (object)obj.FechaPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Padre", SqlDbType.Int).Value = (object)obj.IdEvaluacionPadre ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones_Revision", SqlDbType.NVarChar, 500).Value = (object)obj.ObservacionesRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Rechazo", SqlDbType.NVarChar, 500).Value = (object)obj.MotivoRechazo ?? DBNull.Value;
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

        public void ActualizarEvaluacionInstancia(CeEvaluacionesInstancias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_instancias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 125;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Instancia", SqlDbType.Int).Value = obj.IdEvaluacionInstancia;
            cmd.Parameters.Add("@Codigo_Instancia", SqlDbType.VarChar, 30).Value = (object)obj.CodigoInstancia ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = (object)obj.IdSeccion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Modelo", SqlDbType.Int).Value = (object)obj.IdEvaluacionModelo ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = (object)obj.IdPeriodo ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Programada", SqlDbType.DateTime).Value = (object)obj.FechaProgramada ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Limite", SqlDbType.DateTime).Value = (object)obj.FechaLimite ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Revision_Interna", SqlDbType.Bit).Value = (object)obj.RequiereRevisionInterna ?? DBNull.Value;
            cmd.Parameters.Add("@Numero_Version", SqlDbType.Int).Value = (object)obj.NumeroVersion ?? DBNull.Value;
            cmd.Parameters.Add("@Nivel_Aprobacion_Actual", SqlDbType.TinyInt).Value = (object)obj.NivelAprobacionActual ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object)obj.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object)obj.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Responsable_Revision", SqlDbType.Int).Value = (object)obj.IdResponsableRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Revision", SqlDbType.DateTime).Value = (object)obj.FechaRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Responsable_Publicacion", SqlDbType.Int).Value = (object)obj.IdResponsablePublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime).Value = (object)obj.FechaPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Padre", SqlDbType.Int).Value = (object)obj.IdEvaluacionPadre ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones_Revision", SqlDbType.NVarChar, 500).Value = (object)obj.ObservacionesRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Rechazo", SqlDbType.NVarChar, 500).Value = (object)obj.MotivoRechazo ?? DBNull.Value;
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

        public List<CeEvaluacionesInstancias> ListarTodasLasInstancias(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesInstancias> ltsInstancias = new List<CeEvaluacionesInstancias>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_instancias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 146;
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
                        ltsInstancias.Add(LlenarModelo(dr));
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
            return ltsInstancias;
        }

        private CeEvaluacionesInstancias LlenarModelo(DataRow dr)
        {
            return new CeEvaluacionesInstancias
            {
                IdEvaluacionInstancia = dr["Id_Evaluacion_Instancia"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Evaluacion_Instancia"]),
                CodigoInstancia = dr["Codigo_Instancia"] is DBNull ? string.Empty : Convert.ToString(dr["Codigo_Instancia"]),
                IdSeccion = dr["Id_Seccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Seccion"]),
                IdEvaluacionModelo = dr["Id_Evaluacion_Modelo"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Evaluacion_Modelo"]),
                IdPeriodo = dr["Id_Periodo"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Periodo"]),
                FechaProgramada = dr["Fecha_Programada"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Programada"]),
                FechaLimite = dr["Fecha_Limite"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Limite"]),
                RequiereRevisionInterna = dr["Requiere_Revision_Interna"] is DBNull ? false : Convert.ToBoolean(dr["Requiere_Revision_Interna"]),
                NumeroVersion = dr["Numero_Version"] is DBNull ? null : (int?)Convert.ToInt32(dr["Numero_Version"]),
                NivelAprobacionActual = dr["Nivel_Aprobacion_Actual"] is DBNull ? null : (byte?)Convert.ToByte(dr["Nivel_Aprobacion_Actual"]),
                IdEstado = dr["Id_Estado"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estado"]),
                IdEstadoPublicacion = dr["Id_Estado_Publicacion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estado_Publicacion"]),
                IdResponsableRevision = dr["Id_Responsable_Revision"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Responsable_Revision"]),
                FechaRevision = dr["Fecha_Revision"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Revision"]),
                IdResponsablePublicacion = dr["Id_Responsable_Publicacion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Responsable_Publicacion"]),
                FechaPublicacion = dr["Fecha_Publicacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Publicacion"]),
                IdEvaluacionPadre = dr["Id_Evaluacion_Padre"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Evaluacion_Padre"]),
                ObservacionesRevision = dr["Observaciones_Revision"] is DBNull ? null : Convert.ToString(dr["Observaciones_Revision"]),
                MotivoRechazo = dr["Motivo_Rechazo"] is DBNull ? null : Convert.ToString(dr["Motivo_Rechazo"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Transaccion"]),
                // Propiedades adicionales de JOINs
                CodigoSeccion = dr.Table.Columns.Contains("Codigo_Seccion") && dr["Codigo_Seccion"] is not DBNull ? Convert.ToString(dr["Codigo_Seccion"]) : null,
                NombreMateria = dr.Table.Columns.Contains("Nombre_Materia") && dr["Nombre_Materia"] is not DBNull ? Convert.ToString(dr["Nombre_Materia"]) : null,
                NombreModeloEvaluacion = dr.Table.Columns.Contains("Nombre_Modelo_Evaluacion") && dr["Nombre_Modelo_Evaluacion"] is not DBNull ? Convert.ToString(dr["Nombre_Modelo_Evaluacion"]) : null,
                CodigoModelo = dr.Table.Columns.Contains("Codigo_Modelo") && dr["Codigo_Modelo"] is not DBNull ? Convert.ToString(dr["Codigo_Modelo"]) : null,
                NombrePeriodo = dr.Table.Columns.Contains("Nombre_Periodo") && dr["Nombre_Periodo"] is not DBNull ? Convert.ToString(dr["Nombre_Periodo"]) : null,
                CodigoPeriodo = dr.Table.Columns.Contains("Codigo_Periodo") && dr["Codigo_Periodo"] is not DBNull ? Convert.ToString(dr["Codigo_Periodo"]) : null,
                NombreEstado = dr.Table.Columns.Contains("Nombre_Estado") && dr["Nombre_Estado"] is not DBNull ? Convert.ToString(dr["Nombre_Estado"]) : null,
                NombreEstadoPublicacion = dr.Table.Columns.Contains("Nombre_Estado_Publicacion") && dr["Nombre_Estado_Publicacion"] is not DBNull ? Convert.ToString(dr["Nombre_Estado_Publicacion"]) : null
            };
        }
    }
}

