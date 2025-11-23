using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using umForms.Utilidades;

namespace umForms.Principales.Hijos
{
    public partial class umfSolicitudesBecas : Form
    {
        private readonly CnBecasProgramas cnBecasProgramas = new CnBecasProgramas();
        private readonly CnSolicitudesBecas cnSolicitudesBecas = new CnSolicitudesBecas();

        public umfSolicitudesBecas()
        {
            InitializeComponent();
        }

        private void umfSolicitudesBecas_Load(object sender, EventArgs e)
        {
            UtFormularios.AplicarEstiloDataGridView(dgv_Solicitudes);
            CargarProgramas();
            CargarNiveles();
        }

        private void CargarProgramas()
        {
            var lista = cnBecasProgramas.ListarProgramasActivos(out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var items = lista.Select(p => new
            {
                Id = p.IdBecaPrograma,
                Nombre = $"{p.CodigoPrograma} - {p.NombrePrograma}"
            }).ToList();

            cmb_Programa.DataSource = items;
            cmb_Programa.DisplayMember = "Nombre";
            cmb_Programa.ValueMember = "Id";
        }

        private void CargarNiveles()
        {
            cmb_Nivel.Items.Clear();
            cmb_Nivel.Items.Add("Todos");
            cmb_Nivel.Items.Add("1");
            cmb_Nivel.Items.Add("2");
            cmb_Nivel.Items.Add("3");
            cmb_Nivel.Items.Add("4");
            cmb_Nivel.Items.Add("5");
            cmb_Nivel.SelectedIndex = 0;
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            if (cmb_Programa.SelectedValue == null || !(cmb_Programa.SelectedValue is int))
            {
                MessageBox.Show("Debe seleccionar un programa de beca.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPrograma = (int)cmb_Programa.SelectedValue;
            byte? nivel = null;

            if (cmb_Nivel.SelectedIndex > 0)
            {
                if (byte.TryParse(cmb_Nivel.SelectedItem.ToString(), out byte nivelParsed))
                {
                    nivel = nivelParsed;
                }
            }

            CargarSolicitudesPendientes(idPrograma, nivel);
        }

        private void CargarSolicitudesPendientes(int idPrograma, byte? nivel)
        {
            dgv_Solicitudes.Rows.Clear();
            dgv_Solicitudes.Columns.Clear();

            var lista = cnSolicitudesBecas.ListarPendientesPorProgramaYNivel(idPrograma, nivel, out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgv_Solicitudes.AutoGenerateColumns = false;
            dgv_Solicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Código",
                DataPropertyName = "CodigoSeguimiento",
                Name = "CodigoSeguimiento",
                ReadOnly = true
            });

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estudiante",
                DataPropertyName = "NombreEstudiante",
                Name = "NombreEstudiante",
                ReadOnly = true
            });

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Promedio",
                DataPropertyName = "PromedioVigente",
                Name = "PromedioVigente",
                ReadOnly = true
            });

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Créditos",
                DataPropertyName = "CreditosAprobados",
                Name = "CreditosAprobados",
                ReadOnly = true
            });

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Sanciones Activas",
                DataPropertyName = "TotalSancionesActivas",
                Name = "TotalSancionesActivas",
                ReadOnly = true
            });

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nivel Actual",
                DataPropertyName = "NivelAprobacionActual",
                Name = "NivelAprobacionActual",
                ReadOnly = true
            });

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nivel Máximo",
                DataPropertyName = "NivelAprobacionMaximo",
                Name = "NivelAprobacionMaximo",
                ReadOnly = true
            });

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "IdEstado",
                Name = "IdEstado",
                ReadOnly = true
            });

            dgv_Solicitudes.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Solicitud",
                DataPropertyName = "FechaSolicitud",
                Name = "FechaSolicitud",
                ReadOnly = true
            });

            dgv_Solicitudes.DataSource = lista;
        }
    }
}


