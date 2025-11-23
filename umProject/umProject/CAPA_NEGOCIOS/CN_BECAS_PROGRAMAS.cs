using CAPA_DATOS;
using CAPA_ENTIDADES;
using System.Collections.Generic;

namespace CAPA_NEGOCIOS
{
    public class CnBecasProgramas
    {
        private readonly CdBecasProgramas cdBecasProgramas = new CdBecasProgramas();

        public List<CeBecasProgramas> ListarProgramasActivos(out int oNum, out string oMsg)
        {
            return cdBecasProgramas.ListarProgramasActivos(out oNum, out oMsg);
        }
    }
}


