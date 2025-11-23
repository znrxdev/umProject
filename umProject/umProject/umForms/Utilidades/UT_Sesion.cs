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
    public class UtSesion
    {
        CeUsuarios ceUsuarios = new CeUsuarios();
        CnUsuarios cnUsuarios = new CnUsuarios();
        public event Action<string> UbicacionCambiada;
        public void LlenarSesion(CeUsuarios obj)
        {
            CeSesionUsuario.UsuarioSesion = obj.Usuario;
            CeSesionUsuario.IdPersonaSesion = obj.IdPersona;
            CeSesionUsuario.IdSesion = obj.IdUsuario;
        }
        public void LimpiarSesion()
        {
            CeSesionUsuario.UsuarioSesion = "";
            CeSesionUsuario.IdPersonaSesion = 0;
            CeSesionUsuario.IdSesion = 0;
        }

        public bool IniciarSesion(string usuario, string contrasena, out int oNum, out string oMsg)
        {
            ceUsuarios.Usuario = usuario;
            ceUsuarios.Contrasena = contrasena;

            List<CeUsuarios> ltsUsuario = cnUsuarios.InicioSesion(ceUsuarios, out oNum, out oMsg);

            if (ltsUsuario != null && oNum == 0)
            {
                CeUsuarios objUsuario = new CeUsuarios();
                if (ltsUsuario.Count == 0)
                {
                    objUsuario.IdUsuario = 0;
                    oNum = -1;
                    oMsg = "¡Usuario o Contraseña incorrecta!";
                    return false;
                }

                else
                {
                    objUsuario = ltsUsuario.First();
                    LlenarSesion(objUsuario);
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        public void CargarMenus(Panel pnlBotones, Panel pnlPrincipal)
        {
            var menus = cnUsuarios.ListarMenuPorRol(out int oNum, out string oMsg);

            int topOffset = 10;

            foreach (var menu in menus)
            {
                string menuName = menu.NombreBoton;
                string menuText = menu.Menu;
                var menuItem = menu;

                var btn = new ReaLTaiizor.Controls.Button
                {
                    Name = menuName,
                    Text = menuText,
                    BackColor = Color.FromArgb(10, 28, 51),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                    Height = 48,
                    Width = pnlBotones.Width - 10,
                    Cursor = Cursors.Hand,
                    Top = topOffset,
                    Left = 5,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Tag = menuItem
                };
                btn.Click += (s, e) =>
                {
                    HandleMenuAction(menuName, menuText, pnlPrincipal);
                };
                pnlBotones.Controls.Add(btn);
                topOffset += btn.Height + 12;
            }
        }
        public void HandleMenuAction(string menuName, string menuText, Panel pnlMain)
        {
            switch (menuName)
            {
                case "btn_UsuarioMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfUsuarios());
                    UbicacionCambiada?.Invoke(menuText);
                    break;
                case "btn_MisEvaluacionesMenu":
                    // TODO: Crear umfMisEvaluaciones
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_MisMateriasMenu":
                    // TODO: Crear umfMisMaterias
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_ConvocatoriasMenu":
                    // TODO: Crear umfConvocatorias
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_MisSeccionesMenu":
                    // TODO: Crear umfMisSecciones
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_HistorialMenu":
                    // TODO: Crear umfHistorial
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_CfgSistemaMenu":
                    // TODO: Crear umfConfiguracionesSistema
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_GestionarBecasMenu":
                    // TODO: Crear umfGestionarBecas
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_MateriasMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfMaterias());
                    UbicacionCambiada?.Invoke(menuText);
                    break;
                case "btn_PeriodosMenu":
                    // TODO: Crear umfPeriodosAcademicos
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_SeccionesMenu":
                    // TODO: Crear umfSecciones
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_GruposMenu":
                    // TODO: Crear umfGrupos
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_InscripcionesMenu":
                    // TODO: Crear umfInscripciones
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_EvaluacionesMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfEvaluacionesAcademicas());
                    UbicacionCambiada?.Invoke(menuText);
                    break;
                case "btn_ProgramasBecasMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfProgramasBecas());
                    UbicacionCambiada?.Invoke(menuText);
                    break;
                case "btn_SolicitudesBecasMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfSolicitudesBecas());
                    UbicacionCambiada?.Invoke(menuText);
                    break;
                case "btn_SancionesMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfSancionesAcademicas());
                    UbicacionCambiada?.Invoke(menuText);
                    break;
                case "btn_MiHistorialMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfMiHistorial());
                    UbicacionCambiada?.Invoke(menuText);
                    break;
                case "btn_EstudiantesMenu":
                    // TODO: Crear umfEstudiantes
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_DocentesMenu":
                    // TODO: Crear umfDocentes
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_ReportesMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfReportes());
                    UbicacionCambiada?.Invoke(menuText);
                    break;// TODO: Crear umfReportes
                    MessageBox.Show($"Formulario {menuText} en desarrollo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "btn_AuditoriaMenu":
                    OpenChildForm(pnlMain, new Principales.Hijos.umfAuditoria());
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
