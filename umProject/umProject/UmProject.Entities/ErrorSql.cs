namespace UmProject.Entities
{
    public class ErrorSql
    {
        public int IdError { get; set; }
        public string? OrigenError { get; set; }
        public int? LineaError { get; set; }
        public int? NumeroError { get; set; }
        public string? MensajeError { get; set; }
        public string? FechaError { get; set; }
    }
}

