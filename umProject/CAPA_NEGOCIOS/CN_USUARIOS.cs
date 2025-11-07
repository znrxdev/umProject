using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CN_USUARIOS
    {
        private CD_USUARIOS Usuarios_ = new CD_USUARIOS();

        public List<CE_USUARIOS> INICIO_SESION(CE_USUARIOS Obj, out int o_Num, out string o_Msg)
        {
            return Usuarios_.INICIO_SESION(Obj, out o_Num, out o_Msg);
        }
        public List<CE_MENUS> LISTAR_MENU_POR_ROL(out int o_Num, out string o_Msg)
        {
            return Usuarios_.LISTAR_MENU_POR_ROL(out o_Num, out o_Msg);
        }
    }
}
