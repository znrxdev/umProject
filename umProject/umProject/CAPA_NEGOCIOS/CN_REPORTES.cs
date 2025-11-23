using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnReportes
    {
        private CdReportes cdReportes = new CdReportes();

        public List<CeReporteUsuario> ObtenerReporteUsuariosActivos(DateTime? fechaInicio, DateTime? fechaFin, out int oNum, out string oMsg)
        {
            return cdReportes.ObtenerReporteUsuariosActivos(fechaInicio, fechaFin, out oNum, out oMsg);
        }

        public List<CeReporteUsuario> ObtenerReporteUsuariosInactivos(DateTime? fechaInicio, DateTime? fechaFin, out int oNum, out string oMsg)
        {
            return cdReportes.ObtenerReporteUsuariosInactivos(fechaInicio, fechaFin, out oNum, out oMsg);
        }

        public List<CeReporteAuditoria> ObtenerReporteAuditoria(DateTime? fechaInicio, DateTime? fechaFin, out int oNum, out string oMsg)
        {
            return cdReportes.ObtenerReporteAuditoria(fechaInicio, fechaFin, out oNum, out oMsg);
        }
    }
}

