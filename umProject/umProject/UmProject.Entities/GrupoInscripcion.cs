namespace UmProject.Entities
{
    public class GrupoInscripcion
    {
        public int IdGrupoInscripcion { get; set; }
        public int IdGrupo { get; set; }
        public int IdInscripcion { get; set; }
        public int? IdRolGrupo { get; set; }
        public int IdEstado { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string? MotivoBaja { get; set; }
        public bool EsDelegado { get; set; }
        public string? Observaciones { get; set; }
        public bool Activo { get; set; }
        
        // Información relacionada
        public string? CodigoInscripcion { get; set; }
        public string? EstudianteUsuario { get; set; }
        public string? EstudianteNombreCompleto { get; set; }
        public string? EstudianteDocumento { get; set; }
        public string? EstadoNombre { get; set; }
    }
}

