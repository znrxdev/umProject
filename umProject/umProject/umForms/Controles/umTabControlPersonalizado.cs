using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace umForms.Controles
{
    public partial class umTabControlPersonalizado : UserControl
    {
        private List<Button> botonesTabs;
        private List<Form> formulariosHijos;
        private int indiceSeleccionado = 0;
        private Color colorNormal = Color.FromArgb(10, 28, 51);
        private Color colorSeleccionado = Color.FromArgb(20, 50, 80);

        public umTabControlPersonalizado()
        {
            InitializeComponent();
            botonesTabs = new List<Button>();
            formulariosHijos = new List<Form>();
        }

        public void AgregarTab(string texto, Form formularioHijo)
        {
            if (formularioHijo == null) return;

            // Crear botón para el tab
            Button btnTab = new Button
            {
                Text = texto,
                Height = 40,
                Width = 200,
                BackColor = botonesTabs.Count == 0 ? colorSeleccionado : colorNormal,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = botonesTabs.Count,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            btnTab.FlatAppearance.BorderSize = 0;
            btnTab.Click += BtnTab_Click;

            botonesTabs.Add(btnTab);
            formulariosHijos.Add(formularioHijo);

            // Agregar botón al panel y posicionarlo horizontalmente
            pnl_Botones.Controls.Add(btnTab);
            ActualizarPosicionesBotones();

            // Si es el primer tab, mostrarlo
            if (botonesTabs.Count == 1)
            {
                MostrarFormulario(0);
            }
        }

        private void BtnTab_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag is int indice)
            {
                SeleccionarTab(indice);
            }
        }

        public void SeleccionarTab(int indice)
        {
            if (indice < 0 || indice >= botonesTabs.Count) return;

            // Actualizar colores de botones
            for (int i = 0; i < botonesTabs.Count; i++)
            {
                botonesTabs[i].BackColor = i == indice ? colorSeleccionado : colorNormal;
            }

            indiceSeleccionado = indice;
            MostrarFormulario(indice);
        }

        private void MostrarFormulario(int indice)
        {
            if (indice < 0 || indice >= formulariosHijos.Count) return;

            // Limpiar contenedor
            foreach (Control c in pnl_Contenedor.Controls)
            {
                if (c is Form f)
                {
                    try { f.Hide(); } catch { }
                }
                else
                {
                    try { c.Dispose(); } catch { }
                }
            }
            pnl_Contenedor.Controls.Clear();

            // Mostrar formulario seleccionado
            Form formulario = formulariosHijos[indice];
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            pnl_Contenedor.Controls.Add(formulario);
            formulario.Show();
            formulario.BringToFront();
        }

        public Form ObtenerFormularioActual()
        {
            if (indiceSeleccionado >= 0 && indiceSeleccionado < formulariosHijos.Count)
            {
                return formulariosHijos[indiceSeleccionado];
            }
            return null;
        }

        private void ActualizarPosicionesBotones()
        {
            int x = 0;
            foreach (Button btn in botonesTabs)
            {
                btn.Location = new Point(x, 0);
                x += btn.Width;
            }
        }
    }
}

