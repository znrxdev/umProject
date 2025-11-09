using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CN_ESTADOS
    {
        CD_ESTADOS _CDEstados = new CD_ESTADOS();
        public List<CE_ESTADOS> FILTRAR_ESTADOS_POR_ID(CE_ESTADOS Obj, out int o_Num, out string o_Msg)
        {
            return _CDEstados.FILTRAR_ESTADOS_POR_ID(Obj, out o_Num, out o_Msg);
        }

        public List<CE_ESTADOS> FILTRAR_ESTADOS_POR_TIPO_TRANSACCION(CE_ESTADOS Obj, out int o_Num, out string o_Msg)
        {
            return _CDEstados.FILTRAR_ESTADOS_POR_TIPO_TRANSACCION(Obj, out o_Num, out o_Msg);
        }
    }
}
