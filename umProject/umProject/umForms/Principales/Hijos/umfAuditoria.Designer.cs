namespace umForms.Principales.Hijos
{
    partial class umfAuditoria
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
            this.SuspendLayout();
            // 
            // umfAuditoria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(826, 407);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfAuditoria";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Auditoría";
            Load += umfAuditoria_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}

