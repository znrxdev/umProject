using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnMateriasPeriodos
    {
        private CdMateriasPeriodos cdMateriasPeriodos = new CdMateriasPeriodos();

        public List<CeMateriasPeriodos> ListarMateriasPeriodos(out int oNum, out string oMsg)
        {
            return cdMateriasPeriodos.ListarMateriasPeriodos(out oNum, out oMsg);
        }
    }
}

