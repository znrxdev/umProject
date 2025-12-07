using UmProject.Entities;
using System.Data;

namespace UmProject.Data
{
    public interface IReporteRepository
    {
        Task<List<ReporteUsuario>> GenerarReporteUsuariosAsync(int idSesion, int tipoReporte, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReportePersona>> GenerarReportePersonasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteMateria>> GenerarReporteMateriasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReportePeriodo>> GenerarReportePeriodosAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteSeccion>> GenerarReporteSeccionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteGrupo>> GenerarReporteGruposAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteInscripcion>> GenerarReporteInscripcionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteEvaluacion>> GenerarReporteEvaluacionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteBecaPrograma>> GenerarReporteBecasProgramasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteBecaConvocatoria>> GenerarReporteBecasConvocatoriasAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteBecaSolicitud>> GenerarReporteBecasSolicitudesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteSancion>> GenerarReporteSancionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<List<ReporteTransaccion>> GenerarReporteTransaccionesAsync(int idSesion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
    }
}

