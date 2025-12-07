using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class SeccionService : ISeccionService
    {
        private readonly ISeccionRepository _seccionRepository;

        public SeccionService(ISeccionRepository seccionRepository)
        {
            _seccionRepository = seccionRepository;
        }

        public async Task<List<Seccion>> ListarSeccionesAsync(int idSesion, int? idPeriodoAcademico = null)
        {
            var resultado = await _seccionRepository.ListarSeccionesAsync(idSesion, idPeriodoAcademico);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos ?? new List<Seccion>();
        }

        public async Task<Seccion?> ObtenerSeccionPorIdAsync(int idSeccion, int idSesion)
        {
            var resultado = await _seccionRepository.FiltrarSeccionPorIdAsync(idSeccion, idSesion);
            if (!resultado.Exitoso)
            {
                throw new Exception(resultado.Mensaje);
            }
            return resultado.Datos?.FirstOrDefault();
        }

        public async Task<ResultadoOperacion> AgregarSeccionAsync(Seccion seccion, int idSesion)
        {
            return await _seccionRepository.AgregarSeccionAsync(seccion, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarSeccionAsync(Seccion seccion, int idSesion)
        {
            return await _seccionRepository.ActualizarSeccionAsync(seccion, idSesion);
        }
    }
}

