using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class EvaluacionAlumno
    {
        public int? IdEvaluacionAlumno { get; set; }

        // Se autogenera en BD (usp_evaluaciones_alumnos). No es obligatorio en el modelo.
        [StringLength(30, ErrorMessage = "El código no puede exceder los 30 caracteres.")]
        public string? CodigoRegistro { get; set; }

        [Required(ErrorMessage = "La instancia de evaluación es obligatoria.")]
        public int? IdEvaluacionInstancia { get; set; }
        public string? CodigoInstancia { get; set; } // Para mostrar en UI
        public string? NombreModeloEvaluacion { get; set; } // Para mostrar en UI

        [Required(ErrorMessage = "La inscripción es obligatoria.")]
        public int? IdInscripcion { get; set; }
        public string? CodigoInscripcion { get; set; } // Para mostrar en UI

        // Información del estudiante (a través de inscripción)
        public string? EstudianteUsuario { get; set; } // Para mostrar en UI
        public string? EstudianteNombre { get; set; } // Para mostrar en UI

        // Información de la materia y período (a través de inscripción -> sección)
        public string? NombreMateria { get; set; } // Para mostrar en UI
        public string? CodigoMateria { get; set; } // Para mostrar en UI
        public string? NombrePeriodo { get; set; } // Para mostrar en UI
        public string? CodigoPeriodo { get; set; } // Para mostrar en UI
        public string? CodigoSeccion { get; set; } // Para mostrar en UI

        [Range(0, double.MaxValue, ErrorMessage = "El puntaje obtenido no puede ser negativo.")]
        public decimal PuntajeObtenido { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "El porcentaje logrado debe estar entre 0 y 100.")]
        public decimal? PorcentajeLogrado { get; set; }

        public decimal? PuntajeNormalizado { get; set; }

        public bool EsRecalculo { get; set; } = false;

        [Range(0, int.MaxValue, ErrorMessage = "El número de recálculo no puede ser negativo.")]
        public int NumeroRecalculo { get; set; } = 0;

        [StringLength(500, ErrorMessage = "El motivo de ajuste no puede exceder los 500 caracteres.")]
        public string? MotivoAjuste { get; set; }

        [StringLength(255, ErrorMessage = "Las observaciones no pueden exceder los 255 caracteres.")]
        public string? Observaciones { get; set; }

        public int? IdUsuarioEvaluador { get; set; }
        public string? EvaluadorUsuario { get; set; } // Para mostrar en UI

        public int? IdUsuarioValidador { get; set; }
        public string? ValidadorUsuario { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaValidacion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int? IdEstado { get; set; }
        public string? EstadoNombre { get; set; } // Para mostrar en UI

        public int? IdEstadoPublicacion { get; set; }
        public string? EstadoPublicacionNombre { get; set; } // Para mostrar en UI

        public int? IdEvaluacionReemplazada { get; set; }

        public bool FirmadoPorEstudiante { get; set; } = false;

        [StringLength(255, ErrorMessage = "La firma digital no puede exceder los 255 caracteres.")]
        public string? FirmaDigital { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaNotificacion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaPublicacion { get; set; }

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}

