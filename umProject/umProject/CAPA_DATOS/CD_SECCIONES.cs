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
    public class CdSecciones
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeSecciones> FiltrarSeccionesPorMateriaPeriodo(CeSecciones obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeSecciones> ltsSecciones = new List<CeSecciones>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_secciones";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 100; // FILTRAR SECCIÓN POR MATERIA PERÍODO
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
                        ltsSecciones.Add(LlenarModelo(dr));
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
            return ltsSecciones;
        }

        private CeSecciones LlenarModelo(DataRow dr)
        {
            return new CeSecciones
            {
                IdSeccion = dr["Id_Seccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Seccion"]),
                CodigoSeccion = dr["Codigo_Seccion"] is DBNull ? null : Convert.ToString(dr["Codigo_Seccion"]),
                IdMateriaPeriodo = dr["Id_Materia_Periodo"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Materia_Periodo"]),
                IdDocente = dr["Id_Docente"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Docente"]),
                IdTipoSeccion = dr["Id_Tipo_Seccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Tipo_Seccion"]),
                IdAula = dr["Id_Aula"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Aula"]),
                HorarioDescripcion = dr["Horario_Descripcion"] is DBNull ? null : Convert.ToString(dr["Horario_Descripcion"]),
                Modalidad = dr["Modalidad"] is DBNull ? null : Convert.ToString(dr["Modalidad"]),
                CupoMaximo = dr["Cupo_Maximo"] is DBNull ? null : (int?)Convert.ToInt32(dr["Cupo_Maximo"]),
                RequiereAsistencia = dr["Requiere_Asistencia"] is DBNull ? null : (bool?)Convert.ToBoolean(dr["Requiere_Asistencia"]),
                PorcentajeAsistenciaMinima = dr["Porcentaje_Asistencia_Minima"] is DBNull ? null : (decimal?)Convert.ToDecimal(dr["Porcentaje_Asistencia_Minima"]),
                IdEstado = dr["Id_Estado"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estado"]),
                IdEstadoPublicacion = dr["Id_Estado_Publicacion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Estado_Publicacion"]),
                FechaPublicacion = dr["Fecha_Publicacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Publicacion"]),
                FechaCierre = dr["Fecha_Cierre"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Cierre"]),
                CodigoFirma = dr["Codigo_Firma"] is DBNull ? null : Convert.ToString(dr["Codigo_Firma"]),
                IdUsuarioPublicador = dr["Id_Usuario_Publicador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Usuario_Publicador"]),
                Observaciones = dr["Observaciones"] is DBNull ? null : Convert.ToString(dr["Observaciones"]),
                Activo = dr["Activo"] is DBNull ? null : (bool?)Convert.ToBoolean(dr["Activo"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? null : (int?)Convert.ToInt32(dr["Id_Transaccion"])
            };
        }
    }
}


