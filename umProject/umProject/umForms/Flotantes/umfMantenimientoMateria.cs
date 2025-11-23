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
    public partial class umfMantenimientoMateria : Form
    {
        CeMaterias datosMateria = new CeMaterias();
        CnMaterias cnMaterias = new CnMaterias();
        public umfMantenimientoMateria(CeMaterias datosMateria)
        {
            InitializeComponent();
            this.datosMateria = datosMateria;
            lb_Titulo.Text = datosMateria.IdMateria != 0 ? "MODIFICAR MATERIA" : "AGREGAR MATERIA";
            CargarDatosMateria(this.datosMateria);
        }

        private void umfMantenimientoMateria_Load(object sender, EventArgs e)
        {
            new UtFormularios().CargarCmbEstado(datosMateria.IdMateria != 0 ? 74 : 73, cmb_Estados, out int oNum, out string oMsg);
        }

        private void CargarDatosMateria(CeMaterias datosMateria)
        {
            if (datosMateria.IdMateria != 0)
            {
                txt_CodigoMateria.Text = datosMateria.CodigoMateria;
                txt_NombreMateria.Text = datosMateria.NombreMateria;
                cmb_Estados.SelectedValue = datosMateria.Activo == true ? 1 : 2;
            }
            else
            {
                txt_CodigoMateria.Text = string.Empty;
                txt_NombreMateria.Text = string.Empty;
            }
        }

        private void btn_GuardarDatos_Click(object sender, EventArgs e)
        {
            if (ValidacionCampos(datosMateria.IdMateria != 0))
            {
                if (datosMateria.IdMateria != 0)
                {
                    cnMaterias.ActualizarMaterias(LlenarObjeto(true), out int oNum, out string oMsg);
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
                    cnMaterias.AgregarMaterias(LlenarObjeto(false), out int oNum, out string oMsg);
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

        private CeMaterias LlenarObjeto(bool esActualizar)
        {
            CeMaterias objPase = new CeMaterias();
            if (esActualizar)
            {
                objPase.IdMateria = datosMateria.IdMateria;
                objPase.CodigoMateria = txt_CodigoMateria.Text?.Trim().ToUpper();
                objPase.NombreMateria = txt_NombreMateria.Text?.Trim();
                objPase.Activo = Convert.ToInt32(cmb_Estados.SelectedValue) == 1;
            }
            else
            {
                objPase.CodigoMateria = txt_CodigoMateria.Text?.Trim().ToUpper();
                objPase.NombreMateria = txt_NombreMateria.Text?.Trim();
                objPase.Activo = Convert.ToInt32(cmb_Estados.SelectedValue) == 1;
            }
            return objPase;
        }

        private bool ValidacionCampos(bool esActualizar)
        {
            bool rta = true;
            if (string.IsNullOrWhiteSpace(txt_CodigoMateria.Text))
            {
                MessageBox.Show("El campo Código Materia es obligatorio.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_CodigoMateria.Focus();
                rta = false;
            }
            else if (txt_CodigoMateria.Text.Length > 10)
            {
                MessageBox.Show("El código de materia no puede exceder 10 caracteres.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_CodigoMateria.Focus();
                rta = false;
            }
            if (string.IsNullOrWhiteSpace(txt_NombreMateria.Text))
            {
                MessageBox.Show("El campo Nombre Materia es obligatorio.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_NombreMateria.Focus();
                rta = false;
            }
            if (cmb_Estados.SelectedValue == null || cmb_Estados.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un estado válido.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Estados.Focus();
                rta = false;
            }
            return rta;
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

