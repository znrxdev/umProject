namespace UmProject.Entities
{
    public class Persona
    {
        public int? IdPersona { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string? ValorDocumento { get; set; }
        public int? IdGeneroPersona { get; set; }
        public string? FechaNacimiento { get; set; }
        public int? IdNacionalidad { get; set; }
        public int? IdEstadoCivil { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        public int? IdEstado { get; set; }
    }
}

