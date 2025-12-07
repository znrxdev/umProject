namespace UmProject.Entities
{
    public class Estado
    {
        public int? IdEstado { get; set; }
        public string? NombreEstado { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        public bool? Activo { get; set; }
    }
}

