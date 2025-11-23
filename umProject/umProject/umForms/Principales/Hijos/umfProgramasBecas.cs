using CAPA_NEGOCIOS;
using System;
using System.Windows.Forms;
using umForms.Utilidades;

namespace umForms.Principales.Hijos
{
    public partial class umfProgramasBecas : Form
    {
        private readonly CnBecasProgramas cnBecasProgramas = new CnBecasProgramas();

        public umfProgramasBecas()
        {
            InitializeComponent();
        }

        private void umfProgramasBecas_Load(object sender, EventArgs e)
        {
            UtFormularios.AplicarEstiloDataGridView(dgv_Programas);
            CargarProgramas();
        }

        private void CargarProgramas()
        {
            dgv_Programas.Rows.Clear();
            dgv_Programas.Columns.Clear();

            var lista = cnBecasProgramas.ListarProgramasActivos(out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgv_Programas.AutoGenerateColumns = false;
            dgv_Programas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv_Programas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Código",
                DataPropertyName = "CodigoPrograma",
                Name = "CodigoPrograma",
                ReadOnly = true
            });

            dgv_Programas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre Programa",
                DataPropertyName = "NombrePrograma",
                Name = "NombrePrograma",
                ReadOnly = true
            });

            dgv_Programas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Name = "Descripcion",
                ReadOnly = true
            });

            dgv_Programas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Promedio Mínimo",
                DataPropertyName = "PromedioMinimo",
                Name = "PromedioMinimo",
                ReadOnly = true
            });

            dgv_Programas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Créditos Mínimos",
                DataPropertyName = "CreditosMinimos",
                Name = "CreditosMinimos",
                ReadOnly = true
            });

            dgv_Programas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Niveles Aprobación",
                DataPropertyName = "NivelesAprobacion",
                Name = "NivelesAprobacion",
                ReadOnly = true
            });

            dgv_Programas.DataSource = lista;
        }
    }
}


