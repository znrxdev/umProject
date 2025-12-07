using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class Inscripcion
    {
        public int? IdInscripcion { get; set; }

        // Código de inscripción se autogenera en el SP si no se proporciona
        [StringLength(30, ErrorMessage = "El código no puede exceder los 30 caracteres.")]
        public string? CodigoInscripcion { get; set; }

        [Required(ErrorMessage = "El estudiante es obligatorio.")]
        public int? IdEstudiante { get; set; }
        public string? EstudianteUsuario { get; set; } // Para mostrar en UI
        public string? EstudianteNombre { get; set; } // Para mostrar en UI

        public int? IdTipoInscripcion { get; set; }
        public string? TipoInscripcionNombre { get; set; } // Para mostrar en UI

        // Estado inicial siempre EN REVISION (4) al crear, solo ACTIVO (1) o INACTIVO (2) al actualizar
        public int? IdEstado { get; set; }
        public string? EstadoNombre { get; set; } // Para mostrar en UI

        [DataType(DataType.DateTime)]
        public DateTime? FechaValidacion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaRetiro { get; set; }

        [StringLength(500, ErrorMessage = "El motivo de retiro no puede exceder los 500 caracteres.")]
        public string? MotivoRetiro { get; set; }

        public int? IdUsuarioValidador { get; set; }
        public string? ValidadorUsuario { get; set; } // Para mostrar en UI

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}

