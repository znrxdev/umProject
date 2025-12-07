namespace UmProject.Entities
{
    public class ReporteBecaConvocatoria
    {
        public int? IdConvocatoria { get; set; }
        public string? CodigoConvocatoria { get; set; }
        public string? NombreConvocatoria { get; set; }
        public string? NombrePrograma { get; set; }
        public string? NombrePeriodo { get; set; }
        public int? CupoTotal { get; set; }
        public int? CupoReservado { get; set; }
        public int? CupoAsignado { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaPublicacion { get; set; }
        public string? FechaFin { get; set; }
        public string? Estado { get; set; }
        public string? EstadoPublicacion { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
    }
}

