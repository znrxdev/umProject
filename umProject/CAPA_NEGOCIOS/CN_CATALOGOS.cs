using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CN_CATALOGOS
    {
        CD_CATALOGOS CDCatalogos = new CD_CATALOGOS();
        public List<CE_CATALOGOS> FILTRAR_CATALOGOS_POR_TIPO_CATALOGO(CE_CATALOGOS obj, out int o_Num, out string o_Msg)
        {
            return CDCatalogos.FILTRAR_CATALOGOS_POR_TIPO_CATALOGO(obj, out o_Num, out o_Msg);
        }
    }
}
