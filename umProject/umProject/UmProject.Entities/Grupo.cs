using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class Grupo
    {
        public int? IdGrupo { get; set; }

        [Required(ErrorMessage = "El código de grupo es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder los 20 caracteres.")]
        public string? CodigoGrupo { get; set; }

        [Required(ErrorMessage = "El nombre de grupo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string? NombreGrupo { get; set; }

        [Required(ErrorMessage = "El período académico es obligatorio.")]
        public int? IdPeriodo { get; set; }
        public string? NombrePeriodo { get; set; } // Para mostrar en UI
        public string? CodigoPeriodo { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El tipo de grupo es obligatorio.")]
        public int? IdTipoGrupo { get; set; }
        public string? NombreTipoGrupo { get; set; } // Para mostrar en UI

        public int? IdCoordinador { get; set; }
        public string? CoordinadorUsuario { get; set; } // Para mostrar en UI
        public string? CoordinadorNombre { get; set; } // Para mostrar en UI

        public int? IdJornada { get; set; }
        public string? NombreJornada { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int? IdEstado { get; set; }
        public string? NombreEstado { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaCierre { get; set; }

        [StringLength(255, ErrorMessage = "Las observaciones no pueden exceder los 255 caracteres.")]
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "El código de seguimiento es obligatorio.")]
        [StringLength(30, ErrorMessage = "El código de seguimiento no puede exceder los 30 caracteres.")]
        public string? CodigoSeguimiento { get; set; }

        public bool? Activo { get; set; }

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}

