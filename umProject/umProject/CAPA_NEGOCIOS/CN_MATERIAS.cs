using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnMaterias
    {
        private CdMaterias cdMaterias = new CdMaterias();

        public List<CeMaterias> FiltrarMateriasPorId(CeMaterias obj, out int oNum, out string oMsg)
        {
            return cdMaterias.FiltrarMateriasPorId(obj, out oNum, out oMsg);
        }

        public List<CeMaterias> FiltrarMateriasPorCodigo(CeMaterias obj, out int oNum, out string oMsg)
        {
            return cdMaterias.FiltrarMateriasPorCodigo(obj, out oNum, out oMsg);
        }

        public List<CeMaterias> FiltrarMateriasPorNombre(CeMaterias obj, out int oNum, out string oMsg)
        {
            return cdMaterias.FiltrarMateriasPorNombre(obj, out oNum, out oMsg);
        }

        public void AgregarMaterias(CeMaterias obj, out int oNum, out string oMsg)
        {
            cdMaterias.AgregarMaterias(obj, out oNum, out oMsg);
        }

        public void ActualizarMaterias(CeMaterias obj, out int oNum, out string oMsg)
        {
            cdMaterias.ActualizarMaterias(obj, out oNum, out oMsg);
        }
    }
}

