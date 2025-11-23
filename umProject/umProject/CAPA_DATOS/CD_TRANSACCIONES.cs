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
    public class CdTransacciones
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeTransacciones> ListarAuditoriaSistema(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeTransacciones> ltsTransacciones = new List<CeTransacciones>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_transacciones";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 144;
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
                        ltsTransacciones.Add(LlenarModelo(dr));
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
            return ltsTransacciones;
        }

        private CeTransacciones LlenarModelo(DataRow dr)
        {
            CeTransacciones transaccion = new CeTransacciones();
            
            transaccion.IdTransaccion = dr["Id_Transaccion"] is DBNull ? null : Convert.ToInt32(dr["Id_Transaccion"]);
            transaccion.IdTipoTransaccion = dr["Id_Tipo_Transaccion"] is DBNull ? null : Convert.ToInt32(dr["Id_Tipo_Transaccion"]);
            transaccion.NombreTipoTransaccion = dr["Nombre_Tipo_Transaccion"] is DBNull ? null : Convert.ToString(dr["Nombre_Tipo_Transaccion"]);
            transaccion.Concepto = dr["Concepto"] is DBNull ? null : Convert.ToString(dr["Concepto"]);
            transaccion.IdPersona = dr["Id_Persona"] is DBNull ? null : Convert.ToInt32(dr["Id_Persona"]);
            transaccion.NombrePersona = dr["Nombre_Persona"] is DBNull ? null : Convert.ToString(dr["Nombre_Persona"]);
            transaccion.IdUsuario = dr["Id_Usuario"] is DBNull ? null : Convert.ToInt32(dr["Id_Usuario"]);
            transaccion.NombreUsuario = dr["Nombre_Usuario"] is DBNull ? null : Convert.ToString(dr["Nombre_Usuario"]);
            transaccion.IdContacto = dr["Id_Contacto"] is DBNull ? null : Convert.ToInt32(dr["Id_Contacto"]);
            transaccion.IdEvaluacion = dr["Id_Evaluacion"] is DBNull ? null : Convert.ToInt32(dr["Id_Evaluacion"]);
            transaccion.IdSolicitudBeca = dr["Id_Solicitud_Beca"] is DBNull ? null : Convert.ToInt32(dr["Id_Solicitud_Beca"]);
            transaccion.IdInscripcion = dr["Id_Inscripcion"] is DBNull ? null : Convert.ToInt32(dr["Id_Inscripcion"]);
            transaccion.IdAutor = dr["Id_Autor"] is DBNull ? null : Convert.ToInt32(dr["Id_Autor"]);
            transaccion.NombreAutor = dr["Nombre_Autor"] is DBNull ? null : Convert.ToString(dr["Nombre_Autor"]);
            transaccion.FechaCreacion = dr["Fecha_Creacion"] is DBNull ? null : Convert.ToString(dr["Fecha_Creacion"]);
            transaccion.Completado = dr["Completado"] is DBNull ? null : Convert.ToBoolean(dr["Completado"]);
            transaccion.TipoEntidad = dr["Tipo_Entidad"] is DBNull ? null : Convert.ToString(dr["Tipo_Entidad"]);

            return transaccion;
        }
    }
}

