using CAPA_DATOS;
using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umForms.Utilidades
{
    public class UT_Formularios
    {

        public List<CE_USUARIOS> Obtener_Datos_Usuario_Id(int Id_Usuario)
        {
            List<CE_USUARIOS> UsuarioInfo = new List<CE_USUARIOS>();
            if (Id_Usuario != 0)
            {
                UsuarioInfo = new CN_USUARIOS().FILTRAR_USUARIOS_POR_ID(new CE_USUARIOS { Id_Usuario = Id_Usuario }, out int o_Num, out string o_Msg);
            }
            return UsuarioInfo;
        }
        public List<CE_USUARIOS_ROLES> Obtener_Permisos_Usuario(int? Id_Usuario)
        {
            List<CE_USUARIOS_ROLES> PrmUsuario = new List<CE_USUARIOS_ROLES>();
            if (Id_Usuario != 0)
            {
                PrmUsuario = new CN_USUARIOS_ROLES().LISTAR_ROLES_DE_USUARIO(new CE_USUARIOS_ROLES { Id_Usuario = Id_Usuario }, out int o_Num, out string o_Msg);
            }
            return PrmUsuario;
        }
        public List<CE_USUARIOS> Obtener_Datos_Usuario_Id_Persona(int? Id_Persona)
        {
            List<CE_USUARIOS> UsuarioInfo = new List<CE_USUARIOS>();
            if (Id_Persona != 0)
            {
                UsuarioInfo = new CN_USUARIOS().FILTRAR_USUARIOS_POR_ID_PERSONA(new CE_USUARIOS { Id_Persona = Id_Persona }, out int o_Num, out string o_Msg);
                if (UsuarioInfo.Count == 0)
                {
                    CE_USUARIOS ObjVacio = new CE_USUARIOS { Id_Usuario = 0 };
                    UsuarioInfo.Add(ObjVacio);
                }
            }      
            return UsuarioInfo;
        }
        public List<CE_PERSONAS> BUSCAR_PERSONA_ID(int Id_Persona, out int o_Num, out string o_Msg)
        {
            List<CE_PERSONAS> lts_personas = new CN_PERSONAS().FILTRAR_PERSONAS_POR_ID(new CE_PERSONAS { Id_Persona = Id_Persona }, out o_Num, out o_Msg);
            if (o_Num == 0 && lts_personas.Count > 0)
            {
                return lts_personas;
            }
            else
            {
                CE_PERSONAS ObjVacio = new CE_PERSONAS();
                lts_personas.Add(ObjVacio);
                return lts_personas;
            }
        }

        public List<CE_ROLES> FILTRAR_ROLES_ID(int Id_Rol)
        {
            List<CE_ROLES> lts_roles = new CN_ROLES().FILTRAR_ROLES_ID(new CE_ROLES { Id_Rol = Id_Rol }, out int o_Num, out string o_Msg);
            if (o_Num == 0 && lts_roles.Count > 0)
            {
                return lts_roles;
            }
            else
            {
                CE_ROLES ObjVacio = new CE_ROLES();
                lts_roles.Add(ObjVacio);
                return lts_roles;
            }
        }
        public string Obtener_Nombre_Estado(int Id_Estado)
        {
            string Nombre_Estado = string.Empty;
            if (Id_Estado != 0)
            {
                List<CE_ESTADOS> EstadoInfo = new CN_ESTADOS().FILTRAR_ESTADOS_POR_ID(new CE_ESTADOS() { Id_Estado = Id_Estado }, out int o_Num, out string o_Msg);
                foreach (var estado in EstadoInfo)
                {
                    Nombre_Estado = estado.Nombre_Estado;
                }
            }
            else
            {
                Nombre_Estado = "N/A";
            }
            return Nombre_Estado;
        }

        public List<CE_PERSONAS> BUSCAR_PERSONA_DOCUMENTO(string ValorDocumento, out int o_Num, out string o_Msg)
        {
            List<CE_PERSONAS> lts_personas = new CN_PERSONAS().FILTRAR_PERSONAS_POR_NUMERO_DOCUMENTO(new CE_PERSONAS { Valor_Documento = ValorDocumento }, out o_Num, out o_Msg);
            if (o_Num == 0 && lts_personas.Count > 0)
            {
                return lts_personas;
            }
            else
            {
                CE_PERSONAS ObjVacio = new CE_PERSONAS();
                lts_personas.Add(ObjVacio);
                return lts_personas;
            }
        }

        public void CARGAR_CMB_ESTADO(int Id_Tipo_Transaccion, ComboBox cbo, out int o_Num, out string o_Msg)
        {
            cbo.DataSource = new CN_ESTADOS().FILTRAR_ESTADOS_POR_TIPO_TRANSACCION(new CE_ESTADOS { Id_Tipo_Transaccion = Id_Tipo_Transaccion }, out o_Num, out o_Msg);
            cbo.ValueMember = "Id_Estado";
            cbo.DisplayMember = "Nombre_Estado";
        }
        public void CARGAR_CMB_CATALOGOS(int Id_Tipo_Catalogo, ComboBox cbo, out int o_Num, out string o_Msg)
        {
            cbo.DataSource = new CN_CATALOGOS().FILTRAR_CATALOGOS_POR_TIPO_CATALOGO(new CE_CATALOGOS { Id_Tipo_Catalogo = Id_Tipo_Catalogo }, out o_Num, out o_Msg);
            cbo.ValueMember = "Id_Catalogo";
            cbo.DisplayMember = "Nombre_Catalogo";
        }
        public void CARGAR_CMB_ROLES(ComboBox cbo, out int o_Num, out string o_Msg)
        {
            cbo.DataSource = new CN_ROLES().LISTAR_ROLES(out o_Num, out o_Msg);
            cbo.ValueMember = "Id_Rol";
            cbo.DisplayMember = "Nombre_Rol";
        }
    }
}
