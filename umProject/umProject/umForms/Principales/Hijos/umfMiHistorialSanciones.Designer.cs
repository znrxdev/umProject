namespace umForms.Principales.Hijos
{
    partial class umfMiHistorialSanciones
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dgv_Sanciones = new ReaLTaiizor.Controls.PoisonDataGridView();
            Id_Sancion = new DataGridViewTextBoxColumn();
            Codigo_Sancion = new DataGridViewTextBoxColumn();
            Tipo_Sancion = new DataGridViewTextBoxColumn();
            Severidad = new DataGridViewTextBoxColumn();
            Fecha_Registro = new DataGridViewTextBoxColumn();
            Fecha_Fin = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            Motivo = new DataGridViewTextBoxColumn();
            Apelable = new DataGridViewTextBoxColumn();
            Revisado_Por = new DataGridViewTextBoxColumn();
            Accion_VerDetalles = new DataGridViewButtonColumn();
            Accion_Apelar = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dgv_Sanciones).BeginInit();
            SuspendLayout();
            // 
            // dgv_Sanciones
            // 
            dgv_Sanciones.AllowUserToAddRows = false;
            dgv_Sanciones.AllowUserToDeleteRows = false;
            dgv_Sanciones.AllowUserToResizeRows = false;
            dgv_Sanciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Sanciones.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv_Sanciones.BorderStyle = BorderStyle.None;
            dgv_Sanciones.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_Sanciones.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_Sanciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_Sanciones.ColumnHeadersHeight = 40;
            dgv_Sanciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_Sanciones.Columns.AddRange(new DataGridViewColumn[] { Id_Sancion, Codigo_Sancion, Tipo_Sancion, Severidad, Fecha_Registro, Fecha_Fin, Estado, Motivo, Apelable, Revisado_Por, Accion_VerDetalles, Accion_Apelar });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle2.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_Sanciones.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_Sanciones.Dock = DockStyle.Fill;
            dgv_Sanciones.EnableHeadersVisualStyles = false;
            dgv_Sanciones.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv_Sanciones.GridColor = Color.FromArgb(255, 255, 255);
            dgv_Sanciones.Location = new Point(0, 0);
            dgv_Sanciones.MultiSelect = false;
            dgv_Sanciones.Name = "dgv_Sanciones";
            dgv_Sanciones.ReadOnly = false;
            dgv_Sanciones.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_Sanciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_Sanciones.RowHeadersVisible = false;
            dgv_Sanciones.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_Sanciones.RowTemplate.Height = 36;
            dgv_Sanciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Sanciones.Size = new Size(826, 407);
            dgv_Sanciones.TabIndex = 0;
            dgv_Sanciones.CellContentClick += dgv_Sanciones_CellContentClick;
            // 
            // Id_Sancion
            // 
            Id_Sancion.HeaderText = "No. Sanción";
            Id_Sancion.Name = "Id_Sancion";
            Id_Sancion.ReadOnly = true;
            // 
            // Codigo_Sancion
            // 
            Codigo_Sancion.HeaderText = "Código";
            Codigo_Sancion.Name = "Codigo_Sancion";
            Codigo_Sancion.ReadOnly = true;
            // 
            // Tipo_Sancion
            // 
            Tipo_Sancion.HeaderText = "Tipo Sanción";
            Tipo_Sancion.Name = "Tipo_Sancion";
            Tipo_Sancion.ReadOnly = true;
            // 
            // Severidad
            // 
            Severidad.HeaderText = "Severidad";
            Severidad.Name = "Severidad";
            Severidad.ReadOnly = true;
            // 
            // Fecha_Registro
            // 
            Fecha_Registro.HeaderText = "Fecha Registro";
            Fecha_Registro.Name = "Fecha_Registro";
            Fecha_Registro.ReadOnly = true;
            // 
            // Fecha_Fin
            // 
            Fecha_Fin.HeaderText = "Fecha Fin";
            Fecha_Fin.Name = "Fecha_Fin";
            Fecha_Fin.ReadOnly = true;
            // 
            // Estado
            // 
            Estado.HeaderText = "Estado";
            Estado.Name = "Estado";
            Estado.ReadOnly = true;
            // 
            // Motivo
            // 
            Motivo.HeaderText = "Motivo";
            Motivo.Name = "Motivo";
            Motivo.ReadOnly = true;
            // 
            // Apelable
            // 
            Apelable.HeaderText = "Apelable";
            Apelable.Name = "Apelable";
            Apelable.ReadOnly = true;
            Apelable.Width = 80;
            // 
            // Revisado_Por
            // 
            Revisado_Por.HeaderText = "Revisado Por";
            Revisado_Por.Name = "Revisado_Por";
            Revisado_Por.ReadOnly = true;
            Revisado_Por.Width = 150;
            // 
            // Accion_VerDetalles
            // 
            Accion_VerDetalles.HeaderText = "Detalles";
            Accion_VerDetalles.Name = "Accion_VerDetalles";
            Accion_VerDetalles.ReadOnly = false;
            Accion_VerDetalles.Text = "VER DETALLES";
            Accion_VerDetalles.UseColumnTextForButtonValue = true;
            Accion_VerDetalles.Width = 120;
            // 
            // Accion_Apelar
            // 
            Accion_Apelar.HeaderText = "Acción";
            Accion_Apelar.Name = "Accion_Apelar";
            Accion_Apelar.ReadOnly = false;
            Accion_Apelar.Text = "Apelar";
            Accion_Apelar.UseColumnTextForButtonValue = true;
            Accion_Apelar.Width = 80;
            // 
            // umfMiHistorialSanciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(826, 407);
            Controls.Add(dgv_Sanciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfMiHistorialSanciones";
            Text = "Sanciones Académicas";
            Load += umfMiHistorialSanciones_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_Sanciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Sanciones;
        private DataGridViewTextBoxColumn Id_Sancion;
        private DataGridViewTextBoxColumn Codigo_Sancion;
        private DataGridViewTextBoxColumn Tipo_Sancion;
        private DataGridViewTextBoxColumn Severidad;
        private DataGridViewTextBoxColumn Fecha_Registro;
        private DataGridViewTextBoxColumn Fecha_Fin;
        private DataGridViewTextBoxColumn Estado;
        private DataGridViewTextBoxColumn Motivo;
        private DataGridViewTextBoxColumn Apelable;
        private DataGridViewTextBoxColumn Revisado_Por;
        private DataGridViewButtonColumn Accion_VerDetalles;
        private DataGridViewButtonColumn Accion_Apelar;
    }
}

