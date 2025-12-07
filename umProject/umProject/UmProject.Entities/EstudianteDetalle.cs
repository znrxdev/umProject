namespace UmProject.Entities
{
    public class EstudianteDetalle : Estudiante
    {
        public int TotalInscripcionesActivas { get; set; }
        public int TotalGrupos { get; set; }
        public int TotalEvaluaciones { get; set; }
        public decimal? PromedioGeneral { get; set; }
        public int TotalSancionesActivas { get; set; }
        public string? PeriodoActual { get; set; }
    }
}

