using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class SancionAcademica
    {
        public int? IdSancion { get; set; }

        [Required(ErrorMessage = "El código de sanción es obligatorio.")]
        [StringLength(30, ErrorMessage = "El código de sanción no puede exceder los 30 caracteres.")]
        public string? CodigoSancion { get; set; }

        [Required(ErrorMessage = "El estudiante es obligatorio.")]
        public int? IdEstudiante { get; set; }
        public string? UsuarioEstudiante { get; set; } // Para mostrar en UI
        public string? NombreEstudiante { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El tipo de sanción es obligatorio.")]
        public int? IdTipoSancion { get; set; }
        public string? NombreTipoSancion { get; set; } // Para mostrar en UI

        public int? IdTipoFalta { get; set; }
        public string? NombreTipoFalta { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "La severidad es obligatoria.")]
        public int? IdSeveridad { get; set; }
        public string? NombreSeveridad { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int? IdEstado { get; set; }
        public string? NombreEstado { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "La fecha de registro es obligatoria.")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaRegistro { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaFin { get; set; }

        [StringLength(300, ErrorMessage = "El motivo no puede exceder los 300 caracteres.")]
        public string? Motivo { get; set; }

        public bool EsApelable { get; set; } = false;

        [DataType(DataType.DateTime)]
        public DateTime? FechaApelacion { get; set; }

        [StringLength(200, ErrorMessage = "El resultado de la apelación no puede exceder los 200 caracteres.")]
        public string? ResultadoApelacion { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones de apelación no pueden exceder los 500 caracteres.")]
        public string? ObservacionesApelacion { get; set; }

        [StringLength(255, ErrorMessage = "El documento de resolución no puede exceder los 255 caracteres.")]
        public string? DocumentoResolucion { get; set; }

        public int? IdUsuarioResolucion { get; set; }
        public string? UsuarioResolucion { get; set; } // Para mostrar en UI
        public string? NombreUsuarioResolucion { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaResolucion { get; set; }

        public int? IdSancionOrigen { get; set; }

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}

