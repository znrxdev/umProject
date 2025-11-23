using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Linq;
using System.Windows.Forms;
using umForms.Flotantes;

namespace umForms.Principales.Hijos
{
    public partial class umfEvaluacionesCalificaciones : Form
    {
        CnEvaluacionesAlumnos cnEvaluacionesAlumnos = new CnEvaluacionesAlumnos();

        public umfEvaluacionesCalificaciones()
        {
            InitializeComponent();
        }

        private void umfEvaluacionesCalificaciones_Load(object sender, EventArgs e)
        {
            CargarCalificaciones();
        }

        public void CargarCalificaciones()
        {
            try
            {
                dgv_Calificaciones.Rows.Clear();
                var listaCalificaciones = cnEvaluacionesAlumnos.ListarTodasLasCalificaciones(out int oNum, out string oMsg);
                
                if (oNum == -1)
                {
                    MessageBox.Show($"Error al cargar calificaciones: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (listaCalificaciones != null && listaCalificaciones.Count > 0)
                {
                    foreach (var calificacion in listaCalificaciones)
                    {
                        dgv_Calificaciones.Rows.Add(
                            calificacion.IdEvaluacionAlumno ?? 0,
                            calificacion.CodigoRegistro ?? string.Empty,
                            !string.IsNullOrEmpty(calificacion.NombreEstudiante) ? calificacion.NombreEstudiante : "N/A",
                            !string.IsNullOrEmpty(calificacion.CodigoInstancia) ? calificacion.CodigoInstancia : "N/A",
                            calificacion.PuntajeObtenido?.ToString("F2") ?? "0.00",
                            (calificacion.PorcentajeLogrado?.ToString("F2") ?? "0.00") + "%",
                            !string.IsNullOrEmpty(calificacion.NombreEstado) ? calificacion.NombreEstado : "N/A"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar calificaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AgregarCalificacion_Click(object sender, EventArgs e)
        {
            try
            {
                umfMantenimientoEvaluacionAlumno formCalificacion = new umfMantenimientoEvaluacionAlumno(new CeEvaluacionesAlumnos());
                if (formCalificacion.ShowDialog() == DialogResult.OK)
                {
                    CargarCalificaciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir formulario de calificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ModificarCalificacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_Calificaciones.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una calificación", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idCalificacion = Convert.ToInt32(dgv_Calificaciones.CurrentRow.Cells["Id_Calificacion"].Value);
                var calificacion = cnEvaluacionesAlumnos.FiltrarEvaluacionAlumnoPorId(
                    new CeEvaluacionesAlumnos { IdEvaluacionAlumno = idCalificacion },
                    out int oNum,
                    out string oMsg);

                if (oNum == -1 || calificacion == null || calificacion.Count == 0)
                {
                    MessageBox.Show($"Error al obtener la calificación: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                umfMantenimientoEvaluacionAlumno formCalificacion = new umfMantenimientoEvaluacionAlumno(calificacion.First());
                if (formCalificacion.ShowDialog() == DialogResult.OK)
                {
                    CargarCalificaciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar calificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

