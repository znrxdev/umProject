namespace UmProject.Entities
{
    public class UsuarioRol
    {
        public int? IdUsuarioRol { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        public bool? Activo { get; set; }
    }
}

