namespace UmProject.Entities
{
    public class DocenteDetalle : Docente
    {
        public int TotalSeccionesActivas { get; set; }
        public int TotalEvaluacionesRealizadas { get; set; }
        public int TotalEstudiantesActivos { get; set; }
        public string? PeriodoActual { get; set; }
    }
}

