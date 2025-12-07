namespace UmProject.Entities
{
    public class EstudiantePeriodo
    {
        public int IdPeriodo { get; set; }
        public string? CodigoPeriodo { get; set; }
        public string? NombrePeriodo { get; set; }
        public string? TipoPeriodo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool EsPeriodoActual { get; set; }
        public string? EstadoPeriodo { get; set; }
        public int TotalInscripciones { get; set; }
    }
}

