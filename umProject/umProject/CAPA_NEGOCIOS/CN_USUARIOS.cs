using CAPA_DATOS;
using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_NEGOCIOS
{
    public class CnUsuarios
    {
        private CdUsuarios usuarios = new CdUsuarios();

        public List<CeUsuarios> InicioSesion(CeUsuarios obj, out int oNum, out string oMsg)
        {
            return usuarios.InicioSesion(obj, out oNum, out oMsg);
        }
        public List<CeUsuarios> ListarUsuarios(out int oNum, out string oMsg)
        {
            return usuarios.ListarUsuarios(out oNum, out oMsg);
        }
        public List<CeUsuarios> FiltrarUsuarioPorUsuario(CeUsuarios obj, out int oNum, out string oMsg)
        {
            return usuarios.FiltrarUsuarioPorUsuario(obj, out oNum, out oMsg);
        }
        public List<CeUsuarios> FiltrarUsuariosPorId(CeUsuarios obj, out int oNum, out string oMsg)
        {
            return usuarios.FiltrarUsuariosPorId(obj, out oNum, out oMsg);
        }
        public List<CeUsuarios> FiltrarUsuariosPorIdPersona(CeUsuarios obj, out int oNum, out string oMsg)
        {
            return usuarios.FiltrarUsuariosPorIdPersona(obj, out oNum, out oMsg);
        }
        public List<CeUsuarios> FiltrarUsuariosPorRol(int idRol, out int oNum, out string oMsg)
        {
            return usuarios.FiltrarUsuariosPorRol(idRol, out oNum, out oMsg);
        }

        public List<CeMenus> ListarMenuPorRol(out int oNum, out string oMsg)
        {
            return usuarios.ListarMenuPorRol(out oNum, out oMsg);
        }

        public void AgregarUsuarios(CeUsuarios obj, out int oNum, out string oMsg)
        {
            usuarios.AgregarUsuarios(obj, out oNum, out oMsg);
        }
        public void ActualizarUsuarios(CeUsuarios obj, out int oNum, out string oMsg)
        {
            usuarios.ActualizarUsuarios(obj, out oNum, out oMsg);
        }

        public List<CeUsuarios> ObtenerEstudiantePorDocumento(CeUsuarios obj, out int oNum, out string oMsg)
        {
            return usuarios.ObtenerEstudiantePorDocumento(obj, out oNum, out oMsg);
        }

    }
}
