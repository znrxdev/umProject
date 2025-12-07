namespace UmProject.Entities
{
    public class EstudianteSolicitudBeca
    {
        public int IdSolicitudBeca { get; set; }
        public string? CodigoSeguimiento { get; set; }
        public string? NombrePrograma { get; set; }
        public string? CodigoPrograma { get; set; }
        public decimal? PromedioVigente { get; set; }
        public int TotalSancionesActivas { get; set; }
        public bool CumpleCriterios { get; set; }
        public string? EstadoSolicitud { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaUltimaDecision { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string? MotivoUltimaDecision { get; set; }
        public string? Observaciones { get; set; }
    }
}

