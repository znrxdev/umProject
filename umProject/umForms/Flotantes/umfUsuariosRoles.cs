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

namespace umForms.Flotantes
{
    public partial class umfUsuariosRoles : Form
    {
        int? _IdUsuario;
        int? _IdUsuarioRol;
        public umfUsuariosRoles(int? Id_Usuario)
        {
            InitializeComponent();
            new UT_Formularios().CARGAR_CMB_ROLES(cmb_Roles, out int o_Num, out string o_Msg);
            _IdUsuario = Id_Usuario;
        }

        private void umfUsuariosRoles_Load(object sender, EventArgs e)
        {
            LlenarDgv_UsuariosRoles(_IdUsuario);
        }

        public void LlenarDgv_UsuariosRoles(int? Id_Usuario)
        {
            UT_Formularios utf = new UT_Formularios();
            dgv_UsuarioRol.Rows.Clear();
            var ListaRolesUsuario = utf.Obtener_Permisos_Usuario(Id_Usuario);
            string Activotext = "";
            foreach (var rol in ListaRolesUsuario)
            {
                dgv_UsuarioRol.Rows.Add
                    (
                        rol.Id_Usuario_Rol,
                        utf.Obtener_Datos_Usuario_Id(Convert.ToInt32(rol.Id_Usuario)).First().Usuario,
                        utf.FILTRAR_ROLES_ID(Convert.ToInt32(rol.Id_Rol)).First().Nombre_Rol,
                        rol.Fecha_Creacion,
                        rol.Fecha_Modificacion,
                        rol.Activo,
                        Activotext = rol.Activo == true ? "SI" : "NO"
                    );
            }
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (_IdUsuarioRol != 0) // Agregando
            {
                CN_USUARIOS_ROLES CNUsuariosRoles = new CN_USUARIOS_ROLES();
                CNUsuariosRoles.AGREGAR_USUARIOS_ROLES(new CAPA_ENTIDADES.CE_USUARIOS_ROLES
                {
                    Id_Usuario = _IdUsuario,
                    Id_Rol = Convert.ToInt32(cmb_Roles.SelectedValue),
                    Activo = ckb_Activo.Checked
                }, out int o_Num, out string o_Msg);
                if (o_Num != -1)
                {
                    DialogResult dgr = MessageBox.Show("¿Desea agregar mas?", o_Msg, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dgr == DialogResult.Yes)
                    {
                        LIMPIAR_CAMPOS();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(o_Msg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // Actualizando 
            {
                CN_USUARIOS_ROLES CNUsuariosRoles = new CN_USUARIOS_ROLES();
                CNUsuariosRoles.ACTUALIZAR_ROLES_USUARIO(new CAPA_ENTIDADES.CE_USUARIOS_ROLES
                {
                    Id_Usuario_Rol = _IdUsuarioRol,
                    Id_Usuario = _IdUsuario,
                    Id_Rol = Convert.ToInt32(cmb_Roles.SelectedValue),
                    Activo = ckb_Activo.Checked
                }, out int o_Num, out string o_Msg);
                if (o_Num != -1)
                {
                    DialogResult dgr = MessageBox.Show(o_Msg, "Satisfactorio", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    LIMPIAR_CAMPOS();
                }
                else
                {
                    MessageBox.Show(o_Msg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LIMPIAR_CAMPOS()
        {
            _IdUsuarioRol = 0;
            ckb_Activo.Checked = false;
            cmb_Roles.SelectedIndex = 1;
            LlenarDgv_UsuariosRoles(_IdUsuario);
        }

        private void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LIMPIAR_CAMPOS();
            btn_Limpiar.Visible = false;
        }

        private void Btn_Finalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Seleccionar_Click(object sender, EventArgs e)
        {
            if (dgv_UsuarioRol.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila primero.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);        
            }
            else
            {
                btn_Limpiar.Visible = true;
                foreach (DataGridViewRow row in dgv_UsuarioRol.SelectedRows)
                {
                    _IdUsuarioRol = Convert.ToInt32(row.Cells["Id_Usuario_Rol"].Value);
                    cmb_Roles.SelectedValue = row.Cells["Id_Rol"].Value;
                    ckb_Activo.Checked = Convert.ToBoolean(row.Cells["Activo"].Value);
                }
            }
        }

    }
}
