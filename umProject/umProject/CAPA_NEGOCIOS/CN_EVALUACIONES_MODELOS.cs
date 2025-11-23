using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnEvaluacionesModelos
    {
        private CdEvaluacionesModelos cdEvaluacionesModelos = new CdEvaluacionesModelos();

        public List<CeEvaluacionesModelos> FiltrarEvaluacionModeloPorId(CeEvaluacionesModelos obj, out int oNum, out string oMsg)
        {
            return cdEvaluacionesModelos.FiltrarEvaluacionModeloPorId(obj, out oNum, out oMsg);
        }

        public List<CeEvaluacionesModelos> FiltrarEvaluacionModeloPorMateriaPeriodo(CeEvaluacionesModelos obj, out int oNum, out string oMsg)
        {
            return cdEvaluacionesModelos.FiltrarEvaluacionModeloPorMateriaPeriodo(obj, out oNum, out oMsg);
        }

        public void AgregarEvaluacionModelo(CeEvaluacionesModelos obj, out int oNum, out string oMsg)
        {
            cdEvaluacionesModelos.AgregarEvaluacionModelo(obj, out oNum, out oMsg);
        }

        public void ActualizarEvaluacionModelo(CeEvaluacionesModelos obj, out int oNum, out string oMsg)
        {
            cdEvaluacionesModelos.ActualizarEvaluacionModelo(obj, out oNum, out oMsg);
        }

        public List<CeEvaluacionesModelos> ListarTodosLosModelos(out int oNum, out string oMsg)
        {
            return cdEvaluacionesModelos.ListarTodosLosModelos(out oNum, out oMsg);
        }
    }
}

