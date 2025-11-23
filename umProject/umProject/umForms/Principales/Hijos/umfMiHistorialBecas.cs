using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using umForms.Utilidades;

namespace umForms.Principales.Hijos
{
    public partial class umfMiHistorialBecas : Form
    {
        private readonly CnSolicitudesBecas cnSolicitudesBecas = new CnSolicitudesBecas();

        public umfMiHistorialBecas()
        {
            InitializeComponent();
        }

        private void umfMiHistorialBecas_Load(object sender, EventArgs e)
        {
            UtFormularios.AplicarEstiloDataGridView(dgv_Becas);
            CargarSolicitudesBecas();
        }

        private void CargarSolicitudesBecas()
        {
            dgv_Becas.Rows.Clear();
            dgv_Becas.Columns.Clear();

            var lista = cnSolicitudesBecas.ObtenerMisSolicitudes(out int oNum, out string oMsg);

            if (oNum == -1)
            {
                MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Definir columnas principales para el historial de becas del estudiante
            dgv_Becas.AutoGenerateColumns = false;
            dgv_Becas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Código",
                DataPropertyName = "CodigoSeguimiento",
                Name = "CodigoSeguimiento",
                ReadOnly = true
            });

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Programa",
                DataPropertyName = "NombrePrograma",
                Name = "NombrePrograma",
                ReadOnly = true
            });

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Convocatoria",
                DataPropertyName = "NombreConvocatoria",
                Name = "NombreConvocatoria",
                ReadOnly = true
            });

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Promedio",
                DataPropertyName = "PromedioVigente",
                Name = "PromedioVigente",
                ReadOnly = true
            });

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Créditos",
                DataPropertyName = "CreditosAprobados",
                Name = "CreditosAprobados",
                ReadOnly = true
            });

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Sanciones Activas",
                DataPropertyName = "TotalSancionesActivas",
                Name = "TotalSancionesActivas",
                ReadOnly = true
            });

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cumple Criterios",
                DataPropertyName = "CumpleCriterios",
                Name = "CumpleCriterios",
                ReadOnly = true
            });

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "IdEstado",
                Name = "IdEstado",
                ReadOnly = true
            });

            dgv_Becas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Solicitud",
                DataPropertyName = "FechaSolicitud",
                Name = "FechaSolicitud",
                ReadOnly = true
            });

            dgv_Becas.DataSource = lista;
        }
    }
}

