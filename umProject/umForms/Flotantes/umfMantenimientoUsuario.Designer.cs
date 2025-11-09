namespace umForms.Flotantes
{
    partial class umfMantenimientoUsuario
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
            pnl_Arriba = new Panel();
            lb_Titulo = new Label();
            grb_datos_usuario = new GroupBox();
            btn_GuardarDatos = new Button();
            lb_Estado = new Label();
            cmb_Estados = new ComboBox();
            txt_Contrasena = new TextBox();
            lb_Contrasena = new Label();
            txt_Usuario = new TextBox();
            lb_Usuario = new Label();
            pnl_Arriba.SuspendLayout();
            grb_datos_usuario.SuspendLayout();
            SuspendLayout();
            // 
            // pnl_Arriba
            // 
            pnl_Arriba.BackColor = Color.FromArgb(10, 24, 51);
            pnl_Arriba.Controls.Add(lb_Titulo);
            pnl_Arriba.Dock = DockStyle.Top;
            pnl_Arriba.Location = new Point(0, 0);
            pnl_Arriba.Margin = new Padding(3, 2, 3, 2);
            pnl_Arriba.Name = "pnl_Arriba";
            pnl_Arriba.Size = new Size(440, 60);
            pnl_Arriba.TabIndex = 1;
            // 
            // lb_Titulo
            // 
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(116, 21);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(201, 19);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "INFORMACION USUARIO";
            // 
            // grb_datos_usuario
            // 
            grb_datos_usuario.Controls.Add(btn_GuardarDatos);
            grb_datos_usuario.Controls.Add(lb_Estado);
            grb_datos_usuario.Controls.Add(cmb_Estados);
            grb_datos_usuario.Controls.Add(txt_Contrasena);
            grb_datos_usuario.Controls.Add(lb_Contrasena);
            grb_datos_usuario.Controls.Add(txt_Usuario);
            grb_datos_usuario.Controls.Add(lb_Usuario);
            grb_datos_usuario.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grb_datos_usuario.Location = new Point(18, 93);
            grb_datos_usuario.Margin = new Padding(3, 2, 3, 2);
            grb_datos_usuario.Name = "grb_datos_usuario";
            grb_datos_usuario.Padding = new Padding(3, 2, 3, 2);
            grb_datos_usuario.Size = new Size(402, 336);
            grb_datos_usuario.TabIndex = 2;
            grb_datos_usuario.TabStop = false;
            grb_datos_usuario.Text = "Datos Usuario:";
            // 
            // btn_GuardarDatos
            // 
            btn_GuardarDatos.BackColor = Color.FromArgb(9, 46, 85);
            btn_GuardarDatos.FlatStyle = FlatStyle.Flat;
            btn_GuardarDatos.ForeColor = Color.White;
            btn_GuardarDatos.Location = new Point(0, 253);
            btn_GuardarDatos.Margin = new Padding(3, 2, 3, 2);
            btn_GuardarDatos.Name = "btn_GuardarDatos";
            btn_GuardarDatos.Size = new Size(402, 44);
            btn_GuardarDatos.TabIndex = 17;
            btn_GuardarDatos.Text = "GUARDAR";
            btn_GuardarDatos.UseVisualStyleBackColor = false;
            btn_GuardarDatos.Click += btn_GuardarDatos_Click;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Location = new Point(4, 172);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(55, 16);
            lb_Estado.TabIndex = 5;
            lb_Estado.Text = "Estado:";
            // 
            // cmb_Estados
            // 
            cmb_Estados.FormattingEnabled = true;
            cmb_Estados.Location = new Point(10, 197);
            cmb_Estados.Margin = new Padding(3, 2, 3, 2);
            cmb_Estados.Name = "cmb_Estados";
            cmb_Estados.Size = new Size(376, 24);
            cmb_Estados.TabIndex = 4;
            // 
            // txt_Contrasena
            // 
            txt_Contrasena.Location = new Point(10, 130);
            txt_Contrasena.Margin = new Padding(3, 2, 3, 2);
            txt_Contrasena.Name = "txt_Contrasena";
            txt_Contrasena.Size = new Size(376, 23);
            txt_Contrasena.TabIndex = 3;
            // 
            // lb_Contrasena
            // 
            lb_Contrasena.AutoSize = true;
            lb_Contrasena.Location = new Point(4, 102);
            lb_Contrasena.Name = "lb_Contrasena";
            lb_Contrasena.Size = new Size(85, 16);
            lb_Contrasena.TabIndex = 2;
            lb_Contrasena.Text = "Contraseña:";
            // 
            // txt_Usuario
            // 
            txt_Usuario.Location = new Point(10, 65);
            txt_Usuario.Margin = new Padding(3, 2, 3, 2);
            txt_Usuario.Name = "txt_Usuario";
            txt_Usuario.Size = new Size(376, 23);
            txt_Usuario.TabIndex = 1;
            // 
            // lb_Usuario
            // 
            lb_Usuario.AutoSize = true;
            lb_Usuario.Location = new Point(4, 38);
            lb_Usuario.Name = "lb_Usuario";
            lb_Usuario.Size = new Size(59, 16);
            lb_Usuario.TabIndex = 0;
            lb_Usuario.Text = "Usuario:";
            // 
            // umfMantenimientoUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 471);
            Controls.Add(grb_datos_usuario);
            Controls.Add(pnl_Arriba);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            MaximumSize = new Size(440, 471);
            MinimumSize = new Size(440, 471);
            Name = "umfMantenimientoUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfMantenimientoUsuario_Load;
            pnl_Arriba.ResumeLayout(false);
            pnl_Arriba.PerformLayout();
            grb_datos_usuario.ResumeLayout(false);
            grb_datos_usuario.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnl_Arriba;
        private System.Windows.Forms.Label lb_Titulo;
        private System.Windows.Forms.GroupBox grb_datos_usuario;
        private System.Windows.Forms.Label lb_Usuario;
        private System.Windows.Forms.Label lb_Estado;
        private System.Windows.Forms.ComboBox cmb_Estados;
        private System.Windows.Forms.TextBox txt_Contrasena;
        private System.Windows.Forms.Label lb_Contrasena;
        private System.Windows.Forms.TextBox txt_Usuario;
        private System.Windows.Forms.Button btn_GuardarDatos;
    }
}