namespace umForms.Flotantes
{
    partial class umfResponderApelacion
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
            pnl_Contenedor = new Panel();
            btn_Rechazar = new Button();
            btn_Aprobar = new Button();
            txt_ResultadoApelacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_ResultadoApelacion = new ReaLTaiizor.Controls.CrownLabel();
            txt_ComentariosEstudiante = new ReaLTaiizor.Controls.SmallTextBox();
            lb_ComentariosEstudiante = new ReaLTaiizor.Controls.CrownLabel();
            lb_Instrucciones = new ReaLTaiizor.Controls.CrownLabel();
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
            pnl_Encabezado.Size = new Size(700, 70);
            pnl_Encabezado.TabIndex = 0;
            // 
            // btn_CerrarFormulario
            // 
            btn_CerrarFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_CerrarFormulario.BackColor = Color.FromArgb(200, 70, 70);
            btn_CerrarFormulario.Cursor = Cursors.Hand;
            btn_CerrarFormulario.FlatAppearance.BorderSize = 0;
            btn_CerrarFormulario.FlatStyle = FlatStyle.Flat;
            btn_CerrarFormulario.IconChar = FontAwesome.Sharp.IconChar.Close;
            btn_CerrarFormulario.IconColor = Color.White;
            btn_CerrarFormulario.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btn_CerrarFormulario.IconSize = 18;
            btn_CerrarFormulario.Location = new Point(660, 8);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(32, 32);
            btn_CerrarFormulario.TabIndex = 1;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_Titulo
            // 
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(20, 20);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(250, 28);
            lb_Titulo.TabIndex = 0;
            lb_Titulo.Text = "RESPONDER APELACIÓN";
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Contenedor.Controls.Add(btn_Rechazar);
            pnl_Contenedor.Controls.Add(btn_Aprobar);
            pnl_Contenedor.Controls.Add(txt_ResultadoApelacion);
            pnl_Contenedor.Controls.Add(lb_ResultadoApelacion);
            pnl_Contenedor.Controls.Add(txt_ComentariosEstudiante);
            pnl_Contenedor.Controls.Add(lb_ComentariosEstudiante);
            pnl_Contenedor.Controls.Add(lb_Instrucciones);
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Location = new Point(0, 70);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Padding = new Padding(20);
            pnl_Contenedor.Size = new Size(700, 480);
            pnl_Contenedor.TabIndex = 1;
            // 
            // btn_Rechazar
            // 
            btn_Rechazar.BackColor = Color.FromArgb(200, 70, 70);
            btn_Rechazar.Cursor = Cursors.Hand;
            btn_Rechazar.FlatAppearance.BorderSize = 0;
            btn_Rechazar.FlatStyle = FlatStyle.Flat;
            btn_Rechazar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Rechazar.ForeColor = Color.White;
            btn_Rechazar.Location = new Point(360, 420);
            btn_Rechazar.Name = "btn_Rechazar";
            btn_Rechazar.Size = new Size(320, 40);
            btn_Rechazar.TabIndex = 6;
            btn_Rechazar.Text = "RECHAZAR APELACIÓN";
            btn_Rechazar.UseVisualStyleBackColor = false;
            btn_Rechazar.Click += btn_Rechazar_Click;
            // 
            // btn_Aprobar
            // 
            btn_Aprobar.BackColor = Color.FromArgb(0, 150, 0);
            btn_Aprobar.Cursor = Cursors.Hand;
            btn_Aprobar.FlatAppearance.BorderSize = 0;
            btn_Aprobar.FlatStyle = FlatStyle.Flat;
            btn_Aprobar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Aprobar.ForeColor = Color.White;
            btn_Aprobar.Location = new Point(20, 420);
            btn_Aprobar.Name = "btn_Aprobar";
            btn_Aprobar.Size = new Size(320, 40);
            btn_Aprobar.TabIndex = 5;
            btn_Aprobar.Text = "APROBAR APELACIÓN";
            btn_Aprobar.UseVisualStyleBackColor = false;
            btn_Aprobar.Click += btn_Aprobar_Click;
            // 
            // txt_ResultadoApelacion
            // 
            txt_ResultadoApelacion.BackColor = Color.White;
            txt_ResultadoApelacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_ResultadoApelacion.CustomBGColor = Color.White;
            txt_ResultadoApelacion.Font = new Font("Segoe UI", 9F);
            txt_ResultadoApelacion.ForeColor = Color.Black;
            txt_ResultadoApelacion.Location = new Point(20, 320);
            txt_ResultadoApelacion.MaxLength = 200;
            txt_ResultadoApelacion.Multiline = true;
            txt_ResultadoApelacion.Name = "txt_ResultadoApelacion";
            txt_ResultadoApelacion.ReadOnly = false;
            txt_ResultadoApelacion.Size = new Size(660, 80);
            txt_ResultadoApelacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_ResultadoApelacion.TabIndex = 4;
            txt_ResultadoApelacion.Text = "";
            txt_ResultadoApelacion.TextAlignment = HorizontalAlignment.Left;
            txt_ResultadoApelacion.UseSystemPasswordChar = false;
            // 
            // lb_ResultadoApelacion
            // 
            lb_ResultadoApelacion.AutoSize = true;
            lb_ResultadoApelacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_ResultadoApelacion.ForeColor = Color.Black;
            lb_ResultadoApelacion.Location = new Point(20, 295);
            lb_ResultadoApelacion.Name = "lb_ResultadoApelacion";
            lb_ResultadoApelacion.Size = new Size(200, 19);
            lb_ResultadoApelacion.TabIndex = 3;
            lb_ResultadoApelacion.Text = "Resultado de la Apelación: *";
            // 
            // txt_ComentariosEstudiante
            // 
            txt_ComentariosEstudiante.BackColor = Color.FromArgb(245, 245, 245);
            txt_ComentariosEstudiante.BorderColor = Color.FromArgb(180, 180, 180);
            txt_ComentariosEstudiante.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_ComentariosEstudiante.Font = new Font("Segoe UI", 9F);
            txt_ComentariosEstudiante.ForeColor = Color.Black;
            txt_ComentariosEstudiante.Location = new Point(20, 150);
            txt_ComentariosEstudiante.MaxLength = 500;
            txt_ComentariosEstudiante.Multiline = true;
            txt_ComentariosEstudiante.Name = "txt_ComentariosEstudiante";
            txt_ComentariosEstudiante.ReadOnly = true;
            txt_ComentariosEstudiante.Size = new Size(660, 120);
            txt_ComentariosEstudiante.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_ComentariosEstudiante.TabIndex = 2;
            txt_ComentariosEstudiante.Text = "";
            txt_ComentariosEstudiante.TextAlignment = HorizontalAlignment.Left;
            txt_ComentariosEstudiante.UseSystemPasswordChar = false;
            // 
            // lb_ComentariosEstudiante
            // 
            lb_ComentariosEstudiante.AutoSize = true;
            lb_ComentariosEstudiante.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_ComentariosEstudiante.ForeColor = Color.Black;
            lb_ComentariosEstudiante.Location = new Point(20, 125);
            lb_ComentariosEstudiante.Name = "lb_ComentariosEstudiante";
            lb_ComentariosEstudiante.Size = new Size(250, 19);
            lb_ComentariosEstudiante.TabIndex = 1;
            lb_ComentariosEstudiante.Text = "Comentarios del Estudiante:";
            // 
            // lb_Instrucciones
            // 
            lb_Instrucciones.AutoSize = true;
            lb_Instrucciones.Font = new Font("Segoe UI", 9F);
            lb_Instrucciones.ForeColor = Color.FromArgb(100, 100, 100);
            lb_Instrucciones.Location = new Point(20, 20);
            lb_Instrucciones.Name = "lb_Instrucciones";
            lb_Instrucciones.Size = new Size(660, 80);
            lb_Instrucciones.TabIndex = 0;
            lb_Instrucciones.Text = "Revise los comentarios del estudiante y determine si la apelación debe ser aprobada o rechazada. Ingrese el resultado de su decisión en el campo correspondiente. El resultado debe tener al menos 10 caracteres y no exceder 200 caracteres.";
            // 
            // umfResponderApelacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(700, 550);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfResponderApelacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Responder Apelación";
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            pnl_Contenedor.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnl_Encabezado;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private Panel pnl_Contenedor;
        private ReaLTaiizor.Controls.CrownLabel lb_Instrucciones;
        private ReaLTaiizor.Controls.CrownLabel lb_ComentariosEstudiante;
        private ReaLTaiizor.Controls.SmallTextBox txt_ComentariosEstudiante;
        private ReaLTaiizor.Controls.CrownLabel lb_ResultadoApelacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_ResultadoApelacion;
        private Button btn_Aprobar;
        private Button btn_Rechazar;
    }
}

