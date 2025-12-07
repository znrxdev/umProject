using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly IConexionService _conexionService;

        public MateriaRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Materia>> ListarMateriasAsync(int idSesion)
        {
            var materias = new List<Materia>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_materias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 77; // Filtrar por nombre (sin nombre = listar todas)
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Nombre_Materia", SqlDbType.NVarChar, 100).Value = DBNull.Value; // Null para listar todas
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    materias.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return materias;
        }

        public async Task<List<Materia>> FiltrarMateriaPorIdAsync(int idMateria, int idSesion)
        {
            var materias = new List<Materia>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_materias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 75; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Materia", SqlDbType.Int).Value = idMateria;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    materias.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return materias;
        }

        public async Task<List<Materia>> FiltrarMateriaPorCodigoAsync(string codigoMateria, int idSesion)
        {
            var materias = new List<Materia>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_materias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 76; // Filtrar por código
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Materia", SqlDbType.VarChar, 10).Value = codigoMateria;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    materias.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return materias;
        }

        public async Task<List<Materia>> FiltrarMateriaPorNombreAsync(string nombreMateria, int idSesion)
        {
            var materias = new List<Materia>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_materias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 77; // Filtrar por nombre
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Nombre_Materia", SqlDbType.NVarChar, 100).Value = nombreMateria;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    materias.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return materias;
        }

        public async Task<ResultadoOperacion> AgregarMateriaAsync(Materia materia, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_materias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 73; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Codigo_Materia", SqlDbType.VarChar, 10).Value = materia.CodigoMateria;
            cmd.Parameters.Add("@Nombre_Materia", SqlDbType.NVarChar, 100).Value = materia.NombreMateria;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = materia.Activo ?? true;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarMateriaAsync(Materia materia, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_materias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 74; // Actualizar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Materia", SqlDbType.Int).Value = materia.IdMateria;
            cmd.Parameters.Add("@Codigo_Materia", SqlDbType.VarChar, 10).Value = materia.CodigoMateria;
            cmd.Parameters.Add("@Nombre_Materia", SqlDbType.NVarChar, 100).Value = materia.NombreMateria;
            cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = materia.Activo ?? true;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private Materia LlenarModelo(SqlDataReader reader)
        {
            return new Materia
            {
                IdMateria = reader["Id_Materia"] as int? ?? 0,
                CodigoMateria = reader["Codigo_Materia"] as string ?? string.Empty,
                NombreMateria = reader["Nombre_Materia"] as string ?? string.Empty,
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                Activo = reader["Activo"] as bool? ?? false
            };
        }
    }
}

