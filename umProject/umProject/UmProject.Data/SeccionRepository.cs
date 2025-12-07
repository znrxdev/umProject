using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class SeccionRepository : ISeccionRepository
    {
        private readonly IConexionService _conexionService;

        public SeccionRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<ResultadoConsulta<List<Seccion>>> ListarSeccionesAsync(int idSesion, int? idPeriodoAcademico = null)
        {
            var resultado = new ResultadoConsulta<List<Seccion>>();
            var secciones = new List<Seccion>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_secciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 100; // Filtrar por materia período (NULL = listar todas)
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = DBNull.Value; // Null para listar todas
            cmd.Parameters.Add("@Id_Periodo_Academico", SqlDbType.Int).Value = idPeriodoAcademico.HasValue ? (object)idPeriodoAcademico.Value : DBNull.Value; // Filtro por período académico
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    secciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Seccion>>(cmd, secciones);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<Seccion>>> FiltrarSeccionPorIdAsync(int idSeccion, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<Seccion>>();
            var secciones = new List<Seccion>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_secciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 98; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = idSeccion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    secciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Seccion>>(cmd, secciones);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<Seccion>>> FiltrarSeccionPorDocenteAsync(int idDocente, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<Seccion>>();
            var secciones = new List<Seccion>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_secciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 99; // Filtrar por docente
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Docente", SqlDbType.Int).Value = idDocente;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    secciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Seccion>>(cmd, secciones);
            return resultado;
        }

        public async Task<ResultadoConsulta<List<Seccion>>> FiltrarSeccionPorMateriaPeriodoAsync(int idMateriaPeriodo, int idSesion)
        {
            var resultado = new ResultadoConsulta<List<Seccion>>();
            var secciones = new List<Seccion>();
            
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_secciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 100; // Filtrar por materia período
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = idMateriaPeriodo;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    secciones.Add(LlenarModelo(reader));
                }
            }
            
            resultado = RepositorioHelper.ObtenerResultadoConsulta<List<Seccion>>(cmd, secciones);
            return resultado;
        }

        public async Task<ResultadoOperacion> AgregarSeccionAsync(Seccion seccion, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_secciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 96; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            // Si el código viene vacío o null, la base de datos lo autogenerará
            cmd.Parameters.Add("@Codigo_Seccion", SqlDbType.VarChar, 20).Value = string.IsNullOrEmpty(seccion.CodigoSeccion) ? DBNull.Value : (object)seccion.CodigoSeccion;
            cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = seccion.IdMateriaPeriodo ?? 0;
            cmd.Parameters.Add("@Id_Docente", SqlDbType.Int).Value = seccion.IdDocente ?? 0;
            cmd.Parameters.Add("@Id_Tipo_Seccion", SqlDbType.Int).Value = seccion.IdTipoSeccion ?? 0;
            cmd.Parameters.Add("@Id_Aula", SqlDbType.Int).Value = (object?)seccion.IdAula ?? DBNull.Value;
            cmd.Parameters.Add("@Horario_Descripcion", SqlDbType.NVarChar, 255).Value = (object?)seccion.HorarioDescripcion ?? DBNull.Value;
            cmd.Parameters.Add("@Modalidad", SqlDbType.NVarChar, 50).Value = (object?)seccion.Modalidad ?? DBNull.Value;
            cmd.Parameters.Add("@Cupo_Maximo", SqlDbType.Int).Value = (object?)seccion.CupoMaximo ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Asistencia", SqlDbType.Bit).Value = seccion.RequiereAsistencia;
            cmd.Parameters.Add("@Porcentaje_Asistencia_Minima", SqlDbType.Decimal).Value = (object?)seccion.PorcentajeAsistenciaMinima ?? DBNull.Value;
            // El estado se fuerza a EN REVISION (4) en el stored procedure, pero pasamos el valor si existe
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = seccion.IdEstado ?? 4;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object?)seccion.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime).Value = (object?)seccion.FechaPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Cierre", SqlDbType.DateTime).Value = (object?)seccion.FechaCierre ?? DBNull.Value;
            cmd.Parameters.Add("@Codigo_Firma", SqlDbType.NVarChar, 100).Value = (object?)seccion.CodigoFirma ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Publicador", SqlDbType.Int).Value = (object?)seccion.IdUsuarioPublicador ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object?)seccion.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = seccion.Activo ?? true;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarSeccionAsync(Seccion seccion, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_secciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 97; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Seccion", SqlDbType.Int).Value = seccion.IdSeccion ?? 0;
            cmd.Parameters.Add("@Codigo_Seccion", SqlDbType.VarChar, 20).Value = (object?)seccion.CodigoSeccion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Materia_Periodo", SqlDbType.Int).Value = (object?)seccion.IdMateriaPeriodo ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Docente", SqlDbType.Int).Value = (object?)seccion.IdDocente ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Seccion", SqlDbType.Int).Value = (object?)seccion.IdTipoSeccion ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Aula", SqlDbType.Int).Value = (object?)seccion.IdAula ?? DBNull.Value;
            cmd.Parameters.Add("@Horario_Descripcion", SqlDbType.NVarChar, 255).Value = (object?)seccion.HorarioDescripcion ?? DBNull.Value;
            cmd.Parameters.Add("@Modalidad", SqlDbType.NVarChar, 50).Value = (object?)seccion.Modalidad ?? DBNull.Value;
            cmd.Parameters.Add("@Cupo_Maximo", SqlDbType.Int).Value = (object?)seccion.CupoMaximo ?? DBNull.Value;
            cmd.Parameters.Add("@Requiere_Asistencia", SqlDbType.Bit).Value = (object?)seccion.RequiereAsistencia ?? DBNull.Value;
            cmd.Parameters.Add("@Porcentaje_Asistencia_Minima", SqlDbType.Decimal).Value = (object?)seccion.PorcentajeAsistenciaMinima ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = (object?)seccion.IdEstado ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Estado_Publicacion", SqlDbType.Int).Value = (object?)seccion.IdEstadoPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Publicacion", SqlDbType.DateTime).Value = (object?)seccion.FechaPublicacion ?? DBNull.Value;
            cmd.Parameters.Add("@Fecha_Cierre", SqlDbType.DateTime).Value = (object?)seccion.FechaCierre ?? DBNull.Value;
            cmd.Parameters.Add("@Codigo_Firma", SqlDbType.NVarChar, 100).Value = (object?)seccion.CodigoFirma ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Usuario_Publicador", SqlDbType.Int).Value = (object?)seccion.IdUsuarioPublicador ?? DBNull.Value;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar, 255).Value = (object?)seccion.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = (object?)seccion.Activo ?? DBNull.Value;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private Seccion LlenarModelo(SqlDataReader reader)
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

            return new Seccion
            {
                IdSeccion = reader["Id_Seccion"] as int? ?? 0,
                CodigoSeccion = reader["Codigo_Seccion"] as string ?? string.Empty,
                IdMateriaPeriodo = reader["Id_Materia_Periodo"] as int? ?? 0,
                IdDocente = reader["Id_Docente"] as int? ?? 0,
                IdTipoSeccion = reader["Id_Tipo_Seccion"] as int? ?? 0,
                IdAula = reader["Id_Aula"] as int?,
                HorarioDescripcion = reader["Horario_Descripcion"] as string,
                Modalidad = reader["Modalidad"] as string,
                CupoMaximo = reader["Cupo_Maximo"] as int?,
                RequiereAsistencia = reader["Requiere_Asistencia"] as bool? ?? true,
                PorcentajeAsistenciaMinima = reader["Porcentaje_Asistencia_Minima"] as decimal?,
                IdEstado = reader["Id_Estado"] as int? ?? 0,
                IdEstadoPublicacion = reader["Id_Estado_Publicacion"] as int?,
                FechaPublicacion = reader["Fecha_Publicacion"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Publicacion"]),
                FechaCierre = reader["Fecha_Cierre"] is DBNull ? null : (DateTime?)Convert.ToDateTime(reader["Fecha_Cierre"]),
                CodigoFirma = reader["Codigo_Firma"] as string,
                IdUsuarioPublicador = reader["Id_Usuario_Publicador"] as int?,
                Observaciones = reader["Observaciones"] as string,
                Activo = reader["Activo"] as bool? ?? true,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                // Campos adicionales para mostrar en UI (pueden ser NULL si no vienen del SP)
                NombreMateria = LeerColumnaString("Nombre_Materia"),
                CodigoMateria = LeerColumnaString("Codigo_Materia"),
                NombrePeriodo = LeerColumnaString("Nombre_Periodo"),
                CodigoPeriodo = LeerColumnaString("Codigo_Periodo"),
                DocenteUsuario = LeerColumnaString("Docente_Usuario"),
                DocenteNombre = LeerColumnaString("Docente_Nombre"),
                TipoSeccionNombre = LeerColumnaString("Tipo_Seccion_Nombre"),
                AulaNombre = LeerColumnaString("Aula_Nombre"),
                EstadoNombre = LeerColumnaString("Estado_Nombre")
            };
        }
    }
}

