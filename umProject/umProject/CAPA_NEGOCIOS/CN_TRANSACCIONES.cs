using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnTransacciones
    {
        private CdTransacciones transacciones = new CdTransacciones();

        public List<CeTransacciones> ListarAuditoriaSistema(out int oNum, out string oMsg)
        {
            return transacciones.ListarAuditoriaSistema(out oNum, out oMsg);
        }
    }
}

