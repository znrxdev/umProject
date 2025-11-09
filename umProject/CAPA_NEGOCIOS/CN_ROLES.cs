using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CN_ROLES
    {
        CD_ROLES CDRoles = new CD_ROLES();
        public List<CE_ROLES> LISTAR_ROLES(out int o_Num, out string o_Msg)
        {
            return CDRoles.LISTAR_ROLES(out o_Num, out o_Msg);
        }
        public List<CE_ROLES> FILTRAR_ROLES_ID(CE_ROLES Obj,out int o_Num, out string o_Msg)
        {
            return CDRoles.FILTRAR_ROLES_ID(Obj,out o_Num, out o_Msg);
        }
    }
}
