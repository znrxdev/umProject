using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using umForms.Principales;

namespace umForms.Utilidades
{
    public class UT_Sesion
    {
        CE_USUARIOS CEUsuarios = new CE_USUARIOS();
        CN_USUARIOS CNUsuarios = new CN_USUARIOS();
        public event Action<string> UbicacionCambiada;
        public void LLENAR_SESION(CE_USUARIOS Obj)
        {
            CE_SESION_USUARIO.Usuario_Sesion = Obj.Usuario;
            CE_SESION_USUARIO.Id_Persona_Sesion = Obj.Id_Persona;
            CE_SESION_USUARIO.Id_Sesion = Obj.Id_Usuario;
        }
        public void LIMPIAR_SESION()
        {
            CE_SESION_USUARIO.Usuario_Sesion = "";
            CE_SESION_USUARIO.Id_Persona_Sesion = 0;
            CE_SESION_USUARIO.Id_Sesion = 0;
        }

        public bool INICIAR_SESION(string _Usuario, string _Contrasena, out int o_Num, out string o_Msg)
        {
            CEUsuarios.Usuario = _Usuario;
            CEUsuarios.Contrasena = _Contrasena;

            List<CE_USUARIOS> lts_usuario = CNUsuarios.INICIO_SESION(CEUsuarios, out o_Num, out o_Msg);

            if (lts_usuario != null && o_Num == 0)
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

        public void CARGAR_MENUS(Panel pnl_Botones, Panel pnl_Principal)
        {
            var menus = CNUsuarios.LISTAR_MENU_POR_ROL(out int o_Num, out string o_Msg);

            int topOffset = 10;

            foreach (var menu in menus)
            {
                string menuName = menu.Nombre_Boton;
                string menuText = menu.Menu;
                var menuItem = menu;

                var btn = new ReaLTaiizor.Controls.CrownButton
                {
                    Name = menuName,
                    Text = menuText,
                    BackColor = System.Drawing.Color.Gray,
                    Height = 35,
                    Width = pnl_Botones.Width,
                    Cursor = Cursors.Hand,
                    Top = topOffset,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Tag = menuItem 
                };
                btn.Click += (s, e) =>
                {
                    HandleMenuAction(menuName,menuText, pnl_Principal);
                };
                pnl_Botones.Controls.Add(btn);
                topOffset += btn.Height + 5;
            }
        }
        public void HandleMenuAction(string menuName,string menuText, Panel pnlMain)
        {
            switch (menuName)
            {
                case "btn_UsuarioMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfUsuarios());
                    UbicacionCambiada?.Invoke(menuText);
                    break;
                default:
                    MessageBox.Show($"Acción no implementada para {menuText}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// Abre un Form dentro de un Panel.
        /// </summary>
        public void OpenChildForm(Panel pnlMain, Form child)
        {
            if (pnlMain == null || child == null) return;

            try
            {
                foreach (Control c in pnlMain.Controls)
                {
                    if (c is Form f)
                    {
                        try { f.Close(); f.Dispose(); } catch { }
                    }
                    else
                    {
                        try { c.Dispose(); } catch { }
                    }
                }
                pnlMain.Controls.Clear();
                child.TopLevel = false;
                child.FormBorderStyle = FormBorderStyle.None;
                child.Dock = DockStyle.Fill;

                pnlMain.Controls.Add(child);
                pnlMain.Tag = child;

                child.Show();
                child.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir formulario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
