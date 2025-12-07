namespace UmProject.Entities
{
    public class EstudianteSeccion
    {
        public int IdSeccion { get; set; }
        public string? CodigoSeccion { get; set; }
        public string? NombreMateria { get; set; }
        public string? CodigoMateria { get; set; }
        public string? NombrePeriodo { get; set; }
        public string? CodigoPeriodo { get; set; }
        public string? Docente { get; set; }
        public string? TipoSeccion { get; set; }
        public string? Aula { get; set; }
        public string? Modalidad { get; set; }
        public int? CupoMaximo { get; set; }
        public decimal? PorcentajeAsistenciaMinima { get; set; }
        public string? EstadoSeccion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaCierre { get; set; }
    }
}

