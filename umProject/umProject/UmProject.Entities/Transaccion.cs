namespace UmProject.Entities
{
    public class Transaccion
    {
        public int? IdTransaccion { get; set; }
        public int? IdTipoTransaccion { get; set; }
        public string? NombreTipoTransaccion { get; set; }
        public string? Concepto { get; set; }
        public int? IdPersona { get; set; }
        public string? NombrePersona { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public int? IdContacto { get; set; }
        public int? IdEvaluacion { get; set; }
        public int? IdSolicitudBeca { get; set; }
        public int? IdInscripcion { get; set; }
        public int? IdAutor { get; set; }
        public string? NombreAutor { get; set; }
        public string? FechaCreacion { get; set; }
        public bool? Completado { get; set; }
        public string? TipoEntidad { get; set; }
    }
}

