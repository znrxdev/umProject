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
    public partial class umfMantenimientoEvaluacionModelo : Form
    {
        CeEvaluacionesModelos datosModelo = new CeEvaluacionesModelos();
        CnEvaluacionesModelos cnEvaluacionesModelos = new CnEvaluacionesModelos();
        UtFormularios utFormularios = new UtFormularios();

        public umfMantenimientoEvaluacionModelo(CeEvaluacionesModelos datosModelo)
        {
            InitializeComponent();
            this.datosModelo = datosModelo;
            bool esModificar = datosModelo.IdEvaluacionModelo.HasValue && datosModelo.IdEvaluacionModelo.Value != 0;
            lb_Titulo.Text = esModificar ? "MODIFICAR MODELO DE EVALUACIÓN" : "AGREGAR MODELO DE EVALUACIÓN";
        }

        private void umfMantenimientoEvaluacionModelo_Load(object sender, EventArgs e)
        {
            CargarCombos();
            this.BeginInvoke(new Action(() => CargarDatosModelo(this.datosModelo)));
        }

        private void CargarCombos()
        {
            // Cargar tipos de evaluación (Id_Tipo_Catalogo = 8 según umDbData.sql)
            utFormularios.CargarCmbCatalogos(8, cmb_TipoEvaluacion, out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar tipos de evaluación: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar materias períodos
            utFormularios.CargarCmbMateriasPeriodos(cmb_MateriaPeriodo, out oNum, out oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar materias períodos: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosModelo(CeEvaluacionesModelos datosModelo)
        {
            if (datosModelo.IdEvaluacionModelo.HasValue && datosModelo.IdEvaluacionModelo.Value != 0)
            {
                txt_CodigoModelo.Text = datosModelo.CodigoModelo ?? string.Empty;
                txt_NombreEvaluacion.Text = datosModelo.NombreEvaluacion ?? string.Empty;
                txt_Concepto.Text = datosModelo.Concepto ?? string.Empty;
                num_CalificacionMaxima.Value = datosModelo.CalificacionMaxima ?? 100;
                num_PesoPorcentual.Value = datosModelo.PesoPorcentual ?? 0;
                num_Orden.Value = datosModelo.Orden ?? 1;
                chk_RequiereAprobacion.Checked = datosModelo.RequiereAprobacion ?? false;
                num_VersionConfiguracion.Value = datosModelo.VersionConfiguracion ?? 1;
                num_PorcentajeMinimoAprobacion.Value = datosModelo.PorcentajeMinimoAprobacion ?? 0;
                chk_Activo.Checked = datosModelo.Activo ?? true;
                
                if (datosModelo.FechaInicio.HasValue)
                    dtp_FechaInicio.Value = datosModelo.FechaInicio.Value;
                if (datosModelo.FechaFin.HasValue)
                    dtp_FechaFin.Value = datosModelo.FechaFin.Value;
                
                EstablecerValorComboBox(cmb_TipoEvaluacion, datosModelo.IdTipoEvaluacion);
                EstablecerValorComboBox(cmb_MateriaPeriodo, datosModelo.IdMateriaPeriodo);
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
                        if (firstItem is CeCatalogos)
                        {
                            comboBox.ValueMember = "IdCatalogo";
                            comboBox.DisplayMember = "NombreCatalogo";
                        }
                        else if (firstItem is CeEstados)
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

                    if (item is CeCatalogos catalogo)
                    {
                        itemValue = catalogo.IdCatalogo;
                    }
                    else if (item is CeEstados estado)
                    {
                        itemValue = estado.IdEstado;
                    }
                    else
                    {
                        // Para objetos anónimos (como los de materias períodos)
                        var itemType = item.GetType();
                        var idProperty = itemType.GetProperty("IdMateriaPeriodo");
                        if (idProperty != null)
                        {
                            var idValue = idProperty.GetValue(item);
                            if (idValue != null)
                            {
                                itemValue = Convert.ToInt32(idValue);
                            }
                        }
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
            bool esModificar = datosModelo.IdEvaluacionModelo.HasValue && datosModelo.IdEvaluacionModelo.Value != 0;
            if (ValidacionCampos(esModificar))
            {
                if (esModificar)
                {
                    cnEvaluacionesModelos.ActualizarEvaluacionModelo(LlenarObjeto(true), out int oNum, out string oMsg);
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
                    cnEvaluacionesModelos.AgregarEvaluacionModelo(LlenarObjeto(false), out int oNum, out string oMsg);
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

        private CeEvaluacionesModelos LlenarObjeto(bool esActualizar)
        {
            CeEvaluacionesModelos objPase = new CeEvaluacionesModelos();
            if (esActualizar)
            {
                objPase.IdEvaluacionModelo = datosModelo.IdEvaluacionModelo;
            }
            objPase.CodigoModelo = string.IsNullOrWhiteSpace(txt_CodigoModelo.Text) ? null : txt_CodigoModelo.Text.Trim().ToUpper();
            objPase.NombreEvaluacion = string.IsNullOrWhiteSpace(txt_NombreEvaluacion.Text) ? null : txt_NombreEvaluacion.Text.Trim();
            objPase.Concepto = string.IsNullOrWhiteSpace(txt_Concepto.Text) ? null : txt_Concepto.Text.Trim();
            objPase.CalificacionMaxima = num_CalificacionMaxima.Value;
            objPase.PesoPorcentual = num_PesoPorcentual.Value;
            objPase.Orden = (int)num_Orden.Value;
            objPase.RequiereAprobacion = chk_RequiereAprobacion.Checked;
            objPase.VersionConfiguracion = (int)num_VersionConfiguracion.Value;
            objPase.PorcentajeMinimoAprobacion = num_PorcentajeMinimoAprobacion.Value > 0 ? num_PorcentajeMinimoAprobacion.Value : null;
            objPase.Activo = chk_Activo.Checked;
            objPase.FechaInicio = chk_FechaInicio.Checked ? dtp_FechaInicio.Value : (DateTime?)null;
            objPase.FechaFin = chk_FechaFin.Checked ? dtp_FechaFin.Value : (DateTime?)null;
            
            if (cmb_TipoEvaluacion.SelectedValue != null)
                objPase.IdTipoEvaluacion = Convert.ToInt32(cmb_TipoEvaluacion.SelectedValue);
            
            if (cmb_MateriaPeriodo.SelectedValue != null)
                objPase.IdMateriaPeriodo = Convert.ToInt32(cmb_MateriaPeriodo.SelectedValue);
            
            return objPase;
        }

        private bool ValidacionCampos(bool esActualizar)
        {
            bool rta = true;
            if (string.IsNullOrWhiteSpace(txt_CodigoModelo.Text))
            {
                MessageBox.Show("Debe ingresar un código de modelo.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_CodigoModelo.Focus();
                rta = false;
            }
            else if (string.IsNullOrWhiteSpace(txt_NombreEvaluacion.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de evaluación.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_NombreEvaluacion.Focus();
                rta = false;
            }
            else if (num_CalificacionMaxima.Value <= 0)
            {
                MessageBox.Show("La calificación máxima debe ser mayor que cero.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_CalificacionMaxima.Focus();
                rta = false;
            }
            else if (num_PesoPorcentual.Value < 0 || num_PesoPorcentual.Value > 100)
            {
                MessageBox.Show("El peso porcentual debe estar entre 0 y 100.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_PesoPorcentual.Focus();
                rta = false;
            }
            else if (cmb_TipoEvaluacion.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un tipo de evaluación.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_TipoEvaluacion.Focus();
                rta = false;
            }
            else if (cmb_MateriaPeriodo.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una materia período.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_MateriaPeriodo.Focus();
                rta = false;
            }
            return rta;
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chk_FechaInicio_CheckedChanged(object sender, EventArgs e)
        {
            dtp_FechaInicio.Enabled = chk_FechaInicio.Checked;
        }

        private void chk_FechaFin_CheckedChanged(object sender, EventArgs e)
        {
            dtp_FechaFin.Enabled = chk_FechaFin.Checked;
        }
    }
}

