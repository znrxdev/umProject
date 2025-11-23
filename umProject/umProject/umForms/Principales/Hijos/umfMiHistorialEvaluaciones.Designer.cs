namespace umForms.Principales.Hijos
{
    partial class umfMiHistorialEvaluaciones
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
            dgv_Evaluaciones = new ReaLTaiizor.Controls.PoisonDataGridView();
            ((System.ComponentModel.ISupportInitialize)dgv_Evaluaciones).BeginInit();
            SuspendLayout();
            // 
            // dgv_Evaluaciones
            // 
            dgv_Evaluaciones.Dock = DockStyle.Fill;
            dgv_Evaluaciones.Location = new Point(0, 0);
            dgv_Evaluaciones.Name = "dgv_Evaluaciones";
            dgv_Evaluaciones.Size = new Size(826, 407);
            dgv_Evaluaciones.TabIndex = 0;
            // 
            // umfMiHistorialEvaluaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(826, 407);
            Controls.Add(dgv_Evaluaciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfMiHistorialEvaluaciones";
            Text = "Notas/Evaluaciones";
            Load += umfMiHistorialEvaluaciones_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_Evaluaciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Evaluaciones;
    }
}

