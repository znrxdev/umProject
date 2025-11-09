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
            panel1 = new Panel();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            label2 = new Label();
            btn_Usuario = new Button();
            btn_Permisos = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(10, 24, 51);
            panel1.Controls.Add(btn_CerrarFormulario);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(355, 47);
            panel1.TabIndex = 7;
            // 
            // btn_CerrarFormulario
            // 
            btn_CerrarFormulario.Cursor = Cursors.Hand;
            btn_CerrarFormulario.IconChar = FontAwesome.Sharp.IconChar.None;
            btn_CerrarFormulario.IconColor = Color.Black;
            btn_CerrarFormulario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_CerrarFormulario.Location = new Point(317, 3);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(35, 29);
            btn_CerrarFormulario.TabIndex = 11;
            btn_CerrarFormulario.Text = "X";
            btn_CerrarFormulario.UseVisualStyleBackColor = true;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(72, 14);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(220, 19);
            label2.TabIndex = 1;
            label2.Text = "SELECCIONE UNA OPCION";
            // 
            // btn_Usuario
            // 
            btn_Usuario.BackColor = Color.FromArgb(9, 46, 85);
            btn_Usuario.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Usuario.ForeColor = Color.White;
            btn_Usuario.Location = new Point(72, 51);
            btn_Usuario.Margin = new Padding(2);
            btn_Usuario.Name = "btn_Usuario";
            btn_Usuario.Size = new Size(220, 32);
            btn_Usuario.TabIndex = 9;
            btn_Usuario.Text = "DATOS USUARIO";
            btn_Usuario.UseVisualStyleBackColor = false;
            btn_Usuario.Click += btn_Usuario_Click;
            // 
            // btn_Permisos
            // 
            btn_Permisos.BackColor = Color.FromArgb(9, 46, 85);
            btn_Permisos.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_Permisos.ForeColor = Color.White;
            btn_Permisos.Location = new Point(72, 87);
            btn_Permisos.Margin = new Padding(2);
            btn_Permisos.Name = "btn_Permisos";
            btn_Permisos.Size = new Size(220, 32);
            btn_Permisos.TabIndex = 10;
            btn_Permisos.Text = "PERMISOS";
            btn_Permisos.UseVisualStyleBackColor = false;
            btn_Permisos.Click += btn_Permisos_Click;
            // 
            // umfDecisionUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(355, 136);
            Controls.Add(btn_Permisos);
            Controls.Add(btn_Usuario);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "umfDecisionUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfDecisionUsuario_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Usuario;
        private Button btn_Permisos;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
    }
}