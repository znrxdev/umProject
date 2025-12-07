namespace UmProject.Entities
{
    public class ReporteTransaccion
    {
        public int? IdTransaccion { get; set; }
        public string? NombreTipoTransaccion { get; set; }
        public string? Concepto { get; set; }
        public string? TipoEntidad { get; set; }
        public string? Autor { get; set; }
        public string? FechaCreacion { get; set; }
        public string? Estado { get; set; }
    }
}

