using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }

        public async Task<List<Materia>> ListarMateriasAsync(int idSesion)
        {
            return await _materiaRepository.ListarMateriasAsync(idSesion);
        }

        public async Task<List<Materia>> FiltrarMateriaPorIdAsync(int idMateria, int idSesion)
        {
            return await _materiaRepository.FiltrarMateriaPorIdAsync(idMateria, idSesion);
        }

        public async Task<List<Materia>> FiltrarMateriaPorCodigoAsync(string codigoMateria, int idSesion)
        {
            return await _materiaRepository.FiltrarMateriaPorCodigoAsync(codigoMateria, idSesion);
        }

        public async Task<List<Materia>> FiltrarMateriaPorNombreAsync(string nombreMateria, int idSesion)
        {
            return await _materiaRepository.FiltrarMateriaPorNombreAsync(nombreMateria, idSesion);
        }

        public async Task<ResultadoOperacion> AgregarMateriaAsync(Materia materia, int idSesion)
        {
            return await _materiaRepository.AgregarMateriaAsync(materia, idSesion);
        }

        public async Task<ResultadoOperacion> ActualizarMateriaAsync(Materia materia, int idSesion)
        {
            return await _materiaRepository.ActualizarMateriaAsync(materia, idSesion);
        }
    }
}

