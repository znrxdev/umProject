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
using umForms.Utilidades;

namespace umForms.Flotantes
{
    public partial class umfMantenimientoEvaluacionAlumno : Form
    {
        CeEvaluacionesAlumnos datosCalificacion = new CeEvaluacionesAlumnos();
        CnEvaluacionesAlumnos cnEvaluacionesAlumnos = new CnEvaluacionesAlumnos();
        UtFormularios utFormularios = new UtFormularios();

        public umfMantenimientoEvaluacionAlumno(CeEvaluacionesAlumnos datosCalificacion)
        {
            InitializeComponent();
            this.datosCalificacion = datosCalificacion;
            bool esModificar = datosCalificacion.IdEvaluacionAlumno.HasValue && datosCalificacion.IdEvaluacionAlumno.Value != 0;
            lb_Titulo.Text = esModificar ? "MODIFICAR CALIFICACIÓN" : "AGREGAR CALIFICACIÓN";
        }

        private void umfMantenimientoEvaluacionAlumno_Load(object sender, EventArgs e)
        {
            CargarCombos();
            this.BeginInvoke(new Action(() => CargarDatosCalificacion(this.datosCalificacion)));
        }

        private void CargarCombos()
        {
            bool esModificar = datosCalificacion.IdEvaluacionAlumno.HasValue && datosCalificacion.IdEvaluacionAlumno.Value != 0;

            // Cargar estados
            utFormularios.CargarCmbEstado(esModificar ? 129 : 128, cmb_Estado, out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar estados: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar estados de publicación
            utFormularios.CargarCmbEstado(128, cmb_EstadoPublicacion, out oNum, out oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar estados de publicación: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar instancias de evaluación para seleccionar a cuál pertenece la calificación
            var listaInstancias = new CnEvaluacionesInstancias().ListarTodasLasInstancias(out oNum, out oMsg);
            if (oNum == 0)
            {
                var listaConDisplay = listaInstancias.Select(i => new
                {
                    IdEvaluacionInstancia = i.IdEvaluacionInstancia,
                    DisplayText = $"{i.CodigoInstancia} - {(string.IsNullOrEmpty(i.CodigoSeccion) ? "SIN SECCIÓN" : i.CodigoSeccion)}"
                }).ToList();

                cmb_InstanciaEvaluacion.DataSource = listaConDisplay;
                cmb_InstanciaEvaluacion.ValueMember = "IdEvaluacionInstancia";
                cmb_InstanciaEvaluacion.DisplayMember = "DisplayText";
            }
            else
            {
                cmb_InstanciaEvaluacion.DataSource = null;
                if (!string.IsNullOrEmpty(oMsg))
                {
                    MessageBox.Show($"Error al cargar instancias de evaluación: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarDatosCalificacion(CeEvaluacionesAlumnos datosCalificacion)
        {
            bool esModificar = datosCalificacion.IdEvaluacionAlumno.HasValue && datosCalificacion.IdEvaluacionAlumno.Value != 0;

            if (esModificar)
            {
                txt_CodigoRegistro.Text = datosCalificacion.CodigoRegistro ?? string.Empty;
                num_PuntajeObtenido.Value = datosCalificacion.PuntajeObtenido ?? 0;
                num_PorcentajeLogrado.Value = datosCalificacion.PorcentajeLogrado ?? 0;
                txt_Observaciones.Text = datosCalificacion.Observaciones ?? string.Empty;
                chk_EsRecalculo.Checked = datosCalificacion.EsRecalculo ?? false;
                num_NumeroRecalculo.Value = datosCalificacion.NumeroRecalculo ?? 0;
                txt_MotivoAjuste.Text = datosCalificacion.MotivoAjuste ?? string.Empty;
                chk_FirmadoPorEstudiante.Checked = datosCalificacion.FirmadoPorEstudiante ?? false;
                
                if (datosCalificacion.FechaValidacion.HasValue)
                {
                    dtp_FechaValidacion.Value = datosCalificacion.FechaValidacion.Value;
                    chk_FechaValidacion.Checked = true;
                }
                if (datosCalificacion.FechaPublicacion.HasValue)
                {
                    dtp_FechaPublicacion.Value = datosCalificacion.FechaPublicacion.Value;
                    chk_FechaPublicacion.Checked = true;
                }
                
                EstablecerValorComboBox(cmb_Estado, datosCalificacion.IdEstado);
                EstablecerValorComboBox(cmb_EstadoPublicacion, datosCalificacion.IdEstadoPublicacion);

                // Preseleccionar instancia e inscripción si es posible
                if (datosCalificacion.IdEvaluacionInstancia.HasValue)
                {
                    EstablecerValorComboBoxGenerico(cmb_InstanciaEvaluacion, "IdEvaluacionInstancia", datosCalificacion.IdEvaluacionInstancia.Value);
                    CargarInscripcionesParaInstancia(datosCalificacion.IdEvaluacionInstancia.Value);

                    if (datosCalificacion.IdInscripcion.HasValue)
                    {
                        EstablecerValorComboBoxGenerico(cmb_Inscripcion, "IdInscripcion", datosCalificacion.IdInscripcion.Value);
                    }
                }
            }
        }

        private void EstablecerValorComboBox(ComboBox comboBox, int? valor)
        {
            if (comboBox == null || !valor.HasValue || valor.Value == 0)
            {
                return;
            }

            try
            {
                if (comboBox.Items.Count == 0)
                {
                    return;
                }

                if (string.IsNullOrEmpty(comboBox.ValueMember))
                {
                    if (comboBox.Items.Count > 0)
                    {
                        var firstItem = comboBox.Items[0];
                        if (firstItem is CeEstados)
                        {
                            comboBox.ValueMember = "IdEstado";
                            comboBox.DisplayMember = "NombreEstado";
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                try
                {
                    comboBox.SelectedValue = valor.Value;
                    if (comboBox.SelectedValue != null)
                    {
                        int selectedValue = Convert.ToInt32(comboBox.SelectedValue);
                        if (selectedValue == valor.Value)
                        {
                            return;
                        }
                    }
                }
                catch { }

                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    var item = comboBox.Items[i];
                    int? itemValue = null;

                    if (item is CeEstados estado)
                    {
                        itemValue = estado.IdEstado;
                    }

                    if (itemValue.HasValue && itemValue.Value == valor.Value)
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                }
            }
            catch { }
        }

        private void btn_GuardarDatos_Click(object sender, EventArgs e)
        {
            bool esModificar = datosCalificacion.IdEvaluacionAlumno.HasValue && datosCalificacion.IdEvaluacionAlumno.Value != 0;

            if (ValidacionCampos(esModificar))
            {
                if (esModificar)
                {
                    cnEvaluacionesAlumnos.ActualizarEvaluacionAlumno(LlenarObjeto(true), out int oNum, out string oMsg);
                    if (oNum != -1)
                    {
                        MessageBox.Show(oMsg, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    cnEvaluacionesAlumnos.AgregarEvaluacionAlumno(LlenarObjeto(false), out int oNum, out string oMsg);
                    if (oNum != -1)
                    {
                        MessageBox.Show(oMsg, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private CeEvaluacionesAlumnos LlenarObjeto(bool esActualizar)
        {
            CeEvaluacionesAlumnos objPase = new CeEvaluacionesAlumnos();
            if (esActualizar)
            {
                objPase.IdEvaluacionAlumno = datosCalificacion.IdEvaluacionAlumno;
            }
            objPase.CodigoRegistro = string.IsNullOrWhiteSpace(txt_CodigoRegistro.Text) ? null : txt_CodigoRegistro.Text.Trim().ToUpper();
            objPase.PuntajeObtenido = num_PuntajeObtenido.Value;
            objPase.PorcentajeLogrado = num_PorcentajeLogrado.Value > 0 ? num_PorcentajeLogrado.Value : null;
            objPase.Observaciones = string.IsNullOrWhiteSpace(txt_Observaciones.Text) ? null : txt_Observaciones.Text.Trim();
            objPase.EsRecalculo = chk_EsRecalculo.Checked;
            objPase.NumeroRecalculo = (int)num_NumeroRecalculo.Value;
            objPase.MotivoAjuste = string.IsNullOrWhiteSpace(txt_MotivoAjuste.Text) ? null : txt_MotivoAjuste.Text.Trim();
            objPase.FirmadoPorEstudiante = chk_FirmadoPorEstudiante.Checked;
            objPase.FechaValidacion = chk_FechaValidacion.Checked ? dtp_FechaValidacion.Value : (DateTime?)null;
            objPase.FechaPublicacion = chk_FechaPublicacion.Checked ? dtp_FechaPublicacion.Value : (DateTime?)null;
            
            if (cmb_Estado.SelectedValue != null)
                objPase.IdEstado = Convert.ToInt32(cmb_Estado.SelectedValue);
            if (cmb_EstadoPublicacion.SelectedValue != null)
                objPase.IdEstadoPublicacion = Convert.ToInt32(cmb_EstadoPublicacion.SelectedValue);
            
            // IdEvaluacionInstancia e IdInscripcion desde combos
            if (cmb_InstanciaEvaluacion.SelectedValue != null)
                objPase.IdEvaluacionInstancia = Convert.ToInt32(cmb_InstanciaEvaluacion.SelectedValue);
            if (cmb_Inscripcion.SelectedValue != null)
                objPase.IdInscripcion = Convert.ToInt32(cmb_Inscripcion.SelectedValue);
            
            return objPase;
        }

        private bool ValidacionCampos(bool esActualizar)
        {
            bool rta = true;
            if (num_PuntajeObtenido.Value < 0)
            {
                MessageBox.Show("El puntaje obtenido no puede ser negativo.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_PuntajeObtenido.Focus();
                rta = false;
            }
            else if (num_PorcentajeLogrado.Value < 0 || num_PorcentajeLogrado.Value > 100)
            {
                MessageBox.Show("El porcentaje logrado debe estar entre 0 y 100.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_PorcentajeLogrado.Focus();
                rta = false;
            }
            else if (cmb_InstanciaEvaluacion.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una instancia de evaluación.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_InstanciaEvaluacion.Focus();
                rta = false;
            }
            else if (cmb_Inscripcion.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una inscripción (estudiante).", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Inscripcion.Focus();
                rta = false;
            }
            else if (cmb_Estado.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un estado.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Estado.Focus();
                rta = false;
            }
            return rta;
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chk_FechaValidacion_CheckedChanged(object sender, EventArgs e)
        {
            dtp_FechaValidacion.Enabled = chk_FechaValidacion.Checked;
        }

        private void chk_FechaPublicacion_CheckedChanged(object sender, EventArgs e)
        {
            dtp_FechaPublicacion.Enabled = chk_FechaPublicacion.Checked;
        }

        private void cmb_InstanciaEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_InstanciaEvaluacion.SelectedValue == null)
            {
                cmb_Inscripcion.DataSource = null;
                return;
            }

            int idInstancia;
            if (!int.TryParse(cmb_InstanciaEvaluacion.SelectedValue.ToString(), out idInstancia) || idInstancia == 0)
            {
                cmb_Inscripcion.DataSource = null;
                return;
            }

            CargarInscripcionesParaInstancia(idInstancia);
        }

        private void CargarInscripcionesParaInstancia(int idEvaluacionInstancia)
        {
            // Necesitamos obtener la sección de la instancia para luego cargar las inscripciones
            var instancias = new CnEvaluacionesInstancias().FiltrarEvaluacionInstanciaPorId(
                new CeEvaluacionesInstancias { IdEvaluacionInstancia = idEvaluacionInstancia },
                out int oNum,
                out string oMsg);

            if (oNum == -1 || instancias == null || instancias.Count == 0)
            {
                cmb_Inscripcion.DataSource = null;
                if (!string.IsNullOrEmpty(oMsg))
                {
                    MessageBox.Show($"Error al obtener instancia: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            var instancia = instancias.First();
            if (!instancia.IdSeccion.HasValue)
            {
                cmb_Inscripcion.DataSource = null;
                return;
            }

            var listaInscripciones = new CnInscripciones().FiltrarInscripcionesPorSeccion(
                new CeInscripciones { IdSeccion = instancia.IdSeccion },
                out oNum,
                out oMsg);

            if (oNum == 0)
            {
                var listaConDisplay = listaInscripciones.Select(ins => new
                {
                    IdInscripcion = ins.IdInscripcion,
                    DisplayText = $"{ins.CodigoInscripcion} - {(string.IsNullOrEmpty(ins.NombreEstudiante) ? "SIN NOMBRE" : ins.NombreEstudiante)}"
                }).ToList();

                cmb_Inscripcion.DataSource = listaConDisplay;
                cmb_Inscripcion.ValueMember = "IdInscripcion";
                cmb_Inscripcion.DisplayMember = "DisplayText";
            }
            else
            {
                cmb_Inscripcion.DataSource = null;
                if (!string.IsNullOrEmpty(oMsg))
                {
                    MessageBox.Show($"Error al cargar inscripciones: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EstablecerValorComboBoxGenerico(ComboBox comboBox, string valuePropertyName, int valor)
        {
            if (comboBox == null || comboBox.Items.Count == 0)
                return;

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                var item = comboBox.Items[i];
                var type = item.GetType();
                var prop = type.GetProperty(valuePropertyName);
                if (prop != null)
                {
                    var propValue = prop.GetValue(item);
                    if (propValue != null && Convert.ToInt32(propValue) == valor)
                    {
                        comboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}

