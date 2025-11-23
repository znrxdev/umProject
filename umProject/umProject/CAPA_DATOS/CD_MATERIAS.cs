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
    public class CdMaterias
    {
        private CdConexion con = new CdConexion();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        public List<CeMaterias> FiltrarMateriasPorId(CeMaterias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeMaterias> ltsMaterias = new List<CeMaterias>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_materias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 75;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Materia", SqlDbType.Int).Value = obj.IdMateria;
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
                        ltsMaterias.Add(LlenarModelo(dr));
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
            return ltsMaterias;
        }

        public List<CeMaterias> FiltrarMateriasPorCodigo(CeMaterias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeMaterias> ltsMaterias = new List<CeMaterias>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_materias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 76;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Codigo_Materia", SqlDbType.VarChar, 10).Value = obj.CodigoMateria;
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
                        ltsMaterias.Add(LlenarModelo(dr));
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
            return ltsMaterias;
        }

        public List<CeMaterias> FiltrarMateriasPorNombre(CeMaterias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;
            DataTable tbl = new DataTable();
            List<CeMaterias> ltsMaterias = new List<CeMaterias>();
            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_materias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 77;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Nombre_Materia", SqlDbType.NVarChar, 100).Value = obj.NombreMateria;
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
                        ltsMaterias.Add(LlenarModelo(dr));
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
            return ltsMaterias;
        }

        public void AgregarMaterias(CeMaterias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_materias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 73;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Codigo_Materia", SqlDbType.VarChar, 10).Value = obj.CodigoMateria;
            cmd.Parameters.Add("@Nombre_Materia", SqlDbType.NVarChar, 100).Value = obj.NombreMateria;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = obj.Activo ?? true;
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

        public void ActualizarMaterias(CeMaterias obj, out int oNum, out string oMsg)
        {
            oNum = 0;
            oMsg = string.Empty;

            cmd.Connection = con.AbrirConexion();
            cmd.CommandText = "usp_materias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 74;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = CeSesionUsuario.IdSesion;
            cmd.Parameters.Add("@Id_Materia", SqlDbType.Int).Value = obj.IdMateria;
            cmd.Parameters.Add("@Codigo_Materia", SqlDbType.VarChar, 10).Value = obj.CodigoMateria;
            cmd.Parameters.Add("@Nombre_Materia", SqlDbType.NVarChar, 100).Value = obj.NombreMateria;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = obj.Activo;
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

        private CeMaterias LlenarModelo(DataRow dr)
        {
            return new CeMaterias
            {
                IdMateria = dr["Id_Materia"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Materia"]),
                CodigoMateria = dr["Codigo_Materia"] is DBNull ? string.Empty : Convert.ToString(dr["Codigo_Materia"]),
                NombreMateria = dr["Nombre_Materia"] is DBNull ? string.Empty : Convert.ToString(dr["Nombre_Materia"]),
                FechaCreacion = dr["Fecha_Creacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Creacion"]),
                FechaModificacion = dr["Fecha_Modificacion"] is DBNull ? string.Empty : Convert.ToString(dr["Fecha_Modificacion"]),
                IdCreador = dr["Id_Creador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Creador"]),
                IdModificador = dr["Id_Modificador"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Modificador"]),
                IdTransaccion = dr["Id_Transaccion"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Transaccion"]),
                Activo = dr["Activo"] is DBNull ? false : Convert.ToBoolean(dr["Activo"])
            };
        }
    }
}

