using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class BecaProgramaRepository : IBecaProgramaRepository
    {
        private readonly IConexionService _conexionService;

        public BecaProgramaRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<BecaPrograma>>> ListarBecaProgramasAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<BecaPrograma>>();
            var programas = new List<BecaPrograma>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_becas_programas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 184; // Listar todos los programas
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    programas.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<BecaPrograma>>(cmd, programas);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<BecaPrograma>>> FiltrarBecaProgramaPorIdAsync(int idBecaPrograma, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<BecaPrograma>>();
            var programas = new List<BecaPrograma>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_becas_programas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 62; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Beca_Programa", SqlDbType.Int).Value = idBecaPrograma;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    programas.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<BecaPrograma>>(cmd, programas);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarBecaProgramaAsync(BecaPrograma becaPrograma, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_becas_programas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 59; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            var codigo = string.IsNullOrWhiteSpace(becaPrograma.CodigoPrograma)
                ? GenerarCodigoPrograma()
                : becaPrograma.CodigoPrograma!;
            cmd.Parameters.Add("@Codigo_Programa", SqlDbType.VarChar, 30).Value = codigo;
            cmd.Parameters.Add("@Nombre_Programa", SqlDbType.NVarChar, 150).Value = becaPrograma.NombrePrograma ?? string.Empty;
            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500).Value = (object?)becaPrograma.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Programa", SqlDbType.Int).Value = (object?)becaPrograma.IdTipoPrograma ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Modalidad_Programa", SqlDbType.Int).Value = (object?)becaPrograma.IdModalidadPrograma ?? DBNull.Value;
            cmd.Parameters.Add("@Promedio_Minimo", SqlDbType.Decimal).Value = (object?)becaPrograma.PromedioMinimo ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Sin_Sanciones", SqlDbType.Bit).Value = becaPrograma.RequiereSinSanciones;
            cmd.Parameters.Add("@Id_Estado_Programa", SqlDbType.Int).Value = becaPrograma.IdEstadoPrograma ?? 4;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarBecaProgramaAsync(BecaPrograma becaPrograma, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_becas_programas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 60; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Beca_Programa", SqlDbType.Int).Value = becaPrograma.IdBecaPrograma ?? 0;
            cmd.Parameters.Add("@Codigo_Programa", SqlDbType.VarChar, 30).Value = (object?)becaPrograma.CodigoPrograma ?? DBNull.Value;
            cmd.Parameters.Add("@Nombre_Programa", SqlDbType.NVarChar, 150).Value = (object?)becaPrograma.NombrePrograma ?? DBNull.Value;
            cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500).Value = (object?)becaPrograma.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Programa", SqlDbType.Int).Value = (object?)becaPrograma.IdTipoPrograma ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Modalidad_Programa", SqlDbType.Int).Value = (object?)becaPrograma.IdModalidadPrograma ?? DBNull.Value;
            cmd.Parameters.Add("@Promedio_Minimo", SqlDbType.Decimal).Value = (object?)becaPrograma.PromedioMinimo ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Sin_Sanciones", SqlDbType.Bit).Value = (object?)becaPrograma.RequiereSinSanciones ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado_Programa", SqlDbType.Int).Value = (object?)becaPrograma.IdEstadoPrograma ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private static string GenerarCodigoPrograma()
        {
            var sufijo = Guid.NewGuid().ToString("N")[..4].ToUpperInvariant();
            return $"BP-{DateTime.UtcNow:yyyyMMddHHmmss}-{sufijo}";
        }

        private BecaPrograma LlenarModelo(SqlDataReader reader)
        {
            // Helper para leer columnas opcionales de forma segura
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

            decimal? LeerColumnaDecimal(string nombreColumna)
            {
                try
                {
                    return reader[nombreColumna] is DBNull ? null : (decimal?)Convert.ToDecimal(reader[nombreColumna]);
                }
                catch
                {
                    return null;
                }
            }

            return new BecaPrograma
            {
                IdBecaPrograma = reader["Id_Beca_Programa"] as int? ?? 0,
                CodigoPrograma = reader["Codigo_Programa"] as string ?? string.Empty,
                NombrePrograma = reader["Nombre_Programa"] as string ?? string.Empty,
                Descripcion = reader["Descripcion"] as string,
                IdTipoPrograma = reader["Id_Tipo_Programa"] as int?,
                IdModalidadPrograma = reader["Id_Modalidad_Programa"] as int?,
                PromedioMinimo = LeerColumnaDecimal("Promedio_Minimo"),
                RequiereSinSanciones = reader["Requiere_Sin_Sanciones"] as bool? ?? true,
                IdEstadoPrograma = reader["Id_Estado_Programa"] as int? ?? 0,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI (pueden ser NULL si no vienen del SP)
                NombreTipoPrograma = LeerColumnaString("Nombre_Tipo_Programa"),
                NombreModalidadPrograma = LeerColumnaString("Nombre_Modalidad_Programa"),
                NombreEstadoPrograma = LeerColumnaString("Nombre_Estado_Programa")
            };
        }
    }
}

