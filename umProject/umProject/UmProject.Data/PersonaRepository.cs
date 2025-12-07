using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly IConexionService _conexionService;

        public PersonaRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Persona>> ListarPersonasAsync(int idSesion)
        {
            var personas = new List<Persona>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_personas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 16; // Listar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    personas.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return personas;
        }

        public async Task<List<Persona>> FiltrarPersonaPorIdAsync(int idPersona, int idSesion)
        {
            var personas = new List<Persona>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_personas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 17; // Filtrar por ID
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = idPersona;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    personas.Add(LlenarModelo(reader));
                }
            }
            
            // Verificar resultado y lanzar excepción si hay error
            RepositorioHelper.VerificarResultado(cmd, out _, out _);

            return personas;
        }

        public async Task<List<Persona>> FiltrarPersonaPorDocumentoAsync(string valorDocumento, int idSesion)
        {
            var personas = new List<Persona>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_personas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 18; // Filtrar por documento
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = valorDocumento;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    personas.Add(LlenarModelo(reader));
                }
            }
            
            // Obtener resultado sin lanzar excepción automáticamente
            int oNum = cmd.Parameters["@o_Num"].Value != DBNull.Value 
                ? Convert.ToInt32(cmd.Parameters["@o_Num"].Value) 
                : 0;
            string oMsg = cmd.Parameters["@o_Msg"].Value?.ToString() ?? string.Empty;
            
            // Si la persona no existe, es un caso válido de negocio - devolver lista vacía sin lanzar excepción
            // El mensaje puede variar: "No existe una persona", "no existe una persona", "Persona no existe", etc.
            if (oNum == -1 && !string.IsNullOrEmpty(oMsg) && 
                (oMsg.Contains("No existe una persona", StringComparison.OrdinalIgnoreCase) || 
                 oMsg.Contains("no existe una persona", StringComparison.OrdinalIgnoreCase) ||
                 oMsg.Contains("Persona no existe", StringComparison.OrdinalIgnoreCase) ||
                 oMsg.Contains("documento en el sistema", StringComparison.OrdinalIgnoreCase)))
            {
                // Caso válido: persona no existe - devolver lista vacía para que el controlador maneje el flujo de creación
                return personas; // Ya está vacía porque no hubo resultados del SELECT
            }
            
            // Para otros errores, lanzar excepción como siempre
            if (oNum == -1)
            {
                throw new Exception(oMsg);
            }

            return personas;
        }

        public async Task<ResultadoOperacion> AgregarPersonaAsync(Persona persona, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_personas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 15; // Agregar
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Primer_Nombre", SqlDbType.NVarChar, 100).Value = persona.PrimerNombre;
            cmd.Parameters.Add("@Segundo_Nombre", SqlDbType.NVarChar, 100).Value = (object?)persona.SegundoNombre ?? DBNull.Value;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.NVarChar, 100).Value = persona.PrimerApellido;
            cmd.Parameters.Add("@Segundo_Apellido", SqlDbType.NVarChar, 100).Value = (object?)persona.SegundoApellido ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Documento", SqlDbType.Int).Value = persona.IdTipoDocumento;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = persona.ValorDocumento;
            cmd.Parameters.Add("@Id_Genero_Persona", SqlDbType.Int).Value = persona.IdGeneroPersona;
            cmd.Parameters.Add("@Id_Nacionalidad", SqlDbType.Int).Value = persona.IdNacionalidad;
            cmd.Parameters.Add("@Id_Estado_Civil", SqlDbType.Int).Value = persona.IdEstadoCivil;
            cmd.Parameters.Add("@Fecha_Nacimiento", SqlDbType.Date).Value = DateTime.Parse(persona.FechaNacimiento ?? DateTime.Now.ToString());
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = persona.IdEstado ?? 1;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        public async Task<ResultadoOperacion> ActualizarPersonaAsync(Persona persona, int idSesion)
        {
            var resultado = new ResultadoOperacion();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_personas", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Id_Tipo_Transaccion", SqlDbType.Int).Value = 16; // Actualizar (ajustar según SP)
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            cmd.Parameters.Add("@Id_Persona", SqlDbType.Int).Value = persona.IdPersona;
            cmd.Parameters.Add("@Primer_Nombre", SqlDbType.NVarChar, 100).Value = persona.PrimerNombre;
            cmd.Parameters.Add("@Segundo_Nombre", SqlDbType.NVarChar, 100).Value = (object?)persona.SegundoNombre ?? DBNull.Value;
            cmd.Parameters.Add("@Primer_Apellido", SqlDbType.NVarChar, 100).Value = persona.PrimerApellido;
            cmd.Parameters.Add("@Segundo_Apellido", SqlDbType.NVarChar, 100).Value = (object?)persona.SegundoApellido ?? DBNull.Value;
            cmd.Parameters.Add("@Id_Tipo_Documento", SqlDbType.Int).Value = persona.IdTipoDocumento;
            cmd.Parameters.Add("@Valor_Documento", SqlDbType.NVarChar, 100).Value = persona.ValorDocumento;
            cmd.Parameters.Add("@Id_Genero_Persona", SqlDbType.Int).Value = persona.IdGeneroPersona;
            cmd.Parameters.Add("@Id_Nacionalidad", SqlDbType.Int).Value = persona.IdNacionalidad;
            cmd.Parameters.Add("@Id_Estado_Civil", SqlDbType.Int).Value = persona.IdEstadoCivil;
            cmd.Parameters.Add("@Fecha_Nacimiento", SqlDbType.Date).Value = DateTime.Parse(persona.FechaNacimiento ?? DateTime.Now.ToString());
            cmd.Parameters.Add("@Id_Estado", SqlDbType.Int).Value = persona.IdEstado ?? 1;
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            resultado = RepositorioHelper.ObtenerResultado(cmd);
            return resultado;
        }

        private Persona LlenarModelo(SqlDataReader reader)
        {
            return new Persona
            {
                IdPersona = reader["Id_Persona"] as int? ?? 0,
                PrimerNombre = reader["Primer_Nombre"] as string ?? string.Empty,
                SegundoNombre = reader["Segundo_Nombre"] as string,
                PrimerApellido = reader["Primer_Apellido"] as string ?? string.Empty,
                SegundoApellido = reader["Segundo_Apellido"] as string,
                IdTipoDocumento = reader["Id_Tipo_Documento"] as int? ?? 0,
                ValorDocumento = reader["Valor_Documento"] as string ?? string.Empty,
                IdGeneroPersona = reader["Id_Genero_Persona"] as int? ?? 0,
                IdNacionalidad = reader["Id_Nacionalidad"] as int? ?? 0,
                IdEstadoCivil = reader["Id_Estado_Civil"] as int? ?? 0,
                FechaNacimiento = reader["Fecha_Nacimiento"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Nacimiento"]).ToString("yyyy-MM-dd"),
                FechaCreacion = reader["Fecha_Creacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Creacion"]).ToString("dd/MM/yyyy"),
                FechaModificacion = reader["Fecha_Modificacion"] is DBNull ? null : Convert.ToDateTime(reader["Fecha_Modificacion"]).ToString("dd/MM/yyyy"),
                IdCreador = reader["Id_Creador"] as int? ?? 0,
                IdModificador = reader["Id_Modificador"] as int? ?? 0,
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                IdEstado = reader["Id_Estado"] as int? ?? 0
            };
        }
    }
}

