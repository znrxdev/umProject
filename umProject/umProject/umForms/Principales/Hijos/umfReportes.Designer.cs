using System.Windows.Forms;

namespace umForms.Principales.Hijos
{
    partial class umfReportes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnl_Filtros = new Panel();
            lbl_Titulo = new Label();
            lbl_FechaInicio = new Label();
            dtp_FechaInicio = new DateTimePicker();
            lbl_FechaFin = new Label();
            dtp_FechaFin = new DateTimePicker();
            btn_UsuariosActivos = new ReaLTaiizor.Controls.Button();
            btn_UsuariosInactivos = new ReaLTaiizor.Controls.Button();
            btn_Auditoria = new ReaLTaiizor.Controls.Button();
            btn_ExportarPDF = new ReaLTaiizor.Controls.Button();
            dgv_Reporte = new ReaLTaiizor.Controls.PoisonDataGridView();
            pnl_Filtros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Reporte).BeginInit();
            SuspendLayout();
            // 
            // pnl_Filtros
            // 
            pnl_Filtros.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            pnl_Filtros.Controls.Add(btn_ExportarPDF);
            pnl_Filtros.Controls.Add(btn_Auditoria);
            pnl_Filtros.Controls.Add(btn_UsuariosInactivos);
            pnl_Filtros.Controls.Add(btn_UsuariosActivos);
            pnl_Filtros.Controls.Add(dtp_FechaFin);
            pnl_Filtros.Controls.Add(lbl_FechaFin);
            pnl_Filtros.Controls.Add(dtp_FechaInicio);
            pnl_Filtros.Controls.Add(lbl_FechaInicio);
            pnl_Filtros.Controls.Add(lbl_Titulo);
            pnl_Filtros.Dock = DockStyle.Top;
            pnl_Filtros.Location = new System.Drawing.Point(0, 0);
            pnl_Filtros.Name = "pnl_Filtros";
            pnl_Filtros.Size = new System.Drawing.Size(1200, 120);
            pnl_Filtros.TabIndex = 0;
            // 
            // lbl_Titulo
            // 
            lbl_Titulo.AutoSize = true;
            lbl_Titulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lbl_Titulo.Location = new System.Drawing.Point(15, 15);
            lbl_Titulo.Name = "lbl_Titulo";
            lbl_Titulo.Size = new System.Drawing.Size(85, 25);
            lbl_Titulo.TabIndex = 0;
            lbl_Titulo.Text = "REPORTES";
            // 
            // lbl_FechaInicio
            // 
            lbl_FechaInicio.AutoSize = true;
            lbl_FechaInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            lbl_FechaInicio.Location = new System.Drawing.Point(15, 55);
            lbl_FechaInicio.Name = "lbl_FechaInicio";
            lbl_FechaInicio.Size = new System.Drawing.Size(90, 19);
            lbl_FechaInicio.TabIndex = 1;
            lbl_FechaInicio.Text = "Fecha Inicio:";
            // 
            // dtp_FechaInicio
            // 
            dtp_FechaInicio.Format = DateTimePickerFormat.Short;
            dtp_FechaInicio.Location = new System.Drawing.Point(110, 53);
            dtp_FechaInicio.Name = "dtp_FechaInicio";
            dtp_FechaInicio.Size = new System.Drawing.Size(150, 23);
            dtp_FechaInicio.TabIndex = 2;
            // 
            // lbl_FechaFin
            // 
            lbl_FechaFin.AutoSize = true;
            lbl_FechaFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            lbl_FechaFin.Location = new System.Drawing.Point(280, 55);
            lbl_FechaFin.Name = "lbl_FechaFin";
            lbl_FechaFin.Size = new System.Drawing.Size(75, 19);
            lbl_FechaFin.TabIndex = 3;
            lbl_FechaFin.Text = "Fecha Fin:";
            // 
            // dtp_FechaFin
            // 
            dtp_FechaFin.Format = DateTimePickerFormat.Short;
            dtp_FechaFin.Location = new System.Drawing.Point(360, 53);
            dtp_FechaFin.Name = "dtp_FechaFin";
            dtp_FechaFin.Size = new System.Drawing.Size(150, 23);
            dtp_FechaFin.TabIndex = 4;
            // 
            // btn_UsuariosActivos
            // 
            btn_UsuariosActivos.BackColor = System.Drawing.Color.FromArgb(34, 139, 34);
            btn_UsuariosActivos.ForeColor = System.Drawing.Color.White;
            btn_UsuariosActivos.Location = new System.Drawing.Point(540, 50);
            btn_UsuariosActivos.Name = "btn_UsuariosActivos";
            btn_UsuariosActivos.Size = new System.Drawing.Size(150, 30);
            btn_UsuariosActivos.TabIndex = 5;
            btn_UsuariosActivos.Text = "Usuarios Activos";
            btn_UsuariosActivos.Click += btn_UsuariosActivos_Click;
            // 
            // btn_UsuariosInactivos
            // 
            btn_UsuariosInactivos.BackColor = System.Drawing.Color.FromArgb(220, 20, 60);
            btn_UsuariosInactivos.ForeColor = System.Drawing.Color.White;
            btn_UsuariosInactivos.Location = new System.Drawing.Point(700, 50);
            btn_UsuariosInactivos.Name = "btn_UsuariosInactivos";
            btn_UsuariosInactivos.Size = new System.Drawing.Size(150, 30);
            btn_UsuariosInactivos.TabIndex = 6;
            btn_UsuariosInactivos.Text = "Usuarios Inactivos";
            btn_UsuariosInactivos.Click += btn_UsuariosInactivos_Click;
            // 
            // btn_Auditoria
            // 
            btn_Auditoria.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            btn_Auditoria.ForeColor = System.Drawing.Color.White;
            btn_Auditoria.Location = new System.Drawing.Point(860, 50);
            btn_Auditoria.Name = "btn_Auditoria";
            btn_Auditoria.Size = new System.Drawing.Size(150, 30);
            btn_Auditoria.TabIndex = 7;
            btn_Auditoria.Text = "Auditoría";
            btn_Auditoria.Click += btn_Auditoria_Click;
            // 
            // btn_ExportarPDF
            // 
            btn_ExportarPDF.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            btn_ExportarPDF.ForeColor = System.Drawing.Color.White;
            btn_ExportarPDF.Location = new System.Drawing.Point(1020, 50);
            btn_ExportarPDF.Name = "btn_ExportarPDF";
            btn_ExportarPDF.Size = new System.Drawing.Size(150, 30);
            btn_ExportarPDF.TabIndex = 8;
            btn_ExportarPDF.Text = "Exportar PDF";
            btn_ExportarPDF.Click += btn_ExportarPDF_Click;
            // 
            // dgv_Reporte
            // 
            dgv_Reporte.Dock = DockStyle.Fill;
            dgv_Reporte.Location = new System.Drawing.Point(0, 120);
            dgv_Reporte.Name = "dgv_Reporte";
            dgv_Reporte.Size = new System.Drawing.Size(1200, 487);
            dgv_Reporte.TabIndex = 1;
            // 
            // umfReportes
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            ClientSize = new System.Drawing.Size(1200, 607);
            Controls.Add(dgv_Reporte);
            Controls.Add(pnl_Filtros);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "umfReportes";
            Text = "Reportes";
            Load += umfReportes_Load;
            pnl_Filtros.ResumeLayout(false);
            pnl_Filtros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Reporte).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnl_Filtros;
        private Label lbl_Titulo;
        private Label lbl_FechaInicio;
        private DateTimePicker dtp_FechaInicio;
        private Label lbl_FechaFin;
        private DateTimePicker dtp_FechaFin;
        private ReaLTaiizor.Controls.Button btn_UsuariosActivos;
        private ReaLTaiizor.Controls.Button btn_UsuariosInactivos;
        private ReaLTaiizor.Controls.Button btn_Auditoria;
        private ReaLTaiizor.Controls.Button btn_ExportarPDF;
        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Reporte;
    }
}

