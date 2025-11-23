using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnPersonas
    {
        CdPersonas cdPersonas = new CdPersonas();
        public void AgregarPersonas(CePersonas obj, out int oNum, out string oMsg)
        {
            cdPersonas.AgregarPersonas(obj, out oNum, out oMsg);
        }

        public void ActualizarPersonas(CePersonas obj, out int oNum, out string oMsg)
        {
            cdPersonas.ActualizarPersonas(obj, out oNum, out oMsg);
        }

        public List<CePersonas> FiltrarPersonasPorNumeroDocumento(CePersonas obj, out int oNum, out string oMsg)
        {
            return cdPersonas.FiltrarPersonasPorNumeroDocumento(obj, out oNum, out oMsg);
        }
        public List<CePersonas> FiltrarPersonasPorId(CePersonas obj, out int oNum, out string oMsg)
        {
            return cdPersonas.FiltrarPersonasPorId(obj, out oNum, out oMsg);
        }
    }
}
