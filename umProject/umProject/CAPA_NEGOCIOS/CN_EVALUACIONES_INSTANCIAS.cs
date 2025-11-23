using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnEvaluacionesInstancias
    {
        private CdEvaluacionesInstancias cdEvaluacionesInstancias = new CdEvaluacionesInstancias();

        public List<CeEvaluacionesInstancias> FiltrarEvaluacionInstanciaPorId(CeEvaluacionesInstancias obj, out int oNum, out string oMsg)
        {
            return cdEvaluacionesInstancias.FiltrarEvaluacionInstanciaPorId(obj, out oNum, out oMsg);
        }

        public List<CeEvaluacionesInstancias> FiltrarEvaluacionInstanciaPorSeccion(CeEvaluacionesInstancias obj, out int oNum, out string oMsg)
        {
            return cdEvaluacionesInstancias.FiltrarEvaluacionInstanciaPorSeccion(obj, out oNum, out oMsg);
        }

        public void AgregarEvaluacionInstancia(CeEvaluacionesInstancias obj, out int oNum, out string oMsg)
        {
            cdEvaluacionesInstancias.AgregarEvaluacionInstancia(obj, out oNum, out oMsg);
        }

        public void ActualizarEvaluacionInstancia(CeEvaluacionesInstancias obj, out int oNum, out string oMsg)
        {
            cdEvaluacionesInstancias.ActualizarEvaluacionInstancia(obj, out oNum, out oMsg);
        }

        public List<CeEvaluacionesInstancias> ListarTodasLasInstancias(out int oNum, out string oMsg)
        {
            return cdEvaluacionesInstancias.ListarTodasLasInstancias(out oNum, out oMsg);
        }
    }
}

