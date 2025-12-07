using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly IConexionService _conexionService;

        public GrupoRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<Grupo>>> ListarGruposAsync(int idSesion, int? idPeriodo = null)
        {
            var resultado = new ResultadoConsulta<List<Grupo>>();
            var grupos = new List<Grupo>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_grupos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 104; // Listar todos / Filtrar por período
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = idPeriodo.HasValue ? (object)idPeriodo.Value : DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    grupos.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Grupo>>(cmd, grupos);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<Grupo>>> FiltrarGrupoPorIdAsync(int idGrupo, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<Grupo>>();
            var grupos = new List<Grupo>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_grupos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 103; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = idGrupo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    grupos.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Grupo>>(cmd, grupos);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarGrupoAsync(Grupo grupo, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_grupos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 101; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            // Código_Grupo y Codigo_Seguimiento se autogeneran en el SP, no se envían desde aquí
            cmd.Parameters.Add("@Codigo_Grupo", SqlDbType.VarChar, 20).Value = DBNull.Value;
            cmd.Parameters.Add("@Nombre_Grupo", SqlDbType.NVarChar, 100).Value = grupo.NombreGrupo ?? string.Empty;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = grupo.IdPeriodo ?? 0;
            cmd.Parameters.Add("@Id_Tipo_Grupo", SqlDbType.Int).Value = grupo.IdTipoGrupo ?? 0;
            cmd.Parameters.Add("@Id_Coordinador", SqlDbType.Int).Value = (object?)grupo.IdCoordinador ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Jornada", SqlDbType.Int).Value = DBNull.Value; // Siempre NULL
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = grupo.IdEstado ?? 4; // EN REVISION por defecto
            cmd.Parameters.Add("@Fecha_Cierre", SqlDbType.DateTime).Value = (object?)grupo.FechaCierre ?? DBNull.Value; // Se autocalcula en SP si es NULL
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object?)grupo.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Codigo_Seguimiento", SqlDbType.VarChar, 30).Value = DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = true; // Siempre true
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarGrupoAsync(Grupo grupo, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_grupos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 102; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = grupo.IdGrupo ?? 0;
            cmd.Parameters.Add("@Codigo_Grupo", SqlDbType.VarChar, 20).Value = (object?)grupo.CodigoGrupo ?? DBNull.Value;
            cmd.Parameters.Add("@Nombre_Grupo", SqlDbType.NVarChar, 100).Value = (object?)grupo.NombreGrupo ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Periodo", SqlDbType.Int).Value = (object?)grupo.IdPeriodo ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Grupo", SqlDbType.Int).Value = (object?)grupo.IdTipoGrupo ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Coordinador", SqlDbType.Int).Value = (object?)grupo.IdCoordinador ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Jornada", SqlDbType.Int).Value = (object?)grupo.IdJornada ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object?)grupo.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Cierre", SqlDbType.DateTime).Value = (object?)grupo.FechaCierre ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object?)grupo.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Codigo_Seguimiento", SqlDbType.VarChar, 30).Value = (object?)grupo.CodigoSeguimiento ?? DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = (object?)grupo.Activo ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private Grupo LlenarModelo(SqlDataReader reader)
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

            return new Grupo
            {
                IdGrupo = reader["Id_Grupo"] as int? ?? 0,
                CodigoGrupo = reader["Codigo_Grupo"] as string ?? string.Empty,
                NombreGrupo = reader["Nombre_Grupo"] as string ?? string.Empty,
                IdPeriodo = reader["Id_Periodo"] as int? ?? 0,
                IdTipoGrupo = reader["Id_Tipo_Grupo"] as int? ?? 0,
                IdCoordinador = reader["Id_Coordinador"] as int?,
                IdJornada = reader["Id_Jornada"] as int?,
                IdEstado = reader["Id_Estado"] as int? ?? 0,
                FechaCierre = reader["Fecha_Cierre"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Cierre"]),
                Observaciones = reader["Observaciones"] as string,
                CodigoSeguimiento = reader["Codigo_Seguimiento"] as string ?? string.Empty,
                Activo = reader["Activo"] as bool? ?? true,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI (pueden ser NULL si no vienen del SP)
                NombrePeriodo = LeerColumnaString("Nombre_Periodo"),
                CodigoPeriodo = LeerColumnaString("Codigo_Periodo"),
                NombreTipoGrupo = LeerColumnaString("Nombre_Tipo_Grupo"),
                CoordinadorUsuario = LeerColumnaString("Coordinador_Usuario"),
                CoordinadorNombre = LeerColumnaString("Coordinador_Nombre"),
                NombreJornada = LeerColumnaString("Nombre_Jornada"),
                NombreEstado = LeerColumnaString("Nombre_Estado")
            };
        }
    }
}
