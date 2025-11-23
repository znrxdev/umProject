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
    public class CdInscripciones
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeInscripciones> FiltrarInscripcionesPorSeccion(CeInscripciones obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeInscripciones> ltsInscripciones = new List<CeInscripciones>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_inscripciones";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 114; // FILTRAR INSCRIPCIÓN POR SECCIÓN
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
                        ltsInscripciones.Add(LlenarModelo(dr));
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
            return ltsInscripciones;
        }

        private CeInscripciones LlenarModelo(DataRow dr)
        {
            return new CeInscripciones
            {
                IdInscripcion = dr["Id_Inscripcion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Inscripcion"]),
                CodigoInscripcion = dr["Codigo_Inscripcion"] is DBNull ? null : Convert.ToString(dr["Codigo_Inscripcion"]),
                IdSeccion = dr["Id_Seccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Seccion"]),
                IdEstudiante = dr["Id_Estudiante"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estudiante"]),
                IdTipoInscripcion = dr["Id_Tipo_Inscripcion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Tipo_Inscripcion"]),
                IdEstado = dr["Id_Estado"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estado"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Modificacion"]),
                FechaValidacion = dr["Fecha_Validacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Validacion"]),
                FechaRetiro = dr["Fecha_Retiro"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Retiro"]),
                MotivoRetiro = dr["Motivo_Retiro"] is DBNull ? null : Convert.ToString(dr["Motivo_Retiro"]),
                PromedioAcumulado = dr["Promedio_Acumulado"] is DBNull ? null : (decimal?)Convert.ToDecimal(dr["Promedio_Acumulado"]),
                TotalFaltas = dr["Total_Faltas"] is DBNull ? null : (int?)Convert.ToInt32(dr["Total_Faltas"]),
                EsRepetidor = dr["Es_Repetidor"] is DBNull ? null : (bool?)Convert.ToBoolean(dr["Es_Repetidor"]),
                EnRiesgoAcademico = dr["En_Riesgo_Academico"] is DBNull ? null : (bool?)Convert.ToBoolean(dr["En_Riesgo_Academico"]),
                IdCreador = dr["Id_Creador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Modificador"]),
                IdUsuarioValidador = dr["Id_Usuario_Validador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Usuario_Validador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Transaccion"]),
                Activo = dr["Activo"] is DBNull ? null : (bool?)Convert.ToBoolean(dr["Activo"]),
                // Propiedades adicionales de JOINs
                UsuarioEstudiante = dr.Table.Columns.Contains("Usuario_Estudiante") && dr["Usuario_Estudiante"] is not DBNull ? Convert.ToString(dr["Usuario_Estudiante"]) : null,
                NombreEstudiante = dr.Table.Columns.Contains("Nombre_Estudiante") && dr["Nombre_Estudiante"] is not DBNull ? Convert.ToString(dr["Nombre_Estudiante"]) : null
            };
        }
    }
}


