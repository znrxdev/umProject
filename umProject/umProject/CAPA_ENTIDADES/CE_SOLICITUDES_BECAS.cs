using System;

namespace CAPA_ENTIDADES
{
    public class CeSolicitudesBecas
    {
        public int? IdSolicitudBeca { get; set; }
        public string CodigoSeguimiento { get; set; }
        public int? IdBecaPrograma { get; set; }
        public int? IdConvocatoria { get; set; }
        public int? IdEstudiante { get; set; }
        public decimal? PromedioVigente { get; set; }
        public int? CreditosAprobados { get; set; }
        public int? TotalSancionesActivas { get; set; }
        public bool? CumpleCriterios { get; set; }
        public byte? NivelAprobacionActual { get; set; }
        public byte? NivelAprobacionMaximo { get; set; }
        public int? IdUsuarioResponsable { get; set; }
        public int? IdUsuarioSupervisor { get; set; }
        public int? IdTipoDecision { get; set; }
        public int? IdEstado { get; set; }
        public int? IdEstadoFlujo { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaUltimaDecision { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string MotivoUltimaDecision { get; set; }
        public string Observaciones { get; set; }
        public bool? EsPrioritaria { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }

        // Campos adicionales de JOIN para vistas/listados
        public string UsuarioEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public string NombrePrograma { get; set; }
        public string NombreConvocatoria { get; set; }
    }
}


