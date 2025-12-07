using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class Seccion
    {
        public int? IdSeccion { get; set; }

        [Required(ErrorMessage = "El código de sección es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder los 20 caracteres.")]
        public string? CodigoSeccion { get; set; }

        [Required(ErrorMessage = "La materia período es obligatoria.")]
        public int? IdMateriaPeriodo { get; set; }
        public string? NombreMateria { get; set; } // Para mostrar en UI
        public string? CodigoMateria { get; set; } // Para mostrar en UI
        public string? NombrePeriodo { get; set; } // Para mostrar en UI
        public string? CodigoPeriodo { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El docente es obligatorio.")]
        public int? IdDocente { get; set; }
        public string? DocenteUsuario { get; set; } // Para mostrar en UI
        public string? DocenteNombre { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "El tipo de sección es obligatorio.")]
        public int? IdTipoSeccion { get; set; }
        public string? TipoSeccionNombre { get; set; } // Para mostrar en UI

        public int? IdAula { get; set; }
        public string? AulaNombre { get; set; } // Para mostrar en UI

        [StringLength(255, ErrorMessage = "La descripción del horario no puede exceder los 255 caracteres.")]
        public string? HorarioDescripcion { get; set; }

        [StringLength(50, ErrorMessage = "La modalidad no puede exceder los 50 caracteres.")]
        public string? Modalidad { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El cupo máximo debe ser mayor que cero.")]
        public int? CupoMaximo { get; set; }

        public bool RequiereAsistencia { get; set; } = true;

        [Range(0, 100, ErrorMessage = "El porcentaje de asistencia mínima debe estar entre 0 y 100.")]
        public decimal? PorcentajeAsistenciaMinima { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int? IdEstado { get; set; }
        public string? EstadoNombre { get; set; } // Para mostrar en UI

        public int? IdEstadoPublicacion { get; set; }
        public string? EstadoPublicacionNombre { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaPublicacion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaCierre { get; set; }

        [StringLength(100, ErrorMessage = "El código de firma no puede exceder los 100 caracteres.")]
        public string? CodigoFirma { get; set; }

        public int? IdUsuarioPublicador { get; set; }

        [StringLength(255, ErrorMessage = "Las observaciones no pueden exceder los 255 caracteres.")]
        public string? Observaciones { get; set; }

        public bool? Activo { get; set; }

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}

