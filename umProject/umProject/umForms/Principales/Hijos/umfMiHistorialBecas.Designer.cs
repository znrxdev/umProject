namespace umForms.Principales.Hijos
{
    partial class umfMiHistorialBecas
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
            dgv_Becas = new ReaLTaiizor.Controls.PoisonDataGridView();
            ((System.ComponentModel.ISupportInitialize)dgv_Becas).BeginInit();
            SuspendLayout();
            // 
            // dgv_Becas
            // 
            dgv_Becas.Dock = DockStyle.Fill;
            dgv_Becas.Location = new Point(0, 0);
            dgv_Becas.Name = "dgv_Becas";
            dgv_Becas.Size = new Size(826, 407);
            dgv_Becas.TabIndex = 0;
            // 
            // umfMiHistorialBecas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(826, 407);
            Controls.Add(dgv_Becas);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfMiHistorialBecas";
            Text = "Solicitudes de Becas";
            Load += umfMiHistorialBecas_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_Becas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Becas;
    }
}

