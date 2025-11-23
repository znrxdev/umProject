using umForms.Utilidades;

namespace umForms.Principales
{
    public partial class umfInicioSesion : Form
    {
        public umfInicioSesion()
        {
            InitializeComponent();
        }

        private void btn_IniciarSesion_Click(object sender, EventArgs e)
        {
            bool ingresar = new UtSesion().IniciarSesion(txt_Usuario.Text, txt_Contrasena.Text, out int oNum, out string oMsg);
            if (ingresar)
            {
                MessageBox.Show("¡INICIO SESION EXITOSO!", oMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                umfPrincipal umfPrincipal = new umfPrincipal();
                umfPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(oMsg,"¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
