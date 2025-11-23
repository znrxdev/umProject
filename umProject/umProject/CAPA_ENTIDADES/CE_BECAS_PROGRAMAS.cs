using System;

namespace CAPA_ENTIDADES
{
    public class CeBecasProgramas
    {
        public int? IdBecaPrograma { get; set; }
        public string CodigoPrograma { get; set; }
        public string NombrePrograma { get; set; }
        public string Descripcion { get; set; }
        public int? IdTipoPrograma { get; set; }
        public int? IdModalidadPrograma { get; set; }
        public DateTime? FechaVigenciaInicio { get; set; }
        public DateTime? FechaVigenciaFin { get; set; }
        public decimal? MontoMaximo { get; set; }
        public int? IdMoneda { get; set; }
        public decimal? PromedioMinimo { get; set; }
        public int? CreditosMinimos { get; set; }
        public byte? NivelesAprobacion { get; set; }
        public bool? RequiereSinSanciones { get; set; }
        public bool? RequiereCartaCompromiso { get; set; }
        public int? IdEstadoPrograma { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}


