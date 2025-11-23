using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Windows.Forms;
using umForms.Flotantes;

namespace umForms.Principales.Hijos
{
    public partial class umfEvaluacionesInstancias : Form
    {
        CnEvaluacionesInstancias cnEvaluacionesInstancias = new CnEvaluacionesInstancias();

        public umfEvaluacionesInstancias()
        {
            InitializeComponent();
        }

        private void umfEvaluacionesInstancias_Load(object sender, EventArgs e)
        {
            CargarInstancias();
        }

        public void CargarInstancias()
        {
            try
            {
                dgv_Instancias.Rows.Clear();
                var listaInstancias = cnEvaluacionesInstancias.ListarTodasLasInstancias(out int oNum, out string oMsg);
                
                if (oNum == -1)
                {
                    MessageBox.Show($"Error al cargar instancias: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (listaInstancias != null && listaInstancias.Count > 0)
                {
                    foreach (var instancia in listaInstancias)
                    {
                        dgv_Instancias.Rows.Add(
                            instancia.IdEvaluacionInstancia ?? 0,
                            instancia.CodigoInstancia ?? string.Empty,
                            !string.IsNullOrEmpty(instancia.CodigoSeccion) ? instancia.CodigoSeccion : "N/A",
                            !string.IsNullOrEmpty(instancia.NombreModeloEvaluacion) ? instancia.NombreModeloEvaluacion : "N/A",
                            instancia.FechaProgramada?.ToString("dd/MM/yyyy HH:mm") ?? "N/A",
                            instancia.FechaLimite?.ToString("dd/MM/yyyy HH:mm") ?? "N/A",
                            !string.IsNullOrEmpty(instancia.NombreEstado) ? instancia.NombreEstado : "N/A"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar instancias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AgregarInstancia_Click(object sender, EventArgs e)
        {
            try
            {
                umfMantenimientoEvaluacionInstancia formInstancia = new umfMantenimientoEvaluacionInstancia(new CeEvaluacionesInstancias());
                if (formInstancia.ShowDialog() == DialogResult.OK)
                {
                    CargarInstancias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir formulario de instancia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ModificarInstancia_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Instancias.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una instancia de evaluación", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idInstancia = Convert.ToInt32(dgv_Instancias.CurrentRow.Cells["Id_Instancia"].Value);
                var instancia = cnEvaluacionesInstancias.FiltrarEvaluacionInstanciaPorId(
                    new CeEvaluacionesInstancias { IdEvaluacionInstancia = idInstancia },
                    out int oNum,
                    out string oMsg);

                if (oNum == -1 || instancia == null || instancia.Count == 0)
                {
                    MessageBox.Show($"Error al obtener la instancia: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                umfMantenimientoEvaluacionInstancia formInstancia = new umfMantenimientoEvaluacionInstancia(instancia.First());
                if (formInstancia.ShowDialog() == DialogResult.OK)
                {
                    CargarInstancias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar instancia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

