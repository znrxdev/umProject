using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class BecaPrograma
    {
        public int? IdBecaPrograma { get; set; }

        [StringLength(30, ErrorMessage = "El código del programa no puede exceder los 30 caracteres.")]
        public string? CodigoPrograma { get; set; }

        [Required(ErrorMessage = "El nombre del programa es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre del programa no puede exceder los 150 caracteres.")]
        public string? NombrePrograma { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }

        public int? IdTipoPrograma { get; set; }
        public string? NombreTipoPrograma { get; set; } // Para mostrar en UI

        public int? IdModalidadPrograma { get; set; }
        public string? NombreModalidadPrograma { get; set; } // Para mostrar en UI

        [Range(0, 100, ErrorMessage = "El promedio mínimo debe estar entre 0 y 100.")]
        public decimal? PromedioMinimo { get; set; }

        public bool RequiereSinSanciones { get; set; } = true;

        [Required(ErrorMessage = "El estado del programa es obligatorio.")]
        public int? IdEstadoPrograma { get; set; }
        public string? NombreEstadoPrograma { get; set; } // Para mostrar en UI

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }

        // Información adicional para listados de disponibilidad
        public string? CriteriosResumen { get; set; }
    }
}

