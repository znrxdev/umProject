namespace UmProject.Entities
{
    public class PeriodoAcademico
    {
        public int? IdPeriodo { get; set; }
        public string? CodigoPeriodo { get; set; }
        public string? NombrePeriodo { get; set; }
        public int? IdTipoPeriodo { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public string? FechaCierreCalificaciones { get; set; }
        public bool? EsPeriodoActual { get; set; }
        public string? CodigoIntegracion { get; set; }
        public string? Observaciones { get; set; }
        public int? IdEstado { get; set; }
        public int? IdEstadoPublicacion { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        
        // Propiedades adicionales para la vista
        public string? NombreTipoPeriodo { get; set; }
        public string? NombreEstado { get; set; }
        public string? NombreEstadoPublicacion { get; set; }
    }
}

