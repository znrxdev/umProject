using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnCatalogos
    {
        CdCatalogos cdCatalogos = new CdCatalogos();
        public List<CeCatalogos> FiltrarCatalogosPorTipoCatalogo(CeCatalogos obj, out int oNum, out string oMsg)
        {
            return cdCatalogos.FiltrarCatalogosPorTipoCatalogo(obj, out oNum, out oMsg);
        }

        public List<CeCatalogos> FiltrarCatalogoId(CeCatalogos obj, out int oNum, out string oMsg)
        {
            return cdCatalogos.FiltrarCatalogoId(obj, out oNum, out oMsg);
        }
    }
}
