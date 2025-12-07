namespace UmProject.Entities
{
    public class EstudianteInscripcion
    {
        public int IdInscripcion { get; set; }
        public string? CodigoInscripcion { get; set; }
        public string? TipoInscripcion { get; set; }
        public string? EstadoInscripcion { get; set; }
        public DateTime? FechaInscripcion { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string? MotivoRetiro { get; set; }
    }
}

