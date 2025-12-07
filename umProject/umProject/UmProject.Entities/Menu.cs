namespace UmProject.Entities
{
    public class Menu
    {
        public int? IdMenu { get; set; }
        public string? MenuNombre { get; set; } // Campo Menu de la BD (ej: "Usuarios")
        public string? NombreBoton { get; set; } // Campo Nombre_Boton de la BD (ej: "btn_UsuarioMenu")
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        public bool? Activo { get; set; }
    }
}

