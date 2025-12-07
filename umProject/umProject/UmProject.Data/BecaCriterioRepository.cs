using Microsoft.Data.SqlClient;
using System;
using System.Data;
using UmProject.Entities;

namespace UmProject.Data
{
    public class BecaCriterioRepository : IBecaCriterioRepository
    {
        private readonly IConexionService _conexionService;

        public BecaCriterioRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<BecaCriterio>>> ListarPorProgramaAsync(int idPrograma, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<BecaCriterio>>();
            var criterios = new List<BecaCriterio>();

            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_becas_criterios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 66;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Programa", SqlDbType.Int).Value = idPrograma;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                criterios.Add(LlenarModelo(reader));
            }

            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<BecaCriterio>>(cmd, criterios);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<BecaCriterio>>> ObtenerPorIdAsync(int idBecaCriterio, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<BecaCriterio>>();
            var criterios = new List<BecaCriterio>();

            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_becas_criterios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 65;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Beca_Criterio", SqlDbType.Int).Value = idBecaCriterio;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                criterios.Add(LlenarModelo(reader));
            }

            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<BecaCriterio>>(cmd, criterios);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarAsync(BecaCriterio criterio, int idSesion)
        {
            var resultado = new ResultadoOperacion();

            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_becas_criterios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 64;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            AgregarParametrosCriterio(cmd, criterio);
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarAsync(BecaCriterio criterio, int idSesion)
        {
            var resultado = new ResultadoOperacion();

            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_becas_criterios", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 67;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Beca_Criterio", SqlDbType.Int).Value = criterio.IdBecaCriterio ?? 0;
            AgregarParametrosCriterio(cmd, criterio);
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private static void AgregarParametrosCriterio(SqlCommand cmd, BecaCriterio criterio)
        {
            cmd.Parameters.Add("@Id_Programa", SqlDbType.Int).Value = criterio.IdPrograma ?? 0;
            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 50).Value = (object?)criterio.Codigo ?? DBNull.Value;
            cmd.Parameters.Add("@Nombre_Criterio", SqlDbType.NVarChar, 150).Value = (object?)criterio.NombreCriterio ?? DBNull.Value;
            cmd.Parameters.Add("@Clave_Criterio", SqlDbType.NVarChar, 100).Value = (object?)criterio.ClaveCriterio ?? DBNull.Value;
            cmd.Parameters.Add("@Valor_Criterio", SqlDbType.NVarChar, 255).Value = (object?)criterio.ValorCriterio ?? DBNull.Value;
            cmd.Parameters.Add("@Tipo_Dato_Valor", SqlDbType.NVarChar, 50).Value = (object?)criterio.TipoDatoValor ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Criterio", SqlDbType.Int).Value = (object?)criterio.IdTipoCriterio ?? DBNull.Value;
            cmd.Parameters.Add("@Operador_Comparacion", SqlDbType.NVarChar, 10).Value = (object?)criterio.OperadorComparacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fuente_Validacion", SqlDbType.NVarChar, 150).Value = (object?)criterio.FuenteValidacion ?? DBNull.Value;
            cmd.Parameters.Add("@Expresion_Validacion", SqlDbType.NVarChar, 1000).Value = (object?)criterio.ExpresionValidacion ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 500).Value = (object?)criterio.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = criterio.Activo;
        }

        private static BecaCriterio LlenarModelo(SqlDataReader reader)
        {
            return new BecaCriterio
            {
                IdBecaCriterio = reader["Id_Beca_Criterio"] as int?,
                IdPrograma = reader["Id_Programa"] as int?,
                Codigo = reader["Codigo"] as string,
                NombreCriterio = reader["Nombre_Criterio"] as string,
                ClaveCriterio = reader["Clave_Criterio"] as string,
                ValorCriterio = reader["Valor_Criterio"] as string,
                TipoDatoValor = reader["Tipo_Dato_Valor"] as string,
                IdTipoCriterio = reader["Id_Tipo_Criterio"] as int?,
                OperadorComparacion = reader["Operador_Comparacion"] as string,
                FuenteValidacion = reader["Fuente_Validacion"] as string,
                ExpresionValidacion = reader["Expresion_Validacion"] as string,
                Observaciones = reader["Observaciones"] as string,
                Activo = reader["Activo"] as bool? ?? true,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int?,
                IdModificador = reader["Id_Modificador"] as int?
            };
        }
    }
}

