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
    public partial class umfApelarSancion : Form
    {
        private int idSancion;
        private string codigoSancion;
        private CnSancionesAcademicas cnSancionesAcademicas = new CnSancionesAcademicas();

        public umfApelarSancion(int idSancion, string codigoSancion)
        {
            InitializeComponent();
            this.idSancion = idSancion;
            this.codigoSancion = codigoSancion;
            lb_Titulo.Text = $"APELAR SANCIÓN: {codigoSancion}";
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Apelar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya comentarios
                if (string.IsNullOrWhiteSpace(txt_ComentariosApelacion.Text))
                {
                    MessageBox.Show("Debe ingresar los comentarios de apelación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_ComentariosApelacion.Focus();
                    return;
                }

                if (txt_ComentariosApelacion.Text.Trim().Length < 10)
                {
                    MessageBox.Show("Los comentarios de apelación deben tener al menos 10 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_ComentariosApelacion.Focus();
                    return;
                }

                if (txt_ComentariosApelacion.Text.Trim().Length > 500)
                {
                    MessageBox.Show("Los comentarios de apelación no pueden exceder 500 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_ComentariosApelacion.Focus();
                    return;
                }

                // Confirmar la apelación
                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea apelar la sanción {codigoSancion}?\n\nUna vez enviada la apelación, no podrá modificarla.",
                    "Confirmar Apelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                // Deshabilitar controles durante el proceso
                btn_Apelar.Enabled = false;
                txt_ComentariosApelacion.ReadOnly = true;
                btn_CerrarFormulario.Enabled = false;

                // Procesar la apelación
                cnSancionesAcademicas.ApelarSancionAcademica(
                    new CeSancionesAcademicas
                    {
                        IdSancion = idSancion,
                        ObservacionesApelacion = txt_ComentariosApelacion.Text.Trim()
                    },
                    out int oNum,
                    out string oMsg);

                if (oNum == -1)
                {
                    MessageBox.Show($"Error al procesar la apelación:\n{oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btn_Apelar.Enabled = true;
                    txt_ComentariosApelacion.ReadOnly = false;
                    btn_CerrarFormulario.Enabled = true;
                    return;
                }

                MessageBox.Show("¡Apelación registrada exitosamente!\n\nSu apelación será revisada por las autoridades correspondientes.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al procesar la apelación:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_Apelar.Enabled = true;
                txt_ComentariosApelacion.ReadOnly = false;
                btn_CerrarFormulario.Enabled = true;
            }
        }
    }
}

