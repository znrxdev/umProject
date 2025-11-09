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

namespace umForms.Flotantes
{
    public partial class umfMantenimientoUsuario : Form
    {
        CE_USUARIOS DatosUsuario = new CE_USUARIOS();
        CN_USUARIOS CNUsuarios = new CN_USUARIOS();
        public umfMantenimientoUsuario(CE_USUARIOS _DatosUsuario)
        {
            InitializeComponent();
            DatosUsuario = _DatosUsuario;
            CARGAR_DATOS_USUARIO(DatosUsuario);

        }
        private void umfMantenimientoUsuario_Load(object sender, EventArgs e)
        {
            new umForms.Utilidades.UT_Formularios().CARGAR_CMB_ESTADO(DatosUsuario.Id_Usuario != 0 ? 20 : 21, cmb_Estados, out int o_Num, out string o_Msg);
        }
        private void btn_GuardarDatos_Click(object sender, EventArgs e)
        {
            if (VALIDACION_CAMPOS(DatosUsuario.Id_Usuario != 0 ? true : false))
            {
                if (DatosUsuario.Id_Usuario != 0)
                {
                    CNUsuarios.ACTUALIZAR_USUARIOS(LLENAR_OBJETO(DatosUsuario.Id_Usuario != 0 ? true : false), out int o_Num, out string o_Msg);
                    if (o_Num != -1)
                    {
                        DialogResult dgr = MessageBox.Show("¿Desea actualizar los roles del usuario?", o_Msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dgr == DialogResult.Yes)
                        {
                            umfUsuariosRoles _umfUsuariosRoles = new umfUsuariosRoles(DatosUsuario.Id_Usuario);
                            _umfUsuariosRoles.Show();
                            this.Close();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(o_Msg,"¡ADVERTENCIA!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                else
                {
                    CNUsuarios.AGREGAR_USUARIOS(LLENAR_OBJETO(DatosUsuario.Id_Usuario != 0 ? true : false), out int o_Num, out string o_Msg);
                    if (o_Num != -1)
                    {
                        DialogResult dgr = MessageBox.Show("¿Desea agregarle roles del usuario?", o_Msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dgr == DialogResult.Yes)
                        {
                            umfUsuariosRoles _umfUsuariosRoles = new umfUsuariosRoles(o_Num);
                            _umfUsuariosRoles.Show();
                            this.Close();
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
                    
            }
        }
        private void CARGAR_DATOS_USUARIO(CE_USUARIOS _DatosUsuario)
        {
            if (_DatosUsuario.Id_Usuario != 0)
            {
                txt_Usuario.Text = _DatosUsuario.Usuario;
                cmb_Estados.SelectedValue = _DatosUsuario.Id_Estado;
            }
            else
            {
                txt_Usuario.Text = string.Empty;
            }
        }

        private CE_USUARIOS LLENAR_OBJETO(bool esActualizar)
        {
            CE_USUARIOS ObjPase = new CE_USUARIOS();
            if (esActualizar)
            {
                ObjPase.Id_Usuario = DatosUsuario.Id_Usuario;
                ObjPase.Id_Persona = DatosUsuario.Id_Persona;
                ObjPase.Usuario = txt_Usuario.Text;
                ObjPase.Contrasena = string.IsNullOrEmpty(txt_Contrasena.Text) ? null : txt_Contrasena.Text;
                ObjPase.Id_Estado = Convert.ToInt32(cmb_Estados.SelectedValue);
            }
            else
            {
                ObjPase.Usuario = txt_Usuario.Text;
                ObjPase.Id_Persona = DatosUsuario.Id_Persona;
                ObjPase.Contrasena = txt_Contrasena.Text;
                ObjPase.Id_Estado = Convert.ToInt32(cmb_Estados.SelectedValue);
            }
            return ObjPase;
        }   

        private bool VALIDACION_CAMPOS(bool esActualizar)
        {
            bool Rta = true;
            if (string.IsNullOrEmpty(txt_Usuario.Text))
            {
                MessageBox.Show("El campo Usuario es obligatorio.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Rta = false;
            }
            if (string.IsNullOrEmpty(txt_Contrasena.Text) && !esActualizar)
            {
                MessageBox.Show("El campo Contraseña es obligatorio.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Rta = false;
            }
            if (txt_Contrasena.TextLength < 8 && !esActualizar)
            {
                MessageBox.Show("La longitud de la contraseña debe ser de 8 o más.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Rta = false;
            }
            if (txt_Contrasena.TextLength > 0 && esActualizar)
            {
                if (txt_Contrasena.TextLength < 8)
                {
                    MessageBox.Show("La longitud de la contraseña debe ser de 8 o más.", "Validación de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Rta = false;
                }
            }
            return Rta;
        }
    } 
}
