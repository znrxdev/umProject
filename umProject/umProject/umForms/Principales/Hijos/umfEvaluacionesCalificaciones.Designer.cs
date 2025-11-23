namespace umForms.Principales.Hijos
{
    partial class umfEvaluacionesCalificaciones
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
            pnl_Acciones = new ReaLTaiizor.Controls.NightPanel();
            btn_ModificarCalificacion = new FontAwesome.Sharp.IconButton();
            btn_AgregarCalificacion = new FontAwesome.Sharp.IconButton();
            pnl_dgv = new Panel();
            dgv_Calificaciones = new ReaLTaiizor.Controls.PoisonDataGridView();
            Id_Calificacion = new DataGridViewTextBoxColumn();
            Codigo_Registro = new DataGridViewTextBoxColumn();
            Estudiante = new DataGridViewTextBoxColumn();
            Instancia_Evaluacion = new DataGridViewTextBoxColumn();
            Puntaje_Obtenido = new DataGridViewTextBoxColumn();
            Porcentaje_Logrado = new DataGridViewTextBoxColumn();
            Estado_Calificacion = new DataGridViewTextBoxColumn();
            pnl_Acciones.SuspendLayout();
            pnl_dgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Calificaciones).BeginInit();
            SuspendLayout();
            // 
            // pnl_Acciones
            // 
            pnl_Acciones.Controls.Add(btn_ModificarCalificacion);
            pnl_Acciones.Controls.Add(btn_AgregarCalificacion);
            pnl_Acciones.Dock = DockStyle.Right;
            pnl_Acciones.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Acciones.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Acciones.Location = new Point(696, 0);
            pnl_Acciones.Name = "pnl_Acciones";
            pnl_Acciones.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Acciones.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Acciones.Size = new Size(130, 407);
            pnl_Acciones.TabIndex = 1;
            // 
            // btn_ModificarCalificacion
            // 
            btn_ModificarCalificacion.ForeColor = Color.Black;
            btn_ModificarCalificacion.IconChar = FontAwesome.Sharp.IconChar.Pen;
            btn_ModificarCalificacion.IconColor = Color.Black;
            btn_ModificarCalificacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_ModificarCalificacion.IconSize = 20;
            btn_ModificarCalificacion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ModificarCalificacion.Location = new Point(6, 64);
            btn_ModificarCalificacion.Name = "btn_ModificarCalificacion";
            btn_ModificarCalificacion.Size = new Size(121, 35);
            btn_ModificarCalificacion.TabIndex = 1;
            btn_ModificarCalificacion.Text = "MODIFICAR";
            btn_ModificarCalificacion.UseVisualStyleBackColor = true;
            btn_ModificarCalificacion.Click += btn_ModificarCalificacion_Click;
            // 
            // btn_AgregarCalificacion
            // 
            btn_AgregarCalificacion.ForeColor = Color.Black;
            btn_AgregarCalificacion.IconChar = FontAwesome.Sharp.IconChar.Add;
            btn_AgregarCalificacion.IconColor = Color.Black;
            btn_AgregarCalificacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_AgregarCalificacion.IconSize = 20;
            btn_AgregarCalificacion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_AgregarCalificacion.Location = new Point(6, 14);
            btn_AgregarCalificacion.Name = "btn_AgregarCalificacion";
            btn_AgregarCalificacion.Size = new Size(121, 35);
            btn_AgregarCalificacion.TabIndex = 0;
            btn_AgregarCalificacion.Text = "AGREGAR";
            btn_AgregarCalificacion.UseVisualStyleBackColor = true;
            btn_AgregarCalificacion.Click += btn_AgregarCalificacion_Click;
            // 
            // pnl_dgv
            // 
            pnl_dgv.Controls.Add(dgv_Calificaciones);
            pnl_dgv.Dock = DockStyle.Fill;
            pnl_dgv.Location = new Point(0, 0);
            pnl_dgv.Name = "pnl_dgv";
            pnl_dgv.Size = new Size(696, 407);
            pnl_dgv.TabIndex = 2;
            // 
            // dgv_Calificaciones
            // 
            dgv_Calificaciones.AllowUserToAddRows = false;
            dgv_Calificaciones.AllowUserToDeleteRows = false;
            dgv_Calificaciones.AllowUserToResizeRows = false;
            dgv_Calificaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Calificaciones.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv_Calificaciones.BorderStyle = BorderStyle.None;
            dgv_Calificaciones.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_Calificaciones.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_Calificaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_Calificaciones.ColumnHeadersHeight = 40;
            dgv_Calificaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_Calificaciones.Columns.AddRange(new DataGridViewColumn[] { Id_Calificacion, Codigo_Registro, Estudiante, Instancia_Evaluacion, Puntaje_Obtenido, Porcentaje_Logrado, Estado_Calificacion });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle2.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_Calificaciones.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_Calificaciones.Dock = DockStyle.Fill;
            dgv_Calificaciones.EnableHeadersVisualStyles = false;
            dgv_Calificaciones.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv_Calificaciones.GridColor = Color.FromArgb(255, 255, 255);
            dgv_Calificaciones.Location = new Point(0, 0);
            dgv_Calificaciones.MultiSelect = false;
            dgv_Calificaciones.Name = "dgv_Calificaciones";
            dgv_Calificaciones.ReadOnly = true;
            dgv_Calificaciones.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_Calificaciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_Calificaciones.RowHeadersVisible = false;
            dgv_Calificaciones.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_Calificaciones.RowTemplate.Height = 36;
            dgv_Calificaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Calificaciones.Size = new Size(696, 407);
            dgv_Calificaciones.TabIndex = 0;
            // 
            // Id_Calificacion
            // 
            Id_Calificacion.HeaderText = "ID";
            Id_Calificacion.Name = "Id_Calificacion";
            Id_Calificacion.ReadOnly = true;
            // 
            // Codigo_Registro
            // 
            Codigo_Registro.HeaderText = "Código Registro";
            Codigo_Registro.Name = "Codigo_Registro";
            Codigo_Registro.ReadOnly = true;
            // 
            // Estudiante
            // 
            Estudiante.HeaderText = "Estudiante";
            Estudiante.Name = "Estudiante";
            Estudiante.ReadOnly = true;
            // 
            // Instancia_Evaluacion
            // 
            Instancia_Evaluacion.HeaderText = "Instancia Evaluación";
            Instancia_Evaluacion.Name = "Instancia_Evaluacion";
            Instancia_Evaluacion.ReadOnly = true;
            // 
            // Puntaje_Obtenido
            // 
            Puntaje_Obtenido.HeaderText = "Puntaje";
            Puntaje_Obtenido.Name = "Puntaje_Obtenido";
            Puntaje_Obtenido.ReadOnly = true;
            // 
            // Porcentaje_Logrado
            // 
            Porcentaje_Logrado.HeaderText = "Porcentaje";
            Porcentaje_Logrado.Name = "Porcentaje_Logrado";
            Porcentaje_Logrado.ReadOnly = true;
            // 
            // Estado_Calificacion
            // 
            Estado_Calificacion.HeaderText = "Estado";
            Estado_Calificacion.Name = "Estado_Calificacion";
            Estado_Calificacion.ReadOnly = true;
            // 
            // umfEvaluacionesCalificaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(826, 407);
            Controls.Add(pnl_dgv);
            Controls.Add(pnl_Acciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfEvaluacionesCalificaciones";
            Text = "Calificaciones";
            Load += umfEvaluacionesCalificaciones_Load;
            pnl_Acciones.ResumeLayout(false);
            pnl_dgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_Calificaciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.NightPanel pnl_Acciones;
        private FontAwesome.Sharp.IconButton btn_AgregarCalificacion;
        private FontAwesome.Sharp.IconButton btn_ModificarCalificacion;
        private Panel pnl_dgv;
        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Calificaciones;
        private DataGridViewTextBoxColumn Id_Calificacion;
        private DataGridViewTextBoxColumn Codigo_Registro;
        private DataGridViewTextBoxColumn Estudiante;
        private DataGridViewTextBoxColumn Instancia_Evaluacion;
        private DataGridViewTextBoxColumn Puntaje_Obtenido;
        private DataGridViewTextBoxColumn Porcentaje_Logrado;
        private DataGridViewTextBoxColumn Estado_Calificacion;
    }
}

