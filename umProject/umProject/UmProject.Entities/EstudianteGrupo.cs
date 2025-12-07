namespace UmProject.Entities
{
    public class EstudianteGrupo
    {
        public int IdGrupo { get; set; }
        public string? CodigoGrupo { get; set; }
        public string? NombreGrupo { get; set; }
        public string? NombrePeriodo { get; set; }
        public string? CodigoPeriodo { get; set; }
        public string? TipoGrupo { get; set; }
        public string? Jornada { get; set; }
        public string? Coordinador { get; set; }
        public string? EstadoGrupo { get; set; }
        public string? RolEnGrupo { get; set; }
        public bool EsDelegado { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string? MotivoBaja { get; set; }
    }
}

