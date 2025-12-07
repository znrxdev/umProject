namespace UmProject.Entities
{
    public class EstudianteSancion
    {
        public int IdSancion { get; set; }
        public string? CodigoSancion { get; set; }
        public string? TipoSancion { get; set; }
        public string? TipoFalta { get; set; }
        public string? Severidad { get; set; }
        public string? EstadoSancion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Motivo { get; set; }
        public bool EsApelable { get; set; }
        public DateTime? FechaApelacion { get; set; }
        public string? ResultadoApelacion { get; set; }
        public string? ObservacionesApelacion { get; set; }
        public string? UsuarioResolucion { get; set; }
        public DateTime? FechaResolucion { get; set; }
    }
}

