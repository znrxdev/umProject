using CAPA_DATOS;
using CAPA_ENTIDADES;
using System.Collections.Generic;

namespace CAPA_NEGOCIOS
{
    public class CnSolicitudesBecas
    {
        private readonly CdSolicitudesBecas cdSolicitudes = new CdSolicitudesBecas();

        public List<CeSolicitudesBecas> ObtenerMisSolicitudes(out int oNum, out string oMsg)
        {
            return cdSolicitudes.ObtenerMisSolicitudes(out oNum, out oMsg);
        }

        public CeSolicitudesBecas EvaluarElegibilidad(int idPrograma, int idEstudiante, out int oNum, out string oMsg)
        {
            return cdSolicitudes.EvaluarElegibilidad(idPrograma, idEstudiante, out oNum, out oMsg);
        }

        public List<CeSolicitudesBecas> ListarPendientesPorProgramaYNivel(int idPrograma, byte? nivelAprobacion, out int oNum, out string oMsg)
        {
            return cdSolicitudes.ListarPendientesPorProgramaYNivel(idPrograma, nivelAprobacion, out oNum, out oMsg);
        }

        public bool RegistrarDecision(CeSolicitudesBecas obj, out int oNum, out string oMsg)
        {
            return cdSolicitudes.RegistrarDecision(obj, out oNum, out oMsg);
        }
    }
}


