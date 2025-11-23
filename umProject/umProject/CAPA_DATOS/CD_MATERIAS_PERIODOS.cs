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
    public class CdMateriasPeriodos
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeMateriasPeriodos> ListarMateriasPeriodos(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeMateriasPeriodos> ltsMateriasPeriodos = new List<CeMateriasPeriodos>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_materias_periodos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 145; // LISTAR TODAS LAS MATERIAS PERÍODOS ACTIVAS
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
                        ltsMateriasPeriodos.Add(LlenarModelo(dr));
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
            return ltsMateriasPeriodos;
        }

        private CeMateriasPeriodos LlenarModelo(DataRow dr)
        {
            return new CeMateriasPeriodos
            {
                IdMateriaPeriodo = dr["Id_Materia_Periodo"] is DBNull ? null : Convert.ToInt32(dr["Id_Materia_Periodo"]),
                IdMateria = dr["Id_Materia"] is DBNull ? null : Convert.ToInt32(dr["Id_Materia"]),
                IdPeriodoAcademico = dr["Id_Periodo_Academico"] is DBNull ? null : Convert.ToInt32(dr["Id_Periodo_Academico"]),
                CodigoPlan = dr["Codigo_Plan"] is DBNull ? null : Convert.ToString(dr["Codigo_Plan"]),
                IdJornada = dr["Id_Jornada"] is DBNull ? null : Convert.ToInt32(dr["Id_Jornada"]),
                Modalidad = dr["Modalidad"] is DBNull ? null : Convert.ToString(dr["Modalidad"]),
                HorasTeoricas = dr["Horas_Teoricas"] is DBNull ? null : Convert.ToInt32(dr["Horas_Teoricas"]),
                HorasPracticas = dr["Horas_Practicas"] is DBNull ? null : Convert.ToInt32(dr["Horas_Practicas"]),
                PorcentajeAsistenciaMinima = dr["Porcentaje_Asistencia_Minima"] is DBNull ? null : Convert.ToDecimal(dr["Porcentaje_Asistencia_Minima"]),
                IdEstado = dr["Id_Estado"] is DBNull ? null : Convert.ToInt32(dr["Id_Estado"]),
                FechaPublicacion = dr["Fecha_Publicacion"] is DBNull ? null : Convert.ToDateTime(dr["Fecha_Publicacion"]),
                IdUsuarioPublicador = dr["Id_Usuario_Publicador"] is DBNull ? null : Convert.ToInt32(dr["Id_Usuario_Publicador"]),
                Observaciones = dr["Observaciones"] is DBNull ? null : Convert.ToString(dr["Observaciones"]),
                Activo = dr["Activo"] is DBNull ? null : Convert.ToBoolean(dr["Activo"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? null : Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? null : Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? null : Convert.ToInt32(dr["Id_Transaccion"]),
                NombreMateria = dr["Nombre_Materia"] is DBNull ? null : Convert.ToString(dr["Nombre_Materia"]),
                NombrePeriodo = dr["Nombre_Periodo"] is DBNull ? null : Convert.ToString(dr["Nombre_Periodo"]),
                CodigoPeriodo = dr["Codigo_Periodo"] is DBNull ? null : Convert.ToString(dr["Codigo_Periodo"])
            };
        }
    }
}

