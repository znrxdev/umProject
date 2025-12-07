using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class EvaluacionModeloRepository : IEvaluacionModeloRepository
    {
        private readonly IConexionService _conexionService;

        public EvaluacionModeloRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<EvaluacionModelo>>> ListarEvaluacionesModelosAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<EvaluacionModelo>>();
            var modelos = new List<EvaluacionModelo>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_modelos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 142; // Listar todos los modelos
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    modelos.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<EvaluacionModelo>>(cmd, modelos);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<EvaluacionModelo>>> FiltrarEvaluacionModeloPorIdAsync(int idEvaluacionModelo, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<EvaluacionModelo>>();
            var modelos = new List<EvaluacionModelo>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_modelos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 122; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Modelo", SqlDbType.Int).Value = idEvaluacionModelo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    modelos.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<EvaluacionModelo>>(cmd, modelos);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<EvaluacionModelo>>> FiltrarEvaluacionModeloPorMateriaAsync(int idMateria, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<EvaluacionModelo>>();
            var modelos = new List<EvaluacionModelo>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_modelos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 123; // Filtrar por Id_Materia
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Materia", SqlDbType.Int).Value = idMateria;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    modelos.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<EvaluacionModelo>>(cmd, modelos);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarEvaluacionModeloAsync(EvaluacionModelo evaluacionModelo, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_modelos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 120; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Materia", SqlDbType.Int).Value = evaluacionModelo.IdMateria ?? 0;
            cmd.Parameters.Add("@Id_Tipo_Evaluacion", SqlDbType.Int).Value = evaluacionModelo.IdTipoEvaluacion ?? 0;
            cmd.Parameters.Add("@Codigo_Modelo", SqlDbType.VarChar, 30).Value = (object?)evaluacionModelo.CodigoModelo ?? DBNull.Value;
            cmd.Parameters.Add("@Nombre_Evaluacion", SqlDbType.NVarChar, 100).Value = (object?)evaluacionModelo.NombreEvaluacion ?? DBNull.Value;
            cmd.Parameters.Add("@Concepto", SqlDbType.NVarChar, 255).Value = (object?)evaluacionModelo.Concepto ?? DBNull.Value;
            cmd.Parameters.Add("@Version_Configuracion", SqlDbType.Int).Value = evaluacionModelo.VersionConfiguracion;
            cmd.Parameters.Add("@Rubrica_Detalle", SqlDbType.NVarChar, -1).Value = (object?)evaluacionModelo.RubricaDetalle ?? DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = evaluacionModelo.Activo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarEvaluacionModeloAsync(EvaluacionModelo evaluacionModelo, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_evaluaciones_modelos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 121; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Evaluacion_Modelo", SqlDbType.Int).Value = evaluacionModelo.IdEvaluacionModelo ?? 0;
            cmd.Parameters.Add("@Id_Materia", SqlDbType.Int).Value = (object?)evaluacionModelo.IdMateria ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Evaluacion", SqlDbType.Int).Value = (object?)evaluacionModelo.IdTipoEvaluacion ?? DBNull.Value;
            cmd.Parameters.Add("@Codigo_Modelo", SqlDbType.VarChar, 30).Value = (object?)evaluacionModelo.CodigoModelo ?? DBNull.Value;
            cmd.Parameters.Add("@Nombre_Evaluacion", SqlDbType.NVarChar, 100).Value = (object?)evaluacionModelo.NombreEvaluacion ?? DBNull.Value;
            cmd.Parameters.Add("@Concepto", SqlDbType.NVarChar, 255).Value = (object?)evaluacionModelo.Concepto ?? DBNull.Value;
            cmd.Parameters.Add("@Version_Configuracion", SqlDbType.Int).Value = (object?)evaluacionModelo.VersionConfiguracion ?? DBNull.Value;
            cmd.Parameters.Add("@Rubrica_Detalle", SqlDbType.NVarChar, -1).Value = (object?)evaluacionModelo.RubricaDetalle ?? DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = (object?)evaluacionModelo.Activo ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private EvaluacionModelo LlenarModelo(SqlDataReader reader)
        {
            string? LeerColumnaString(string nombreColumna)
            {
                try
                {
                    return reader[nombreColumna] is DBNull ? null : reader[nombreColumna] as string;
                }
                catch
                {
                    return null;
                }
            }

            return new EvaluacionModelo
            {
                IdEvaluacionModelo = reader["Id_Evaluacion_Modelo"] as int? ?? 0,
                IdMateria = reader["Id_Materia"] as int? ?? 0,
                IdTipoEvaluacion = reader["Id_Tipo_Evaluacion"] as int? ?? 0,
                CodigoModelo = reader["Codigo_Modelo"] as string ?? string.Empty,
                NombreEvaluacion = reader["Nombre_Evaluacion"] as string ?? string.Empty,
                Concepto = reader["Concepto"] as string,
                VersionConfiguracion = reader["Version_Configuracion"] as int? ?? 1,
                RubricaDetalle = reader["Rubrica_Detalle"] as string,
                Activo = reader["Activo"] as bool? ?? true,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI
                NombreMateria = LeerColumnaString("Nombre_Materia"),
                NombreTipoEvaluacion = LeerColumnaString("Nombre_Tipo_Evaluacion")
            };
        }
    }
}

