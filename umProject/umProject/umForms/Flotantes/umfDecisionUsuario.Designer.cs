namespace umForms.Flotantes
{
    partial class umfDecisionUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnl_Encabezado = new Panel();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            lb_Titulo = new ReaLTaiizor.Controls.CrownLabel();
            btn_Usuario = new Button();
            btn_Permisos = new Button();
            pnl_Contenedor = new Panel();
            pnl_Encabezado.SuspendLayout();
            pnl_Contenedor.SuspendLayout();
            SuspendLayout();
            // 
            // pnl_Encabezado
            // 
            pnl_Encabezado.BackColor = Color.FromArgb(20, 40, 70);
            pnl_Encabezado.Controls.Add(btn_CerrarFormulario);
            pnl_Encabezado.Controls.Add(lb_Titulo);
            pnl_Encabezado.Dock = DockStyle.Top;
            pnl_Encabezado.Location = new Point(0, 0);
            pnl_Encabezado.Name = "pnl_Encabezado";
            pnl_Encabezado.Size = new Size(406, 70);
            pnl_Encabezado.TabIndex = 0;
            // 
            // btn_CerrarFormulario
            // 
            btn_CerrarFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_CerrarFormulario.BackColor = Color.FromArgb(200, 70, 70);
            btn_CerrarFormulario.Cursor = Cursors.Hand;
            btn_CerrarFormulario.FlatAppearance.BorderSize = 0;
            btn_CerrarFormulario.FlatStyle = FlatStyle.Flat;
            btn_CerrarFormulario.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btn_CerrarFormulario.IconColor = Color.White;
            btn_CerrarFormulario.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btn_CerrarFormulario.IconSize = 18;
            btn_CerrarFormulario.Location = new Point(365, 8);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(32, 32);
            btn_CerrarFormulario.TabIndex = 11;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_Titulo
            // 
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(40, 22);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(327, 25);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "SELECCIONE UNA OPCIÓN";
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Contenedor.Controls.Add(btn_Permisos);
            pnl_Contenedor.Controls.Add(btn_Usuario);
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Padding = new Padding(20);
            pnl_Contenedor.Location = new Point(0, 70);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Size = new Size(406, 120);
            pnl_Contenedor.TabIndex = 2;
            // 
            // btn_Usuario
            // 
            btn_Usuario.BackColor = Color.FromArgb(0, 120, 215);
            btn_Usuario.Cursor = Cursors.Hand;
            btn_Usuario.FlatAppearance.BorderSize = 0;
            btn_Usuario.FlatStyle = FlatStyle.Flat;
            btn_Usuario.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Usuario.ForeColor = Color.White;
            btn_Usuario.Location = new Point(20, 15);
            btn_Usuario.Name = "btn_Usuario";
            btn_Usuario.Size = new Size(366, 40);
            btn_Usuario.TabIndex = 9;
            btn_Usuario.Text = "👤 DATOS USUARIO";
            btn_Usuario.UseVisualStyleBackColor = false;
            btn_Usuario.Click += btn_Usuario_Click;
            // 
            // btn_Permisos
            // 
            btn_Permisos.BackColor = Color.FromArgb(0, 150, 100);
            btn_Permisos.Cursor = Cursors.Hand;
            btn_Permisos.FlatAppearance.BorderSize = 0;
            btn_Permisos.FlatStyle = FlatStyle.Flat;
            btn_Permisos.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Permisos.ForeColor = Color.White;
            btn_Permisos.Location = new Point(20, 60);
            btn_Permisos.Name = "btn_Permisos";
            btn_Permisos.Size = new Size(366, 40);
            btn_Permisos.TabIndex = 10;
            btn_Permisos.Text = "🔐 PERMISOS";
            btn_Permisos.UseVisualStyleBackColor = false;
            btn_Permisos.Click += btn_Permisos_Click;
            // 
            // umfDecisionUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(406, 190);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfDecisionUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfDecisionUsuario_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Encabezado;
        private System.Windows.Forms.Panel pnl_Contenedor;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private System.Windows.Forms.Button btn_Usuario;
        private System.Windows.Forms.Button btn_Permisos;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
    }
}