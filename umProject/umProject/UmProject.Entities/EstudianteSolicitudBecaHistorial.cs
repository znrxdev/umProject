namespace UmProject.Entities
{
    public class EstudianteSolicitudBecaHistorial
    {
        public int IdHistorialSolicitud { get; set; }
        public int IdSolicitudBeca { get; set; }
        public int? IdEstadoAnterior { get; set; }
        public int IdEstadoNuevo { get; set; }
        public string? EstadoNuevoNombre { get; set; }
        public int IdUsuarioRevisor { get; set; }
        public string? UsuarioRevisor { get; set; }
        public DateTime FechaDecision { get; set; }
        public string? MotivoDecision { get; set; }
        public string? Observaciones { get; set; }
    }
}

