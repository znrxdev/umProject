using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeMateriasPeriodos
    {
        public int? IdMateriaPeriodo { get; set; }
        public int? IdMateria { get; set; }
        public int? IdPeriodoAcademico { get; set; }
        public string? CodigoPlan { get; set; }
        public int? IdJornada { get; set; }
        public string? Modalidad { get; set; }
        public int? HorasTeoricas { get; set; }
        public int? HorasPracticas { get; set; }
        public decimal? PorcentajeAsistenciaMinima { get; set; }
        public int? IdEstado { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public int? IdUsuarioPublicador { get; set; }
        public string? Observaciones { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        
        // Campos adicionales para JOIN
        public string? NombreMateria { get; set; }
        public string? NombrePeriodo { get; set; }
        public string? CodigoPeriodo { get; set; }
    }
}

