using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace umForms.Principales.Hijos
{
    public partial class umfMiHistorialEvaluaciones : Form
    {
        public umfMiHistorialEvaluaciones()
        {
            InitializeComponent();
        }

        private void umfMiHistorialEvaluaciones_Load(object sender, EventArgs e)
        {
            CargarEvaluaciones();
        }

        private void CargarEvaluaciones()
        {
            // TODO: Implementar carga de evaluaciones
            MessageBox.Show("Cargando evaluaciones...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

