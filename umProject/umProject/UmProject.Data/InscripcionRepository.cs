using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class InscripcionRepository : IInscripcionRepository
    {
        private readonly IConexionService _conexionService;

        public InscripcionRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<Inscripcion>>> ListarInscripcionesAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<Inscripcion>>();
            var inscripciones = new List<Inscripcion>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_inscripciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 115; // Listar todas
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    inscripciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Inscripcion>>(cmd, inscripciones);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<Inscripcion>>> FiltrarInscripcionPorIdAsync(int idInscripcion, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<Inscripcion>>();
            var inscripciones = new List<Inscripcion>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_inscripciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 112; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Inscripcion", SqlDbType.Int).Value = idInscripcion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    inscripciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Inscripcion>>(cmd, inscripciones);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarInscripcionAsync(Inscripcion inscripcion, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_inscripciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 110; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            // El código se autogenera en el SP, siempre pasar NULL o vacío
            cmd.Parameters.Add("@Codigo_Inscripcion", SqlDbType.VarChar, 30).Value = DBNull.Value;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = inscripcion.IdEstudiante ?? 0;
            cmd.Parameters.Add("@Id_Tipo_Inscripcion", SqlDbType.Int).Value = (object?)inscripcion.IdTipoInscripcion ?? DBNull.Value;
            // Id_Estado se establece automáticamente a EN REVISION (4) en el SP
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = DBNull.Value;
            cmd.Parameters.Add("@Fecha_Validacion", SqlDbType.DateTime).Value = (object?)inscripcion.FechaValidacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Retiro", SqlDbType.DateTime).Value = (object?)inscripcion.FechaRetiro ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Retiro", SqlDbType.NVarChar, 500).Value = (object?)inscripcion.MotivoRetiro ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Validador", SqlDbType.Int).Value = (object?)inscripcion.IdUsuarioValidador ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarInscripcionAsync(Inscripcion inscripcion, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_inscripciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 111; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Inscripcion", SqlDbType.Int).Value = inscripcion.IdInscripcion ?? 0;
            cmd.Parameters.Add("@Codigo_Inscripcion", SqlDbType.VarChar, 30).Value = (object?)inscripcion.CodigoInscripcion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estudiante", SqlDbType.Int).Value = (object?)inscripcion.IdEstudiante ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Inscripcion", SqlDbType.Int).Value = (object?)inscripcion.IdTipoInscripcion ?? DBNull.Value;
            // Solo se permiten estados ACTIVO (1) o INACTIVO (2) para actualizar
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object?)inscripcion.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Validacion", SqlDbType.DateTime).Value = (object?)inscripcion.FechaValidacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Retiro", SqlDbType.DateTime).Value = (object?)inscripcion.FechaRetiro ?? DBNull.Value;
            cmd.Parameters.Add("@Motivo_Retiro", SqlDbType.NVarChar, 500).Value = (object?)inscripcion.MotivoRetiro ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Validador", SqlDbType.Int).Value = (object?)inscripcion.IdUsuarioValidador ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private Inscripcion LlenarModelo(SqlDataReader reader)
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

            return new Inscripcion
            {
                IdInscripcion = reader["Id_Inscripcion"] as int? ?? 0,
                CodigoInscripcion = reader["Codigo_Inscripcion"] as string ?? string.Empty,
                IdEstudiante = reader["Id_Estudiante"] as int? ?? 0,
                IdTipoInscripcion = reader["Id_Tipo_Inscripcion"] as int?,
                IdEstado = reader["Id_Estado"] as int? ?? 0,
                FechaValidacion = reader["Fecha_Validacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Validacion"]),
                FechaRetiro = reader["Fecha_Retiro"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Retiro"]),
                MotivoRetiro = reader["Motivo_Retiro"] as string,
                IdUsuarioValidador = reader["Id_Usuario_Validador"] as int?,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI (pueden ser NULL si no vienen del SP)
                EstudianteUsuario = LeerColumnaString("Estudiante_Usuario"),
                EstudianteNombre = LeerColumnaString("Estudiante_Nombre"),
                TipoInscripcionNombre = LeerColumnaString("Tipo_Inscripcion_Nombre"),
                EstadoNombre = LeerColumnaString("Estado_Nombre"),
                ValidadorUsuario = LeerColumnaString("Validador_Usuario")
            };
        }

        public async Task<ResultadoConsulta<List<Inscripcion>>> ListarInscripcionesDisponiblesAsync(int idSesion)
        {
            var resultado = new ResultadoConsulta<List<Inscripcion>>();
            var inscripciones = new List<Inscripcion>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_inscripciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 116; // Listar inscripciones disponibles para grupos
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var nombreCompleto = reader["Estudiante_Nombre_Completo"] as string ?? "";
                    var documento = reader["Estudiante_Documento"] as string ?? "";
                    var usuario = reader["Estudiante_Usuario"] as string ?? "";
                    
                    var inscripcion = new Inscripcion
                    {
                        IdInscripcion = reader["Id_Inscripcion"] as int?,
                        CodigoInscripcion = reader["Codigo_Inscripcion"] as string,
                        IdEstudiante = reader["Id_Estudiante"] as int?,
                        EstudianteUsuario = usuario,
                        EstudianteNombre = !string.IsNullOrEmpty(nombreCompleto) && !string.IsNullOrEmpty(documento) 
                            ? nombreCompleto + " - " + documento 
                            : nombreCompleto,
                        TipoInscripcionNombre = reader["Tipo_Inscripcion_Nombre"] as string,
                        EstadoNombre = reader["Estado_Nombre"] as string
                    };
                    inscripciones.Add(inscripcion);
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Inscripcion>>(cmd, inscripciones);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<GrupoInscripcion>>> ListarInscripcionesGrupoAsync(int idGrupo, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<GrupoInscripcion>>();
            var grupoInscripciones = new List<GrupoInscripcion>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_grupos_inscripciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 118; // Filtrar por ID Grupo
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = idGrupo;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    grupoInscripciones.Add(new GrupoInscripcion
                    {
                        IdGrupoInscripcion = reader["Id_Grupo_Inscripcion"] as int? ?? 0,
                        IdGrupo = reader["Id_Grupo"] as int? ?? 0,
                        IdInscripcion = reader["Id_Inscripcion"] as int? ?? 0,
                        IdRolGrupo = reader["Id_Rol_Grupo"] as int?,
                        IdEstado = reader["Id_Estado"] as int? ?? 0,
                        FechaAsignacion = reader["Fecha_Asignacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Asignacion"]),
                        FechaBaja = reader["Fecha_Baja"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Baja"]),
                        MotivoBaja = reader["Motivo_Baja"] as string,
                        EsDelegado = reader["Es_Delegado"] as bool? ?? false,
                        Observaciones = reader["Observaciones"] as string,
                        Activo = reader["Activo"] as bool? ?? false,
                        CodigoInscripcion = reader["Codigo_Inscripcion"] as string,
                        EstudianteUsuario = reader["Estudiante_Usuario"] as string,
                        EstudianteNombreCompleto = reader["Estudiante_Nombre_Completo"] as string,
                        EstudianteDocumento = reader["Estudiante_Documento"] as string,
                        EstadoNombre = reader["Estado_Nombre"] as string
                    });
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<GrupoInscripcion>>(cmd, grupoInscripciones);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarInscripcionGrupoAsync(int idGrupo, int idInscripcion, string? observaciones, int idSesion)
        {
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_grupos_inscripciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 115; // AGREGAR GRUPO INSCRIPCION
            cmd.Parameters.Add("@Id_Grupo", SqlDbType.Int).Value = idGrupo;
            cmd.Parameters.Add("@Id_Inscripcion", SqlDbType.Int).Value = idInscripcion;
            cmd.Parameters.Add("@Id_Rol_Grupo", SqlDbType.Int).Value = DBNull.Value; // Siempre NULL
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = DBNull.Value; // Se establece en el SP (ACTIVO = 1)
            cmd.Parameters.Add("@Fecha_Asignacion", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Fecha_Baja", SqlDbType.DateTime).Value = DBNull.Value;
            cmd.Parameters.Add("@Motivo_Baja", SqlDbType.NVarChar, 255).Value = DBNull.Value;
            cmd.Parameters.Add("@Es_Delegado", SqlDbType.Bit).Value = false; // Siempre false
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = string.IsNullOrEmpty(observaciones) ? DBNull.Value : observaciones;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return RepositorioHelper.ObtenerResultado(cmd);
        }
    }
}

