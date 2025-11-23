using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnEstados
    {
        CdEstados cdEstados = new CdEstados();
        public List<CeEstados> FiltrarEstadosPorId(CeEstados obj, out int oNum, out string oMsg)
        {
            return cdEstados.FiltrarEstadosPorId(obj, out oNum, out oMsg);
        }

        public List<CeEstados> FiltrarEstadosPorTipoTransaccion(CeEstados obj, out int oNum, out string oMsg)
        {
            return cdEstados.FiltrarEstadosPorTipoTransaccion(obj, out oNum, out oMsg);
        }
    }
}
