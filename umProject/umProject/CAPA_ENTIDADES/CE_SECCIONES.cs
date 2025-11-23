using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeSecciones
    {
        public int? IdSeccion { get; set; }
        public string? CodigoSeccion { get; set; }
        public int? IdMateriaPeriodo { get; set; }
        public int? IdDocente { get; set; }
        public int? IdTipoSeccion { get; set; }
        public int? IdAula { get; set; }
        public string? HorarioDescripcion { get; set; }
        public string? Modalidad { get; set; }
        public int? CupoMaximo { get; set; }
        public bool? RequiereAsistencia { get; set; }
        public decimal? PorcentajeAsistenciaMinima { get; set; }
        public int? IdEstado { get; set; }
        public int? IdEstadoPublicacion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string? CodigoFirma { get; set; }
        public int? IdUsuarioPublicador { get; set; }
        public string? Observaciones { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
    }
}


