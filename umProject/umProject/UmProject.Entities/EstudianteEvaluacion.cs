namespace UmProject.Entities
{
    public class EstudianteEvaluacion
    {
        public int IdEvaluacionAlumno { get; set; }
        public string? CodigoRegistro { get; set; }
        public int? IdEvaluacionInstancia { get; set; }
        public string? CodigoInstancia { get; set; }
        public string? NombreEvaluacion { get; set; }
        public string? CodigoModelo { get; set; }
        public string? TipoEvaluacion { get; set; }
        public string? NombreMateria { get; set; }
        public string? CodigoMateria { get; set; }
        public string? CodigoSeccion { get; set; }
        public string? NombrePeriodo { get; set; }
        public string? CodigoPeriodo { get; set; }
        public decimal PuntajeObtenido { get; set; }
        public decimal? PorcentajeLogrado { get; set; }
        public decimal CalificacionMaxima { get; set; }
        public string? EstadoEvaluacion { get; set; }
        public bool EsRecalculo { get; set; }
        public int NumeroRecalculo { get; set; }
        public DateTime? FechaEvaluacion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public string? UsuarioEvaluador { get; set; }
        public string? UsuarioValidador { get; set; }
    }
}

