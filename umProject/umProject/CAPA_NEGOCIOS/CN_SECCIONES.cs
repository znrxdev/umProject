using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnSecciones
    {
        private CdSecciones cdSecciones = new CdSecciones();

        public List<CeSecciones> FiltrarSeccionesPorMateriaPeriodo(CeSecciones obj, out int oNum, out string oMsg)
        {
            return cdSecciones.FiltrarSeccionesPorMateriaPeriodo(obj, out oNum, out oMsg);
        }
    }
}


