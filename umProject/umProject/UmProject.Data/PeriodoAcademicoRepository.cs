using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;
using System.Globalization;

namespace UmProject.Data
{
    public class PeriodoAcademicoRepository : IPeriodoAcademicoRepository
    {
        private readonly IConexionService _conexionService;

        public PeriodoAcademicoRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<PeriodoAcademico>> ListarPeriodosAsync(int idSesion)
        {
            var periodos = new List<PeriodoAcademico>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_periodos_academicos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 81; // Filtrar por código (NULL = listar todos)
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Periodo", SqlDbType.VarChar, 20).Value = DBNull.Value; // Null para listar todas
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    periodos.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return periodos;
        }

        public async Task<List<PeriodoAcademico>> FiltrarPeriodoPorIdAsync(int idPeriodo, int idSesion)
        {
            var periodos = new List<PeriodoAcademico>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_periodos_academicos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 80; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = idPeriodo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    periodos.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return periodos;
        }

        public async Task<List<PeriodoAcademico>> FiltrarPeriodoPorCodigoAsync(string codigoPeriodo, int idSesion)
        {
            var periodos = new List<PeriodoAcademico>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_periodos_academicos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 81; // Filtrar por código
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Periodo", SqlDbType.VarChar, 20).Value = codigoPeriodo ?? string.Empty;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    periodos.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return periodos;
        }

        public async Task<ResultadoOperacion> AgregarPeriodoAsync(PeriodoAcademico periodo, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_periodos_academicos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 78; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Periodo", SqlDbType.VarChar, 20).Value = periodo.CodigoPeriodo;
            cmd.Parameters.Add("@Nombre_Periodo", SqlDbType.NVarChar, 100).Value = periodo.NombrePeriodo;
            cmd.Parameters.Add("@Id_Tipo_Periodo", SqlDbType.Int).Value = (object?)periodo.IdTipoPeriodo ?? DBNull.Value;
            
            // Parsear fechas desde formato DD/MM/YYYY y convertir a DateTime para SQL
            DateTime fechaInicio = ParseFechaDDMMYYYY(periodo.FechaInicio ?? DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime fechaFin = ParseFechaDDMMYYYY(periodo.FechaFin ?? DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime? fechaCierre = string.IsNullOrEmpty(periodo.FechaCierreCalificaciones) 
                ? null 
                : ParseFechaDDMMYYYY(periodo.FechaCierreCalificaciones);
            
            cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.Date).Value = fechaInicio;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.Date).Value = fechaFin;
            cmd.Parameters.Add("@Fecha_Cierre_Calificaciones", SqlDbType.Date).Value = fechaCierre.HasValue ? (object)fechaCierre.Value : DBNull.Value;
            cmd.Parameters.Add("@Es_Periodo_Actual", SqlDbType.Bit).Value = periodo.EsPeriodoActual ?? false;
            // El código de integración se autogenera en el SP, siempre pasar NULL
            cmd.Parameters.Add("@Codigo_Integracion", SqlDbType.VarChar, 30).Value = DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object?)periodo.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = periodo.IdEstado ?? 4; // EN REVISION por defecto
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object?)periodo.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarPeriodoAsync(PeriodoAcademico periodo, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_periodos_academicos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 79; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = periodo.IdPeriodo;
            cmd.Parameters.Add("@Codigo_Periodo", SqlDbType.VarChar, 20).Value = periodo.CodigoPeriodo;
            cmd.Parameters.Add("@Nombre_Periodo", SqlDbType.NVarChar, 100).Value = periodo.NombrePeriodo;
            cmd.Parameters.Add("@Id_Tipo_Periodo", SqlDbType.Int).Value = (object?)periodo.IdTipoPeriodo ?? DBNull.Value;
            
            // Parsear fechas desde formato DD/MM/YYYY y convertir a DateTime para SQL
            DateTime fechaInicio = ParseFechaDDMMYYYY(periodo.FechaInicio ?? DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime fechaFin = ParseFechaDDMMYYYY(periodo.FechaFin ?? DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime? fechaCierre = string.IsNullOrEmpty(periodo.FechaCierreCalificaciones) 
                ? null 
                : ParseFechaDDMMYYYY(periodo.FechaCierreCalificaciones);
            
            cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.Date).Value = fechaInicio;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.Date).Value = fechaFin;
            cmd.Parameters.Add("@Fecha_Cierre_Calificaciones", SqlDbType.Date).Value = fechaCierre.HasValue ? (object)fechaCierre.Value : DBNull.Value;
            cmd.Parameters.Add("@Es_Periodo_Actual", SqlDbType.Bit).Value = periodo.EsPeriodoActual ?? false;
            cmd.Parameters.Add("@Codigo_Integracion", SqlDbType.VarChar, 30).Value = (object?)periodo.CodigoIntegracion ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object?)periodo.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = periodo.IdEstado ?? 1;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object?)periodo.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private PeriodoAcademico LlenarModelo(SqlDataReader reader)
        {
            return new PeriodoAcademico
            {
                IdPeriodo = reader["Id_Periodo"] as int? ?? 0,
                CodigoPeriodo = reader["Codigo_Periodo"] as string ?? string.Empty,
                NombrePeriodo = reader["Nombre_Periodo"] as string ?? string.Empty,
                IdTipoPeriodo = reader["Id_Tipo_Periodo"] as int?,
                FechaInicio = reader["Fecha_Inicio"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Inicio"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaFin = reader["Fecha_Fin"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Fin"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaCierreCalificaciones = reader["Fecha_Cierre_Calificaciones"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Cierre_Calificaciones"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                EsPeriodoActual = reader["Es_Periodo_Actual"] as bool? ?? false,
                CodigoIntegracion = reader["Codigo_Integracion"] as string,
                Observaciones = reader["Observaciones"] as string,
                IdEstado = reader["Id_Estado"] as int? ?? 0,
                IdEstadoPublicacion = reader["Id_Estado_Publicacion"] as int?,
                NombreEstado = reader["Nombre_Estado"] as string ?? string.Empty, // Nombre del estado desde cls_estados
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0
            };
        }

        /// <summary>
        /// Parsea una fecha desde formato DD/MM/YYYY a DateTime
        /// </summary>
        private DateTime ParseFechaDDMMYYYY(string fechaString)
        {
            if (string.IsNullOrWhiteSpace(fechaString))
                return DateTime.Now;

            // Intentar parsear en formato DD/MM/YYYY
            var formatos = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy", "yyyy-MM-dd" };
            
            foreach (var formato in formatos)
            {
                if (DateTime.TryParseExact(fechaString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
                {
                    return fecha;
                }
            }

            // Si no funciona con formato específico, intentar parseo normal
            if (DateTime.TryParse(fechaString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaParsed))
            {
                return fechaParsed;
            }

            // Si todo falla, retornar fecha actual
            return DateTime.Now;
        }
    }
}

