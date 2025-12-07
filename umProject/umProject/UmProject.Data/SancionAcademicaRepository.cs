using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class SancionAcademicaRepository : ISancionAcademicaRepository
    {
        private readonly IConexionService _conexionService;

        public SancionAcademicaRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<SancionAcademica>>> ListarSancionesAcademicasAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<SancionAcademica>>();
            var sanciones = new List<SancionAcademica>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_sanciones_academicas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 183; // Listar todas las sanciones
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    sanciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<SancionAcademica>>(cmd, sanciones);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<SancionAcademica>>> FiltrarSancionAcademicaPorIdAsync(int idSancion, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<SancionAcademica>>();
            var sanciones = new List<SancionAcademica>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_sanciones_academicas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 88; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Sancion", SqlDbType.Int).Value = idSancion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    sanciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<SancionAcademica>>(cmd, sanciones);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarSancionAcademicaAsync(SancionAcademica sancionAcademica, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_sanciones_academicas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 87; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Sancion", SqlDbType.VarChar, 30).Value = (object?)sancionAcademica.CodigoSancion ?? DBNull.Value; // Puede ser NULL, el SP lo genera
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = sancionAcademica.IdEstudiante ?? 0;
            cmd.Parameters.Add("@Id_Tipo_Sancion", SqlDbType.Int).Value = sancionAcademica.IdTipoSancion ?? 0;
            cmd.Parameters.Add("@Id_Tipo_Falta", SqlDbType.Int).Value = (object?)sancionAcademica.IdTipoFalta ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Severidad", SqlDbType.Int).Value = sancionAcademica.IdSeveridad ?? 0;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = sancionAcademica.IdEstado ?? 1;
            cmd.Parameters.Add("@Fecha_Registro", SqlDbType.DateTime).Value = (object?)sancionAcademica.FechaRegistro ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = (object?)sancionAcademica.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo", SqlDbType.NVarChar, 300).Value = (object?)sancionAcademica.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Apelable", SqlDbType.Bit).Value = sancionAcademica.EsApelable;
            cmd.Parameters.Add("@Fecha_Apelacion", SqlDbType.DateTime).Value = (object?)sancionAcademica.FechaApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Resultado_Apelacion", SqlDbType.NVarChar, 200).Value = (object?)sancionAcademica.ResultadoApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones_Apelacion", SqlDbType.NVarChar, 500).Value = (object?)sancionAcademica.ObservacionesApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Documento_Resolucion", SqlDbType.NVarChar, 255).Value = (object?)sancionAcademica.DocumentoResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Resolucion", SqlDbType.Int).Value = (object?)sancionAcademica.IdUsuarioResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Resolucion", SqlDbType.DateTime).Value = (object?)sancionAcademica.FechaResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Sancion_Origen", SqlDbType.Int).Value = (object?)sancionAcademica.IdSancionOrigen ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<SancionAcademica>>> ObtenerMisSancionesAcademicasAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<SancionAcademica>>();
            var sanciones = new List<SancionAcademica>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_sanciones_academicas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 136; // Obtener mis sanciones académicas
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    sanciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<SancionAcademica>>(cmd, sanciones);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarSancionAcademicaAsync(SancionAcademica sancionAcademica, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_sanciones_academicas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 90; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Sancion", SqlDbType.Int).Value = sancionAcademica.IdSancion ?? 0;
            cmd.Parameters.Add("@Codigo_Sancion", SqlDbType.VarChar, 30).Value = (object?)sancionAcademica.CodigoSancion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = (object?)sancionAcademica.IdEstudiante ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Sancion", SqlDbType.Int).Value = (object?)sancionAcademica.IdTipoSancion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Falta", SqlDbType.Int).Value = (object?)sancionAcademica.IdTipoFalta ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Severidad", SqlDbType.Int).Value = (object?)sancionAcademica.IdSeveridad ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object?)sancionAcademica.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Registro", SqlDbType.DateTime).Value = (object?)sancionAcademica.FechaRegistro ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = (object?)sancionAcademica.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo", SqlDbType.NVarChar, 300).Value = (object?)sancionAcademica.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("@Es_Apelable", SqlDbType.Bit).Value = (object?)sancionAcademica.EsApelable ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Apelacion", SqlDbType.DateTime).Value = (object?)sancionAcademica.FechaApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Resultado_Apelacion", SqlDbType.NVarChar, 200).Value = (object?)sancionAcademica.ResultadoApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones_Apelacion", SqlDbType.NVarChar, 500).Value = (object?)sancionAcademica.ObservacionesApelacion ?? DBNull.Value;
            cmd.Parameters.Add("@Documento_Resolucion", SqlDbType.NVarChar, 255).Value = (object?)sancionAcademica.DocumentoResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Resolucion", SqlDbType.Int).Value = (object?)sancionAcademica.IdUsuarioResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Resolucion", SqlDbType.DateTime).Value = (object?)sancionAcademica.FechaResolucion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Sancion_Origen", SqlDbType.Int).Value = (object?)sancionAcademica.IdSancionOrigen ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ApelarSancionAcademicaAsync(int idSancion, string observacionesApelacion, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_sanciones_academicas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 139; // Apelar sanción académica
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Sancion", SqlDbType.Int).Value = idSancion;
            cmd.Parameters.Add("@Observaciones_Apelacion", SqlDbType.NVarChar, 500).Value = observacionesApelacion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private SancionAcademica LlenarModelo(SqlDataReader reader)
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

            return new SancionAcademica
            {
                IdSancion = reader["Id_Sancion"] as int? ?? 0,
                CodigoSancion = reader["Codigo_Sancion"] as string ?? string.Empty,
                IdEstudiante = reader["Id_Estudiante"] as int? ?? 0,
                IdTipoSancion = reader["Id_Tipo_Sancion"] as int? ?? 0,
                IdTipoFalta = reader["Id_Tipo_Falta"] as int?,
                IdSeveridad = reader["Id_Severidad"] as int? ?? 0,
                IdEstado = reader["Id_Estado"] as int? ?? 0,
                FechaRegistro = reader["Fecha_Registro"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Registro"]),
                FechaFin = reader["Fecha_Fin"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Fin"]),
                Motivo = reader["Motivo"] as string,
                EsApelable = reader["Es_Apelable"] as bool? ?? false,
                FechaApelacion = reader["Fecha_Apelacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Apelacion"]),
                ResultadoApelacion = reader["Resultado_Apelacion"] as string,
                ObservacionesApelacion = reader["Observaciones_Apelacion"] as string,
                DocumentoResolucion = reader["Documento_Resolucion"] as string,
                IdUsuarioResolucion = reader["Id_Usuario_Resolucion"] as int?,
                FechaResolucion = reader["Fecha_Resolucion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Resolucion"]),
                IdSancionOrigen = reader["Id_Sancion_Origen"] as int?,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI (pueden ser NULL si no vienen del SP)
                UsuarioEstudiante = LeerColumnaString("Usuario_Estudiante"),
                NombreEstudiante = LeerColumnaString("Nombre_Estudiante"),
                NombreTipoSancion = LeerColumnaString("Nombre_Tipo_Sancion"),
                NombreTipoFalta = LeerColumnaString("Nombre_Tipo_Falta"),
                NombreSeveridad = LeerColumnaString("Nombre_Severidad"),
                NombreEstado = LeerColumnaString("Nombre_Estado"),
                UsuarioResolucion = LeerColumnaString("Usuario_Resolucion"),
                NombreUsuarioResolucion = LeerColumnaString("Nombre_Usuario_Resolucion")
            };
        }
    }
}

