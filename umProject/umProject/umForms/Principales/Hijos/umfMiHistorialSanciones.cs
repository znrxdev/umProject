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
using umForms.Flotantes;

namespace umForms.Principales.Hijos
{
    public partial class umfMiHistorialSanciones : Form
    {
        CnSancionesAcademicas cnSancionesAcademicas = new CnSancionesAcademicas();
        UtFormularios utFormularios = new UtFormularios();

        public umfMiHistorialSanciones()
        {
            InitializeComponent();
        }

        private void umfMiHistorialSanciones_Load(object sender, EventArgs e)
        {
            CargarSanciones();
        }

        public void CargarSanciones()
        {
            try
            {
                if (CeSesionUsuario.IdSesion == null || CeSesionUsuario.IdSesion == 0)
                {
                    MessageBox.Show("No hay sesión activa. Por favor, inicie sesión nuevamente.", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var listaSanciones = cnSancionesAcademicas.ObtenerMisSancionesAcademicas(
                    new CeSancionesAcademicas { IdEstudiante = CeSesionUsuario.IdSesion },
                    out int oNum,
                    out string oMsg);

                if (oNum == -1)
                {
                    MessageBox.Show($"Error al cargar las sanciones académicas:\n{oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (listaSanciones == null)
                {
                    MessageBox.Show("No se pudo obtener la información de las sanciones académicas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LlenarDgvSanciones(listaSanciones);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al cargar las sanciones académicas:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarDgvSanciones(List<CeSancionesAcademicas> listaSanciones)
        {
            try
            {
                dgv_Sanciones.Rows.Clear();
                UtFormularios util = new UtFormularios();
                CnUsuarios cnUsuarios = new CnUsuarios();

                if (listaSanciones == null || listaSanciones.Count == 0)
                {
                    return;
                }

                foreach (var sancion in listaSanciones)
                {
                    try
                    {
                        string tipoSancion = "N/A";
                        if (sancion.IdTipoSancion.HasValue && sancion.IdTipoSancion.Value > 0)
                        {
                            tipoSancion = util.ObtenerNombreCatalogo(sancion.IdTipoSancion.Value);
                            if (string.IsNullOrEmpty(tipoSancion))
                                tipoSancion = "N/A";
                        }

                        string severidad = "N/A";
                        if (sancion.IdSeveridad.HasValue && sancion.IdSeveridad.Value > 0)
                        {
                            severidad = util.ObtenerNombreCatalogo(sancion.IdSeveridad.Value);
                            if (string.IsNullOrEmpty(severidad))
                                severidad = "N/A";
                        }

                        string estado = "N/A";
                        if (sancion.IdEstado.HasValue && sancion.IdEstado.Value > 0)
                        {
                            estado = util.ObtenerNombreEstado(sancion.IdEstado.Value);
                            if (string.IsNullOrEmpty(estado))
                                estado = "N/A";
                        }

                        // Columna Apelable
                        string apelable = sancion.EsApelable == true ? "SI" : "NO";

                        // Columna Revisado Por
                        string revisadoPor = "N/A";
                        if (sancion.IdUsuarioResolucion.HasValue && sancion.IdUsuarioResolucion.Value > 0)
                        {
                            var usuarioRevisor = cnUsuarios.FiltrarUsuariosPorId(
                                new CeUsuarios { IdUsuario = sancion.IdUsuarioResolucion.Value }, 
                                out int oNum, 
                                out string oMsg);
                            if (oNum != -1 && usuarioRevisor.Count > 0)
                            {
                                revisadoPor = usuarioRevisor[0].Usuario ?? "N/A";
                            }
                        }

                        // Determinar si se puede apelar
                        bool puedeApelar = sancion.EsApelable == true 
                                        && sancion.IdEstado.HasValue 
                                        && sancion.IdEstado.Value == 1 // ACTIVO
                                        && sancion.FechaApelacion == null; // No ha sido apelada aún

                        string textoBoton = "N/A";
                        if (puedeApelar)
                        {
                            textoBoton = "Apelar";
                        }
                        else if (sancion.FechaApelacion != null)
                        {
                            textoBoton = "Apelada";
                        }
                        else if (sancion.EsApelable == false)
                        {
                            textoBoton = "No apelable";
                        }
                        else
                        {
                            textoBoton = "No disponible";
                        }

                        int rowIndex = dgv_Sanciones.Rows.Add(
                            sancion.IdSancion,
                            sancion.CodigoSancion ?? "N/A",
                            tipoSancion,
                            severidad,
                            sancion.FechaRegistro?.ToString("dd/MM/yyyy") ?? "N/A",
                            sancion.FechaFin?.ToString("dd/MM/yyyy") ?? "N/A",
                            estado,
                            sancion.Motivo ?? "N/A",
                            apelable,
                            revisadoPor,
                            "VER DETALLES",
                            textoBoton
                        );

                        // Configurar el botón según si se puede apelar
                        DataGridViewCell cellApelar = dgv_Sanciones.Rows[rowIndex].Cells["Accion_Apelar"];
                        cellApelar.ReadOnly = !puedeApelar;
                        
                        if (!puedeApelar)
                        {
                            cellApelar.Style.ForeColor = Color.Gray;
                            cellApelar.Style.BackColor = Color.FromArgb(240, 240, 240);
                        }
                        else
                        {
                            cellApelar.Style.ForeColor = Color.White;
                            cellApelar.Style.BackColor = Color.FromArgb(0, 120, 215);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Continuar con la siguiente sanción si hay error en una
                        System.Diagnostics.Debug.WriteLine($"Error al procesar sanción {sancion.IdSancion}: {ex.Message}");
                        MessageBox.Show($"Error al procesar sanción {sancion.IdSancion}:\n{ex.Message}", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al llenar la tabla de sanciones:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_Sanciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Solo procesar si es una columna de botón y la fila es válida
            if (e.RowIndex < 0)
            {
                return;
            }

            // Verificar si se hizo clic en el botón de ver detalles
            if (e.ColumnIndex == dgv_Sanciones.Columns["Accion_VerDetalles"].Index)
            {
                ProcesarClicVerDetalles(e);
            }
            // Verificar si se hizo clic en el botón de apelar
            else if (e.ColumnIndex == dgv_Sanciones.Columns["Accion_Apelar"].Index)
            {
                ProcesarClicBotonApelar(e);
            }
        }

        private void ProcesarClicBotonApelar(DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgv_Sanciones.Rows[e.RowIndex];
                
                // Verificar que el botón esté habilitado
                if (row.Cells["Accion_Apelar"].ReadOnly)
                {
                    // Mostrar mensaje explicativo según el motivo
                    string textoBoton = row.Cells["Accion_Apelar"].Value?.ToString() ?? string.Empty;
                    string apelable = row.Cells["Apelable"].Value?.ToString() ?? "NO";
                    
                    if (textoBoton == "No apelable" || apelable == "NO")
                    {
                        MessageBox.Show("Esta sanción no es apelable según las políticas establecidas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (textoBoton == "Apelada")
                    {
                        MessageBox.Show("Esta sanción ya fue apelada anteriormente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Esta sanción no puede ser apelada en este momento.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }

                // Verificar que el texto del botón sea "Apelar"
                string textoBotonVerificar = row.Cells["Accion_Apelar"].Value?.ToString() ?? string.Empty;
                if (textoBotonVerificar != "Apelar")
                {
                    return;
                }

                // Verificar que sea apelable
                string apelableVerificar = row.Cells["Apelable"].Value?.ToString() ?? "NO";
                if (apelableVerificar != "SI")
                {
                    MessageBox.Show("Esta sanción no es apelable según las políticas establecidas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el ID de la sanción
                if (row.Cells["Id_Sancion"].Value == null)
                {
                    MessageBox.Show("No se pudo identificar la sanción seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idSancion = Convert.ToInt32(row.Cells["Id_Sancion"].Value);
                string codigoSancion = row.Cells["Codigo_Sancion"].Value?.ToString() ?? "N/A";

                // Abrir formulario de apelación
                umfApelarSancion formApelar = new umfApelarSancion(idSancion, codigoSancion);
                if (formApelar.ShowDialog() == DialogResult.OK)
                {
                    // Recargar las sanciones después de apelar
                    CargarSanciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la acción:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcesarClicVerDetalles(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                DataGridViewRow row = dgv_Sanciones.Rows[e.RowIndex];

                // Obtener el ID de la sanción
                if (row.Cells["Id_Sancion"].Value == null)
                {
                    MessageBox.Show("No se pudo identificar la sanción seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idSancion = Convert.ToInt32(row.Cells["Id_Sancion"].Value);
                
                // Obtener los datos completos de la sanción
                var sancion = cnSancionesAcademicas.FiltrarSancionesPorId(
                    new CeSancionesAcademicas { IdSancion = idSancion }, 
                    out int oNum, 
                    out string oMsg);

                if (oNum == -1 || sancion == null || sancion.Count == 0)
                {
                    MessageBox.Show($"Error al obtener los detalles de la sanción:\n{oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Abrir formulario de detalles
                umfDetallesSancion formDetalles = new umfDetallesSancion(sancion.First());
                formDetalles.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la acción:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

