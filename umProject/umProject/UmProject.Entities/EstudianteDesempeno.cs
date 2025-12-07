namespace UmProject.Entities
{
    public class EstudianteDesempeno
    {
        public int IdPeriodo { get; set; }
        public string? NombrePeriodo { get; set; }
        public string? CodigoPeriodo { get; set; }
        public int TotalMaterias { get; set; }
        public int TotalEvaluaciones { get; set; }
        public decimal? PromedioGeneral { get; set; }
        public int MateriasAprobadas { get; set; }
        public int MateriasReprobadas { get; set; }
    }
}

