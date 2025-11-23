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
    public class CdEvaluacionesAlumnos
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeEvaluacionesAlumnos> FiltrarEvaluacionAlumnoPorId(CeEvaluacionesAlumnos obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesAlumnos> ltsEvaluaciones = new List<CeEvaluacionesAlumnos>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_alumnos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 130;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Alumno", SqlDbType.Int).Value = obj.IdEvaluacionAlumno;
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
                        ltsEvaluaciones.Add(LlenarModelo(dr));
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
            return ltsEvaluaciones;
        }

        public List<CeEvaluacionesAlumnos> FiltrarEvaluacionAlumnoPorInstancia(CeEvaluacionesAlumnos obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesAlumnos> ltsEvaluaciones = new List<CeEvaluacionesAlumnos>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_alumnos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 132;
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
                        ltsEvaluaciones.Add(LlenarModelo(dr));
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
            return ltsEvaluaciones;
        }

        public List<CeEvaluacionesAlumnos> ObtenerMisEvaluacionesPublicadas(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesAlumnos> ltsEvaluaciones = new List<CeEvaluacionesAlumnos>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_alumnos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 137;
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
                        ltsEvaluaciones.Add(LlenarModelo(dr));
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
            return ltsEvaluaciones;
        }

        public void AgregarEvaluacionAlumno(CeEvaluacionesAlumnos obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_alumnos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 128;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Codigo_Registro", SqlDbType.VarChar, 30).Value = (object)obj.CodigoRegistro ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Instancia", SqlDbType.Int).Value = obj.IdEvaluacionInstancia;
            cmd.Parameters.Add("@Id_Inscripcion", SqlDbType.Int).Value = obj.IdInscripcion;
            cmd.Parameters.Add("@Puntaje_Obtenido", SqlDbType.Decimal).Value = (object)obj.PuntajeObtenido ?? 0;
            cmd.Parameters.Add("@Porcentaje_Logrado", SqlDbType.Decimal).Value = (object)obj.PorcentajeLogrado ?? DBNull.Value;
            cmd.Parameters.Add("@Puntaje_Normalizado", SqlDbType.Decimal).Value = (object)obj.PuntajeNormalizado ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Recalculo", SqlDbType.Bit).Value = (object)obj.EsRecalculo ?? false;
            cmd.Parameters.Add("@Numero_Recalculo", SqlDbType.Int).Value = (object)obj.NumeroRecalculo ?? 0;
            cmd.Parameters.Add("@Motivo_Ajuste", SqlDbType.NVarChar, 500).Value = (object)obj.MotivoAjuste ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object)obj.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Evaluador", SqlDbType.Int).Value = (object)obj.IdUsuarioEvaluador ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Validador", SqlDbType.Int).Value = (object)obj.IdUsuarioValidador ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Validacion", SqlDbType.DateTime).Value = (object)obj.FechaValidacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = obj.IdEstado;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object)obj.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Reemplazada", SqlDbType.Int).Value = (object)obj.IdEvaluacionReemplazada ?? DBNull.Value;
            cmd.Parameters.Add("@Firmado_Por_Estudiante", SqlDbType.Bit).Value = (object)obj.FirmadoPorEstudiante ?? false;
            cmd.Parameters.Add("@Firma_Digital", SqlDbType.NVarChar, 255).Value = (object)obj.FirmaDigital ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Notificacion", SqlDbType.DateTime).Value = (object)obj.FechaNotificacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime).Value = (object)obj.FechaPublicacion ?? DBNull.Value;
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

        public void ActualizarEvaluacionAlumno(CeEvaluacionesAlumnos obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_alumnos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 129;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Alumno", SqlDbType.Int).Value = obj.IdEvaluacionAlumno;
            cmd.Parameters.Add("@Codigo_Registro", SqlDbType.VarChar, 30).Value = (object)obj.CodigoRegistro ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Instancia", SqlDbType.Int).Value = (object)obj.IdEvaluacionInstancia ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Inscripcion", SqlDbType.Int).Value = (object)obj.IdInscripcion ?? DBNull.Value;
            cmd.Parameters.Add("@Puntaje_Obtenido", SqlDbType.Decimal).Value = (object)obj.PuntajeObtenido ?? DBNull.Value;
            cmd.Parameters.Add("@Porcentaje_Logrado", SqlDbType.Decimal).Value = (object)obj.PorcentajeLogrado ?? DBNull.Value;
            cmd.Parameters.Add("@Puntaje_Normalizado", SqlDbType.Decimal).Value = (object)obj.PuntajeNormalizado ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Recalculo", SqlDbType.Bit).Value = (object)obj.EsRecalculo ?? DBNull.Value;
            cmd.Parameters.Add("@Numero_Recalculo", SqlDbType.Int).Value = (object)obj.NumeroRecalculo ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Ajuste", SqlDbType.NVarChar, 500).Value = (object)obj.MotivoAjuste ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object)obj.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Evaluador", SqlDbType.Int).Value = (object)obj.IdUsuarioEvaluador ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Validador", SqlDbType.Int).Value = (object)obj.IdUsuarioValidador ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Validacion", SqlDbType.DateTime).Value = (object)obj.FechaValidacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object)obj.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object)obj.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Evaluacion_Reemplazada", SqlDbType.Int).Value = (object)obj.IdEvaluacionReemplazada ?? DBNull.Value;
            cmd.Parameters.Add("@Firmado_Por_Estudiante", SqlDbType.Bit).Value = (object)obj.FirmadoPorEstudiante ?? DBNull.Value;
            cmd.Parameters.Add("@Firma_Digital", SqlDbType.NVarChar, 255).Value = (object)obj.FirmaDigital ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Notificacion", SqlDbType.DateTime).Value = (object)obj.FechaNotificacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime).Value = (object)obj.FechaPublicacion ?? DBNull.Value;
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

        public List<CeEvaluacionesAlumnos> ListarTodasLasCalificaciones(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesAlumnos> ltsEvaluaciones = new List<CeEvaluacionesAlumnos>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_alumnos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 147;
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
                        ltsEvaluaciones.Add(LlenarModelo(dr));
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
            return ltsEvaluaciones;
        }

        private CeEvaluacionesAlumnos LlenarModelo(DataRow dr)
        {
            return new CeEvaluacionesAlumnos
            {
                IdEvaluacionAlumno = dr["Id_Evaluacion_Alumno"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Evaluacion_Alumno"]),
                CodigoRegistro = dr["Codigo_Registro"] is DBNull ? string.Empty : Convert.ToString(dr["Codigo_Registro"]),
                IdEvaluacionInstancia = dr["Id_Evaluacion_Instancia"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Evaluacion_Instancia"]),
                IdInscripcion = dr["Id_Inscripcion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Inscripcion"]),
                PuntajeObtenido = dr["Puntaje_Obtenido"] is DBNull ? null : (decimal?)Convert.ToDecimal(dr["Puntaje_Obtenido"]),
                PorcentajeLogrado = dr["Porcentaje_Logrado"] is DBNull ? null : (decimal?)Convert.ToDecimal(dr["Porcentaje_Logrado"]),
                PuntajeNormalizado = dr["Puntaje_Normalizado"] is DBNull ? null : (decimal?)Convert.ToDecimal(dr["Puntaje_Normalizado"]),
                EsRecalculo = dr["Es_Recalculo"] is DBNull ? false : Convert.ToBoolean(dr["Es_Recalculo"]),
                NumeroRecalculo = dr["Numero_Recalculo"] is DBNull ? null : (int?)Convert.ToInt32(dr["Numero_Recalculo"]),
                MotivoAjuste = dr["Motivo_Ajuste"] is DBNull ? null : Convert.ToString(dr["Motivo_Ajuste"]),
                Observaciones = dr["Observaciones"] is DBNull ? null : Convert.ToString(dr["Observaciones"]),
                IdUsuarioEvaluador = dr["Id_Usuario_Evaluador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Usuario_Evaluador"]),
                IdUsuarioValidador = dr["Id_Usuario_Validador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Usuario_Validador"]),
                FechaValidacion = dr["Fecha_Validacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Validacion"]),
                IdEstado = dr["Id_Estado"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estado"]),
                IdEstadoPublicacion = dr["Id_Estado_Publicacion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estado_Publicacion"]),
                IdEvaluacionReemplazada = dr["Id_Evaluacion_Reemplazada"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Evaluacion_Reemplazada"]),
                FirmadoPorEstudiante = dr["Firmado_Por_Estudiante"] is DBNull ? false : Convert.ToBoolean(dr["Firmado_Por_Estudiante"]),
                FirmaDigital = dr["Firma_Digital"] is DBNull ? null : Convert.ToString(dr["Firma_Digital"]),
                FechaNotificacion = dr["Fecha_Notificacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Notificacion"]),
                FechaPublicacion = dr["Fecha_Publicacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Publicacion"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Transaccion"]),
                // Propiedades adicionales de JOINs
                CodigoInstancia = dr.Table.Columns.Contains("Codigo_Instancia") && dr["Codigo_Instancia"] is not DBNull ? Convert.ToString(dr["Codigo_Instancia"]) : null,
                CodigoSeccion = dr.Table.Columns.Contains("Codigo_Seccion") && dr["Codigo_Seccion"] is not DBNull ? Convert.ToString(dr["Codigo_Seccion"]) : null,
                NombreMateria = dr.Table.Columns.Contains("Nombre_Materia") && dr["Nombre_Materia"] is not DBNull ? Convert.ToString(dr["Nombre_Materia"]) : null,
                NombreModeloEvaluacion = dr.Table.Columns.Contains("Nombre_Modelo_Evaluacion") && dr["Nombre_Modelo_Evaluacion"] is not DBNull ? Convert.ToString(dr["Nombre_Modelo_Evaluacion"]) : null,
                NombrePeriodo = dr.Table.Columns.Contains("Nombre_Periodo") && dr["Nombre_Periodo"] is not DBNull ? Convert.ToString(dr["Nombre_Periodo"]) : null,
                CodigoPeriodo = dr.Table.Columns.Contains("Codigo_Periodo") && dr["Codigo_Periodo"] is not DBNull ? Convert.ToString(dr["Codigo_Periodo"]) : null,
                CodigoInscripcion = dr.Table.Columns.Contains("Codigo_Inscripcion") && dr["Codigo_Inscripcion"] is not DBNull ? Convert.ToString(dr["Codigo_Inscripcion"]) : null,
                UsuarioEstudiante = dr.Table.Columns.Contains("Usuario_Estudiante") && dr["Usuario_Estudiante"] is not DBNull ? Convert.ToString(dr["Usuario_Estudiante"]) : null,
                NombreEstudiante = dr.Table.Columns.Contains("Nombre_Estudiante") && dr["Nombre_Estudiante"] is not DBNull ? Convert.ToString(dr["Nombre_Estudiante"]) : null,
                NombreEstado = dr.Table.Columns.Contains("Nombre_Estado") && dr["Nombre_Estado"] is not DBNull ? Convert.ToString(dr["Nombre_Estado"]) : null,
                NombreEstadoPublicacion = dr.Table.Columns.Contains("Nombre_Estado_Publicacion") && dr["Nombre_Estado_Publicacion"] is not DBNull ? Convert.ToString(dr["Nombre_Estado_Publicacion"]) : null
            };
        }
    }
}

