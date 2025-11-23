namespace umForms.Principales.Hijos
{
    partial class umfAuditoriaSistema
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
            dgv_Auditoria = new ReaLTaiizor.Controls.PoisonDataGridView();
            Id_Transaccion = new DataGridViewTextBoxColumn();
            Tipo_Transaccion = new DataGridViewTextBoxColumn();
            Concepto = new DataGridViewTextBoxColumn();
            Persona = new DataGridViewTextBoxColumn();
            Usuario = new DataGridViewTextBoxColumn();
            Autor = new DataGridViewTextBoxColumn();
            Tipo_Entidad = new DataGridViewTextBoxColumn();
            Fecha_Creacion = new DataGridViewTextBoxColumn();
            Completado = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgv_Auditoria).BeginInit();
            SuspendLayout();
            // 
            // dgv_Auditoria
            // 
            dgv_Auditoria.AllowUserToAddRows = false;
            dgv_Auditoria.AllowUserToDeleteRows = false;
            dgv_Auditoria.AllowUserToResizeRows = false;
            dgv_Auditoria.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgv_Auditoria.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv_Auditoria.BorderStyle = BorderStyle.None;
            dgv_Auditoria.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_Auditoria.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_Auditoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_Auditoria.ColumnHeadersHeight = 40;
            dgv_Auditoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_Auditoria.Columns.AddRange(new DataGridViewColumn[] { Id_Transaccion, Tipo_Transaccion, Concepto, Persona, Usuario, Autor, Tipo_Entidad, Fecha_Creacion, Completado });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle2.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_Auditoria.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_Auditoria.EnableHeadersVisualStyles = false;
            dgv_Auditoria.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv_Auditoria.GridColor = Color.FromArgb(255, 255, 255);
            dgv_Auditoria.Location = new Point(0, 0);
            dgv_Auditoria.MultiSelect = false;
            dgv_Auditoria.Name = "dgv_Auditoria";
            dgv_Auditoria.ReadOnly = true;
            dgv_Auditoria.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_Auditoria.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_Auditoria.RowHeadersVisible = false;
            dgv_Auditoria.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_Auditoria.RowTemplate.Height = 36;
            dgv_Auditoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Auditoria.Size = new Size(826, 407);
            dgv_Auditoria.TabIndex = 0;
            // 
            // Id_Transaccion
            // 
            Id_Transaccion.FillWeight = 5F;
            Id_Transaccion.HeaderText = "ID";
            Id_Transaccion.Name = "Id_Transaccion";
            Id_Transaccion.ReadOnly = true;
            Id_Transaccion.Width = 60;
            // 
            // Tipo_Transaccion
            // 
            Tipo_Transaccion.FillWeight = 18F;
            Tipo_Transaccion.HeaderText = "Tipo Transacción";
            Tipo_Transaccion.Name = "Tipo_Transaccion";
            Tipo_Transaccion.ReadOnly = true;
            Tipo_Transaccion.Width = 180;
            // 
            // Concepto
            // 
            Concepto.FillWeight = 20F;
            Concepto.HeaderText = "Concepto";
            Concepto.Name = "Concepto";
            Concepto.ReadOnly = true;
            Concepto.Width = 200;
            // 
            // Persona
            // 
            Persona.FillWeight = 15F;
            Persona.HeaderText = "Persona";
            Persona.Name = "Persona";
            Persona.ReadOnly = true;
            Persona.Width = 150;
            // 
            // Usuario
            // 
            Usuario.FillWeight = 12F;
            Usuario.HeaderText = "Usuario";
            Usuario.Name = "Usuario";
            Usuario.ReadOnly = true;
            Usuario.Width = 120;
            // 
            // Autor
            // 
            Autor.FillWeight = 12F;
            Autor.HeaderText = "Autor";
            Autor.Name = "Autor";
            Autor.ReadOnly = true;
            Autor.Width = 120;
            // 
            // Tipo_Entidad
            // 
            Tipo_Entidad.FillWeight = 12F;
            Tipo_Entidad.HeaderText = "Tipo Entidad";
            Tipo_Entidad.Name = "Tipo_Entidad";
            Tipo_Entidad.ReadOnly = true;
            Tipo_Entidad.Width = 120;
            // 
            // Fecha_Creacion
            // 
            Fecha_Creacion.FillWeight = 15F;
            Fecha_Creacion.HeaderText = "Fecha Creación";
            Fecha_Creacion.Name = "Fecha_Creacion";
            Fecha_Creacion.ReadOnly = true;
            Fecha_Creacion.Width = 150;
            // 
            // Completado
            // 
            Completado.FillWeight = 8F;
            Completado.HeaderText = "Completado";
            Completado.Name = "Completado";
            Completado.ReadOnly = true;
            // 
            // umfAuditoriaSistema
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(826, 407);
            Controls.Add(dgv_Auditoria);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfAuditoriaSistema";
            Text = "Auditoría Sistema";
            Load += umfAuditoriaSistema_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_Auditoria).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Auditoria;
        private DataGridViewTextBoxColumn Id_Transaccion;
        private DataGridViewTextBoxColumn Tipo_Transaccion;
        private DataGridViewTextBoxColumn Concepto;
        private DataGridViewTextBoxColumn Persona;
        private DataGridViewTextBoxColumn Usuario;
        private DataGridViewTextBoxColumn Autor;
        private DataGridViewTextBoxColumn Tipo_Entidad;
        private DataGridViewTextBoxColumn Fecha_Creacion;
        private DataGridViewTextBoxColumn Completado;
    }
}

