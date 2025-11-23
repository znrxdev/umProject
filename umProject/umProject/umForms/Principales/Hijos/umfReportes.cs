using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using umForms.Utilidades;

namespace umForms.Principales.Hijos
{
    public partial class umfReportes : Form
    {
        private readonly CnReportes cnReportes = new CnReportes();
        private List<CeReporteUsuario> listaUsuarios = new List<CeReporteUsuario>();
        private List<CeReporteAuditoria> listaAuditoria = new List<CeReporteAuditoria>();
        private string tipoReporteActual = "";

        public umfReportes()
        {
            InitializeComponent();
        }

        private void umfReportes_Load(object sender, EventArgs e)
        {
            dtp_FechaInicio.Value = DateTime.Now.AddMonths(-1);
            dtp_FechaFin.Value = DateTime.Now;
            dgv_Reporte.AutoGenerateColumns = false;
            ConfigurarEstiloDgv();
        }

        private void btn_UsuariosActivos_Click(object sender, EventArgs e)
        {
            try
            {
                tipoReporteActual = "UsuariosActivos";
                listaUsuarios = cnReportes.ObtenerReporteUsuariosActivos(
                    dtp_FechaInicio.Value.Date,
                    dtp_FechaFin.Value.Date,
                    out int oNum,
                    out string oMsg);

                if (oNum == -1)
                {
                    MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CargarDgvUsuarios(listaUsuarios);
                MessageBox.Show($"Se encontraron {listaUsuarios.Count} usuarios activos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_UsuariosInactivos_Click(object sender, EventArgs e)
        {
            try
            {
                tipoReporteActual = "UsuariosInactivos";
                listaUsuarios = cnReportes.ObtenerReporteUsuariosInactivos(
                    dtp_FechaInicio.Value.Date,
                    dtp_FechaFin.Value.Date,
                    out int oNum,
                    out string oMsg);

                if (oNum == -1)
                {
                    MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CargarDgvUsuarios(listaUsuarios);
                MessageBox.Show($"Se encontraron {listaUsuarios.Count} usuarios inactivos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Auditoria_Click(object sender, EventArgs e)
        {
            try
            {
                tipoReporteActual = "Auditoria";
                listaAuditoria = cnReportes.ObtenerReporteAuditoria(
                    dtp_FechaInicio.Value.Date,
                    dtp_FechaFin.Value.Date,
                    out int oNum,
                    out string oMsg);

                if (oNum == -1)
                {
                    MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CargarDgvAuditoria(listaAuditoria);
                MessageBox.Show($"Se encontraron {listaAuditoria.Count} registros de auditoría.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tipoReporteActual))
                {
                    MessageBox.Show("Por favor, genere un reporte primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Reporte_{tipoReporteActual}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (tipoReporteActual == "UsuariosActivos" || tipoReporteActual == "UsuariosInactivos")
                    {
                        string titulo = tipoReporteActual == "UsuariosActivos" 
                            ? "REPORTE DE USUARIOS ACTIVOS" 
                            : "REPORTE DE USUARIOS INACTIVOS";
                        
                        UtPdf.GenerarReporteUsuarios(
                            listaUsuarios, 
                            titulo, 
                            saveFileDialog.FileName,
                            dtp_FechaInicio.Value.Date,
                            dtp_FechaFin.Value.Date);
                    }
                    else if (tipoReporteActual == "Auditoria")
                    {
                        UtPdf.GenerarReporteAuditoria(
                            listaAuditoria, 
                            "REPORTE DE AUDITORÍA DEL SISTEMA", 
                            saveFileDialog.FileName,
                            dtp_FechaInicio.Value.Date,
                            dtp_FechaFin.Value.Date);
                    }

                    MessageBox.Show("PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDgvUsuarios(List<CeReporteUsuario> usuarios)
        {
            // Remover eventos anteriores
            dgv_Reporte.CellFormatting -= Dgv_Reporte_CellFormatting;
            
            dgv_Reporte.DataSource = null;
            dgv_Reporte.Rows.Clear();
            dgv_Reporte.Columns.Clear();

            // Configurar estilo del DataGridView igual que umfUsuarios
            ConfigurarEstiloDgv();

            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuario", Name = "Usuario", DataPropertyName = "Usuario", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre Completo", Name = "NombreCompleto", DataPropertyName = "NombreCompleto", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Documento", Name = "ValorDocumento", DataPropertyName = "ValorDocumento", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tipo Documento", Name = "TipoDocumento", DataPropertyName = "TipoDocumento", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha Nacimiento", Name = "FechaNacimiento", DataPropertyName = "FechaNacimiento", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Género", Name = "Genero", DataPropertyName = "Genero", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nacionalidad", Name = "Nacionalidad", DataPropertyName = "Nacionalidad", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado Civil", Name = "EstadoCivil", DataPropertyName = "EstadoCivil", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha Creación", Name = "FechaCreacionUsuario", DataPropertyName = "FechaCreacionUsuario", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Última Sesión", Name = "UltimaSesion", DataPropertyName = "UltimaSesion", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", Name = "EstadoUsuario", DataPropertyName = "EstadoUsuario", ReadOnly = true });

            dgv_Reporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Reporte.DataSource = usuarios;
        }

        private void CargarDgvAuditoria(List<CeReporteAuditoria> auditoria)
        {
            // Remover eventos anteriores si existen
            dgv_Reporte.CellFormatting -= Dgv_Reporte_CellFormatting;
            
            dgv_Reporte.DataSource = null;
            dgv_Reporte.Rows.Clear();
            dgv_Reporte.Columns.Clear();

            // Configurar estilo del DataGridView igual que umfUsuarios
            ConfigurarEstiloDgv();

            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", Name = "IdTransaccion", DataPropertyName = "IdTransaccion", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tipo Transacción", Name = "NombreTipoTransaccion", DataPropertyName = "NombreTipoTransaccion", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Concepto", Name = "Concepto", DataPropertyName = "Concepto", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tipo Entidad", Name = "TipoEntidad", DataPropertyName = "TipoEntidad", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Persona", Name = "NombrePersona", DataPropertyName = "NombrePersona", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuario", Name = "NombreUsuario", DataPropertyName = "NombreUsuario", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Autor", Name = "NombreAutor", DataPropertyName = "NombreAutor", ReadOnly = true });
            dgv_Reporte.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", Name = "FechaCreacion", DataPropertyName = "FechaCreacion", ReadOnly = true });
            
            // Columna para Completado vinculada al campo Completado
            var colCompletado = new DataGridViewTextBoxColumn 
            { 
                HeaderText = "Completado", 
                Name = "Completado", 
                DataPropertyName = "Completado",
                ReadOnly = true 
            };
            dgv_Reporte.Columns.Add(colCompletado);

            dgv_Reporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Reporte.DataSource = auditoria;

            // Agregar evento para formatear la columna Completado
            dgv_Reporte.CellFormatting += Dgv_Reporte_CellFormatting;
        }

        private void ConfigurarEstiloDgv()
        {
            dgv_Reporte.AllowUserToAddRows = false;
            dgv_Reporte.AllowUserToDeleteRows = false;
            dgv_Reporte.AllowUserToResizeRows = false;
            dgv_Reporte.BackgroundColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dgv_Reporte.BorderStyle = BorderStyle.None;
            dgv_Reporte.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_Reporte.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel),
                ForeColor = System.Drawing.Color.Black,
                SelectionBackColor = System.Drawing.Color.Gray,
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = DataGridViewTriState.True
            };
            dgv_Reporte.ColumnHeadersDefaultCellStyle = headerStyle;
            dgv_Reporte.ColumnHeadersHeight = 40;
            dgv_Reporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            var cellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.FromArgb(255, 255, 255),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel),
                ForeColor = System.Drawing.Color.Black,
                Padding = new Padding(8, 4, 8, 4),
                SelectionBackColor = System.Drawing.Color.Gray,
                SelectionForeColor = System.Drawing.Color.White,
                WrapMode = DataGridViewTriState.False
            };
            dgv_Reporte.DefaultCellStyle = cellStyle;
            dgv_Reporte.EnableHeadersVisualStyles = false;
            dgv_Reporte.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dgv_Reporte.GridColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dgv_Reporte.MultiSelect = false;
            dgv_Reporte.ReadOnly = true;
            dgv_Reporte.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_Reporte.RowHeadersVisible = false;
            dgv_Reporte.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_Reporte.RowTemplate.Height = 36;
            dgv_Reporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Dgv_Reporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (tipoReporteActual == "Auditoria" && dgv_Reporte.Columns[e.ColumnIndex].Name == "Completado")
            {
                if (e.Value != null && e.Value is bool completado)
                {
                    e.Value = completado ? "SI" : "NO";
                    e.FormattingApplied = true;
                }
                else if (e.Value != null && e.Value is bool completadoNullable)
                {
                    e.Value = (completadoNullable == true) ? "SI" : "NO";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}

