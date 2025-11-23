using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using umForms.Flotantes;
using umForms.Utilidades;

namespace umForms.Principales.Hijos
{
    public partial class umfEvaluacionesModelos : Form
    {
        CnEvaluacionesModelos cnEvaluacionesModelos = new CnEvaluacionesModelos();
        UtFormularios utFormularios = new UtFormularios();

        public umfEvaluacionesModelos()
        {
            InitializeComponent();
        }

        private void umfEvaluacionesModelos_Load(object sender, EventArgs e)
        {
            CargarModelos();
        }

        public void CargarModelos()
        {
            try
            {
                var listaModelos = cnEvaluacionesModelos.ListarTodosLosModelos(out int oNum, out string oMsg);
                if (oNum == -1)
                {
                    MessageBox.Show($"Error al cargar modelos: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LlenarDgvModelos(listaModelos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar modelos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarDgvModelos(List<CeEvaluacionesModelos> listaModelos)
        {
            dgv_Modelos.Rows.Clear();
            UtFormularios util = new UtFormularios();

            foreach (var modelo in listaModelos)
            {
                string materiaPeriodo = string.Empty;
                if (!string.IsNullOrEmpty(modelo.NombreMateria) && !string.IsNullOrEmpty(modelo.NombrePeriodo))
                {
                    materiaPeriodo = $"{modelo.NombreMateria} - {modelo.NombrePeriodo}";
                }
                else if (!string.IsNullOrEmpty(modelo.NombreMateria))
                {
                    materiaPeriodo = modelo.NombreMateria;
                }

                string tipoEvaluacion = !string.IsNullOrEmpty(modelo.NombreTipoEvaluacion) 
                    ? modelo.NombreTipoEvaluacion 
                    : "N/A";

                dgv_Modelos.Rows.Add(
                    modelo.IdEvaluacionModelo ?? 0,
                    modelo.CodigoModelo ?? string.Empty,
                    modelo.NombreEvaluacion ?? string.Empty,
                    materiaPeriodo,
                    tipoEvaluacion,
                    modelo.CalificacionMaxima?.ToString("N2") ?? "0.00",
                    modelo.PesoPorcentual?.ToString("N2") ?? "0.00"
                );
            }
        }

        private void btn_AgregarModelo_Click(object sender, EventArgs e)
        {
            try
            {
                umfMantenimientoEvaluacionModelo formModelo = new umfMantenimientoEvaluacionModelo(new CeEvaluacionesModelos());
                if (formModelo.ShowDialog() == DialogResult.OK)
                {
                    CargarModelos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir formulario de modelo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ModificarModelo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Modelos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un modelo de evaluación", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idModelo = Convert.ToInt32(dgv_Modelos.CurrentRow.Cells["Id_Modelo"].Value);
                var modelo = cnEvaluacionesModelos.FiltrarEvaluacionModeloPorId(
                    new CeEvaluacionesModelos { IdEvaluacionModelo = idModelo },
                    out int oNum,
                    out string oMsg);

                if (oNum == -1 || modelo == null || modelo.Count == 0)
                {
                    MessageBox.Show($"Error al obtener el modelo: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                umfMantenimientoEvaluacionModelo formModelo = new umfMantenimientoEvaluacionModelo(modelo.First());
                if (formModelo.ShowDialog() == DialogResult.OK)
                {
                    CargarModelos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar modelo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

