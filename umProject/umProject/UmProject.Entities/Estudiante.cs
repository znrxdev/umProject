namespace UmProject.Entities
{
    public class Estudiante
    {
        public int IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public int? IdPersona { get; set; }
        public string? NombreCompleto { get; set; }
        public string? ValorDocumento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? EstadoUsuario { get; set; }
        public DateTime? UltimaSesion { get; set; }
        public DateTime? FechaCreacionUsuario { get; set; }
    }
}

