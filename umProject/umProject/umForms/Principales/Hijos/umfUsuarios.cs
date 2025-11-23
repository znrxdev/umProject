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
        CeUsuarios ceUsuarios = new CeUsuarios();
        CnUsuarios cnUsuarios = new CnUsuarios();
        UtFormularios utFormularios = new UtFormularios();
        public umfUsuarios()
        {
            InitializeComponent();
        }

        private void umfUsuarios_Load(object sender, EventArgs e)
        {
            utFormularios.CargarCmbRoles(cmb_Rol, out int oNum, out string oMsg);
            
            // Agregar opción "Todos los roles" al inicio
            var listaRoles = cmb_Rol.DataSource as List<CeRoles>;
            if (listaRoles != null)
            {
                // Crear un nuevo rol para "Todos los roles"
                var todosLosRoles = new CeRoles
                {
                    IdRol = 0,
                    NombreRol = "Todos los roles"
                };
                
                // Crear nueva lista con "Todos los roles" primero, luego los demás roles
                var nuevaLista = new List<CeRoles> { todosLosRoles };
                nuevaLista.AddRange(listaRoles);
                
                cmb_Rol.DataSource = nuevaLista;
                cmb_Rol.DisplayMember = "NombreRol";
                cmb_Rol.ValueMember = "IdRol";
            }
            
            cmb_Rol.SelectedIndex = 0;
            
            var listaUsuarios = cnUsuarios.ListarUsuarios(out oNum, out oMsg);
            LlenarDgvUsuarios(listaUsuarios);
        }

        private void LlenarDgvUsuarios(List<CeUsuarios> listaUsuarios)
        {
            dgv_Usuarios.Rows.Clear();
            UtFormularios util = new UtFormularios();

            foreach (var usuario in listaUsuarios)
            {
                dgv_Usuarios.Rows.Add(
                    usuario.IdUsuario,
                    usuario.IdPersona,
                    usuario.Usuario,
                    usuario.UltimaSesion,
                    util.ObtenerDatosUsuarioId(Convert.ToInt32(usuario.IdCreador)).First().Usuario,
                    util.ObtenerDatosUsuarioId(Convert.ToInt32(usuario.IdModificador)).First().Usuario,
                    usuario.IdTransaccion,
                    utFormularios.ObtenerNombreEstado(Convert.ToInt32(usuario.IdEstado)) // si este es estático, ok
                );
            }
        }
        private void btn_AgregarUsuario_Click(object sender, EventArgs e)
        {
            Flotantes.umfValidarPersona umfValidarPersona = new umfValidarPersona();
            umfValidarPersona.Show();
        }

        private void txt_Usuario_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void cmb_Rol_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            try
            {
                List<CeUsuarios> listaUsuarios = new List<CeUsuarios>();
                
                // Obtener el rol seleccionado
                int idRol = 0;
                if (cmb_Rol.SelectedValue != null)
                {
                    // SelectedValue debería ser el IdRol directamente
                    if (cmb_Rol.SelectedValue is int)
                    {
                        idRol = (int)cmb_Rol.SelectedValue;
                    }
                    else if (cmb_Rol.SelectedValue is int?)
                    {
                        idRol = ((int?)cmb_Rol.SelectedValue) ?? 0;
                    }
                    else
                    {
                        int.TryParse(cmb_Rol.SelectedValue.ToString(), out idRol);
                    }
                }
                else if (cmb_Rol.SelectedItem != null && cmb_Rol.SelectedItem is CeRoles rol)
                {
                    // Si SelectedValue falla, obtener desde SelectedItem
                    idRol = rol.IdRol ?? 0;
                }

                // Verificar si hay texto de búsqueda válido (no el placeholder)
                bool tieneTextoBusqueda = !string.IsNullOrWhiteSpace(txt_Usuario.Text) && 
                                          txt_Usuario.Text.Trim() != "Buscar Usuario";
                
                // Si hay texto de búsqueda válido, filtrar por usuario
                if (tieneTextoBusqueda)
                {
                    listaUsuarios = cnUsuarios.FiltrarUsuarioPorUsuario(new CeUsuarios { Usuario = txt_Usuario.Text.Trim() }, out int oNum, out string oMsg);
                    
                    // Si también hay un rol seleccionado, filtrar por rol también
                    if (idRol > 0 && oNum == 0)
                    {
                        var usuariosPorRol = cnUsuarios.FiltrarUsuariosPorRol(idRol, out oNum, out oMsg);
                        // Intersectar ambas listas
                        listaUsuarios = listaUsuarios.Where(u => usuariosPorRol.Any(ur => ur.IdUsuario == u.IdUsuario)).ToList();
                    }
                }
                // Si solo hay rol seleccionado
                else if (idRol > 0)
                {
                    listaUsuarios = cnUsuarios.FiltrarUsuariosPorRol(idRol, out int oNum, out string oMsg);
                }
                // Si no hay filtros, listar todos
                else
                {
                    listaUsuarios = cnUsuarios.ListarUsuarios(out int oNum, out string oMsg);
                }

                LlenarDgvUsuarios(listaUsuarios);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ModificarUsuario_Click(object sender, EventArgs e)
        {
            if (dgv_Usuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Para modificar necesita seleccionar un usuario de la lista", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                umfDecisionUsuario umfDecisionUsuario = new umfDecisionUsuario(Convert.ToInt32(dgv_Usuarios.CurrentRow.Cells[0].Value));
                umfDecisionUsuario.Show();
            }

        }
    }
}
