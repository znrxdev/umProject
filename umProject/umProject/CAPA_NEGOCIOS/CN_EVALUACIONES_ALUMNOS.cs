using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnEvaluacionesAlumnos
    {
        private CdEvaluacionesAlumnos cdEvaluacionesAlumnos = new CdEvaluacionesAlumnos();

        public List<CeEvaluacionesAlumnos> FiltrarEvaluacionAlumnoPorId(CeEvaluacionesAlumnos obj, out int oNum, out string oMsg)
        {
            return cdEvaluacionesAlumnos.FiltrarEvaluacionAlumnoPorId(obj, out oNum, out oMsg);
        }

        public List<CeEvaluacionesAlumnos> FiltrarEvaluacionAlumnoPorInstancia(CeEvaluacionesAlumnos obj, out int oNum, out string oMsg)
        {
            return cdEvaluacionesAlumnos.FiltrarEvaluacionAlumnoPorInstancia(obj, out oNum, out oMsg);
        }

        public List<CeEvaluacionesAlumnos> ObtenerMisEvaluacionesPublicadas(out int oNum, out string oMsg)
        {
            return cdEvaluacionesAlumnos.ObtenerMisEvaluacionesPublicadas(out oNum, out oMsg);
        }

        public void AgregarEvaluacionAlumno(CeEvaluacionesAlumnos obj, out int oNum, out string oMsg)
        {
            cdEvaluacionesAlumnos.AgregarEvaluacionAlumno(obj, out oNum, out oMsg);
        }

        public void ActualizarEvaluacionAlumno(CeEvaluacionesAlumnos obj, out int oNum, out string oMsg)
        {
            cdEvaluacionesAlumnos.ActualizarEvaluacionAlumno(obj, out oNum, out oMsg);
        }

        public List<CeEvaluacionesAlumnos> ListarTodasLasCalificaciones(out int oNum, out string oMsg)
        {
            return cdEvaluacionesAlumnos.ListarTodasLasCalificaciones(out oNum, out oMsg);
        }
    }
}

