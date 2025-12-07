namespace UmProject.Entities
{
    public class DocenteSeccionEstudiante
    {
        public int IdInscripcion { get; set; }
        public string? CodigoInscripcion { get; set; }
        public int IdEstudiante { get; set; }
        public string? NombreEstudiante { get; set; }
        public string? UsuarioEstudiante { get; set; }
        public string? ValorDocumento { get; set; }
        public string? TipoInscripcion { get; set; }
        public string? EstadoInscripcion { get; set; }
        public DateTime? FechaInscripcion { get; set; }
        public DateTime? FechaValidacion { get; set; }
    }
}

