using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IInscripcionRepository _inscripcionRepository;

        public InscripcionService(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
        }

        public async Task<List<Inscripcion>> ListarInscripcionesAsync(int idSesion)
        {
            var resultado = await _inscripcionRepository.ListarInscripcionesAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<Inscripcion>();
        }

        public async Task<Inscripcion?> ObtenerInscripcionPorIdAsync(int idInscripcion, int idSesion)
        {
            var resultado = await _inscripcionRepository.FiltrarInscripcionPorIdAsync(idInscripcion, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarInscripcionAsync(Inscripcion inscripcion, int idSesion)
        {
            return await _inscripcionRepository.AgregarInscripcionAsync(inscripcion, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarInscripcionAsync(Inscripcion inscripcion, int idSesion)
        {
            return await _inscripcionRepository.ActualizarInscripcionAsync(inscripcion, idSesion);
        }

        public async Task<List<Inscripcion>> ListarInscripcionesDisponiblesAsync(int idSesion)
        {
            var resultado = await _inscripcionRepository.ListarInscripcionesDisponiblesAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<Inscripcion>();
        }

        public async Task<List<GrupoInscripcion>> ListarInscripcionesGrupoAsync(int idGrupo, int idSesion)
        {
            var resultado = await _inscripcionRepository.ListarInscripcionesGrupoAsync(idGrupo, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<GrupoInscripcion>();
        }

        public async Task<ResultadoOperacion> AgregarInscripcionGrupoAsync(int idGrupo, int idInscripcion, string? observaciones, int idSesion)
        {
            return await _inscripcionRepository.AgregarInscripcionGrupoAsync(idGrupo, idInscripcion, observaciones, idSesion);
        }
    }
}

