using CAPA_ENTIDADES;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace CAPA_DATOS
{
    public class CdSolicitudesBecas
    {
        private readonly CdConexion con = new CdConexion();
        private readonly SqlDataAdapter da = new SqlDataAdapter();
        private readonly SqlCommand cmd = new SqlCommand();

        private CeSolicitudesBecas LlenarModelo(DataRow dr)
        {
            return new CeSolicitudesBecas
            {
                IdSolicitudBeca = dr.Table.Columns.Contains("Id_Solicitud_Beca") && dr["Id_Solicitud_Beca"] != DBNull.Value ? Convert.ToInt32(dr["Id_Solicitud_Beca"]) : null,
                CodigoSeguimiento = dr.Table.Columns.Contains("Codigo_Seguimiento") && dr["Codigo_Seguimiento"] != DBNull.Value ? Convert.ToString(dr["Codigo_Seguimiento"]) : null,
                IdBecaPrograma = dr.Table.Columns.Contains("Id_Beca_Programa") && dr["Id_Beca_Programa"] != DBNull.Value ? Convert.ToInt32(dr["Id_Beca_Programa"]) : null,
                IdConvocatoria = dr.Table.Columns.Contains("Id_Convocatoria") && dr["Id_Convocatoria"] != DBNull.Value ? Convert.ToInt32(dr["Id_Convocatoria"]) : null,
                IdEstudiante = dr.Table.Columns.Contains("Id_Estudiante") && dr["Id_Estudiante"] != DBNull.Value ? Convert.ToInt32(dr["Id_Estudiante"]) : null,
                PromedioVigente = dr.Table.Columns.Contains("Promedio_Vigente") && dr["Promedio_Vigente"] != DBNull.Value ? Convert.ToDecimal(dr["Promedio_Vigente"]) : null,
                CreditosAprobados = dr.Table.Columns.Contains("Creditos_Aprobados") && dr["Creditos_Aprobados"] != DBNull.Value ? Convert.ToInt32(dr["Creditos_Aprobados"]) : null,
                TotalSancionesActivas = dr.Table.Columns.Contains("Total_Sanciones_Activas") && dr["Total_Sanciones_Activas"] != DBNull.Value ? Convert.ToInt32(dr["Total_Sanciones_Activas"]) : null,
                CumpleCriterios = dr.Table.Columns.Contains("Cumple_Criterios") && dr["Cumple_Criterios"] != DBNull.Value ? Convert.ToBoolean(dr["Cumple_Criterios"]) : null,
                NivelAprobacionActual = dr.Table.Columns.Contains("Nivel_Aprobacion_Actual") && dr["Nivel_Aprobacion_Actual"] != DBNull.Value ? Convert.ToByte(dr["Nivel_Aprobacion_Actual"]) : null,
                NivelAprobacionMaximo = dr.Table.Columns.Contains("Nivel_Aprobacion_Maximo") && dr["Nivel_Aprobacion_Maximo"] != DBNull.Value ? Convert.ToByte(dr["Nivel_Aprobacion_Maximo"]) : null,
                IdUsuarioResponsable = dr.Table.Columns.Contains("Id_Usuario_Responsable") && dr["Id_Usuario_Responsable"] != DBNull.Value ? Convert.ToInt32(dr["Id_Usuario_Responsable"]) : null,
                IdUsuarioSupervisor = dr.Table.Columns.Contains("Id_Usuario_Supervisor") && dr["Id_Usuario_Supervisor"] != DBNull.Value ? Convert.ToInt32(dr["Id_Usuario_Supervisor"]) : null,
                IdTipoDecision = dr.Table.Columns.Contains("Id_Tipo_Decision") && dr["Id_Tipo_Decision"] != DBNull.Value ? Convert.ToInt32(dr["Id_Tipo_Decision"]) : null,
                IdEstado = dr.Table.Columns.Contains("Id_Estado") && dr["Id_Estado"] != DBNull.Value ? Convert.ToInt32(dr["Id_Estado"]) : null,
                IdEstadoFlujo = dr.Table.Columns.Contains("Id_Estado_Flujo") && dr["Id_Estado_Flujo"] != DBNull.Value ? Convert.ToInt32(dr["Id_Estado_Flujo"]) : null,
                FechaSolicitud = dr.Table.Columns.Contains("Fecha_Solicitud") && dr["Fecha_Solicitud"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Solicitud"]) : null,
                FechaUltimaDecision = dr.Table.Columns.Contains("Fecha_Ultima_Decision") && dr["Fecha_Ultima_Decision"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Ultima_Decision"]) : null,
                FechaCierre = dr.Table.Columns.Contains("Fecha_Cierre") && dr["Fecha_Cierre"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Cierre"]) : null,
                MotivoUltimaDecision = dr.Table.Columns.Contains("Motivo_Ultima_Decision") && dr["Motivo_Ultima_Decision"] != DBNull.Value ? Convert.ToString(dr["Motivo_Ultima_Decision"]) : null,
                Observaciones = dr.Table.Columns.Contains("Observaciones") && dr["Observaciones"] != DBNull.Value ? Convert.ToString(dr["Observaciones"]) : null,
                EsPrioritaria = dr.Table.Columns.Contains("Es_Prioritaria") && dr["Es_Prioritaria"] != DBNull.Value ? Convert.ToBoolean(dr["Es_Prioritaria"]) : null,
                FechaCreacion = dr.Table.Columns.Contains("Fecha_Creacion") && dr["Fecha_Creacion"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Creacion"]) : null,
                FechaModificacion = dr.Table.Columns.Contains("Fecha_Modificacion") && dr["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Modificacion"]) : null,
                IdCreador = dr.Table.Columns.Contains("Id_Creador") && dr["Id_Creador"] != DBNull.Value ? Convert.ToInt32(dr["Id_Creador"]) : null,
                IdModificador = dr.Table.Columns.Contains("Id_Modificador") && dr["Id_Modificador"] != DBNull.Value ? Convert.ToInt32(dr["Id_Modificador"]) : null,
                IdTransaccion = dr.Table.Columns.Contains("Id_Transaccion") && dr["Id_Transaccion"] != DBNull.Value ? Convert.ToInt32(dr["Id_Transaccion"]) : null,
                UsuarioEstudiante = dr.Table.Columns.Contains("Usuario_Estudiante") && dr["Usuario_Estudiante"] != DBNull.Value ? Convert.ToString(dr["Usuario_Estudiante"]) : null,
                NombreEstudiante = dr.Table.Columns.Contains("Nombre_Estudiante") && dr["Nombre_Estudiante"] != DBNull.Value ? Convert.ToString(dr["Nombre_Estudiante"]) : null,
                NombrePrograma = dr.Table.Columns.Contains("Nombre_Programa") && dr["Nombre_Programa"] != DBNull.Value ? Convert.ToString(dr["Nombre_Programa"]) : null,
                NombreConvocatoria = dr.Table.Columns.Contains("Nombre_Convocatoria") && dr["Nombre_Convocatoria"] != DBNull.Value ? Convert.ToString(dr["Nombre_Convocatoria"]) : null
            };
        }

        public List<CeSolicitudesBecas> ObtenerMisSolicitudes(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeSolicitudesBecas> lista = new List<CeSolicitudesBecas>();

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_solicitudes_becas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 138; // OBTENER MIS SOLICITUDES DE BECAS
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tbl);
                oNum = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                oMsg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                foreach (DataRow dr in tbl.Rows)
                {
                    lista.Add(LlenarModelo(dr));
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

        public CeSolicitudesBecas EvaluarElegibilidad(int idPrograma, int idEstudiante, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_solicitudes_becas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 148; // EVALUAR ELEGIBILIDAD BECA
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Beca_Programa", SqlDbType.Int).Value = idPrograma;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = idEstudiante;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            CeSolicitudesBecas resultado = null;

            try
            {
                da.Fill(tbl);
                oNum = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                oMsg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                if (tbl.Rows.Count > 0)
                {
                    resultado = LlenarModelo(tbl.Rows[0]);
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

            return resultado;
        }

        public List<CeSolicitudesBecas> ListarPendientesPorProgramaYNivel(int idPrograma, byte? nivelAprobacion, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeSolicitudesBecas> lista = new List<CeSolicitudesBecas>();

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_solicitudes_becas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 149; // LISTAR PENDIENTES
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Beca_Programa", SqlDbType.Int).Value = idPrograma;
            if (nivelAprobacion.HasValue)
                cmd.Parameters.Add("@Nivel_Aprobacion_Actual", SqlDbType.TinyInt).Value = nivelAprobacion.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tbl);
                oNum = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                oMsg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                foreach (DataRow dr in tbl.Rows)
                {
                    lista.Add(LlenarModelo(dr));
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

        public bool RegistrarDecision(CeSolicitudesBecas obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_solicitudes_becas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 150; // REGISTRAR DECISIÓN
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Solicitud_Beca", SqlDbType.Int).Value = obj.IdSolicitudBeca;
            cmd.Parameters.Add("@Id_Tipo_Decision", SqlDbType.Int).Value = obj.IdTipoDecision;
            cmd.Parameters.Add("@Motivo_Ultima_Decision", SqlDbType.NVarChar, 500).Value = (object?)obj.MotivoUltimaDecision ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 1000).Value = (object?)obj.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Cierre", SqlDbType.DateTime).Value = (object?)obj.FechaCierre ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Ultima_Decision", SqlDbType.DateTime).Value = (object?)obj.FechaUltimaDecision ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(tbl);
                oNum = Convert.ToInt32(cmd.Parameters["@o_Num"].Value);
                oMsg = Convert.ToString(cmd.Parameters["@o_Msg"].Value);
                return oNum == 0;
            }
            catch (Exception ex)
            {
                oNum = -1;
                oMsg = ex.Message;
                return false;
            }
            finally
            {
                cmd.Connection = con.CerrarConexion();
                cmd.Parameters.Clear();
            }
        }
    }
}


