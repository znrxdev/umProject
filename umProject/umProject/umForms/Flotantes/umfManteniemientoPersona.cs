using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using umForms.Utilidades;

namespace umForms.Flotantes
{
    public partial class umfManteniemientoPersona : Form
    {
        CePersonas datosPersona = new CePersonas();
        CnPersonas cnPersonas = new CnPersonas();
        private const int CedulaRawLength = 14;
        public umfManteniemientoPersona(CePersonas personaInfo)
        {
            InitializeComponent();
            datosPersona = personaInfo;
        }

        private void umfManteniemientoPersona_Load(object sender, EventArgs e)
        {
            new UtFormularios().CargarCmbEstado(datosPersona.IdPersona != 0 ? 16 : 15, cmb_Estado, out int oNum, out string oMsg);
            new UtFormularios().CargarCmbCatalogos(1, cmb_TipoDocumento, out oNum, out oMsg);
            new UtFormularios().CargarCmbCatalogos(2, cmb_Generos, out oNum, out oMsg);
            new UtFormularios().CargarCmbCatalogos(3, cmb_Nacionalidades, out oNum, out oMsg);
            new UtFormularios().CargarCmbCatalogos(4, cmb_Estado_Civil, out oNum, out oMsg);
            CargarDatosPersona(datosPersona);
        }
        private void CargarDatosPersona(CePersonas datosPersona)
        {
            if (datosPersona.IdPersona != 0)
            {
                txt_PrimerNombre.Text = datosPersona.PrimerNombre;
                txt_SegundoNombre.Text = datosPersona.SegundoNombre;
                txt_PrimerApellido.Text = datosPersona.PrimerApellido;
                txt_SegundoApellido.Text = datosPersona.SegundoApellido;
                cmb_TipoDocumento.SelectedValue = datosPersona.IdTipoDocumento;
                txt_NumeroIdentificacion.Text = datosPersona.ValorDocumento;
                if (DateTime.TryParse(datosPersona.FechaNacimiento, out DateTime fechaGuardada))
                {
                    dtm_FechaNacimiento.Value = fechaGuardada > dtm_FechaNacimiento.MaxDate
                        ? dtm_FechaNacimiento.MaxDate
                        : fechaGuardada;
                }
                cmb_Generos.SelectedValue = datosPersona.IdGeneroPersona;
                cmb_Nacionalidades.SelectedValue = datosPersona.IdNacionalidad;
                cmb_Estado_Civil.SelectedValue = datosPersona.IdEstadoCivil;
                cmb_Estado.SelectedValue = datosPersona.IdEstado;
            }
            else
            {
                txt_NumeroIdentificacion.Text = datosPersona.ValorDocumento;
            }
        }

