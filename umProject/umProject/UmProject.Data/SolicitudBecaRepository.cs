using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class SolicitudBecaRepository : ISolicitudBecaRepository
    {
        private readonly IConexionService _conexionService;

        public SolicitudBecaRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<SolicitudBeca>>> ListarSolicitudesBecaAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<SolicitudBeca>>();
            var solicitudes = new List<SolicitudBeca>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_solicitudes_becas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 151; // Listar todas las solicitudes
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    solicitudes.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<SolicitudBeca>>(cmd, solicitudes);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<SolicitudBeca>>> FiltrarSolicitudBecaPorIdAsync(int idSolicitudBeca, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<SolicitudBeca>>();
            var solicitudes = new List<SolicitudBeca>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_solicitudes_becas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 70; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Solicitud_Beca", SqlDbType.Int).Value = idSolicitudBeca;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    solicitudes.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<SolicitudBeca>>(cmd, solicitudes);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarSolicitudBecaAsync(SolicitudBeca solicitudBeca, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_solicitudes_becas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 68; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Seguimiento", SqlDbType.VarChar, 30).Value = (object?)solicitudBeca.CodigoSeguimiento ?? DBNull.Value; // Puede ser NULL, el SP lo genera
            cmd.Parameters.Add("@Id_Beca_Programa", SqlDbType.Int).Value = solicitudBeca.IdBecaPrograma ?? 0;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = solicitudBeca.IdEstudiante ?? 0;
            cmd.Parameters.Add("@Promedio_Vigente", SqlDbType.Decimal).Value = (object?)solicitudBeca.PromedioVigente ?? DBNull.Value;
            cmd.Parameters.Add("@Total_Sanciones_Activas", SqlDbType.Int).Value = solicitudBeca.TotalSancionesActivas;
            cmd.Parameters.Add("@Cumple_Criterios", SqlDbType.Bit).Value = solicitudBeca.CumpleCriterios;
            cmd.Parameters.Add("@Id_Tipo_Decision", SqlDbType.Int).Value = (object?)solicitudBeca.IdTipoDecision ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = solicitudBeca.IdEstado ?? 1;
            cmd.Parameters.Add("@Fecha_Solicitud", SqlDbType.DateTime).Value = (object?)solicitudBeca.FechaSolicitud ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Ultima_Decision", SqlDbType.DateTime).Value = (object?)solicitudBeca.FechaUltimaDecision ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Cierre", SqlDbType.DateTime).Value = (object?)solicitudBeca.FechaCierre ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Ultima_Decision", SqlDbType.NVarChar, 500).Value = (object?)solicitudBeca.MotivoUltimaDecision ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 1000).Value = (object?)solicitudBeca.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Prioritaria", SqlDbType.Bit).Value = solicitudBeca.EsPrioritaria;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarSolicitudBecaAsync(SolicitudBeca solicitudBeca, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_solicitudes_becas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 69; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Solicitud_Beca", SqlDbType.Int).Value = solicitudBeca.IdSolicitudBeca ?? 0;
            cmd.Parameters.Add("@Codigo_Seguimiento", SqlDbType.VarChar, 30).Value = (object?)solicitudBeca.CodigoSeguimiento ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Beca_Programa", SqlDbType.Int).Value = (object?)solicitudBeca.IdBecaPrograma ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = (object?)solicitudBeca.IdEstudiante ?? DBNull.Value;
            cmd.Parameters.Add("@Promedio_Vigente", SqlDbType.Decimal).Value = (object?)solicitudBeca.PromedioVigente ?? DBNull.Value;
            cmd.Parameters.Add("@Total_Sanciones_Activas", SqlDbType.Int).Value = (object?)solicitudBeca.TotalSancionesActivas ?? DBNull.Value;
            cmd.Parameters.Add("@Cumple_Criterios", SqlDbType.Bit).Value = (object?)solicitudBeca.CumpleCriterios ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Decision", SqlDbType.Int).Value = (object?)solicitudBeca.IdTipoDecision ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object?)solicitudBeca.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Solicitud", SqlDbType.DateTime).Value = (object?)solicitudBeca.FechaSolicitud ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Ultima_Decision", SqlDbType.DateTime).Value = (object?)solicitudBeca.FechaUltimaDecision ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Cierre", SqlDbType.DateTime).Value = (object?)solicitudBeca.FechaCierre ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Ultima_Decision", SqlDbType.NVarChar, 500).Value = (object?)solicitudBeca.MotivoUltimaDecision ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 1000).Value = (object?)solicitudBeca.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Prioritaria", SqlDbType.Bit).Value = (object?)solicitudBeca.EsPrioritaria ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private SolicitudBeca LlenarModelo(SqlDataReader reader)
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

            return new SolicitudBeca
            {
                IdSolicitudBeca = reader["Id_Solicitud_Beca"] as int? ?? 0,
                CodigoSeguimiento = reader["Codigo_Seguimiento"] as string ?? string.Empty,
                IdBecaPrograma = reader["Id_Beca_Programa"] as int? ?? 0,
                IdEstudiante = reader["Id_Estudiante"] as int? ?? 0,
                PromedioVigente = reader["Promedio_Vigente"] is DBNull ? null : (decimal?)Convert.ToDecimal(reader["Promedio_Vigente"]),
                TotalSancionesActivas = reader["Total_Sanciones_Activas"] as int? ?? 0,
                CumpleCriterios = reader["Cumple_Criterios"] as bool? ?? false,
                IdTipoDecision = reader["Id_Tipo_Decision"] as int?,
                IdEstado = reader["Id_Estado"] as int? ?? 0,
                FechaSolicitud = reader["Fecha_Solicitud"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Solicitud"]),
                FechaUltimaDecision = reader["Fecha_Ultima_Decision"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Ultima_Decision"]),
                FechaCierre = reader["Fecha_Cierre"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Cierre"]),
                MotivoUltimaDecision = reader["Motivo_Ultima_Decision"] as string,
                Observaciones = reader["Observaciones"] as string,
                EsPrioritaria = reader["Es_Prioritaria"] as bool? ?? false,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI (pueden ser NULL si no vienen del SP)
                NombreProgramaBeca = LeerColumnaString("Nombre_Programa_Beca"),
                CodigoProgramaBeca = LeerColumnaString("Codigo_Programa_Beca"),
                EstudianteUsuario = LeerColumnaString("Usuario_Estudiante"),
                EstudianteNombre = LeerColumnaString("Nombre_Estudiante"),
                EstadoNombre = LeerColumnaString("Estado_Nombre"),
                TipoDecisionNombre = LeerColumnaString("Tipo_Decision_Nombre")
            };
        }
    }
}

