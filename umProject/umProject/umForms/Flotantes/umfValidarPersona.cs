using CAPA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using umForms.Utilidades;

namespace umForms.Flotantes
{
    public partial class umfValidarPersona : Form
    {
        public umfValidarPersona()
        {
            InitializeComponent();
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ValidarPersona_Click(object sender, EventArgs e)
        {
            if (txt_DocumentoPersona.Text != string.Empty || txt_DocumentoPersona.Text != "")
            {
                CePersonas personaInfo = new UtFormularios().BuscarPersonaDocumento(txt_DocumentoPersona.Text, out int oNum, out string oMsg).First();
                if (personaInfo.IdPersona != 0)
                {
                    DialogResult result = MessageBox.Show("LA PERSONA EXISTE, ¿DESEA MODIFICARLA?", "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        umfManteniemientoPersona umfMantenimientoPersona = new umfManteniemientoPersona(personaInfo);
                        umfMantenimientoPersona.Show();
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("LA PERSONA NO EXISTE, ¿DESEA CREARLA?", "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        personaInfo.IdPersona = 0;
                        personaInfo.ValorDocumento = txt_DocumentoPersona.Text;
                        umfManteniemientoPersona umfMantenimientoPersona = new umfManteniemientoPersona(personaInfo);
                        umfMantenimientoPersona.Show();
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("EL VALOR DEL DOCUMENTO NO PUEDE ESTAR VACIO", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