        private void btn_GuardarDatos_Click(object sender, EventArgs e)
        {
            if (ValidacionDatos())
            {
                if (datosPersona.IdPersona != 0)
                {
                    cnPersonas.ActualizarPersonas(LlenarObjeto(true), out int oNum, out string oMsg);
                    if (oNum != -1)
                    {
                        string accion = string.Empty;
                        CeUsuarios datosUsuario = new UtFormularios().ObtenerDatosUsuarioIdPersona(datosPersona.IdPersona).First();
                        datosUsuario.IdPersona = datosPersona.IdPersona;
                        if (datosUsuario.IdUsuario != 0)
                            accion = "Desea actualizarle el usuario";
                        else
                            accion = "Desea agregarle un usuario";
                        DialogResult dgr = MessageBox.Show($"¿{accion}?", oMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dgr == DialogResult.Yes)
                        {
                            umfMantenimientoUsuario umfMantenimientoUsuario = new umfMantenimientoUsuario(datosUsuario);
                            umfMantenimientoUsuario.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(oMsg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    cnPersonas.AgregarPersonas(LlenarObjeto(false), out int oNum, out string oMsg);
                    if (oNum != -1)
                    {
                        DialogResult dgr = MessageBox.Show("¿Desea agregarle un usuario?", oMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dgr == DialogResult.Yes)
                        {
                            umfMantenimientoUsuario umfMantenimientoUsuario = new umfMantenimientoUsuario(new CeUsuarios { IdUsuario = 0, IdPersona = oNum });
                            umfMantenimientoUsuario.Show();
                            this.Close();
                        }
                        else
                        {
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show(oMsg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private CePersonas LlenarObjeto(bool esActualizar)
        {
            CePersonas obj = new CePersonas();

            if (esActualizar)
                obj.IdPersona = datosPersona.IdPersona;

            obj.PrimerNombre = txt_PrimerNombre.Text?.Trim();
            obj.SegundoNombre = txt_SegundoNombre.Text?.Trim();
            obj.PrimerApellido = txt_PrimerApellido.Text?.Trim();
            obj.SegundoApellido = txt_SegundoApellido.Text?.Trim();
            obj.ValorDocumento = txt_NumeroIdentificacion.Text?.Trim().ToUpperInvariant();
            obj.FechaNacimiento = dtm_FechaNacimiento.Value.ToString("yyyy-MM-dd");
            obj.IdTipoDocumento = Convert.ToInt32(cmb_TipoDocumento.SelectedValue);
            obj.IdGeneroPersona = Convert.ToInt32(cmb_Generos.SelectedValue);
            obj.IdEstado = Convert.ToInt32(cmb_Estado.SelectedValue);
            obj.IdNacionalidad = Convert.ToInt32(cmb_Nacionalidades.SelectedValue);
            obj.IdEstadoCivil = Convert.ToInt32(cmb_Estado_Civil.SelectedValue);
            return obj;
        }

        private bool ValidacionDatos()
        {
            if (string.IsNullOrWhiteSpace(txt_PrimerNombre.Text))
            {
                MessageBox.Show("El primer nombre no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_PrimerNombre.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_SegundoNombre.Text))
            {
                MessageBox.Show("El segundo nombre no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_SegundoNombre.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_PrimerApellido.Text))
            {
                MessageBox.Show("El primer apellido no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_PrimerApellido.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_SegundoApellido.Text))
            {
                MessageBox.Show("El segundo apellido no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_SegundoApellido.Focus();
                return false;
            }
            string cedulaRaw = txt_NumeroIdentificacion.Text?.Trim().ToUpperInvariant() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(cedulaRaw))
            {
                MessageBox.Show("El número de identificación no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_NumeroIdentificacion.Focus();
                return false;
            }
            if (cedulaRaw.Length != CedulaRawLength)
            {
                MessageBox.Show("El número de identificación debe contener 14 caracteres válidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_NumeroIdentificacion.Focus();
                return false;
            }
            if (!char.IsLetter(cedulaRaw[CedulaRawLength - 1]))
            {
                MessageBox.Show("El número de identificación debe finalizar con una letra.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_NumeroIdentificacion.Focus();
                return false;
            }
            if (cmb_TipoDocumento.SelectedValue == null || cmb_TipoDocumento.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un tipo de documento válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_TipoDocumento.Focus();
                return false;
            }
            if (cmb_Generos.SelectedValue == null || cmb_Generos.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un género válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Generos.Focus();
                return false;
            }
            if (cmb_Nacionalidades.SelectedValue == null || cmb_Nacionalidades.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar una nacionalidad válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Nacionalidades.Focus();
                return false;
            }
            if (cmb_Estado_Civil.SelectedValue == null || cmb_Estado_Civil.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un estado civil válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Estado_Civil.Focus();
                return false;
            }
            if (cmb_Estado.SelectedValue == null || cmb_Estado.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un estado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Estado.Focus();
                return false;
            }
            return true;
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_NumeroIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (e.KeyChar == '-' || !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            e.KeyChar = char.ToUpperInvariant(e.KeyChar);
        }
    }
}
