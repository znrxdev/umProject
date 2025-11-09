using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CN_USUARIOS_ROLES
    {
        CD_USUARIOS_ROLES CDUsuariosRoles = new CD_USUARIOS_ROLES();
        public void AGREGAR_USUARIOS_ROLES(CE_USUARIOS_ROLES obj, out int o_Num, out string o_Msg)
        {
            CDUsuariosRoles.AGREGAR_USUARIOS_ROLES(obj, out o_Num, out o_Msg);
        }
        public void ACTUALIZAR_ROLES_USUARIO(CE_USUARIOS_ROLES obj, out int o_Num, out string o_Msg)
        {
            CDUsuariosRoles.ACTUALIZAR_ROLES_USUARIO(obj, out o_Num, out o_Msg);
        }
        public List<CE_USUARIOS_ROLES> LISTAR_ROLES_DE_USUARIO(CE_USUARIOS_ROLES obj, out int o_Num, out string o_Msg)
        {
            return CDUsuariosRoles.LISTAR_ROLES_DE_USUARIO(obj, out o_Num, out o_Msg);
        }
    }
}
