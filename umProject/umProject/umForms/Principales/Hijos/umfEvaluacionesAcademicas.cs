using System;
using System.Windows.Forms;
using umForms.Controles;
using umForms.Principales.Hijos;

namespace umForms.Principales.Hijos
{
    public partial class umfEvaluacionesAcademicas : Form
    {
        private umTabControlPersonalizado tabControlPersonalizado;
        private umfEvaluacionesModelos formModelos;
        private umfEvaluacionesInstancias formInstancias;
        private umfEvaluacionesCalificaciones formCalificaciones;

        public umfEvaluacionesAcademicas()
        {
            InitializeComponent();
        }

        private void umfEvaluacionesAcademicas_Load(object sender, EventArgs e)
        {
            // Crear formularios hijos
            formModelos = new umfEvaluacionesModelos();
            formInstancias = new umfEvaluacionesInstancias();
            formCalificaciones = new umfEvaluacionesCalificaciones();

            // Configurar el control personalizado
            tabControlPersonalizado = new umTabControlPersonalizado();
            tabControlPersonalizado.Dock = DockStyle.Fill;
            tabControlPersonalizado.Location = new Point(0, 0);
            tabControlPersonalizado.Name = "tabControlPersonalizado";
            tabControlPersonalizado.Size = new Size(826, 407);

            // Agregar tabs
            tabControlPersonalizado.AgregarTab("Modelos de Evaluación", formModelos);
            tabControlPersonalizado.AgregarTab("Instancias de Evaluación", formInstancias);
            tabControlPersonalizado.AgregarTab("Calificaciones", formCalificaciones);

            // Agregar el control al formulario
            Controls.Add(tabControlPersonalizado);
            tabControlPersonalizado.BringToFront();
        }
    }
}
