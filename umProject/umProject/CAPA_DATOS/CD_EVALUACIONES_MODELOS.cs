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
    public class CdEvaluacionesModelos
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeEvaluacionesModelos> FiltrarEvaluacionModeloPorId(CeEvaluacionesModelos obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesModelos> ltsModelos = new List<CeEvaluacionesModelos>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_modelos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 122;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Modelo", SqlDbType.Int).Value = obj.IdEvaluacionModelo;
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
                        ltsModelos.Add(LlenarModelo(dr));
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
            return ltsModelos;
        }

        public List<CeEvaluacionesModelos> FiltrarEvaluacionModeloPorMateriaPeriodo(CeEvaluacionesModelos obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesModelos> ltsModelos = new List<CeEvaluacionesModelos>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_modelos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 123;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = obj.IdMateriaPeriodo;
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
                        ltsModelos.Add(LlenarModelo(dr));
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
            return ltsModelos;
        }

        public void AgregarEvaluacionModelo(CeEvaluacionesModelos obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_modelos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 120;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = obj.IdMateriaPeriodo;
            cmd.Parameters.Add("@Id_Tipo_Evaluacion", SqlDbType.Int).Value = obj.IdTipoEvaluacion;
            cmd.Parameters.Add("@Codigo_Modelo", SqlDbType.VarChar, 30).Value = obj.CodigoModelo;
            cmd.Parameters.Add("@Nombre_Evaluacion", SqlDbType.NVarChar, 100).Value = obj.NombreEvaluacion;
            cmd.Parameters.Add("@Concepto", SqlDbType.NVarChar, 255).Value = (object)obj.Concepto ?? DBNull.Value;
            cmd.Parameters.Add("@Calificacion_Maxima", SqlDbType.Decimal).Value = obj.CalificacionMaxima;
            cmd.Parameters.Add("@Peso_Porcentual", SqlDbType.Decimal).Value = obj.PesoPorcentual;
            cmd.Parameters.Add("@Orden", SqlDbType.Int).Value = (object)obj.Orden ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Aprobacion", SqlDbType.Bit).Value = (object)obj.RequiereAprobacion ?? false;
            cmd.Parameters.Add("@Version_Configuracion", SqlDbType.Int).Value = (object)obj.VersionConfiguracion ?? 1;
            cmd.Parameters.Add("@Id_Metodo_Calculo", SqlDbType.Int).Value = (object)obj.IdMetodoCalculo ?? DBNull.Value;
            cmd.Parameters.Add("@Rubrica_Detalle", SqlDbType.NVarChar, -1).Value = (object)obj.RubricaDetalle ?? DBNull.Value;
            cmd.Parameters.Add("@Porcentaje_Minimo_Aprobacion", SqlDbType.Decimal).Value = (object)obj.PorcentajeMinimoAprobacion ?? DBNull.Value;
            cmd.Parameters.Add("@Niveles_Revision", SqlDbType.TinyInt).Value = (object)obj.NivelesRevision ?? 1;
            cmd.Parameters.Add("@Id_Rol_Aprobador", SqlDbType.Int).Value = (object)obj.IdRolAprobador ?? DBNull.Value;
            cmd.Parameters.Add("@Permite_Recalculo", SqlDbType.Bit).Value = (object)obj.PermiteRecalculo ?? false;
            cmd.Parameters.Add("@Tiempo_Maximo_Carga", SqlDbType.Int).Value = (object)obj.TiempoMaximoCarga ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = (object)obj.FechaInicio ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = (object)obj.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = (object)obj.Activo ?? true;
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

        public void ActualizarEvaluacionModelo(CeEvaluacionesModelos obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_modelos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 121;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Modelo", SqlDbType.Int).Value = obj.IdEvaluacionModelo;
            cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = (object)obj.IdMateriaPeriodo ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Evaluacion", SqlDbType.Int).Value = (object)obj.IdTipoEvaluacion ?? DBNull.Value;
            cmd.Parameters.Add("@Codigo_Modelo", SqlDbType.VarChar, 30).Value = (object)obj.CodigoModelo ?? DBNull.Value;
            cmd.Parameters.Add("@Nombre_Evaluacion", SqlDbType.NVarChar, 100).Value = (object)obj.NombreEvaluacion ?? DBNull.Value;
            cmd.Parameters.Add("@Concepto", SqlDbType.NVarChar, 255).Value = (object)obj.Concepto ?? DBNull.Value;
            cmd.Parameters.Add("@Calificacion_Maxima", SqlDbType.Decimal).Value = (object)obj.CalificacionMaxima ?? DBNull.Value;
            cmd.Parameters.Add("@Peso_Porcentual", SqlDbType.Decimal).Value = (object)obj.PesoPorcentual ?? DBNull.Value;
            cmd.Parameters.Add("@Orden", SqlDbType.Int).Value = (object)obj.Orden ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Aprobacion", SqlDbType.Bit).Value = (object)obj.RequiereAprobacion ?? DBNull.Value;
            cmd.Parameters.Add("@Version_Configuracion", SqlDbType.Int).Value = (object)obj.VersionConfiguracion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Metodo_Calculo", SqlDbType.Int).Value = (object)obj.IdMetodoCalculo ?? DBNull.Value;
            cmd.Parameters.Add("@Rubrica_Detalle", SqlDbType.NVarChar, -1).Value = (object)obj.RubricaDetalle ?? DBNull.Value;
            cmd.Parameters.Add("@Porcentaje_Minimo_Aprobacion", SqlDbType.Decimal).Value = (object)obj.PorcentajeMinimoAprobacion ?? DBNull.Value;
            cmd.Parameters.Add("@Niveles_Revision", SqlDbType.TinyInt).Value = (object)obj.NivelesRevision ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Rol_Aprobador", SqlDbType.Int).Value = (object)obj.IdRolAprobador ?? DBNull.Value;
            cmd.Parameters.Add("@Permite_Recalculo", SqlDbType.Bit).Value = (object)obj.PermiteRecalculo ?? DBNull.Value;
            cmd.Parameters.Add("@Tiempo_Maximo_Carga", SqlDbType.Int).Value = (object)obj.TiempoMaximoCarga ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = (object)obj.FechaInicio ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = (object)obj.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = (object)obj.Activo ?? DBNull.Value;
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

        public List<CeEvaluacionesModelos> ListarTodosLosModelos(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeEvaluacionesModelos> ltsModelos = new List<CeEvaluacionesModelos>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_evaluaciones_modelos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 142;
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
                        var modelo = LlenarModelo(dr);
                        // Agregar información adicional para mostrar
                        if (dr.Table.Columns.Contains("Nombre_Materia"))
                            modelo.NombreMateria = dr["Nombre_Materia"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Materia"]);
                        if (dr.Table.Columns.Contains("Nombre_Periodo"))
                            modelo.NombrePeriodo = dr["Nombre_Periodo"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Periodo"]);
                        if (dr.Table.Columns.Contains("Nombre_Tipo_Evaluacion"))
                            modelo.NombreTipoEvaluacion = dr["Nombre_Tipo_Evaluacion"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Tipo_Evaluacion"]);
                        ltsModelos.Add(modelo);
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
            return ltsModelos;
        }

        private CeEvaluacionesModelos LlenarModelo(DataRow dr)
        {
            return new CeEvaluacionesModelos
            {
                IdEvaluacionModelo = dr["Id_Evaluacion_Modelo"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Evaluacion_Modelo"]),
                IdMateriaPeriodo = dr["Id_Materia_Periodo"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Materia_Periodo"]),
                IdTipoEvaluacion = dr["Id_Tipo_Evaluacion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Tipo_Evaluacion"]),
                CodigoModelo = dr["Codigo_Modelo"] is DBNull ? string.Empty : Convert.ToString(dr["Codigo_Modelo"]),
                NombreEvaluacion = dr["Nombre_Evaluacion"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Evaluacion"]),
                Concepto = dr["Concepto"] is DBNull ? null : Convert.ToString(dr["Concepto"]),
                CalificacionMaxima = dr["Calificacion_Maxima"] is DBNull ? null : (decimal?)Convert.ToDecimal(dr["Calificacion_Maxima"]),
                PesoPorcentual = dr["Peso_Porcentual"] is DBNull ? null : (decimal?)Convert.ToDecimal(dr["Peso_Porcentual"]),
                Orden = dr["Orden"] is DBNull ? null : (int?)Convert.ToInt32(dr["Orden"]),
                RequiereAprobacion = dr["Requiere_Aprobacion"] is DBNull ? false : Convert.ToBoolean(dr["Requiere_Aprobacion"]),
                VersionConfiguracion = dr["Version_Configuracion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Version_Configuracion"]),
                IdMetodoCalculo = dr["Id_Metodo_Calculo"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Metodo_Calculo"]),
                RubricaDetalle = dr["Rubrica_Detalle"] is DBNull ? null : Convert.ToString(dr["Rubrica_Detalle"]),
                PorcentajeMinimoAprobacion = dr["Porcentaje_Minimo_Aprobacion"] is DBNull ? null : (decimal?)Convert.ToDecimal(dr["Porcentaje_Minimo_Aprobacion"]),
                NivelesRevision = dr["Niveles_Revision"] is DBNull ? null : (byte?)Convert.ToByte(dr["Niveles_Revision"]),
                IdRolAprobador = dr["Id_Rol_Aprobador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Rol_Aprobador"]),
                PermiteRecalculo = dr["Permite_Recalculo"] is DBNull ? false : Convert.ToBoolean(dr["Permite_Recalculo"]),
                TiempoMaximoCarga = dr["Tiempo_Maximo_Carga"] is DBNull ? null : (int?)Convert.ToInt32(dr["Tiempo_Maximo_Carga"]),
                FechaInicio = dr["Fecha_Inicio"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Inicio"]),
                FechaFin = dr["Fecha_Fin"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Fin"]),
                Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Transaccion"])
            };
        }
    }
}

