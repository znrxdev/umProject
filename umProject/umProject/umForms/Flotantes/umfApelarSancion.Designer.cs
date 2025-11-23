namespace umForms.Flotantes
{
    partial class umfApelarSancion
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
            btn_Apelar = new Button();
            txt_ComentariosApelacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_ComentariosApelacion = new ReaLTaiizor.Controls.CrownLabel();
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
            pnl_Encabezado.Size = new Size(600, 70);
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
            btn_CerrarFormulario.Location = new Point(560, 8);
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
            lb_Titulo.Size = new Size(200, 28);
            lb_Titulo.TabIndex = 0;
            lb_Titulo.Text = "APELAR SANCIÓN";
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Contenedor.Controls.Add(btn_Apelar);
            pnl_Contenedor.Controls.Add(txt_ComentariosApelacion);
            pnl_Contenedor.Controls.Add(lb_ComentariosApelacion);
            pnl_Contenedor.Controls.Add(lb_Instrucciones);
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Location = new Point(0, 70);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Padding = new Padding(20);
            pnl_Contenedor.Size = new Size(600, 330);
            pnl_Contenedor.TabIndex = 1;
            // 
            // btn_Apelar
            // 
            btn_Apelar.BackColor = Color.FromArgb(0, 120, 215);
            btn_Apelar.Cursor = Cursors.Hand;
            btn_Apelar.FlatAppearance.BorderSize = 0;
            btn_Apelar.FlatStyle = FlatStyle.Flat;
            btn_Apelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Apelar.ForeColor = Color.White;
            btn_Apelar.Location = new Point(20, 270);
            btn_Apelar.Name = "btn_Apelar";
            btn_Apelar.Size = new Size(560, 40);
            btn_Apelar.TabIndex = 3;
            btn_Apelar.Text = "ENVIAR APELACIÓN";
            btn_Apelar.UseVisualStyleBackColor = false;
            btn_Apelar.Click += btn_Apelar_Click;
            // 
            // txt_ComentariosApelacion
            // 
            txt_ComentariosApelacion.BackColor = Color.White;
            txt_ComentariosApelacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_ComentariosApelacion.CustomBGColor = Color.White;
            txt_ComentariosApelacion.Font = new Font("Segoe UI", 9F);
            txt_ComentariosApelacion.ForeColor = Color.Black;
            txt_ComentariosApelacion.Location = new Point(20, 100);
            txt_ComentariosApelacion.MaxLength = 500;
            txt_ComentariosApelacion.Multiline = true;
            txt_ComentariosApelacion.Name = "txt_ComentariosApelacion";
            txt_ComentariosApelacion.ReadOnly = false;
            txt_ComentariosApelacion.Size = new Size(560, 150);
            txt_ComentariosApelacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_ComentariosApelacion.TabIndex = 2;
            txt_ComentariosApelacion.Text = "";
            txt_ComentariosApelacion.TextAlignment = HorizontalAlignment.Left;
            txt_ComentariosApelacion.UseSystemPasswordChar = false;
            // 
            // lb_ComentariosApelacion
            // 
            lb_ComentariosApelacion.AutoSize = true;
            lb_ComentariosApelacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_ComentariosApelacion.ForeColor = Color.Black;
            lb_ComentariosApelacion.Location = new Point(20, 75);
            lb_ComentariosApelacion.Name = "lb_ComentariosApelacion";
            lb_ComentariosApelacion.Size = new Size(200, 19);
            lb_ComentariosApelacion.TabIndex = 1;
            lb_ComentariosApelacion.Text = "Comentarios de Apelación: *";
            // 
            // lb_Instrucciones
            // 
            lb_Instrucciones.AutoSize = true;
            lb_Instrucciones.Font = new Font("Segoe UI", 9F);
            lb_Instrucciones.ForeColor = Color.FromArgb(100, 100, 100);
            lb_Instrucciones.Location = new Point(20, 20);
            lb_Instrucciones.Name = "lb_Instrucciones";
            lb_Instrucciones.Size = new Size(560, 40);
            lb_Instrucciones.TabIndex = 0;
            lb_Instrucciones.Text = "Por favor, explique detalladamente los motivos de su apelación. Los comentarios deben tener al menos 10 caracteres y no exceder 500 caracteres.";
            // 
            // umfApelarSancion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(600, 400);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfApelarSancion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Apelar Sanción Académica";
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
        private ReaLTaiizor.Controls.CrownLabel lb_ComentariosApelacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_ComentariosApelacion;
        private Button btn_Apelar;
    }
}

