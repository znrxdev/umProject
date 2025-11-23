using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using umForms.Controles;

namespace umForms.Principales.Hijos
{
    public partial class umfMiHistorial : Form
    {
        private umTabControlPersonalizado tabControlPersonalizado;

        public umfMiHistorial()
        {
            InitializeComponent();
            InitializeCustomTabControl();
        }

        private void InitializeCustomTabControl()
        {
            tabControlPersonalizado = new umTabControlPersonalizado();
            tabControlPersonalizado.Dock = DockStyle.Fill;
            this.Controls.Add(tabControlPersonalizado);
            tabControlPersonalizado.BringToFront();

            // Agregar formularios hijos como tabs
            tabControlPersonalizado.AgregarTab("Sanciones Académicas", new umfMiHistorialSanciones());
            tabControlPersonalizado.AgregarTab("Notas/Evaluaciones", new umfMiHistorialEvaluaciones());
            tabControlPersonalizado.AgregarTab("Solicitudes de Becas", new umfMiHistorialBecas());
        }

        private void umfMiHistorial_Load(object sender, EventArgs e)
        {
            // El tab control personalizado maneja la carga inicial
        }
    }
}

