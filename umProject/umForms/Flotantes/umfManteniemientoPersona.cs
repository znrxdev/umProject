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

namespace umForms.Flotantes
{
    public partial class umfManteniemientoPersona : Form
    {
        CE_PERSONAS DatosPersona = new CE_PERSONAS();
        CN_PERSONAS _CNPersonas = new CN_PERSONAS();
        public umfManteniemientoPersona(CE_PERSONAS _PersonaInfo)
        {
            InitializeComponent();
            DatosPersona = _PersonaInfo;
        }

        private void umfManteniemientoPersona_Load(object sender, EventArgs e)
        {
            new UT_Formularios().CARGAR_CMB_ESTADO(DatosPersona.Id_Persona != 0 ? 16 : 15, cmb_Estado, out int o_Num, out string o_Msg);
            new UT_Formularios().CARGAR_CMB_CATALOGOS(1, cmb_TipoDocumento, out o_Num, out o_Msg);
            new UT_Formularios().CARGAR_CMB_CATALOGOS(2, cmb_Generos, out o_Num, out o_Msg);
            new UT_Formularios().CARGAR_CMB_CATALOGOS(3, cmb_Nacionalidades, out o_Num, out o_Msg);
            new UT_Formularios().CARGAR_CMB_CATALOGOS(4, cmb_Estado_Civil, out o_Num, out o_Msg);
            CARGAR_DATOS_PERSONA(DatosPersona);
        }
        private void CARGAR_DATOS_PERSONA(CE_PERSONAS _DatosPersona)
        {
            if (_DatosPersona.Id_Persona != 0)
            {
                txt_PrimerNombre.Text = _DatosPersona.Primer_Nombre;
                txt_SegundoNombre.Text = _DatosPersona.Segundo_Nombre;
                txt_PrimerApellido.Text = _DatosPersona.Primer_Apellido;
                txt_SegundoApellido.Text = _DatosPersona.Segundo_Apellido;
                cmb_TipoDocumento.SelectedValue = _DatosPersona.Id_Tipo_Documento;
                txt_NumeroIdentificacion.Text = _DatosPersona.Valor_Documento;
                dtm_FechaNacimiento.Value = Convert.ToDateTime(_DatosPersona.Fecha_Nacimiento);
                cmb_Generos.SelectedValue = _DatosPersona.Id_Genero_Persona;
                cmb_Nacionalidades.SelectedValue = _DatosPersona.Id_Nacionalidad;
                cmb_Estado_Civil.SelectedValue = _DatosPersona.Id_Estado_Civil;
                cmb_Estado.SelectedValue = _DatosPersona.Id_Estado;
            }
            else
            {
                txt_NumeroIdentificacion.Text = _DatosPersona.Valor_Documento;
            }
        }

        private void btn_GuardarDatos_Click(object sender, EventArgs e)
        {
            if (VALIDACION_DATOS())
            {
                if (DatosPersona.Id_Persona != 0)
                {
                    _CNPersonas.ACTUALIZAR_PERSONAS(LLENAR_OBJETO(true), out int o_Num, out string o_Msg);
                    if (o_Num != -1)
                    {
                        string accion = string.Empty;
                        CE_USUARIOS DatosUsuario = new UT_Formularios().Obtener_Datos_Usuario_Id_Persona(DatosPersona.Id_Persona).First();
                        DatosUsuario.Id_Persona = DatosPersona.Id_Persona;
                        if (DatosUsuario.Id_Usuario != 0)
                            accion = "Desea actualizarle el usuario";
                        else
                            accion = "Desea agregarle un usuario";
                        DialogResult dgr = MessageBox.Show($"¿{accion}?", o_Msg, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dgr == DialogResult.Yes)
                        {
                            umfMantenimientoUsuario _umfMantenimientoUsuario = new umfMantenimientoUsuario(DatosUsuario);
                            _umfMantenimientoUsuario.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(o_Msg, "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    _CNPersonas.AGREGAR_PERSONAS(LLENAR_OBJETO(false), out int o_Num, out string o_Msg);
                    if (o_Num != -1)
                    {
                        DialogResult dgr = MessageBox.Show("¿Desea agregarle un usuario?", o_Msg, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dgr == DialogResult.Yes)
                        {
                            umfMantenimientoUsuario _umfMantenimientoUsuario = new umfMantenimientoUsuario(new CE_USUARIOS { Id_Usuario = 0, Id_Persona = o_Num });
                            _umfMantenimientoUsuario.Show();
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
        private CE_PERSONAS LLENAR_OBJETO(bool esActualizar)
        {
            CE_PERSONAS obj = new CE_PERSONAS();

            if (esActualizar)
                obj.Id_Persona = DatosPersona.Id_Persona;

            obj.Primer_Nombre = txt_PrimerNombre.Text?.Trim();
            obj.Segundo_Nombre = txt_SegundoNombre.Text?.Trim();
            obj.Primer_Apellido = txt_PrimerApellido.Text?.Trim();
            obj.Segundo_Apellido = txt_SegundoApellido.Text?.Trim();
            obj.Valor_Documento = txt_NumeroIdentificacion.Text?.Trim();
            obj.Fecha_Nacimiento = dtm_FechaNacimiento.Value.ToString("yyyy-MM-dd");
            obj.Id_Tipo_Documento = Convert.ToInt32(cmb_TipoDocumento.SelectedValue);
            obj.Id_Genero_Persona = Convert.ToInt32(cmb_Generos.SelectedValue);
            obj.Id_Estado = Convert.ToInt32(cmb_Estado.SelectedValue);
            obj.Id_Nacionalidad = Convert.ToInt32(cmb_Nacionalidades.SelectedValue);
            obj.Id_Estado_Civil = Convert.ToInt32(cmb_Estado_Civil.SelectedValue);
            return obj;
        }

        private bool VALIDACION_DATOS()
        {
            if (string.IsNullOrWhiteSpace(txt_PrimerNombre.Text))
            {
                MessageBox.Show("El primer nombre no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_PrimerNombre.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_SegundoNombre.Text))
            {
                MessageBox.Show("El segundo nombre no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_SegundoNombre.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_PrimerApellido.Text))
            {
                MessageBox.Show("El primer apellido no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_PrimerApellido.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_SegundoApellido.Text))
            {
                MessageBox.Show("El segundo apellido no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_SegundoApellido.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txt_NumeroIdentificacion.Text))
            {
                MessageBox.Show("El número de identificación no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_NumeroIdentificacion.Focus();
                return false;
            }
            if (cmb_TipoDocumento.SelectedValue == null || cmb_TipoDocumento.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un tipo de documento válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_TipoDocumento.Focus();
                return false;
            }
            if (cmb_Generos.SelectedValue == null || cmb_Generos.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un género válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Generos.Focus();
                return false;
            }
            if (cmb_Nacionalidades.SelectedValue == null || cmb_Nacionalidades.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar una nacionalidad válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Nacionalidades.Focus();
                return false;
            }
            if (cmb_Estado_Civil.SelectedValue == null || cmb_Estado_Civil.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un estado civil válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Estado_Civil.Focus();
                return false;
            }
            if (cmb_Estado.SelectedValue == null || cmb_Estado.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Debe seleccionar un estado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_Estado.Focus();
                return false;
            }
            return true;
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
