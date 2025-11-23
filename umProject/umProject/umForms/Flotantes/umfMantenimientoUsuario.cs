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

namespace umForms.Flotantes
{
    public partial class umfMantenimientoUsuario : Form
    {
        CeUsuarios datosUsuario = new CeUsuarios();
        CnUsuarios cnUsuarios = new CnUsuarios();
        public umfMantenimientoUsuario(CeUsuarios datosUsuario)
        {
            InitializeComponent();
            this.datosUsuario = datosUsuario;
            CargarDatosUsuario(this.datosUsuario);

        }
        private void umfMantenimientoUsuario_Load(object sender, EventArgs e)
        {
            new umForms.Utilidades.UtFormularios().CargarCmbEstado(datosUsuario.IdUsuario != 0 ? 20 : 21, cmb_Estados, out int oNum, out string oMsg);
        }
        private void btn_GuardarDatos_Click(object sender, EventArgs e)
        {
            if (ValidacionCampos(datosUsuario.IdUsuario != 0 ? true : false))
            {
                if (datosUsuario.IdUsuario != 0)
                {
                    cnUsuarios.ActualizarUsuarios(LlenarObjeto(datosUsuario.IdUsuario != 0 ? true : false), out int oNum, out string oMsg);
                    if (oNum != -1)
                    {
                        DialogResult dgr = MessageBox.Show("¿Desea actualizar los roles del usuario?", oMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dgr == DialogResult.Yes)
                        {
                            umfUsuariosRoles umfUsuariosRoles = new umfUsuariosRoles(datosUsuario.IdUsuario);
                            umfUsuariosRoles.Show();
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
                else
                {
                    cnUsuarios.AgregarUsuarios(LlenarObjeto(datosUsuario.IdUsuario != 0 ? true : false), out int oNum, out string oMsg);
                    if (oNum != -1)
                    {
                        DialogResult dgr = MessageBox.Show("¿Desea agregarle roles del usuario?", oMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dgr == DialogResult.Yes)
                        {
                            umfUsuariosRoles umfUsuariosRoles = new umfUsuariosRoles(oNum);
                            umfUsuariosRoles.Show();
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
        private void CargarDatosUsuario(CeUsuarios datosUsuario)
        {
            if (datosUsuario.IdUsuario != 0)
            {
                txt_Usuario.Text = datosUsuario.Usuario;
                cmb_Estados.SelectedValue = datosUsuario.IdEstado;
            }
            else
            {
                txt_Usuario.Text = string.Empty;
            }
        }

        private CeUsuarios LlenarObjeto(bool esActualizar)
        {
            CeUsuarios objPase = new CeUsuarios();
            if (esActualizar)
            {
                objPase.IdUsuario = datosUsuario.IdUsuario;
                objPase.IdPersona = datosUsuario.IdPersona;
                objPase.Usuario = txt_Usuario.Text;
                objPase.Contrasena = string.IsNullOrEmpty(txt_Contrasena.Text) ? null : txt_Contrasena.Text;
                objPase.IdEstado = Convert.ToInt32(cmb_Estados.SelectedValue);
            }
            else
            {
                objPase.Usuario = txt_Usuario.Text;
                objPase.IdPersona = datosUsuario.IdPersona;
                objPase.Contrasena = txt_Contrasena.Text;
                objPase.IdEstado = Convert.ToInt32(cmb_Estados.SelectedValue);
            }
            return objPase;
        }

        private bool ValidacionCampos(bool esActualizar)
        {
            bool rta = true;
            if (string.IsNullOrEmpty(txt_Usuario.Text))
            {
                MessageBox.Show("El campo Usuario es obligatorio.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rta = false;
            }
            if (string.IsNullOrEmpty(txt_Contrasena.Text) && !esActualizar)
            {
                MessageBox.Show("El campo Contraseña es obligatorio.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rta = false;
            }
            if (txt_Contrasena.Text.Length < 8 && !esActualizar)
            {
                MessageBox.Show("La longitud de la contraseña debe ser de 8 o más.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rta = false;
            }
            if (txt_Contrasena.Text.Length > 0 && esActualizar)
            {
                if (txt_Contrasena.Text.Length < 8)
                {
                    MessageBox.Show("La longitud de la contraseña debe ser de 8 o más.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rta = false;
                }
            }
            return rta;
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
