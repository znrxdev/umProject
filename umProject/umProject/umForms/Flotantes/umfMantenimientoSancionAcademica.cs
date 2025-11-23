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
    public partial class umfMantenimientoSancionAcademica : Form
    {
        CeSancionesAcademicas datosSancion = new CeSancionesAcademicas();
        CnSancionesAcademicas cnSancionesAcademicas = new CnSancionesAcademicas();
        CnUsuarios cnUsuarios = new CnUsuarios();
        UtFormularios utFormularios = new UtFormularios();
        private int? idEstudianteValidado = null;

        public umfMantenimientoSancionAcademica(CeSancionesAcademicas datosSancion)
        {
            InitializeComponent();
            this.datosSancion = datosSancion;
            lb_Titulo.Text = datosSancion.IdSancion != 0 ? "MODIFICAR SANCIÓN ACADÉMICA" : "AGREGAR SANCIÓN ACADÉMICA";
        }

        private void umfMantenimientoSancionAcademica_Load(object sender, EventArgs e)
        {
            CargarCombos();
            // Usar BeginInvoke para asegurar que los ComboBox estén completamente cargados antes de establecer valores
            this.BeginInvoke(new Action(() => CargarDatosSancion(this.datosSancion)));
        }

        private void CargarCombos()
        {
            // Cargar tipos de sanción (Id_Tipo_Catalogo = 6 según umDbData.sql)
            utFormularios.CargarCmbCatalogos(6, cmb_TipoSancion, out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar tipos de sanción: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar tipos de falta (Id_Tipo_Catalogo = 24 según umDbData.sql)
            utFormularios.CargarCmbCatalogos(24, cmb_TipoFalta, out oNum, out oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar tipos de falta: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar severidad (Id_Tipo_Catalogo = 7 según umDbData.sql)
            utFormularios.CargarCmbCatalogos(7, cmb_Severidad, out oNum, out oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar severidad: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cargar estados
            utFormularios.CargarCmbEstado(datosSancion.IdSancion != 0 ? 90 : 87, cmb_Estado, out oNum, out oMsg);
            if (oNum == -1)
            {
                MessageBox.Show($"Error al cargar estados: {oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosSancion(CeSancionesAcademicas datosSancion)
        {
            if (datosSancion.IdSancion != 0)
            {
                txt_CodigoSancion.Text = datosSancion.CodigoSancion;
                idEstudianteValidado = datosSancion.IdEstudiante;
                
                // Cargar datos del estudiante si existe
                if (datosSancion.IdEstudiante.HasValue)
                {
                    var estudiante = cnUsuarios.FiltrarUsuariosPorId(new CeUsuarios { IdUsuario = datosSancion.IdEstudiante.Value }, out int oNum, out string oMsg);
                    if (oNum != -1 && estudiante.Count > 0)
                    {
                        // Obtener el número de documento desde la persona
                        var persona = new CnPersonas().FiltrarPersonasPorId(new CePersonas { IdPersona = estudiante[0].IdPersona }, out oNum, out oMsg);
                        if (oNum != -1 && persona.Count > 0)
                        {
                            txt_NumeroDocumento.Text = persona[0].ValorDocumento;
                            ValidarEstudiante();
                        }
                    }
                }
                
                // Establecer valores de los ComboBox después de que estén cargados
                EstablecerValorComboBox(cmb_Severidad, datosSancion.IdSeveridad);
                EstablecerValorComboBox(cmb_TipoSancion, datosSancion.IdTipoSancion);
                EstablecerValorComboBox(cmb_TipoFalta, datosSancion.IdTipoFalta);
                EstablecerValorComboBox(cmb_Estado, datosSancion.IdEstado);
                
                dtp_FechaRegistro.Value = datosSancion.FechaRegistro ?? DateTime.Now;
                if (datosSancion.FechaFin.HasValue)
                    dtp_FechaFin.Value = datosSancion.FechaFin.Value;
                else
                    chk_FechaFin.Checked = false;
                txt_Motivo.Text = datosSancion.Motivo ?? string.Empty;
                chk_EsApelable.Checked = datosSancion.EsApelable ?? false;
                if (datosSancion.FechaApelacion.HasValue)
                {
                    dtp_FechaApelacion.Value = datosSancion.FechaApelacion.Value;
                    chk_FechaApelacion.Checked = true;
                }
                txt_ResultadoApelacion.Text = datosSancion.ResultadoApelacion ?? string.Empty;
                txt_ObservacionesApelacion.Text = datosSancion.ObservacionesApelacion ?? string.Empty;
                txt_DocumentoResolucion.Text = datosSancion.DocumentoResolucion ?? string.Empty;
            }
            else
            {
                txt_CodigoSancion.Text = string.Empty;
                txt_NumeroDocumento.Text = string.Empty;
                lb_NombreEstudiante.Text = string.Empty;
                dtp_FechaRegistro.Value = DateTime.Now;
                chk_FechaFin.Checked = false;
                chk_FechaApelacion.Checked = false;
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
                // Verificar que el ComboBox tenga datos
                if (comboBox.Items.Count == 0)
                {
                    return;
                }

                // Verificar que ValueMember esté configurado
                if (string.IsNullOrEmpty(comboBox.ValueMember))
                {
                    // Intentar determinar el tipo de datos y configurarlo
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

                // Intentar establecer el valor usando SelectedValue
                try
                {
                    comboBox.SelectedValue = valor.Value;
                    
                    // Verificar si se estableció correctamente
                    if (comboBox.SelectedValue != null)
                    {
                        int selectedValue = Convert.ToInt32(comboBox.SelectedValue);
                        if (selectedValue == valor.Value)
                        {
                            return;
                        }
                    }
                }
                catch
                {
                    // Si SelectedValue falla, continuar con búsqueda manual
                }
                
                // Si SelectedValue no funcionó, buscar manualmente por el índice
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

                    if (itemValue.HasValue && itemValue.Value == valor.Value)
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                }
            }
            catch
            {
                // Silenciar errores
            }
        }

        private void btn_GuardarDatos_Click(object sender, EventArgs e)
        {
            if (ValidacionCampos(datosSancion.IdSancion != 0))
            {
                if (datosSancion.IdSancion != 0)
                {
                    cnSancionesAcademicas.ActualizarSancionAcademica(LlenarObjeto(true), out int oNum, out string oMsg);
                    if (oNum != -1)
                    {
                        MessageBox.Show(oMsg, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(oMsg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    cnSancionesAcademicas.AgregarSancionAcademica(LlenarObjeto(false), out int oNum, out string oMsg);
                    if (oNum != -1)
                    {
                        MessageBox.Show(oMsg, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(oMsg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private CeSancionesAcademicas LlenarObjeto(bool esActualizar)
        {
            CeSancionesAcademicas objPase = new CeSancionesAcademicas();
            if (esActualizar)
            {
                objPase.IdSancion = datosSancion.IdSancion;
            }
            objPase.CodigoSancion = string.IsNullOrWhiteSpace(txt_CodigoSancion.Text) ? null : txt_CodigoSancion.Text.Trim().ToUpper();
            objPase.IdEstudiante = idEstudianteValidado;
            objPase.IdTipoSancion = Convert.ToInt32(cmb_TipoSancion.SelectedValue);
            objPase.IdTipoFalta = cmb_TipoFalta.SelectedValue != null ? Convert.ToInt32(cmb_TipoFalta.SelectedValue) : null;
            objPase.IdSeveridad = Convert.ToInt32(cmb_Severidad.SelectedValue);
            objPase.IdEstado = Convert.ToInt32(cmb_Estado.SelectedValue);
            objPase.FechaRegistro = dtp_FechaRegistro.Value;
            objPase.FechaFin = chk_FechaFin.Checked ? dtp_FechaFin.Value : (DateTime?)null;
            objPase.Motivo = string.IsNullOrWhiteSpace(txt_Motivo.Text) ? null : txt_Motivo.Text.Trim();
            objPase.EsApelable = chk_EsApelable.Checked;
            objPase.FechaApelacion = chk_FechaApelacion.Checked ? dtp_FechaApelacion.Value : (DateTime?)null;
            objPase.ResultadoApelacion = string.IsNullOrWhiteSpace(txt_ResultadoApelacion.Text) ? null : txt_ResultadoApelacion.Text.Trim();
            objPase.ObservacionesApelacion = string.IsNullOrWhiteSpace(txt_ObservacionesApelacion.Text) ? null : txt_ObservacionesApelacion.Text.Trim();
            objPase.DocumentoResolucion = string.IsNullOrWhiteSpace(txt_DocumentoResolucion.Text) ? null : txt_DocumentoResolucion.Text.Trim();
            return objPase;
        }

        private bool ValidacionCampos(bool esActualizar)
        {
            bool rta = true;
            if (idEstudianteValidado == null || idEstudianteValidado == 0)
            {
                MessageBox.Show("Debe ingresar y validar un número de documento de estudiante.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_NumeroDocumento.Focus();
                rta = false;
            }
            else if (cmb_TipoSancion.SelectedValue == null || cmb_TipoSancion.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un tipo de sanción.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_TipoSancion.Focus();
                rta = false;
            }
            else if (cmb_Severidad.SelectedValue == null || cmb_Severidad.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar una severidad.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Severidad.Focus();
                rta = false;
            }
            else if (cmb_Estado.SelectedValue == null || cmb_Estado.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un estado.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Estado.Focus();
                rta = false;
            }
            else if (chk_FechaFin.Checked && dtp_FechaFin.Value < dtp_FechaRegistro.Value)
            {
                MessageBox.Show("La fecha de fin no puede ser anterior a la fecha de registro.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtp_FechaFin.Focus();
                rta = false;
            }
            return rta;
        }

        private void chk_FechaFin_CheckedChanged(object sender, EventArgs e)
        {
            dtp_FechaFin.Enabled = chk_FechaFin.Checked;
        }

        private void chk_FechaApelacion_CheckedChanged(object sender, EventArgs e)
        {
            dtp_FechaApelacion.Enabled = chk_FechaApelacion.Checked;
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_NumeroDocumento_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_NumeroDocumento.Text))
            {
                ValidarEstudiante();
            }
        }

        private void txt_NumeroDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrWhiteSpace(txt_NumeroDocumento.Text))
                {
                    ValidarEstudiante();
                }
            }
        }

        private void ValidarEstudiante()
        {
            string numeroDocumento = txt_NumeroDocumento.Text.Trim();
            if (string.IsNullOrWhiteSpace(numeroDocumento))
            {
                lb_NombreEstudiante.Text = string.Empty;
                idEstudianteValidado = null;
                return;
            }

            var estudiante = cnUsuarios.ObtenerEstudiantePorDocumento(new CeUsuarios { ValorDocumento = numeroDocumento }, out int oNum, out string oMsg);
            
            if (oNum == -1)
            {
                MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lb_NombreEstudiante.Text = string.Empty;
                lb_NombreEstudiante.ForeColor = Color.Red;
                idEstudianteValidado = null;
                txt_NumeroDocumento.Focus();
            }
            else if (estudiante.Count > 0)
            {
                lb_NombreEstudiante.Text = $"✓ {estudiante[0].NombreCompleto}";
                lb_NombreEstudiante.ForeColor = Color.FromArgb(0, 150, 100);
                idEstudianteValidado = estudiante[0].IdUsuario;
            }
            else
            {
                lb_NombreEstudiante.Text = "No se encontró estudiante";
                lb_NombreEstudiante.ForeColor = Color.Red;
                idEstudianteValidado = null;
            }
        }
    }
}

