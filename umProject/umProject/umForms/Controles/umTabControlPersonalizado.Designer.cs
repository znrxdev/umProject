namespace umForms.Controles
{
    partial class umTabControlPersonalizado
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            pnl_Botones = new Panel();
            pnl_Contenedor = new Panel();
            SuspendLayout();
            // 
            // pnl_Botones
            // 
            pnl_Botones.BackColor = Color.FromArgb(10, 28, 51);
            pnl_Botones.Dock = DockStyle.Top;
            pnl_Botones.Location = new Point(0, 0);
            pnl_Botones.Name = "pnl_Botones";
            pnl_Botones.Size = new Size(826, 40);
            pnl_Botones.TabIndex = 0;
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Location = new Point(0, 40);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Size = new Size(826, 367);
            pnl_Contenedor.TabIndex = 1;
            // 
            // umTabControlPersonalizado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Botones);
            Name = "umTabControlPersonalizado";
            Size = new Size(826, 407);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnl_Botones;
        private Panel pnl_Contenedor;
    }
}

