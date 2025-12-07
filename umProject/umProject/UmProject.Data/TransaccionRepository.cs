using Microsoft.Data.SqlClient;
using System.Data;
using UmProject.Entities;
using System;

namespace UmProject.Data
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly IConexionService _conexionService;

        public TransaccionRepository(IConexionService conexionService)
        {
            _conexionService = conexionService;
        }

        public async Task<List<Transaccion>> ListarAuditoriaAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var transacciones = new List<Transaccion>();
            using var conexion = _conexionService.ObtenerConexion();
            using var cmd = new SqlCommand("usp_transacciones", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Tipo_Transaccion", SqlDbType.Int).Value = 144;
            cmd.Parameters.Add("@Id_Sesion", SqlDbType.Int).Value = idSesion;
            
            if (fechaInicio.HasValue)
            {
                cmd.Parameters.Add("@Fecha_Inicio", SqlDbType.DateTime).Value = fechaInicio.Value;
            }
            
            if (fechaFin.HasValue)
            {
                cmd.Parameters.Add("@Fecha_Fin", SqlDbType.DateTime).Value = fechaFin.Value;
            }
            
            cmd.Parameters.Add("@o_Num", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@o_Msg", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            await conexion.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                transacciones.Add(LlenarModelo(reader));
            }

            return transacciones;
        }

        private Transaccion LlenarModelo(SqlDataReader reader)
        {
            return new Transaccion
            {
                IdTransaccion = reader["Id_Transaccion"] as int? ?? 0,
                IdTipoTransaccion = reader["Id_Tipo_Transaccion"] as int?,
                NombreTipoTransaccion = reader["Nombre_Tipo_Transaccion"] as string,
                Concepto = reader["Concepto"] as string,
                IdPersona = reader["Id_Persona"] as int?,
                NombrePersona = reader["Nombre_Persona"] as string,
                IdUsuario = reader["Id_Usuario"] as int?,
                NombreUsuario = reader["Nombre_Usuario"] as string,
                IdContacto = reader["Id_Contacto"] as int?,
                IdEvaluacion = reader["Id_Evaluacion"] as int?,
                IdSolicitudBeca = reader["Id_Solicitud_Beca"] as int?,
                IdInscripcion = reader["Id_Inscripcion"] as int?,
                IdAutor = reader["Id_Autor"] as int?,
                NombreAutor = reader["Nombre_Autor"] as string,
                FechaCreacion = reader["Fecha_Creacion"] as string,
                Completado = reader["Completado"] as bool?,
                TipoEntidad = reader["Tipo_Entidad"] as string
            };
        }
    }
}

