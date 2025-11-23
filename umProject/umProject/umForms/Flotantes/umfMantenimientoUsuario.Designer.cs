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
            pnl_Encabezado = new Panel();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            lb_Titulo = new ReaLTaiizor.Controls.CrownLabel();
            pnl_Contenedor = new Panel();
            btn_GuardarDatos = new Button();
            cmb_Estados = new ComboBox();
            lb_Estado = new ReaLTaiizor.Controls.CrownLabel();
            txt_Contrasena = new ReaLTaiizor.Controls.SmallTextBox();
            lb_Contrasena = new ReaLTaiizor.Controls.CrownLabel();
            txt_Usuario = new ReaLTaiizor.Controls.SmallTextBox();
            lb_Usuario = new ReaLTaiizor.Controls.CrownLabel();
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
            pnl_Encabezado.Margin = new Padding(3, 4, 3, 4);
            pnl_Encabezado.Name = "pnl_Encabezado";
            pnl_Encabezado.Size = new Size(575, 93);
            pnl_Encabezado.TabIndex = 1;
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
            btn_CerrarFormulario.Location = new Point(529, 11);
            btn_CerrarFormulario.Margin = new Padding(3, 4, 3, 4);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(37, 43);
            btn_CerrarFormulario.TabIndex = 12;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_Titulo
            // 
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(40, 29);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(273, 30);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "INFORMACIÓN USUARIO";
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Contenedor.Controls.Add(btn_GuardarDatos);
            pnl_Contenedor.Controls.Add(cmb_Estados);
            pnl_Contenedor.Controls.Add(lb_Estado);
            pnl_Contenedor.Controls.Add(txt_Contrasena);
            pnl_Contenedor.Controls.Add(lb_Contrasena);
            pnl_Contenedor.Controls.Add(txt_Usuario);
            pnl_Contenedor.Controls.Add(lb_Usuario);
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Location = new Point(0, 93);
            pnl_Contenedor.Margin = new Padding(3, 4, 3, 4);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Padding = new Padding(34, 40, 34, 40);
            pnl_Contenedor.Size = new Size(575, 744);
            pnl_Contenedor.TabIndex = 2;
            // 
            // btn_GuardarDatos
            // 
            btn_GuardarDatos.BackColor = Color.FromArgb(0, 150, 100);
            btn_GuardarDatos.Cursor = Cursors.Hand;
            btn_GuardarDatos.FlatAppearance.BorderSize = 0;
            btn_GuardarDatos.FlatStyle = FlatStyle.Flat;
            btn_GuardarDatos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn_GuardarDatos.ForeColor = Color.White;
            btn_GuardarDatos.Location = new Point(34, 373);
            btn_GuardarDatos.Margin = new Padding(3, 4, 3, 4);
            btn_GuardarDatos.Name = "btn_GuardarDatos";
            btn_GuardarDatos.Size = new Size(506, 60);
            btn_GuardarDatos.TabIndex = 17;
            btn_GuardarDatos.Text = "💾 GUARDAR";
            btn_GuardarDatos.UseVisualStyleBackColor = false;
            btn_GuardarDatos.Click += btn_GuardarDatos_Click;
            // 
            // cmb_Estados
            // 
            cmb_Estados.BackColor = Color.White;
            cmb_Estados.ForeColor = Color.Black;
            cmb_Estados.FormattingEnabled = true;
            cmb_Estados.Location = new Point(34, 287);
            cmb_Estados.Margin = new Padding(3, 4, 3, 4);
            cmb_Estados.Name = "cmb_Estados";
            cmb_Estados.Size = new Size(506, 28);
            cmb_Estados.TabIndex = 4;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Estado.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Estado.Location = new Point(34, 253);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(68, 23);
            lb_Estado.TabIndex = 4;
            lb_Estado.Text = "Estado:";
            // 
            // txt_Contrasena
            // 
            txt_Contrasena.BackColor = Color.White;
            txt_Contrasena.Font = new Font("Segoe UI", 9F);
            txt_Contrasena.ForeColor = Color.FromArgb(124, 133, 142);
            txt_Contrasena.Location = new Point(34, 180);
            txt_Contrasena.Margin = new Padding(3, 4, 3, 4);
            txt_Contrasena.MaxLength = 50;
            txt_Contrasena.Name = "txt_Contrasena";
            txt_Contrasena.Size = new Size(506, 27);
            txt_Contrasena.TabIndex = 3;
            txt_Contrasena.UseSystemPasswordChar = true;
            // 
            // lb_Contrasena
            // 
            lb_Contrasena.AutoSize = true;
            lb_Contrasena.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Contrasena.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Contrasena.Location = new Point(34, 147);
            lb_Contrasena.Name = "lb_Contrasena";
            lb_Contrasena.Size = new Size(104, 23);
            lb_Contrasena.TabIndex = 2;
            lb_Contrasena.Text = "Contraseña:";
            // 
            // txt_Usuario
            // 
            txt_Usuario.BackColor = Color.White;
            txt_Usuario.Font = new Font("Segoe UI", 9F);
            txt_Usuario.ForeColor = Color.Black;
            txt_Usuario.Location = new Point(34, 73);
            txt_Usuario.Margin = new Padding(3, 4, 3, 4);
            txt_Usuario.MaxLength = 50;
            txt_Usuario.Name = "txt_Usuario";
            txt_Usuario.Size = new Size(506, 27);
            txt_Usuario.TabIndex = 1;
            // 
            // lb_Usuario
            // 
            lb_Usuario.AutoSize = true;
            lb_Usuario.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Usuario.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Usuario.Location = new Point(34, 40);
            lb_Usuario.Name = "lb_Usuario";
            lb_Usuario.Size = new Size(75, 23);
            lb_Usuario.TabIndex = 0;
            lb_Usuario.Text = "Usuario:";
            // 
            // umfMantenimientoUsuario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(575, 837);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(575, 837);
            MinimumSize = new Size(575, 837);
            Name = "umfMantenimientoUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfMantenimientoUsuario_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            pnl_Contenedor.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnl_Encabezado;
        private System.Windows.Forms.Panel pnl_Contenedor;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private ReaLTaiizor.Controls.CrownLabel lb_Usuario;
        private ReaLTaiizor.Controls.CrownLabel lb_Estado;
        private ReaLTaiizor.Controls.CrownLabel lb_Contrasena;
        private System.Windows.Forms.ComboBox cmb_Estados;
        private ReaLTaiizor.Controls.SmallTextBox txt_Contrasena;
        private ReaLTaiizor.Controls.SmallTextBox txt_Usuario;
        private System.Windows.Forms.Button btn_GuardarDatos;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
    }
}