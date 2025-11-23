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
    public partial class umfResponderApelacion : Form
    {
        private int idSancion;
        private string codigoSancion;
        private CnSancionesAcademicas cnSancionesAcademicas = new CnSancionesAcademicas();

        public umfResponderApelacion(int idSancion, string codigoSancion, string comentariosEstudiante)
        {
            InitializeComponent();
            this.idSancion = idSancion;
            this.codigoSancion = codigoSancion;
            lb_Titulo.Text = $"RESPONDER APELACIÓN: {codigoSancion}";
            txt_ComentariosEstudiante.Text = comentariosEstudiante;
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Aprobar_Click(object sender, EventArgs e)
        {
            ProcesarRespuesta(5); // 5 = APROBADA
        }

        private void btn_Rechazar_Click(object sender, EventArgs e)
        {
            ProcesarRespuesta(6); // 6 = RECHAZADA
        }

        private void ProcesarRespuesta(int idEstado)
        {
            try
            {
                // Validar que haya resultado
                if (string.IsNullOrWhiteSpace(txt_ResultadoApelacion.Text))
                {
                    MessageBox.Show("Debe ingresar el resultado de la apelación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_ResultadoApelacion.Focus();
                    return;
                }

                if (txt_ResultadoApelacion.Text.Trim().Length < 10)
                {
                    MessageBox.Show("El resultado de la apelación debe tener al menos 10 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_ResultadoApelacion.Focus();
                    return;
                }

                if (txt_ResultadoApelacion.Text.Trim().Length > 200)
                {
                    MessageBox.Show("El resultado de la apelación no puede exceder 200 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_ResultadoApelacion.Focus();
                    return;
                }

                string accion = idEstado == 5 ? "aprobar" : "rechazar";
                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea {accion} la apelación de la sanción {codigoSancion}?\n\nUna vez enviada la respuesta, no podrá modificarla.",
                    $"Confirmar {accion.ToUpper()} Apelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                // Deshabilitar controles durante el proceso
                btn_Aprobar.Enabled = false;
                btn_Rechazar.Enabled = false;
                txt_ResultadoApelacion.ReadOnly = true;
                btn_CerrarFormulario.Enabled = false;

                // Procesar la respuesta
                cnSancionesAcademicas.ResponderApelacion(
                    new CeSancionesAcademicas
                    {
                        IdSancion = idSancion,
                        IdEstado = idEstado,
                        ResultadoApelacion = txt_ResultadoApelacion.Text.Trim()
                    },
                    out int oNum,
                    out string oMsg);

                if (oNum == -1)
                {
                    MessageBox.Show($"Error al procesar la respuesta:\n{oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btn_Aprobar.Enabled = true;
                    btn_Rechazar.Enabled = true;
                    txt_ResultadoApelacion.ReadOnly = false;
                    btn_CerrarFormulario.Enabled = true;
                    return;
                }

                string mensajeExito = idEstado == 5 
                    ? "¡Apelación aprobada exitosamente!\n\nLa sanción ha sido aprobada y el estudiante será notificado."
                    : "¡Apelación rechazada exitosamente!\n\nLa sanción permanece vigente y el estudiante será notificado.";

                MessageBox.Show(mensajeExito, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al procesar la respuesta:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_Aprobar.Enabled = true;
                btn_Rechazar.Enabled = true;
                txt_ResultadoApelacion.ReadOnly = false;
                btn_CerrarFormulario.Enabled = true;
            }
        }
    }
}

