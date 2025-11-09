namespace umForms.Flotantes
{
    partial class umfValidarPersona
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
            btn_ValidarPersona = new FontAwesome.Sharp.IconButton();
            txt_DocumentoPersona = new ReaLTaiizor.Controls.AloneTextBox();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            lb_ValidarPersona = new ReaLTaiizor.Controls.CrownLabel();
            SuspendLayout();
            // 
            // btn_ValidarPersona
            // 
            btn_ValidarPersona.BackColor = Color.FromArgb(10, 28, 51);
            btn_ValidarPersona.FlatAppearance.BorderSize = 0;
            btn_ValidarPersona.FlatStyle = FlatStyle.Flat;
            btn_ValidarPersona.ForeColor = SystemColors.ControlLightLight;
            btn_ValidarPersona.IconChar = FontAwesome.Sharp.IconChar.Search;
            btn_ValidarPersona.IconColor = Color.White;
            btn_ValidarPersona.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_ValidarPersona.IconSize = 15;
            btn_ValidarPersona.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ValidarPersona.Location = new Point(68, 107);
            btn_ValidarPersona.Name = "btn_ValidarPersona";
            btn_ValidarPersona.Size = new Size(193, 30);
            btn_ValidarPersona.TabIndex = 2;
            btn_ValidarPersona.Text = "BUSCAR PERSONA";
            btn_ValidarPersona.UseVisualStyleBackColor = false;
            btn_ValidarPersona.Click += btn_ValidarPersona_Click;
            // 
            // txt_DocumentoPersona
            // 
            txt_DocumentoPersona.BackColor = Color.White;
            txt_DocumentoPersona.BackgroundImageLayout = ImageLayout.None;
            txt_DocumentoPersona.EnabledCalc = true;
            txt_DocumentoPersona.Font = new Font("Segoe UI", 9F);
            txt_DocumentoPersona.ForeColor = Color.FromArgb(124, 133, 142);
            txt_DocumentoPersona.Location = new Point(12, 51);
            txt_DocumentoPersona.MaxLength = 20;
            txt_DocumentoPersona.MultiLine = false;
            txt_DocumentoPersona.Name = "txt_DocumentoPersona";
            txt_DocumentoPersona.ReadOnly = false;
            txt_DocumentoPersona.Size = new Size(309, 29);
            txt_DocumentoPersona.TabIndex = 1;
            txt_DocumentoPersona.Text = "DIGITAR DOCUMENTO";
            txt_DocumentoPersona.TextAlign = HorizontalAlignment.Center;
            txt_DocumentoPersona.UseSystemPasswordChar = false;
            // 
            // btn_CerrarFormulario
            // 
            btn_CerrarFormulario.Cursor = Cursors.Hand;
            btn_CerrarFormulario.IconChar = FontAwesome.Sharp.IconChar.None;
            btn_CerrarFormulario.IconColor = Color.Black;
            btn_CerrarFormulario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_CerrarFormulario.Location = new Point(313, 2);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(22, 23);
            btn_CerrarFormulario.TabIndex = 0;
            btn_CerrarFormulario.Text = "X";
            btn_CerrarFormulario.UseVisualStyleBackColor = true;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_ValidarPersona
            // 
            lb_ValidarPersona.AutoSize = true;
            lb_ValidarPersona.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_ValidarPersona.ForeColor = Color.FromArgb(10, 28, 51);
            lb_ValidarPersona.Location = new Point(84, 11);
            lb_ValidarPersona.Name = "lb_ValidarPersona";
            lb_ValidarPersona.Size = new Size(155, 21);
            lb_ValidarPersona.TabIndex = 4;
            lb_ValidarPersona.Text = "VALIDAR PERSONA";
            // 
            // umfValidarPersona
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(337, 149);
            Controls.Add(lb_ValidarPersona);
            Controls.Add(btn_CerrarFormulario);
            Controls.Add(txt_DocumentoPersona);
            Controls.Add(btn_ValidarPersona);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfValidarPersona";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FontAwesome.Sharp.IconButton btn_ValidarPersona;
        private ReaLTaiizor.Controls.AloneTextBox txt_DocumentoPersona;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private ReaLTaiizor.Controls.CrownLabel lb_ValidarPersona;
    }
}