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
            pnl_Encabezado = new Panel();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            lb_ValidarPersona = new ReaLTaiizor.Controls.CrownLabel();
            pnl_Contenedor = new Panel();
            btn_ValidarPersona = new Button();
            txt_DocumentoPersona = new ReaLTaiizor.Controls.SmallTextBox();
            pnl_Encabezado.SuspendLayout();
            pnl_Contenedor.SuspendLayout();
            SuspendLayout();
            // 
            // pnl_Encabezado
            // 
            pnl_Encabezado.BackColor = Color.FromArgb(20, 40, 70);
            pnl_Encabezado.Controls.Add(btn_CerrarFormulario);
            pnl_Encabezado.Controls.Add(lb_ValidarPersona);
            pnl_Encabezado.Dock = DockStyle.Top;
            pnl_Encabezado.Location = new Point(0, 0);
            pnl_Encabezado.Margin = new Padding(3, 4, 3, 4);
            pnl_Encabezado.Name = "pnl_Encabezado";
            pnl_Encabezado.Size = new Size(400, 80);
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
            btn_CerrarFormulario.Location = new Point(354, 11);
            btn_CerrarFormulario.Margin = new Padding(3, 4, 3, 4);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(37, 43);
            btn_CerrarFormulario.TabIndex = 0;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_ValidarPersona
            // 
            lb_ValidarPersona.AutoSize = true;
            lb_ValidarPersona.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_ValidarPersona.ForeColor = Color.White;
            lb_ValidarPersona.Location = new Point(83, 26);
            lb_ValidarPersona.Name = "lb_ValidarPersona";
            lb_ValidarPersona.Size = new Size(228, 28);
            lb_ValidarPersona.TabIndex = 4;
            lb_ValidarPersona.Text = "🔍 VALIDAR PERSONA";
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Contenedor.Controls.Add(btn_ValidarPersona);
            pnl_Contenedor.Controls.Add(txt_DocumentoPersona);
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Location = new Point(0, 80);
            pnl_Contenedor.Margin = new Padding(3, 4, 3, 4);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Padding = new Padding(17, 20, 17, 20);
            pnl_Contenedor.Size = new Size(400, 127);
            pnl_Contenedor.TabIndex = 1;
            // 
            // btn_ValidarPersona
            // 
            btn_ValidarPersona.BackColor = Color.FromArgb(0, 120, 215);
            btn_ValidarPersona.Cursor = Cursors.Hand;
            btn_ValidarPersona.FlatAppearance.BorderSize = 0;
            btn_ValidarPersona.FlatStyle = FlatStyle.Flat;
            btn_ValidarPersona.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_ValidarPersona.ForeColor = Color.White;
            btn_ValidarPersona.Location = new Point(17, 69);
            btn_ValidarPersona.Margin = new Padding(3, 4, 3, 4);
            btn_ValidarPersona.Name = "btn_ValidarPersona";
            btn_ValidarPersona.Size = new Size(366, 47);
            btn_ValidarPersona.TabIndex = 2;
            btn_ValidarPersona.Text = "🔎 BUSCAR PERSONA";
            btn_ValidarPersona.UseVisualStyleBackColor = false;
            btn_ValidarPersona.Click += btn_ValidarPersona_Click;
            // 
            // txt_DocumentoPersona
            // 
            txt_DocumentoPersona.BackColor = Color.White;
            txt_DocumentoPersona.BorderColor = Color.FromArgb(180, 180, 180);
            txt_DocumentoPersona.CustomBGColor = Color.White;
            txt_DocumentoPersona.Font = new Font("Segoe UI", 9F);
            txt_DocumentoPersona.ForeColor = Color.FromArgb(124, 133, 142);
            txt_DocumentoPersona.Location = new Point(17, 13);
            txt_DocumentoPersona.Margin = new Padding(3, 4, 3, 4);
            txt_DocumentoPersona.MaxLength = 20;
            txt_DocumentoPersona.Multiline = false;
            txt_DocumentoPersona.Name = "txt_DocumentoPersona";
            txt_DocumentoPersona.ReadOnly = false;
            txt_DocumentoPersona.Size = new Size(365, 30);
            txt_DocumentoPersona.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_DocumentoPersona.TabIndex = 1;
            txt_DocumentoPersona.Text = "Ingrese documento";
            txt_DocumentoPersona.TextAlignment = HorizontalAlignment.Left;
            txt_DocumentoPersona.UseSystemPasswordChar = false;
            // 
            // umfValidarPersona
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(400, 207);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "umfValidarPersona";
            StartPosition = FormStartPosition.CenterScreen;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnl_Encabezado;
        private System.Windows.Forms.Panel pnl_Contenedor;
        private ReaLTaiizor.Controls.CrownLabel lb_ValidarPersona;
        private ReaLTaiizor.Controls.SmallTextBox txt_DocumentoPersona;
        private System.Windows.Forms.Button btn_ValidarPersona;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
    }
}