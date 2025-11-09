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
using umForms.Flotantes;
using umForms.Utilidades;

namespace umForms.Principales.Hijos
{
    public partial class umfUsuarios : Form
    {
        CE_USUARIOS CEUsuarios = new CE_USUARIOS();
        CN_USUARIOS CNUsuarios = new CN_USUARIOS();
        UT_Formularios UTFormularios = new UT_Formularios();
        public umfUsuarios()
        {
            InitializeComponent();
        }

        private void umfUsuarios_Load(object sender, EventArgs e)
        {
            var ListaUsuarios = CNUsuarios.LISTAR_USUARIOS(out int o_Num, out string o_Msg);
            Llenar_DgvUsuarios(ListaUsuarios);
        }

        private void Llenar_DgvUsuarios(List<CE_USUARIOS> ListaUsuarios)
        {
            dgv_Usuarios.Rows.Clear();
            UT_Formularios util = new UT_Formularios();

            foreach (var usuario in ListaUsuarios)
            {
                dgv_Usuarios.Rows.Add(
                    usuario.Id_Usuario,
                    usuario.Id_Persona,
                    usuario.Usuario,
                    usuario.Ultima_Sesion,
                    util.Obtener_Datos_Usuario_Id(Convert.ToInt32(usuario.Id_Creador)).First().Usuario,
                    util.Obtener_Datos_Usuario_Id(Convert.ToInt32(usuario.Id_Modificador)).First().Usuario,
                    usuario.Id_Transaccion,
                    UTFormularios.Obtener_Nombre_Estado(Convert.ToInt32(usuario.Id_Estado)) // si este es estático, ok
                );
            }
        }
        private void btn_AgregarUsuario_Click(object sender, EventArgs e)
        {
            Flotantes.umfValidarPersona _umfValidarPersona = new umfValidarPersona();
            _umfValidarPersona.Show();
        }

        private void txt_Usuario_TextChanged(object sender, EventArgs e)
        {
            var ListaUsuarios = CNUsuarios.FILTRAR_USUARIO_POR_USUARIO(new CE_USUARIOS { Usuario = txt_Usuario.Text }, out int o_Num, out string o_Msg);
            Llenar_DgvUsuarios(ListaUsuarios);
        }

        private void btn_ModificarUsuario_Click(object sender, EventArgs e)
        {
            if (dgv_Usuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Para modificar necesita seleccionar un usuario de la lista", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                umfDecisionUsuario _umfDecisionUsuario = new umfDecisionUsuario(Convert.ToInt32(dgv_Usuarios.CurrentRow.Cells[0].Value));
                _umfDecisionUsuario.Show();
            }

        }
    }
}
