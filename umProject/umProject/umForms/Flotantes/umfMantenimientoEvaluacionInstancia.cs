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
    public partial class umfMantenimientoEvaluacionInstancia : Form
    {
        CeEvaluacionesInstancias datosInstancia = new CeEvaluacionesInstancias();
        CnEvaluacionesInstancias cnEvaluacionesInstancias = new CnEvaluacionesInstancias();
        UtFormularios utFormularios = new UtFormularios();

        public umfMantenimientoEvaluacionInstancia(CeEvaluacionesInstancias datosInstancia)
        {
            InitializeComponent();
            this.datosInstancia = datosInstancia;
            bool esModificar = datosInstancia.IdEvaluacionInstancia.HasValue && datosInstancia.IdEvaluacionInstancia.Value != 0;
            lb_Titulo.Text = esModificar ? "MODIFICAR INSTANCIA DE EVALUACIÓN" : "AGREGAR INSTANCIA DE EVALUACIÓN";
        }

        private void umfMantenimientoEvaluacionInstancia_Load(object sender, EventArgs e)
        {
            CargarCombos();
            this.BeginInvoke(new Action(() => CargarDatosInstancia(this.datosInstancia)));
        }

        private void CargarCombos()
        {
            bool esModificar = datosInstancia.IdEvaluacionInstancia.HasValue && datosInstancia.IdEvaluacionInstancia.Value != 0;

            // Cargar estados
            utFormularios.CargarCmbEstado(esModificar ? 125 : 124, cmb_Estado, out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar estados: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar estados de publicación
            utFormularios.CargarCmbEstado(124, cmb_EstadoPublicacion, out oNum, out oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar estados de publicación: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar materias períodos (para elegir asignatura + período)
            utFormularios.CargarCmbMateriasPeriodos(cmb_MateriaPeriodo, out oNum, out oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar materias períodos: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Si es modificación, intentamos precargar combos de materia-período, sección y modelo
            if (esModificar)
            {
                // Seleccionar materia-período a partir de datosInstancia.IdSeccion o IdEvaluacionModelo
                int? idMateriaPeriodo = null;
                if (datosInstancia.IdSeccion.HasValue)
                {
                    // Necesitaríamos una consulta adicional para obtener Id_Materia_Periodo desde la sección si fuese necesario.
                    // Para mantener la coherencia y evitar consultas adicionales complejas, asumimos que el usuario puede volver a seleccionar.
                }

                // Si tenemos un IdMateriaPeriodo lo usamos para seleccionar el combo
                if (idMateriaPeriodo.HasValue)
                {
                    EstablecerValorComboBoxGenerico(cmb_MateriaPeriodo, "IdMateriaPeriodo", idMateriaPeriodo.Value);
                }
            }
        }

        private void CargarDatosInstancia(CeEvaluacionesInstancias datosInstancia)
        {
            bool esModificar = datosInstancia.IdEvaluacionInstancia.HasValue && datosInstancia.IdEvaluacionInstancia.Value != 0;

            if (esModificar)
            {
                txt_CodigoInstancia.Text = datosInstancia.CodigoInstancia ?? string.Empty;
                
                if (datosInstancia.FechaProgramada.HasValue)
                    dtp_FechaProgramada.Value = datosInstancia.FechaProgramada.Value;
                if (datosInstancia.FechaLimite.HasValue)
                {
                    dtp_FechaLimite.Value = datosInstancia.FechaLimite.Value;
                    chk_FechaLimite.Checked = true;
                }
                
                chk_RequiereRevisionInterna.Checked = datosInstancia.RequiereRevisionInterna ?? false;
                num_NumeroVersion.Value = datosInstancia.NumeroVersion ?? 1;
                
                EstablecerValorComboBox(cmb_Estado, datosInstancia.IdEstado);
                EstablecerValorComboBox(cmb_EstadoPublicacion, datosInstancia.IdEstadoPublicacion);
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
            bool esModificar = datosInstancia.IdEvaluacionInstancia.HasValue && datosInstancia.IdEvaluacionInstancia.Value != 0;

            if (ValidacionCampos(esModificar))
            {
                if (esModificar)
                {
                    cnEvaluacionesInstancias.ActualizarEvaluacionInstancia(LlenarObjeto(true), out int oNum, out string oMsg);
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
                    cnEvaluacionesInstancias.AgregarEvaluacionInstancia(LlenarObjeto(false), out int oNum, out string oMsg);
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

        private CeEvaluacionesInstancias LlenarObjeto(bool esActualizar)
        {
            CeEvaluacionesInstancias objPase = new CeEvaluacionesInstancias();
            if (esActualizar)
            {
                objPase.IdEvaluacionInstancia = datosInstancia.IdEvaluacionInstancia;
            }
            objPase.CodigoInstancia = string.IsNullOrWhiteSpace(txt_CodigoInstancia.Text) ? null : txt_CodigoInstancia.Text.Trim().ToUpper();
            objPase.FechaProgramada = dtp_FechaProgramada.Value;
            objPase.FechaLimite = chk_FechaLimite.Checked ? dtp_FechaLimite.Value : (DateTime?)null;
            objPase.RequiereRevisionInterna = chk_RequiereRevisionInterna.Checked;
            objPase.NumeroVersion = (int)num_NumeroVersion.Value;
            
            if (cmb_Estado.SelectedValue != null)
                objPase.IdEstado = Convert.ToInt32(cmb_Estado.SelectedValue);
            if (cmb_EstadoPublicacion.SelectedValue != null)
                objPase.IdEstadoPublicacion = Convert.ToInt32(cmb_EstadoPublicacion.SelectedValue);
            
            // IdSeccion e IdEvaluacionModelo desde combos
            if (cmb_Seccion.SelectedValue != null)
                objPase.IdSeccion = Convert.ToInt32(cmb_Seccion.SelectedValue);
            if (cmb_ModeloEvaluacion.SelectedValue != null)
                objPase.IdEvaluacionModelo = Convert.ToInt32(cmb_ModeloEvaluacion.SelectedValue);

            // IdPeriodo se obtiene del MateriaPeriodo seleccionado (CeMateriasPeriodos)
            if (cmb_MateriaPeriodo.SelectedValue != null)
            {
                int idMateriaPeriodo = Convert.ToInt32(cmb_MateriaPeriodo.SelectedValue);
                int oNum;
                string oMsg;
                var listaMp = new CnMateriasPeriodos().ListarMateriasPeriodos(out oNum, out oMsg);
                if (oNum == 0 && listaMp != null)
                {
                    var mp = listaMp.FirstOrDefault(x => x.IdMateriaPeriodo == idMateriaPeriodo);
                    if (mp != null)
                    {
                        objPase.IdPeriodo = mp.IdPeriodoAcademico;
                    }
                }
            }
            
            return objPase;
        }

        private bool ValidacionCampos(bool esActualizar)
        {
            bool rta = true;
            if (chk_FechaLimite.Checked && dtp_FechaLimite.Value < dtp_FechaProgramada.Value)
            {
                MessageBox.Show("La fecha límite no puede ser anterior a la fecha programada.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtp_FechaLimite.Focus();
                rta = false;
            }
            else if (cmb_Estado.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un estado.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Estado.Focus();
                rta = false;
            }
            else if (cmb_EstadoPublicacion.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un estado de publicación.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_EstadoPublicacion.Focus();
                rta = false;
            }
            return rta;
        }

        private void cmb_MateriaPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_MateriaPeriodo.SelectedValue == null)
            {
                cmb_Seccion.DataSource = null;
                cmb_ModeloEvaluacion.DataSource = null;
                return;
            }

            int idMateriaPeriodo;
            if (!int.TryParse(cmb_MateriaPeriodo.SelectedValue.ToString(), out idMateriaPeriodo) || idMateriaPeriodo == 0)
            {
                cmb_Seccion.DataSource = null;
                cmb_ModeloEvaluacion.DataSource = null;
                return;
            }

            // Cargar secciones de la materia-período
            utFormularios.CargarCmbSeccionesPorMateriaPeriodo(idMateriaPeriodo, cmb_Seccion, out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar secciones: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar modelos de evaluación de la materia-período
            var modelos = new CnEvaluacionesModelos().FiltrarEvaluacionModeloPorMateriaPeriodo(
                new CeEvaluacionesModelos { IdMateriaPeriodo = idMateriaPeriodo },
                out oNum,
                out oMsg);
            if (oNum == 0)
            {
                var listaConDisplay = modelos.Select(m => new
                {
                    IdEvaluacionModelo = m.IdEvaluacionModelo,
                    DisplayText = !string.IsNullOrEmpty(m.NombreEvaluacion)
                        ? $"{m.NombreEvaluacion} ({m.CodigoModelo})"
                        : m.CodigoModelo ?? "SIN CÓDIGO"
                }).ToList();

                cmb_ModeloEvaluacion.DataSource = listaConDisplay;
                cmb_ModeloEvaluacion.ValueMember = "IdEvaluacionModelo";
                cmb_ModeloEvaluacion.DisplayMember = "DisplayText";
            }
            else
            {
                cmb_ModeloEvaluacion.DataSource = null;
                if (!string.IsNullOrEmpty(oMsg))
                {
                    MessageBox.Show($"Error al cargar modelos de evaluación: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chk_FechaLimite_CheckedChanged(object sender, EventArgs e)
        {
            dtp_FechaLimite.Enabled = chk_FechaLimite.Checked;
        }
    }
}

