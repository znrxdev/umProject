using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class SancionAcademicaService : ISancionAcademicaService
    {
        private readonly ISancionAcademicaRepository _sancionAcademicaRepository;

        public SancionAcademicaService(ISancionAcademicaRepository sancionAcademicaRepository)
        {
            _sancionAcademicaRepository = sancionAcademicaRepository;
        }

        public async Task<List<SancionAcademica>> ListarSancionesAcademicasAsync(int idSesion)
        {
            var resultado = await _sancionAcademicaRepository.ListarSancionesAcademicasAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<SancionAcademica>();
        }

        public async Task<SancionAcademica?> ObtenerSancionAcademicaPorIdAsync(int idSancion, int idSesion)
        {
            var resultado = await _sancionAcademicaRepository.FiltrarSancionAcademicaPorIdAsync(idSancion, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarSancionAcademicaAsync(SancionAcademica sancionAcademica, int idSesion)
        {
            return await _sancionAcademicaRepository.AgregarSancionAcademicaAsync(sancionAcademica, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarSancionAcademicaAsync(SancionAcademica sancionAcademica, int idSesion)
        {
            return await _sancionAcademicaRepository.ActualizarSancionAcademicaAsync(sancionAcademica, idSesion);
        }

        public async Task<List<SancionAcademica>> ObtenerMisSancionesAcademicasAsync(int idSesion)
        {
            var resultado = await _sancionAcademicaRepository.ObtenerMisSancionesAcademicasAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<SancionAcademica>();
        }

        public async Task<ResultadoOperacion> ApelarSancionAcademicaAsync(int idSancion, string observacionesApelacion, int idSesion)
        {
            return await _sancionAcademicaRepository.ApelarSancionAcademicaAsync(idSancion, observacionesApelacion, idSesion);
        }
    }
}

