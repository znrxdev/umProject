using UmProject.Data;
using UmProject.Entities;

namespace UmProject.Business
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<List<ReporteUsuario>> GenerarReporteUsuariosAsync(int idSesion, int tipoReporte, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteUsuariosAsync(idSesion, tipoReporte, fechaInicio, fechaFin);
        }

        public async Task<List<ReportePersona>> GenerarReportePersonasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReportePersonasAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteMateria>> GenerarReporteMateriasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteMateriasAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReportePeriodo>> GenerarReportePeriodosAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReportePeriodosAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteSeccion>> GenerarReporteSeccionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteSeccionesAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteGrupo>> GenerarReporteGruposAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteGruposAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteInscripcion>> GenerarReporteInscripcionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteInscripcionesAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteEvaluacion>> GenerarReporteEvaluacionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteEvaluacionesAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteBecaPrograma>> GenerarReporteBecasProgramasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteBecasProgramasAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteBecaConvocatoria>> GenerarReporteBecasConvocatoriasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteBecasConvocatoriasAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteBecaSolicitud>> GenerarReporteBecasSolicitudesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteBecasSolicitudesAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteSancion>> GenerarReporteSancionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteSancionesAsync(idSesion, fechaInicio, fechaFin);
        }

        public async Task<List<ReporteTransaccion>> GenerarReporteTransaccionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            return await _reporteRepository.GenerarReporteTransaccionesAsync(idSesion, fechaInicio, fechaFin);
        }
    }
}

