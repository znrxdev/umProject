using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class SolicitudBecaService : ISolicitudBecaService
    {
        private readonly ISolicitudBecaRepository _solicitudBecaRepository;

        public SolicitudBecaService(ISolicitudBecaRepository solicitudBecaRepository)
        {
            _solicitudBecaRepository = solicitudBecaRepository;
        }

        public async Task<List<SolicitudBeca>> ListarSolicitudesBecaAsync(int idSesion)
        {
            var resultado = await _solicitudBecaRepository.ListarSolicitudesBecaAsync(idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<SolicitudBeca>();
        }

        public async Task<SolicitudBeca?> ObtenerSolicitudBecaPorIdAsync(int idSolicitudBeca, int idSesion)
        {
            var resultado = await _solicitudBecaRepository.FiltrarSolicitudBecaPorIdAsync(idSolicitudBeca, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarSolicitudBecaAsync(SolicitudBeca solicitudBeca, int idSesion)
        {
            return await _solicitudBecaRepository.AgregarSolicitudBecaAsync(solicitudBeca, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarSolicitudBecaAsync(SolicitudBeca solicitudBeca, int idSesion)
        {
            return await _solicitudBecaRepository.ActualizarSolicitudBecaAsync(solicitudBeca, idSesion);
        }
    }
}

