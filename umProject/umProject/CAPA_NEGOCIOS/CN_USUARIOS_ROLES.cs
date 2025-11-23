using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnUsuariosRoles
    {
        CdUsuariosRoles cdUsuariosRoles = new CdUsuariosRoles();
        public void AgregarUsuariosRoles(CeUsuariosRoles obj, out int oNum, out string oMsg)
        {
            cdUsuariosRoles.AgregarUsuariosRoles(obj, out oNum, out oMsg);
        }
        public List<CeUsuariosRoles> ActualizarRolesUsuario(CeUsuariosRoles obj, out int oNum, out string oMsg)
        {
            return cdUsuariosRoles.ActualizarRolesUsuario(obj, out oNum, out oMsg);
        }
        public List<CeUsuariosRoles> ListarRolesDeUsuario(CeUsuariosRoles obj, out int oNum, out string oMsg)
        {
            return cdUsuariosRoles.ListarRolesDeUsuario(obj, out oNum, out oMsg);
        }
    }
}
