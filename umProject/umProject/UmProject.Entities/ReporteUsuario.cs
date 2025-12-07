namespace UmProject.Entities
{
    public class ReporteUsuario
    {
        public int? IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public int? IdPersona { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? NombreCompleto { get; set; }
        public string? ValorDocumento { get; set; }
        public string? TipoDocumento { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
        public string? Nacionalidad { get; set; }
        public string? EstadoCivil { get; set; }
        public string? FechaCreacionUsuario { get; set; }
        public string? FechaModificacionUsuario { get; set; }
        public string? UltimaSesion { get; set; }
        public string? UltimoCambioContrasena { get; set; }
        public string? EstadoUsuario { get; set; }
        public string? FechaCreacionPersona { get; set; }
        public string? FechaModificacionPersona { get; set; }
    }
}

