namespace umForms.Principales
{
    partial class umfInicioSesion
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
            txt_Usuario = new ReaLTaiizor.Controls.AloneTextBox();
            txt_Contrasena = new ReaLTaiizor.Controls.AloneTextBox();
            btn_IniciarSesion = new ReaLTaiizor.Controls.Button();
            cnt_ControlVentana = new ReaLTaiizor.Controls.ControlBox();
            lb_InicioSesion = new ReaLTaiizor.Controls.CrownLabel();
            pnl_Logo = new ReaLTaiizor.Controls.NightPanel();
            pb_Logo = new ReaLTaiizor.Controls.HopePictureBox();
            pnl_Logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Logo).BeginInit();
            SuspendLayout();
            // 
            // txt_Usuario
            // 
            txt_Usuario.BackColor = Color.Transparent;
            txt_Usuario.EnabledCalc = true;
            txt_Usuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txt_Usuario.ForeColor = Color.FromArgb(124, 133, 142);
            txt_Usuario.Location = new Point(231, 65);
            txt_Usuario.MaxLength = 50;
            txt_Usuario.MultiLine = false;
            txt_Usuario.Name = "txt_Usuario";
            txt_Usuario.ReadOnly = false;
            txt_Usuario.Size = new Size(220, 33);
            txt_Usuario.TabIndex = 0;
            txt_Usuario.Text = "Usuario";
            txt_Usuario.TextAlign = HorizontalAlignment.Left;
            txt_Usuario.UseSystemPasswordChar = false;
            // 
            // txt_Contrasena
            // 
            txt_Contrasena.BackColor = Color.Transparent;
            txt_Contrasena.EnabledCalc = true;
            txt_Contrasena.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txt_Contrasena.ForeColor = Color.FromArgb(124, 133, 142);
            txt_Contrasena.Location = new Point(231, 104);
            txt_Contrasena.MaxLength = 50;
            txt_Contrasena.MultiLine = false;
            txt_Contrasena.Name = "txt_Contrasena";
            txt_Contrasena.ReadOnly = false;
            txt_Contrasena.Size = new Size(220, 33);
            txt_Contrasena.TabIndex = 1;
            txt_Contrasena.Text = "Contrasena";
            txt_Contrasena.TextAlign = HorizontalAlignment.Left;
            txt_Contrasena.UseSystemPasswordChar = true;
            // 
            // btn_IniciarSesion
            // 
            btn_IniciarSesion.BackColor = Color.Transparent;
            btn_IniciarSesion.BorderColor = Color.FromArgb(10, 28, 51);
            btn_IniciarSesion.EnteredBorderColor = Color.FromArgb(10, 28, 51);
            btn_IniciarSesion.EnteredColor = Color.FromArgb(10, 28, 51);
            btn_IniciarSesion.Font = new Font("Microsoft Sans Serif", 12F);
            btn_IniciarSesion.Image = null;
            btn_IniciarSesion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_IniciarSesion.InactiveColor = Color.FromArgb(10, 28, 51);
            btn_IniciarSesion.Location = new Point(231, 157);
            btn_IniciarSesion.Name = "btn_IniciarSesion";
            btn_IniciarSesion.PressedBorderColor = Color.FromArgb(10, 28, 51);
            btn_IniciarSesion.PressedColor = Color.FromArgb(10, 28, 51);
            btn_IniciarSesion.Size = new Size(220, 40);
            btn_IniciarSesion.TabIndex = 2;
            btn_IniciarSesion.Text = "INGRESAR";
            btn_IniciarSesion.TextAlignment = StringAlignment.Center;
            btn_IniciarSesion.Click += btn_IniciarSesion_Click;
            // 
            // cnt_ControlVentana
            // 
            cnt_ControlVentana.BackColor = Color.White;
            cnt_ControlVentana.CloseHoverColor = Color.FromArgb(230, 17, 35);
            cnt_ControlVentana.DefaultLocation = true;
            cnt_ControlVentana.Dock = DockStyle.Right;
            cnt_ControlVentana.EnableHoverHighlight = true;
            cnt_ControlVentana.EnableMaximizeButton = false;
            cnt_ControlVentana.EnableMinimizeButton = true;
            cnt_ControlVentana.ForeColor = Color.Black;
            cnt_ControlVentana.Location = new Point(393, 0);
            cnt_ControlVentana.MaximizeHoverColor = Color.FromArgb(74, 74, 74);
            cnt_ControlVentana.MinimizeHoverColor = Color.FromArgb(63, 63, 65);
            cnt_ControlVentana.Name = "cnt_ControlVentana";
            cnt_ControlVentana.Size = new Size(90, 25);
            cnt_ControlVentana.TabIndex = 4;
            cnt_ControlVentana.Text = "ControlVentana";
            // 
            // lb_InicioSesion
            // 
            lb_InicioSesion.AutoSize = true;
            lb_InicioSesion.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_InicioSesion.ForeColor = Color.Black;
            lb_InicioSesion.Location = new Point(244, 33);
            lb_InicioSesion.Name = "lb_InicioSesion";
            lb_InicioSesion.Size = new Size(193, 21);
            lb_InicioSesion.TabIndex = 3;
            lb_InicioSesion.Text = "Ingrese sus credenciales";
            // 
            // pnl_Logo
            // 
            pnl_Logo.Controls.Add(pb_Logo);
            pnl_Logo.Dock = DockStyle.Left;
            pnl_Logo.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Logo.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Logo.Location = new Point(0, 0);
            pnl_Logo.Name = "pnl_Logo";
            pnl_Logo.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Logo.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Logo.Size = new Size(175, 222);
            pnl_Logo.TabIndex = 5;
            // 
            // pb_Logo
            // 
            pb_Logo.BackColor = Color.FromArgb(10, 28, 51);
            pb_Logo.Dock = DockStyle.Fill;
            pb_Logo.Image = Properties.Resources.LogoUm;
            pb_Logo.Location = new Point(0, 0);
            pb_Logo.Name = "pb_Logo";
            pb_Logo.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            pb_Logo.Size = new Size(175, 222);
            pb_Logo.SizeMode = PictureBoxSizeMode.Zoom;
            pb_Logo.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            pb_Logo.TabIndex = 0;
            pb_Logo.TabStop = false;
            pb_Logo.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // umfInicioSesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(483, 222);
            Controls.Add(pnl_Logo);
            Controls.Add(lb_InicioSesion);
            Controls.Add(cnt_ControlVentana);
            Controls.Add(btn_IniciarSesion);
            Controls.Add(txt_Contrasena);
            Controls.Add(txt_Usuario);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "umfInicioSesion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UM - University Management";
            TransparencyKey = Color.Fuchsia;
            pnl_Logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb_Logo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.AloneTextBox txt_Usuario;
        private ReaLTaiizor.Controls.AloneTextBox txt_Contrasena;
        private ReaLTaiizor.Controls.Button btn_IniciarSesion;
        private ReaLTaiizor.Controls.ControlBox cnt_ControlVentana;
        private ReaLTaiizor.Controls.CrownLabel lb_InicioSesion;
        private ReaLTaiizor.Controls.NightPanel pnl_Logo;
        private ReaLTaiizor.Controls.HopePictureBox pb_Logo;
    }
}