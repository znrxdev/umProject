namespace UmProject.Entities
{
    public class Materia
    {
        public int? IdMateria { get; set; }
        public string? CodigoMateria { get; set; }
        public string? NombreMateria { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        public bool? Activo { get; set; }
    }
}

