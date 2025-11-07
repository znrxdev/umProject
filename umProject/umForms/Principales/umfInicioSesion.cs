using umForms.Utilidades;

namespace umForms.Principales
{
    public partial class umfInicioSesion : Form
    {
        public umfInicioSesion()
        {
            InitializeComponent();
        }

        private void umMainForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_IniciarSesion_Click(object sender, EventArgs e)
        {
            bool Ingresar = new UT_Sesion().INICIAR_SESION(txt_Usuario.Text, txt_Contrasena.Text, out int o_Num, out string o_Msg);
            if (Ingresar)
            {
                MessageBox.Show("¡INICIO SESION EXITOSO!", o_Msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                umfPrincipal _umfPrincipal = new umfPrincipal();
                _umfPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("¡ADVERTENCIA!", o_Msg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
