namespace UmProject.Entities
{
    public class Catalogo
    {
        public int? IdCatalogo { get; set; }
        public int? IdTipoCatalogo { get; set; }
        public string? NombreCatalogo { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        public bool? Activo { get; set; }
    }
}

