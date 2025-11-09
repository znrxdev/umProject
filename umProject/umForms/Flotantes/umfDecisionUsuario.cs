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
        int _IdUsuario;
        CE_USUARIOS DatosUsuario = new CE_USUARIOS();
        public umfDecisionUsuario(int Id_Usuario)
        {
            InitializeComponent();
            _IdUsuario = Id_Usuario;
        }
        private void umfDecisionUsuario_Load(object sender, EventArgs e)
        {
            DatosUsuario = new UT_Formularios().Obtener_Datos_Usuario_Id(_IdUsuario).First();
        }
        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Usuario_Click(object sender, EventArgs e)
        {
            umfMantenimientoUsuario _umfMantenimientoUsuario = new umfMantenimientoUsuario(DatosUsuario);
            _umfMantenimientoUsuario.Show();
            this.Close();
        }

        private void btn_Permisos_Click(object sender, EventArgs e)
        {
            umfUsuariosRoles _umfUsuariosRoles = new umfUsuariosRoles(DatosUsuario.Id_Usuario);
            _umfUsuariosRoles.Show();
            this.Close();
        }
    }
}
