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

namespace umForms.Principales
{
    public partial class umfPrincipal : Form
    {
        UtSesion sesion = new UtSesion();
        public CnUsuarios cnUsuarios = new CnUsuarios();
        public umfPrincipal()
        {
            InitializeComponent();
        }

        private void umfPrincipal_Load(object sender, EventArgs e)
        {
            icon_User.Text = "Usuario: " + CeSesionUsuario.UsuarioSesion;

            sesion.UbicacionCambiada += (texto) => icon_Ubicacion.Text = texto;
            sesion.CargarMenus(pnl_Botones, pnl_Principal);
        }

        private void btn_CerrarSesion_Click(object sender, EventArgs e)
        {
            sesion.LimpiarSesion();
            umfInicioSesion umfInicioSesion = new umfInicioSesion();
            umfInicioSesion.Show();
            this.Close();
        }
    }
}
