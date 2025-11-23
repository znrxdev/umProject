using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnSancionesAcademicas
    {
        private CdSancionesAcademicas cdSancionesAcademicas = new CdSancionesAcademicas();

        public List<CeSancionesAcademicas> FiltrarSancionesPorId(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            return cdSancionesAcademicas.FiltrarSancionesPorId(obj, out oNum, out oMsg);
        }

        public List<CeSancionesAcademicas> FiltrarSancionesPorEstudiante(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            return cdSancionesAcademicas.FiltrarSancionesPorEstudiante(obj, out oNum, out oMsg);
        }

        public List<CeSancionesAcademicas> FiltrarSancionesPorEstudianteYEstado(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            return cdSancionesAcademicas.FiltrarSancionesPorEstudianteYEstado(obj, out oNum, out oMsg);
        }

        public List<CeSancionesAcademicas> ObtenerMisSancionesAcademicas(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            return cdSancionesAcademicas.ObtenerMisSancionesAcademicas(obj, out oNum, out oMsg);
        }

        public void ApelarSancionAcademica(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            cdSancionesAcademicas.ApelarSancionAcademica(obj, out oNum, out oMsg);
        }

        public void AgregarSancionAcademica(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            cdSancionesAcademicas.AgregarSancionAcademica(obj, out oNum, out oMsg);
        }

        public void ActualizarSancionAcademica(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            cdSancionesAcademicas.ActualizarSancionAcademica(obj, out oNum, out oMsg);
        }

        public List<CeUsuarios> ListarEstudiantesConSanciones(out int oNum, out string oMsg)
        {
            return cdSancionesAcademicas.ListarEstudiantesConSanciones(out oNum, out oMsg);
        }

        public List<CeSancionesAcademicas> ObtenerSancionesEnEsperaDeApelacion(out int oNum, out string oMsg)
        {
            return cdSancionesAcademicas.ObtenerSancionesEnEsperaDeApelacion(out oNum, out oMsg);
        }

        public void ResponderApelacion(CeSancionesAcademicas obj, out int oNum, out string oMsg)
        {
            cdSancionesAcademicas.ResponderApelacion(obj, out oNum, out oMsg);
        }
    }
}

