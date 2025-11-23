using CAPA_ENTIDADES;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace CAPA_DATOS
{
    public class CdBecasProgramas
    {
        private readonly CdConexion con = new CdConexion();
        private readonly SqlDataAdapter da = new SqlDataAdapter();
        private readonly SqlCommand cmd = new SqlCommand();

        private CeBecasProgramas LlenarModelo(DataRow dr)
        {
            return new CeBecasProgramas
            {
                IdBecaPrograma = dr.Table.Columns.Contains("Id_Beca_Programa") && dr["Id_Beca_Programa"] != DBNull.Value ? Convert.ToInt32(dr["Id_Beca_Programa"]) : null,
                CodigoPrograma = dr.Table.Columns.Contains("Codigo_Programa") && dr["Codigo_Programa"] != DBNull.Value ? Convert.ToString(dr["Codigo_Programa"]) : null,
                NombrePrograma = dr.Table.Columns.Contains("Nombre_Programa") && dr["Nombre_Programa"] != DBNull.Value ? Convert.ToString(dr["Nombre_Programa"]) : null,
                Descripcion = dr.Table.Columns.Contains("Descripcion") && dr["Descripcion"] != DBNull.Value ? Convert.ToString(dr["Descripcion"]) : null,
                IdTipoPrograma = dr.Table.Columns.Contains("Id_Tipo_Programa") && dr["Id_Tipo_Programa"] != DBNull.Value ? Convert.ToInt32(dr["Id_Tipo_Programa"]) : null,
                IdModalidadPrograma = dr.Table.Columns.Contains("Id_Modalidad_Programa") && dr["Id_Modalidad_Programa"] != DBNull.Value ? Convert.ToInt32(dr["Id_Modalidad_Programa"]) : null,
                FechaVigenciaInicio = dr.Table.Columns.Contains("Fecha_Vigencia_Inicio") && dr["Fecha_Vigencia_Inicio"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Vigencia_Inicio"]) : null,
                FechaVigenciaFin = dr.Table.Columns.Contains("Fecha_Vigencia_Fin") && dr["Fecha_Vigencia_Fin"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Vigencia_Fin"]) : null,
                MontoMaximo = dr.Table.Columns.Contains("Monto_Maximo") && dr["Monto_Maximo"] != DBNull.Value ? Convert.ToDecimal(dr["Monto_Maximo"]) : null,
                IdMoneda = dr.Table.Columns.Contains("Id_Moneda") && dr["Id_Moneda"] != DBNull.Value ? Convert.ToInt32(dr["Id_Moneda"]) : null,
                PromedioMinimo = dr.Table.Columns.Contains("Promedio_Minimo") && dr["Promedio_Minimo"] != DBNull.Value ? Convert.ToDecimal(dr["Promedio_Minimo"]) : null,
                CreditosMinimos = dr.Table.Columns.Contains("Creditos_Minimos") && dr["Creditos_Minimos"] != DBNull.Value ? Convert.ToInt32(dr["Creditos_Minimos"]) : null,
                NivelesAprobacion = dr.Table.Columns.Contains("Niveles_Aprobacion") && dr["Niveles_Aprobacion"] != DBNull.Value ? Convert.ToByte(dr["Niveles_Aprobacion"]) : null,
                RequiereSinSanciones = dr.Table.Columns.Contains("Requiere_Sin_Sanciones") && dr["Requiere_Sin_Sanciones"] != DBNull.Value ? Convert.ToBoolean(dr["Requiere_Sin_Sanciones"]) : null,
                RequiereCartaCompromiso = dr.Table.Columns.Contains("Requiere_Carta_Compromiso") && dr["Requiere_Carta_Compromiso"] != DBNull.Value ? Convert.ToBoolean(dr["Requiere_Carta_Compromiso"]) : null,
                IdEstadoPrograma = dr.Table.Columns.Contains("Id_Estado_Programa") && dr["Id_Estado_Programa"] != DBNull.Value ? Convert.ToInt32(dr["Id_Estado_Programa"]) : null,
                Activo = dr.Table.Columns.Contains("Activo") && dr["Activo"] != DBNull.Value ? Convert.ToBoolean(dr["Activo"]) : null,
                FechaCreacion = dr.Table.Columns.Contains("Fecha_Creacion") && dr["Fecha_Creacion"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Creacion"]) : null,
                FechaModificacion = dr.Table.Columns.Contains("Fecha_Modificacion") && dr["Fecha_Modificacion"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha_Modificacion"]) : null,
                IdCreador = dr.Table.Columns.Contains("Id_Creador") && dr["Id_Creador"] != DBNull.Value ? Convert.ToInt32(dr["Id_Creador"]) : null,
                IdModificador = dr.Table.Columns.Contains("Id_Modificador") && dr["Id_Modificador"] != DBNull.Value ? Convert.ToInt32(dr["Id_Modificador"]) : null,
                IdTransaccion = dr.Table.Columns.Contains("Id_Transaccion") && dr["Id_Transaccion"] != DBNull.Value ? Convert.ToInt32(dr["Id_Transaccion"]) : null
            };
        }

        /// <summary>
        /// Lista programas de becas activos usando el SP usp_becas_programas (Id_Tipo_Transaccion = 63) con un filtro general.
        /// </summary>
        public List<CeBecasProgramas> ListarProgramasActivos(out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeBecasProgramas> lista = new List<CeBecasProgramas>();

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_becas_programas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 63; // FILTRAR POR NOMBRE
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Nombre_Programa", SqlDbType.NVarChar, 150).Value = "BECA"; // Coincide con todos los programas de becas creados
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
    }
}


