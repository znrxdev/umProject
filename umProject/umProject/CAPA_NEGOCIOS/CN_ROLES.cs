using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnRoles
    {
        CdRoles cdRoles = new CdRoles();
        public List<CeRoles> ListarRoles(out int oNum, out string oMsg)
        {
            return cdRoles.ListarRoles(out oNum, out oMsg);
        }
        public List<CeRoles> FiltrarRolesId(CeRoles obj, out int oNum, out string oMsg)
        {
            return cdRoles.FiltrarRolesId(obj, out oNum, out oMsg);
        }
    }
}
