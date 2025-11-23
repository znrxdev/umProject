namespace umForms.Principales.Hijos
{
    partial class umfSolicitudesBecas
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
            pnl_Top = new System.Windows.Forms.Panel();
            lbl_Programa = new System.Windows.Forms.Label();
            cmb_Programa = new System.Windows.Forms.ComboBox();
            lbl_Nivel = new System.Windows.Forms.Label();
            cmb_Nivel = new System.Windows.Forms.ComboBox();
            btn_Buscar = new System.Windows.Forms.Button();
            dgv_Solicitudes = new ReaLTaiizor.Controls.PoisonDataGridView();
            pnl_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Solicitudes).BeginInit();
            SuspendLayout();
            // 
            // pnl_Top
            // 
            pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            pnl_Top.Height = 50;
            pnl_Top.BackColor = System.Drawing.Color.FromArgb(230, 232, 235);
            pnl_Top.Controls.Add(lbl_Programa);
            pnl_Top.Controls.Add(cmb_Programa);
            pnl_Top.Controls.Add(lbl_Nivel);
            pnl_Top.Controls.Add(cmb_Nivel);
            pnl_Top.Controls.Add(btn_Buscar);
            // 
            // lbl_Programa
            // 
            lbl_Programa.AutoSize = true;
            lbl_Programa.Location = new System.Drawing.Point(15, 16);
            lbl_Programa.Name = "lbl_Programa";
            lbl_Programa.Size = new System.Drawing.Size(64, 15);
            lbl_Programa.Text = "Programa:";
            // 
            // cmb_Programa
            // 
            cmb_Programa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmb_Programa.Location = new System.Drawing.Point(85, 12);
            cmb_Programa.Width = 250;
            cmb_Programa.Name = "cmb_Programa";
            // 
            // lbl_Nivel
            // 
            lbl_Nivel.AutoSize = true;
            lbl_Nivel.Location = new System.Drawing.Point(350, 16);
            lbl_Nivel.Name = "lbl_Nivel";
            lbl_Nivel.Size = new System.Drawing.Size(37, 15);
            lbl_Nivel.Text = "Nivel:";
            // 
            // cmb_Nivel
            // 
            cmb_Nivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmb_Nivel.Location = new System.Drawing.Point(393, 12);
            cmb_Nivel.Width = 80;
            cmb_Nivel.Name = "cmb_Nivel";
            // 
            // btn_Buscar
            // 
            btn_Buscar.Location = new System.Drawing.Point(490, 10);
            btn_Buscar.Name = "btn_Buscar";
            btn_Buscar.Size = new System.Drawing.Size(100, 27);
            btn_Buscar.Text = "Buscar";
            btn_Buscar.UseVisualStyleBackColor = true;
            btn_Buscar.Click += btn_Buscar_Click;
            // 
            // dgv_Solicitudes
            // 
            dgv_Solicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            dgv_Solicitudes.Location = new System.Drawing.Point(0, 50);
            dgv_Solicitudes.Name = "dgv_Solicitudes";
            dgv_Solicitudes.Size = new System.Drawing.Size(826, 357);
            dgv_Solicitudes.TabIndex = 1;
            // 
            // umfSolicitudesBecas
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            ClientSize = new System.Drawing.Size(826, 407);
            Controls.Add(dgv_Solicitudes);
            Controls.Add(pnl_Top);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "umfSolicitudesBecas";
            Text = "Solicitudes de Becas";
            Load += umfSolicitudesBecas_Load;
            pnl_Top.ResumeLayout(false);
            pnl_Top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Solicitudes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.Label lbl_Programa;
        private System.Windows.Forms.ComboBox cmb_Programa;
        private System.Windows.Forms.Label lbl_Nivel;
        private System.Windows.Forms.ComboBox cmb_Nivel;
        private System.Windows.Forms.Button btn_Buscar;
        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Solicitudes;
    }
}


