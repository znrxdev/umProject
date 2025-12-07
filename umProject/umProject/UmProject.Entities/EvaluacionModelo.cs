using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class EvaluacionModelo
    {
        public int? IdEvaluacionModelo { get; set; }

        [Required(ErrorMessage = "La materia es obligatoria.")]
        public int? IdMateria { get; set; }
        public string? NombreMateria { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El tipo de evaluación es obligatorio.")]
        public int? IdTipoEvaluacion { get; set; }
        public string? NombreTipoEvaluacion { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El código del modelo es obligatorio.")]
        [StringLength(30, ErrorMessage = "El código del modelo no puede exceder los 30 caracteres.")]
        public string? CodigoModelo { get; set; }

        [Required(ErrorMessage = "El nombre de la evaluación es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la evaluación no puede exceder los 100 caracteres.")]
        public string? NombreEvaluacion { get; set; }

        [StringLength(255, ErrorMessage = "El concepto no puede exceder los 255 caracteres.")]
        public string? Concepto { get; set; }

        public int VersionConfiguracion { get; set; } = 1;

        public string? RubricaDetalle { get; set; }

        public bool Activo { get; set; } = true;

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}
