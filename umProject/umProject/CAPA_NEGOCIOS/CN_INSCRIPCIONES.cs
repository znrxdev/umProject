using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnInscripciones
    {
        private CdInscripciones cdInscripciones = new CdInscripciones();

        public List<CeInscripciones> FiltrarInscripcionesPorSeccion(CeInscripciones obj, out int oNum, out string oMsg)
        {
            return cdInscripciones.FiltrarInscripcionesPorSeccion(obj, out oNum, out oMsg);
        }
    }
}


