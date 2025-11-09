using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CN_PERSONAS
    {
        CD_PERSONAS _CDPersonas = new CD_PERSONAS();
        public void AGREGAR_PERSONAS(CE_PERSONAS obj, out int o_Num, out string o_Msg)
        {
            _CDPersonas.AGREGAR_PERSONAS(obj, out o_Num, out o_Msg);
        }

        public void ACTUALIZAR_PERSONAS(CE_PERSONAS obj, out int o_Num, out string o_Msg)
        {
            _CDPersonas.ACTUALIZAR_PERSONAS(obj, out o_Num, out o_Msg);
        }

        public List<CE_PERSONAS> FILTRAR_PERSONAS_POR_NUMERO_DOCUMENTO(CE_PERSONAS obj, out int o_Num, out string o_Msg)
        {
            return _CDPersonas.FILTRAR_PERSONAS_POR_NUMERO_DOCUMENTO(obj, out o_Num, out o_Msg);
        }
        public List<CE_PERSONAS> FILTRAR_PERSONAS_POR_ID(CE_PERSONAS obj, out int o_Num, out string o_Msg)
        {
            return _CDPersonas.FILTRAR_PERSONAS_POR_ID(obj, out o_Num, out o_Msg);
        }
    }
}
