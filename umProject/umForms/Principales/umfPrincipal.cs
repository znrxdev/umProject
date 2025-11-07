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
        public CN_USUARIOS CNUsuarios = new CN_USUARIOS();
        public umfPrincipal()
        {
            InitializeComponent();
        }

        private void umfPrincipal_Load(object sender, EventArgs e)
        {
            icon_User.Text = "Usuario: " + CE_SESION_USUARIO.Usuario_Sesion;
            UT_Sesion sesion = new UT_Sesion();
            sesion.CARGAR_MENUS(pnl_Botones);
        }
    }
}
