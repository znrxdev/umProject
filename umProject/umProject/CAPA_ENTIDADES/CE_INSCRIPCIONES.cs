using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeInscripciones
    {
        public int? IdInscripcion { get; set; }
        public string? CodigoInscripcion { get; set; }
        public int? IdSeccion { get; set; }
        public int? IdEstudiante { get; set; }
        public int? IdTipoInscripcion { get; set; }
        public int? IdEstado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string? MotivoRetiro { get; set; }
        public decimal? PromedioAcumulado { get; set; }
        public int? TotalFaltas { get; set; }
        public bool? EsRepetidor { get; set; }
        public bool? EnRiesgoAcademico { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdUsuarioValidador { get; set; }
        public int? IdTransaccion { get; set; }
        public bool? Activo { get; set; }
        // Propiedades adicionales de JOINs
        public string? UsuarioEstudiante { get; set; }
        public string? NombreEstudiante { get; set; }
    }
}


