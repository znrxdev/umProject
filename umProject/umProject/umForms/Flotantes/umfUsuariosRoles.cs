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
        int? idUsuario;
        int? idUsuarioRol = 0;
        public umfUsuariosRoles(int? idUsuario)
        {
            InitializeComponent();
            new UtFormularios().CargarCmbRoles(cmb_Roles, out int oNum, out string oMsg);
            this.idUsuario = idUsuario == null ? 0 : idUsuario;
        }

        private void umfUsuariosRoles_Load(object sender, EventArgs e)
        {
            LlenarDgvUsuariosRoles(idUsuario);
        }

        public void LlenarDgvUsuariosRoles(int? idUsuario)
        {
            UtFormularios utf = new UtFormularios();
            dgv_UsuarioRol.Rows.Clear();
            var listaRolesUsuario = utf.ObtenerPermisosUsuario(idUsuario);
            string activoText = "";
            foreach (var rol in listaRolesUsuario)
            {
                dgv_UsuarioRol.Rows.Add
                    (
                        rol.IdUsuarioRol,
                        utf.ObtenerDatosUsuarioId(Convert.ToInt32(rol.IdUsuario)).First().Usuario,
                        rol.IdRol,
                        utf.FiltrarRolesId(Convert.ToInt32(rol.IdRol)).First().NombreRol,
                        rol.FechaCreacion,
                        rol.FechaModificacion,
                        rol.Activo,
                        activoText = rol.Activo == true ? "SI" : "NO"
                    );
            }
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (idUsuarioRol != 0 ) // Actualizando
            {
                CnUsuariosRoles cnUsuariosRoles = new CnUsuariosRoles();
                cnUsuariosRoles.ActualizarRolesUsuario(new CAPA_ENTIDADES.CeUsuariosRoles
                {
                    IdUsuarioRol = idUsuarioRol,
                    IdUsuario = idUsuario,
                    IdRol = Convert.ToInt32(cmb_Roles.SelectedValue),
                    Activo = ckb_Activo.Checked
                }, out int oNum, out string oMsg);
                if (oNum != -1)
                {
                    DialogResult dgr = MessageBox.Show("¿Desea seguir realizando mas acciones?", oMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dgr == DialogResult.Yes)
                    {
                        LimpiarCampos();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(oMsg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // Agregando 
            {
                CnUsuariosRoles cnUsuariosRoles = new CnUsuariosRoles();
                cnUsuariosRoles.AgregarUsuariosRoles(new CAPA_ENTIDADES.CeUsuariosRoles
                {
                    IdUsuarioRol = idUsuarioRol,
                    IdUsuario = idUsuario,
                    IdRol = Convert.ToInt32(cmb_Roles.SelectedValue),
                    Activo = ckb_Activo.Checked
                }, out int oNum, out string oMsg);
                if (oNum != -1)
                {
                    DialogResult dgr = MessageBox.Show(oMsg, "Satisfactorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(oMsg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LimpiarCampos()
        {
            idUsuarioRol = 0;
            ckb_Activo.Checked = false;
            cmb_Roles.SelectedIndex = 1;
            LlenarDgvUsuariosRoles(idUsuario);
        }

        private void btn_Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
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
                    idUsuarioRol = Convert.ToInt32(row.Cells["Id_Usuario_Rol"].Value);
                    cmb_Roles.SelectedValue = row.Cells["Id_Rol"].Value;
                    ckb_Activo.Checked = Convert.ToBoolean(row.Cells["ActivoBool"].Value);
                }
            }
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
