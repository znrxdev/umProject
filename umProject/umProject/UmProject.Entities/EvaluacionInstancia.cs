using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class EvaluacionInstancia
    {
        public int? IdEvaluacionInstancia { get; set; }

        // CodigoInstancia es autogenerable por el stored procedure, no requiere validación
        [StringLength(30, ErrorMessage = "El código de instancia no puede exceder los 30 caracteres.")]
        public string? CodigoInstancia { get; set; }

        [Required(ErrorMessage = "La sección es obligatoria.")]
        public int? IdSeccion { get; set; }
        public string? CodigoSeccion { get; set; } // Para mostrar en UI
        public string? NombreMateria { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El modelo de evaluación es obligatorio.")]
        public int? IdEvaluacionModelo { get; set; }
        public string? CodigoModelo { get; set; } // Para mostrar en UI
        public string? NombreModeloEvaluacion { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El período es obligatorio.")]
        public int? IdPeriodo { get; set; }
        public string? NombrePeriodo { get; set; } // Para mostrar en UI
        public string? CodigoPeriodo { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaProgramada { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaLimite { get; set; }

        public bool RequiereRevisionInterna { get; set; } = false;

        public int NumeroVersion { get; set; } = 1;

        public byte NivelAprobacionActual { get; set; } = 1;

        [Range(0.01, 9999.99, ErrorMessage = "La calificación máxima debe ser mayor a 0.")]
        public decimal CalificacionMaxima { get; set; } = 100;

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int? IdEstado { get; set; }
        public string? NombreEstado { get; set; } // Para mostrar en UI

        public int? IdResponsableRevision { get; set; }
        public string? UsuarioResponsableRevision { get; set; } // Para mostrar en UI
        public string? NombreResponsableRevision { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaRevision { get; set; }

        public int? IdResponsablePublicacion { get; set; }
        public string? UsuarioResponsablePublicacion { get; set; } // Para mostrar en UI
        public string? NombreResponsablePublicacion { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaPublicacion { get; set; }

        public int? IdEvaluacionPadre { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones de revisión no pueden exceder los 500 caracteres.")]
        public string? ObservacionesRevision { get; set; }

        [StringLength(500, ErrorMessage = "El motivo de rechazo no puede exceder los 500 caracteres.")]
        public string? MotivoRechazo { get; set; }

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}

