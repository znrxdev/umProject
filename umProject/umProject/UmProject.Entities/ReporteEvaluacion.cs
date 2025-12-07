namespace UmProject.Entities
{
    public class ReporteEvaluacion
    {
        public int? IdEvaluacionAlumno { get; set; }
        public string? CodigoRegistro { get; set; }
        public string? CodigoInstancia { get; set; }
        public string? NombreEvaluacion { get; set; }
        public string? NombreMateria { get; set; }
        public string? Estudiante { get; set; }
        public string? NombreEstudiante { get; set; }
        public decimal? PuntajeObtenido { get; set; }
        public decimal? PorcentajeLogrado { get; set; }
        public string? Estado { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaValidacion { get; set; }
        public string? FechaPublicacion { get; set; }
    }
}

