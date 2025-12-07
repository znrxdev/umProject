namespace UmProject.Entities
{
    public class ReporteBecaSolicitud
    {
        public int? IdSolicitudBeca { get; set; }
        public string? CodigoSeguimiento { get; set; }
        public string? NombrePrograma { get; set; }
        public string? Estudiante { get; set; }
        public string? NombreEstudiante { get; set; }
        public decimal? PromedioVigente { get; set; }
        public int? TotalSancionesActivas { get; set; }
        public string? CumpleCriterios { get; set; }
        public string? Estado { get; set; }
        public string? FechaSolicitud { get; set; }
        public string? FechaUltimaDecision { get; set; }
        public string? FechaCierre { get; set; }
    }
}

