using UmProject.Entities;

namespace UmProject.Web.Services
{
    public interface IPdfService
    {
        Task<byte[]> GenerarPdfUsuariosAsync(List<ReporteUsuario> usuarios, string titulo, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfPersonasAsync(List<ReportePersona> personas, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfMateriasAsync(List<ReporteMateria> materias, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfPeriodosAsync(List<ReportePeriodo> periodos, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfSeccionesAsync(List<ReporteSeccion> secciones, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfGruposAsync(List<ReporteGrupo> grupos, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfInscripcionesAsync(List<ReporteInscripcion> inscripciones, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfEvaluacionesAsync(List<ReporteEvaluacion> evaluaciones, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfBecasProgramasAsync(List<ReporteBecaPrograma> programas, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfBecasConvocatoriasAsync(List<ReporteBecaConvocatoria> convocatorias, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfBecasSolicitudesAsync(List<ReporteBecaSolicitud> solicitudes, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfSancionesAsync(List<ReporteSancion> sanciones, DateTime? fechaInicio, DateTime? fechaFin);
        Task<byte[]> GenerarPdfTransaccionesAsync(List<ReporteTransaccion> transacciones, DateTime? fechaInicio, DateTime? fechaFin);
    }
}

