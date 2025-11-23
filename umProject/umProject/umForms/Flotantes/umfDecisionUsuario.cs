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
using umForms.Utilidades;

namespace umForms.Flotantes
{
    public partial class umfDecisionUsuario : Form
    {
        int idUsuario;
        CeUsuarios datosUsuario = new CeUsuarios();
        public umfDecisionUsuario(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
        }
        private void umfDecisionUsuario_Load(object sender, EventArgs e)
        {
            datosUsuario = new UtFormularios().ObtenerDatosUsuarioId(idUsuario).First();
        }
        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Usuario_Click(object sender, EventArgs e)
        {
            umfMantenimientoUsuario umfMantenimientoUsuario = new umfMantenimientoUsuario(datosUsuario);
            umfMantenimientoUsuario.Show();
            this.Close();
        }

        private void btn_Permisos_Click(object sender, EventArgs e)
        {
            umfUsuariosRoles umfUsuariosRoles = new umfUsuariosRoles(datosUsuario.IdUsuario);
            umfUsuariosRoles.Show();
            this.Close();
        }
    }
}
