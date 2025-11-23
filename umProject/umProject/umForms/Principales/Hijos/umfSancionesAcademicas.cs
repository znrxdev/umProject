using CAPA_ENTIDADES;
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
using umForms.Flotantes;
using umForms.Utilidades;

namespace umForms.Principales.Hijos
{
    public partial class umfSancionesAcademicas : Form
    {
        CeSancionesAcademicas ceSancionesAcademicas = new CeSancionesAcademicas();
        CnSancionesAcademicas cnSancionesAcademicas = new CnSancionesAcademicas();
        UtFormularios utFormularios = new UtFormularios();

        public umfSancionesAcademicas()
        {
            InitializeComponent();
        }

        private void umfSancionesAcademicas_Load(object sender, EventArgs e)
        {
            CargarEstudiantes();
        }

        private void CargarEstudiantes()
        {
            var listaEstudiantes = cnSancionesAcademicas.ListarEstudiantesConSanciones(out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar que la lista no esté vacía
            if (listaEstudiantes == null || listaEstudiantes.Count == 0)
            {
                cmb_Estudiante.DataSource = null;
                cmb_Estudiante.Items.Clear();
                return;
            }

            // Deshabilitar temporalmente el evento SelectedIndexChanged para evitar conflictos
            cmb_Estudiante.SelectedIndexChanged -= cmb_Estudiante_SelectedIndexChanged;

            try
            {
                // Limpiar el DataSource primero
                cmb_Estudiante.DataSource = null;
                cmb_Estudiante.Items.Clear();
                
                // Establecer ValueMember y DisplayMember antes de asignar el DataSource
                cmb_Estudiante.ValueMember = "IdUsuario";
                cmb_Estudiante.DisplayMember = "NombreCompleto";
                
                // Asignar el DataSource después de configurar las propiedades
                cmb_Estudiante.DataSource = listaEstudiantes;
                cmb_Estudiante.SelectedIndex = -1;
            }
            finally
            {
                // Rehabilitar el evento SelectedIndexChanged
                cmb_Estudiante.SelectedIndexChanged += cmb_Estudiante_SelectedIndexChanged;
            }
        }

        private void CargarSanciones()
        {
            List<CeSancionesAcademicas> listaSanciones;

            // Si el checkbox de filtrar apelaciones está marcado, mostrar solo apelaciones en espera
            if (ckb_FiltrarApelaciones.Checked)
            {
                listaSanciones = cnSancionesAcademicas.ObtenerSancionesEnEsperaDeApelacion(
                    out int oNum,
                    out string oMsg);

                if (oNum == -1)
                {
                    MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ocultar controles de estudiante y estado cuando se filtra por apelaciones
                cmb_Estudiante.Enabled = false;
                cmb_Estado.Visible = false;
                lb_Estado.Visible = false;
            }
            else
            {
                // Restaurar controles de estudiante
                cmb_Estudiante.Enabled = true;

                if (cmb_Estudiante.SelectedValue == null || cmb_Estudiante.SelectedIndex == -1)
                {
                    dgv_SancionesAcademicas.Rows.Clear();
                    cmb_Estado.Visible = false;
                    lb_Estado.Visible = false;
                    return;
                }

                int idEstudiante = Convert.ToInt32(cmb_Estudiante.SelectedValue);

                // Si hay un estado seleccionado, filtrar por estudiante y estado
                if (cmb_Estado.SelectedValue != null && cmb_Estado.SelectedIndex != -1)
                {
                    // Obtener el IdEstado del objeto CeEstados seleccionado
                    int? idEstado;
                    if (cmb_Estado.SelectedValue is CeEstados estadoObj)
                    {
                        idEstado = estadoObj.IdEstado;
                    }
                    else
                    {
                        idEstado = Convert.ToInt32(cmb_Estado.SelectedValue);
                    }

                    listaSanciones = cnSancionesAcademicas.FiltrarSancionesPorEstudianteYEstado(
                        new CeSancionesAcademicas { IdEstudiante = idEstudiante, IdEstado = idEstado },
                        out int oNum,
                        out string oMsg);

                    if (oNum == -1)
                    {
                        MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Si no hay estado seleccionado, filtrar solo por estudiante
                    listaSanciones = cnSancionesAcademicas.FiltrarSancionesPorEstudiante(
                        new CeSancionesAcademicas { IdEstudiante = idEstudiante },
                        out int oNum,
                        out string oMsg);

                    if (oNum == -1)
                    {
                        MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            LlenarDgvSanciones(listaSanciones);
        }

        private void LlenarDgvSanciones(List<CeSancionesAcademicas> listaSanciones)
        {
            dgv_SancionesAcademicas.Rows.Clear();
            UtFormularios util = new UtFormularios();

            foreach (var sancion in listaSanciones)
            {
                string tipoSancion = sancion.IdTipoSancion.HasValue ? util.ObtenerNombreCatalogo(sancion.IdTipoSancion.Value) : "N/A";
                string severidad = sancion.IdSeveridad.HasValue ? util.ObtenerNombreCatalogo(sancion.IdSeveridad.Value) : "N/A";
                string estado = sancion.IdEstado.HasValue ? util.ObtenerNombreEstado(sancion.IdEstado.Value) : "N/A";
                string estudiante = sancion.IdEstudiante.HasValue ? util.ObtenerDatosUsuarioId(sancion.IdEstudiante.Value).FirstOrDefault()?.Usuario ?? "N/A" : "N/A";

                dgv_SancionesAcademicas.Rows.Add(
                    sancion.IdSancion,
                    sancion.CodigoSancion ?? "N/A",
                    estudiante,
                    tipoSancion,
                    severidad,
                    sancion.FechaRegistro?.ToString("dd/MM/yyyy") ?? "N/A",
                    sancion.FechaFin?.ToString("dd/MM/yyyy") ?? "N/A",
                    estado,
                    sancion.Motivo ?? "N/A"
                );
            }
        }

        private void btn_AgregarSancion_Click(object sender, EventArgs e)
        {
            umfMantenimientoSancionAcademica umfMantenimiento = new umfMantenimientoSancionAcademica(new CeSancionesAcademicas { IdSancion = 0 });
            umfMantenimiento.ShowDialog();
            CargarSanciones();
        }

        private void cmb_Estudiante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Estudiante.SelectedValue != null && cmb_Estudiante.SelectedIndex != -1)
            {
                // Mostrar el ComboBox de estados cuando se selecciona un estudiante
                cmb_Estado.Visible = true;
                lb_Estado.Visible = true;
                CargarEstados();
            }
            else
            {
                // Ocultar el ComboBox de estados cuando no hay estudiante seleccionado
                cmb_Estado.Visible = false;
                lb_Estado.Visible = false;
                cmb_Estado.DataSource = null;
                cmb_Estado.Items.Clear();
            }
            CargarSanciones();
        }

        private void CargarEstados()
        {
            // Deshabilitar temporalmente el evento SelectedIndexChanged para evitar conflictos
            cmb_Estado.SelectedIndexChanged -= cmb_Estado_SelectedIndexChanged;

            try
            {
                // Limpiar el DataSource primero
                cmb_Estado.DataSource = null;
                cmb_Estado.Items.Clear();

                // Cargar estados usando la transacción 135 (FILTRAR SANCIÓN POR ESTUDIANTE Y ESTADO)
                utFormularios.CargarCmbEstado(135, cmb_Estado, out int oNum, out string oMsg);
                if (oNum == -1)
                {
                    MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Asegurar que ValueMember y DisplayMember estén configurados
                cmb_Estado.ValueMember = "IdEstado";
                cmb_Estado.DisplayMember = "NombreEstado";
                cmb_Estado.SelectedIndex = -1;
            }
            finally
            {
                // Rehabilitar el evento SelectedIndexChanged
                cmb_Estado.SelectedIndexChanged += cmb_Estado_SelectedIndexChanged;
            }
        }

        private void cmb_Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSanciones();
        }

        private void btn_ModificarSancion_Click(object sender, EventArgs e)
        {
            if (dgv_SancionesAcademicas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Para modificar necesita seleccionar una sanción de la lista", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idSancion = Convert.ToInt32(dgv_SancionesAcademicas.CurrentRow.Cells[0].Value);
                var sancion = cnSancionesAcademicas.FiltrarSancionesPorId(new CeSancionesAcademicas { IdSancion = idSancion }, out int oNum, out string oMsg);
                if (oNum == 0 && sancion.Count > 0)
                {
                    umfMantenimientoSancionAcademica umfMantenimiento = new umfMantenimientoSancionAcademica(sancion.First());
                    umfMantenimiento.ShowDialog();
                    CargarSanciones();
                }
                else
                {
                    MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ckb_FiltrarApelaciones_CheckedChanged(object sender, EventArgs e)
        {
            CargarSanciones();
        }

        private void btn_ResponderApelacion_Click(object sender, EventArgs e)
        {
            if (dgv_SancionesAcademicas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una sanción con apelación pendiente de la lista", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idSancion = Convert.ToInt32(dgv_SancionesAcademicas.CurrentRow.Cells[0].Value);
            string codigoSancion = dgv_SancionesAcademicas.CurrentRow.Cells[1].Value?.ToString() ?? "N/A";

            // Verificar que la sanción tenga una apelación pendiente
            var sancion = cnSancionesAcademicas.FiltrarSancionesPorId(new CeSancionesAcademicas { IdSancion = idSancion }, out int oNum, out string oMsg);
            if (oNum == 0 && sancion.Count > 0)
            {
                var sancionSeleccionada = sancion.First();
                if (sancionSeleccionada.FechaApelacion == null || sancionSeleccionada.IdEstado != 4 || sancionSeleccionada.ResultadoApelacion != null)
                {
                    MessageBox.Show("La sanción seleccionada no tiene una apelación pendiente de revisión.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                umfResponderApelacion formResponder = new umfResponderApelacion(idSancion, codigoSancion, sancionSeleccionada.ObservacionesApelacion ?? string.Empty);
                if (formResponder.ShowDialog() == DialogResult.OK)
                {
                    CargarSanciones();
                }
            }
            else
            {
                MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

