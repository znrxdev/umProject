using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace umForms.Utilidades
{
    public class UT_Sesion
    {
        CE_USUARIOS CEUsuarios = new CE_USUARIOS();
        CN_USUARIOS CNUsuarios = new CN_USUARIOS();

        public void LLENAR_SESION(CE_USUARIOS Obj)
        {
            CE_SESION_USUARIO.Usuario_Sesion = Obj.Usuario;
            CE_SESION_USUARIO.Id_Persona_Sesion = Obj.Id_Persona;
            CE_SESION_USUARIO.Id_Sesion = Obj.Id_Usuario;
        }

        public bool INICIAR_SESION(string _Usuario, string _Contrasena, out int o_Num, out string o_Msg)
        {
            CEUsuarios.Usuario = _Usuario;
            CEUsuarios.Contrasena = _Contrasena;

            List<CE_USUARIOS> lts_usuario = CNUsuarios.INICIO_SESION(CEUsuarios, out o_Num, out o_Msg);

            if (lts_usuario != null && lts_usuario.Count > 0)
            {
                var user = lts_usuario.First();
                LLENAR_SESION(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CARGAR_MENUS(Panel pnl_Botones)
        {
            var menus = CNUsuarios.LISTAR_MENU_POR_ROL(out int o_Num, out string o_Msg);
            int topOffset = 10;

            foreach (var menu in menus)
            {
                var btn = new ReaLTaiizor.Controls.CrownButton
                {
                    Name = menu.Nombre_Boton,
                    Text = menu.Menu,
                    BackColor = System.Drawing.Color.Gray,
                    Height = 25,
                    Width = pnl_Botones.Width - 10,
                    Cursor = Cursors.Hand,
                    Top = topOffset,
                    Left = 5
                };

                btn.Click += (s, e) =>
                {

                    switch (btn.Name)
                    {
                        default:
                            break;
                    }
                };

                pnl_Botones.Controls.Add(btn);
                topOffset += btn.Height + 5;
            }
        }
    }
}
