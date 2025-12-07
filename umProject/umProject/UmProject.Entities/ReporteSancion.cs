namespace UmProject.Entities
{
    public class ReporteSancion
    {
        public int? IdSancion { get; set; }
        public string? CodigoSancion { get; set; }
        public string? Estudiante { get; set; }
        public string? NombreEstudiante { get; set; }
        public string? TipoSancion { get; set; }
        public string? TipoFalta { get; set; }
        public string? Severidad { get; set; }
        public string? Estado { get; set; }
        public string? FechaRegistro { get; set; }
        public string? FechaFin { get; set; }
        public string? Motivo { get; set; }
        public string? EsApelable { get; set; }
        public string? FechaApelacion { get; set; }
        public string? ResultadoApelacion { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
    }
}

