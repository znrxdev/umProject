using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class SolicitudBeca
    {
        public int? IdSolicitudBeca { get; set; }

        [Required(ErrorMessage = "El código de seguimiento es obligatorio.")]
        [StringLength(30, ErrorMessage = "El código no puede exceder los 30 caracteres.")]
        public string? CodigoSeguimiento { get; set; }

        [Required(ErrorMessage = "El programa de beca es obligatorio.")]
        public int? IdBecaPrograma { get; set; }
        public string? NombreProgramaBeca { get; set; } // Para mostrar en UI
        public string? CodigoProgramaBeca { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El estudiante es obligatorio.")]
        public int? IdEstudiante { get; set; }
        public string? EstudianteUsuario { get; set; } // Para mostrar en UI
        public string? EstudianteNombre { get; set; } // Para mostrar en UI

        [Range(0, 100, ErrorMessage = "El promedio vigente debe estar entre 0 y 100.")]
        public decimal? PromedioVigente { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El total de sanciones activas no puede ser negativo.")]
        public int TotalSancionesActivas { get; set; } = 0;

        public bool CumpleCriterios { get; set; } = false;

        public int? IdTipoDecision { get; set; }
        public string? TipoDecisionNombre { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int? IdEstado { get; set; }
        public string? EstadoNombre { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaSolicitud { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaUltimaDecision { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaCierre { get; set; }

        [StringLength(500, ErrorMessage = "El motivo de la última decisión no puede exceder los 500 caracteres.")]
        public string? MotivoUltimaDecision { get; set; }

        [StringLength(1000, ErrorMessage = "Las observaciones no pueden exceder los 1000 caracteres.")]
        public string? Observaciones { get; set; }

        public bool EsPrioritaria { get; set; } = false;

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}

