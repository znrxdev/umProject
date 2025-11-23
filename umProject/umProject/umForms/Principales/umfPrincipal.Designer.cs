namespace umForms.Principales
{
    partial class umfPrincipal
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
            pnl_Izquierda = new ReaLTaiizor.Controls.NightPanel();
            pnl_Botones = new ReaLTaiizor.Controls.NightPanel();
            btn_CerrarSesion = new ReaLTaiizor.Controls.CrownButton();
            pnl_Logo = new ReaLTaiizor.Controls.NightPanel();
            pb_Logo = new ReaLTaiizor.Controls.HopePictureBox();
            pnl_Arriba = new ReaLTaiizor.Controls.NightPanel();
            icon_Ubicacion = new FontAwesome.Sharp.IconButton();
            icon_User = new FontAwesome.Sharp.IconButton();
            cnt_ControlVentana = new ReaLTaiizor.Controls.ControlBox();
            pnl_Principal = new ReaLTaiizor.Controls.ExtendedPanel();
            pnl_Izquierda.SuspendLayout();
            pnl_Logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Logo).BeginInit();
            pnl_Arriba.SuspendLayout();
            SuspendLayout();
            // 
            // pnl_Izquierda
            // 
            pnl_Izquierda.Controls.Add(pnl_Botones);
            pnl_Izquierda.Controls.Add(btn_CerrarSesion);
            pnl_Izquierda.Controls.Add(pnl_Logo);
            pnl_Izquierda.Dock = DockStyle.Left;
            pnl_Izquierda.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Izquierda.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Izquierda.Location = new Point(0, 0);
            pnl_Izquierda.Margin = new Padding(3, 2, 3, 2);
            pnl_Izquierda.Name = "pnl_Izquierda";
            pnl_Izquierda.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Izquierda.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Izquierda.Size = new Size(158, 450);
            pnl_Izquierda.TabIndex = 0;
            // 
            // pnl_Botones
            // 
            pnl_Botones.Dock = DockStyle.Fill;
            pnl_Botones.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Botones.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Botones.Location = new Point(0, 90);
            pnl_Botones.Margin = new Padding(3, 2, 3, 2);
            pnl_Botones.Name = "pnl_Botones";
            pnl_Botones.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Botones.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Botones.Size = new Size(158, 343);
            pnl_Botones.TabIndex = 2;
            // 
            // btn_CerrarSesion
            // 
            btn_CerrarSesion.BackColor = Color.White;
            btn_CerrarSesion.Dock = DockStyle.Bottom;
            btn_CerrarSesion.Location = new Point(0, 433);
            btn_CerrarSesion.Margin = new Padding(3, 2, 3, 2);
            btn_CerrarSesion.Name = "btn_CerrarSesion";
            btn_CerrarSesion.Padding = new Padding(4);
            btn_CerrarSesion.Size = new Size(158, 17);
            btn_CerrarSesion.TabIndex = 2;
            btn_CerrarSesion.Text = "CERRAR SESION";
            btn_CerrarSesion.Click += btn_CerrarSesion_Click;
            // 
            // pnl_Logo
            // 
            pnl_Logo.Controls.Add(pb_Logo);
            pnl_Logo.Dock = DockStyle.Top;
            pnl_Logo.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Logo.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Logo.Location = new Point(0, 0);
            pnl_Logo.Margin = new Padding(3, 2, 3, 2);
            pnl_Logo.Name = "pnl_Logo";
            pnl_Logo.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Logo.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Logo.Size = new Size(158, 90);
            pnl_Logo.TabIndex = 1;
            // 
            // pb_Logo
            // 
            pb_Logo.BackColor = Color.FromArgb(10, 28, 51);
            pb_Logo.Dock = DockStyle.Fill;
            pb_Logo.Image = Properties.Resources.LogoUm;
            pb_Logo.Location = new Point(0, 0);
            pb_Logo.Margin = new Padding(3, 2, 3, 2);
            pb_Logo.Name = "pb_Logo";
            pb_Logo.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            pb_Logo.Size = new Size(158, 90);
            pb_Logo.SizeMode = PictureBoxSizeMode.Zoom;
            pb_Logo.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            pb_Logo.TabIndex = 0;
            pb_Logo.TabStop = false;
            pb_Logo.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // pnl_Arriba
            // 
            pnl_Arriba.Controls.Add(icon_Ubicacion);
            pnl_Arriba.Controls.Add(icon_User);
            pnl_Arriba.Controls.Add(cnt_ControlVentana);
            pnl_Arriba.Dock = DockStyle.Top;
            pnl_Arriba.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Arriba.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Arriba.Location = new Point(158, 0);
            pnl_Arriba.Margin = new Padding(3, 2, 3, 2);
            pnl_Arriba.Name = "pnl_Arriba";
            pnl_Arriba.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Arriba.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Arriba.Size = new Size(804, 45);
            pnl_Arriba.TabIndex = 1;
            // 
            // icon_Ubicacion
            // 
            icon_Ubicacion.Anchor = AnchorStyles.Left;
            icon_Ubicacion.Enabled = false;
            icon_Ubicacion.ForeColor = Color.Black;
            icon_Ubicacion.IconChar = FontAwesome.Sharp.IconChar.Square;
            icon_Ubicacion.IconColor = Color.Black;
            icon_Ubicacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            icon_Ubicacion.IconSize = 15;
            icon_Ubicacion.ImageAlign = ContentAlignment.MiddleLeft;
            icon_Ubicacion.Location = new Point(172, 8);
            icon_Ubicacion.Margin = new Padding(3, 2, 3, 2);
            icon_Ubicacion.Name = "icon_Ubicacion";
            icon_Ubicacion.Size = new Size(190, 33);
            icon_Ubicacion.TabIndex = 7;
            icon_Ubicacion.Text = "PRINCIPAL";
            icon_Ubicacion.UseVisualStyleBackColor = true;
            // 
            // icon_User
            // 
            icon_User.Anchor = AnchorStyles.Left;
            icon_User.Enabled = false;
            icon_User.ForeColor = Color.Black;
            icon_User.IconChar = FontAwesome.Sharp.IconChar.User;
            icon_User.IconColor = Color.Black;
            icon_User.IconFont = FontAwesome.Sharp.IconFont.Auto;
            icon_User.IconSize = 15;
            icon_User.ImageAlign = ContentAlignment.MiddleLeft;
            icon_User.Location = new Point(5, 8);
            icon_User.Margin = new Padding(3, 2, 3, 2);
            icon_User.Name = "icon_User";
            icon_User.Size = new Size(161, 33);
            icon_User.TabIndex = 6;
            icon_User.Text = "USUARIO";
            icon_User.UseVisualStyleBackColor = true;
            // 
            // cnt_ControlVentana
            // 
            cnt_ControlVentana.BackColor = Color.FromArgb(10, 28, 51);
            cnt_ControlVentana.CloseHoverColor = Color.Red;
            cnt_ControlVentana.DefaultLocation = true;
            cnt_ControlVentana.Dock = DockStyle.Right;
            cnt_ControlVentana.EnableHoverHighlight = true;
            cnt_ControlVentana.EnableMaximizeButton = true;
            cnt_ControlVentana.EnableMinimizeButton = true;
            cnt_ControlVentana.ForeColor = Color.White;
            cnt_ControlVentana.Location = new Point(714, 0);
            cnt_ControlVentana.Margin = new Padding(3, 2, 3, 2);
            cnt_ControlVentana.MaximizeHoverColor = Color.FromArgb(74, 74, 74);
            cnt_ControlVentana.MinimizeHoverColor = Color.FromArgb(63, 63, 65);
            cnt_ControlVentana.Name = "cnt_ControlVentana";
            cnt_ControlVentana.Size = new Size(90, 25);
            cnt_ControlVentana.TabIndex = 5;
            cnt_ControlVentana.Text = "ControlVentana";
            // 
            // pnl_Principal
            // 
            pnl_Principal.BackColor = Color.Transparent;
            pnl_Principal.Dock = DockStyle.Fill;
            pnl_Principal.DrawMode = ReaLTaiizor.Controls.ExtendedPanel.Drawer.Default;
            pnl_Principal.Location = new Point(158, 45);
            pnl_Principal.Margin = new Padding(3, 2, 3, 2);
            pnl_Principal.MostInterval = 100;
            pnl_Principal.Name = "pnl_Principal";
            pnl_Principal.Opacity = 50;
            pnl_Principal.Size = new Size(804, 405);
            pnl_Principal.TabIndex = 2;
            pnl_Principal.TopMost = true;
            // 
            // umfPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(962, 450);
            Controls.Add(pnl_Principal);
            Controls.Add(pnl_Arriba);
            Controls.Add(pnl_Izquierda);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "umfPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "umfPrincipal";
            Load += umfPrincipal_Load;
            pnl_Izquierda.ResumeLayout(false);
            pnl_Logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb_Logo).EndInit();
            pnl_Arriba.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.NightPanel pnl_Izquierda;
        private ReaLTaiizor.Controls.NightPanel pnl_Arriba;
        private ReaLTaiizor.Controls.ControlBox cnt_ControlVentana;
        private FontAwesome.Sharp.IconButton icon_User;
        private ReaLTaiizor.Controls.NightPanel pnl_Logo;
        private ReaLTaiizor.Controls.HopePictureBox pb_Logo;
        private ReaLTaiizor.Controls.CrownButton btn_CerrarSesion;
        private ReaLTaiizor.Controls.ExtendedPanel pnl_Principal;
        private ReaLTaiizor.Controls.NightPanel pnl_Botones;
        private FontAwesome.Sharp.IconButton icon_Ubicacion;
    }
}