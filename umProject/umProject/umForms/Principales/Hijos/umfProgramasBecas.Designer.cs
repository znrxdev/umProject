namespace umForms.Principales.Hijos
{
    partial class umfProgramasBecas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgv_Programas = new ReaLTaiizor.Controls.PoisonDataGridView();
            ((System.ComponentModel.ISupportInitialize)dgv_Programas).BeginInit();
            SuspendLayout();
            // 
            // dgv_Programas
            // 
            dgv_Programas.Dock = DockStyle.Fill;
            dgv_Programas.Location = new System.Drawing.Point(0, 0);
            dgv_Programas.Name = "dgv_Programas";
            dgv_Programas.Size = new System.Drawing.Size(826, 407);
            dgv_Programas.TabIndex = 0;
            // 
            // umfProgramasBecas
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            ClientSize = new System.Drawing.Size(826, 407);
            Controls.Add(dgv_Programas);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "umfProgramasBecas";
            Text = "Programas de Becas";
            Load += umfProgramasBecas_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_Programas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Programas;
    }
}


