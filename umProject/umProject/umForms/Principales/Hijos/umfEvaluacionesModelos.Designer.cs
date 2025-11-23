namespace umForms.Principales.Hijos
{
    partial class umfEvaluacionesModelos
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
            btn_ModificarModelo = new FontAwesome.Sharp.IconButton();
            btn_AgregarModelo = new FontAwesome.Sharp.IconButton();
            pnl_dgv = new Panel();
            dgv_Modelos = new ReaLTaiizor.Controls.PoisonDataGridView();
            Id_Modelo = new DataGridViewTextBoxColumn();
            Codigo_Modelo = new DataGridViewTextBoxColumn();
            Nombre_Evaluacion = new DataGridViewTextBoxColumn();
            Materia_Periodo = new DataGridViewTextBoxColumn();
            Tipo_Evaluacion = new DataGridViewTextBoxColumn();
            Calificacion_Maxima = new DataGridViewTextBoxColumn();
            Peso_Porcentual = new DataGridViewTextBoxColumn();
            pnl_Acciones.SuspendLayout();
            pnl_dgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Modelos).BeginInit();
            SuspendLayout();
            // 
            // pnl_Acciones
            // 
            pnl_Acciones.Controls.Add(btn_ModificarModelo);
            pnl_Acciones.Controls.Add(btn_AgregarModelo);
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
            // btn_ModificarModelo
            // 
            btn_ModificarModelo.ForeColor = Color.Black;
            btn_ModificarModelo.IconChar = FontAwesome.Sharp.IconChar.Pen;
            btn_ModificarModelo.IconColor = Color.Black;
            btn_ModificarModelo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_ModificarModelo.IconSize = 20;
            btn_ModificarModelo.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ModificarModelo.Location = new Point(6, 64);
            btn_ModificarModelo.Name = "btn_ModificarModelo";
            btn_ModificarModelo.Size = new Size(121, 35);
            btn_ModificarModelo.TabIndex = 1;
            btn_ModificarModelo.Text = "MODIFICAR";
            btn_ModificarModelo.UseVisualStyleBackColor = true;
            btn_ModificarModelo.Click += btn_ModificarModelo_Click;
            // 
            // btn_AgregarModelo
            // 
            btn_AgregarModelo.ForeColor = Color.Black;
            btn_AgregarModelo.IconChar = FontAwesome.Sharp.IconChar.Add;
            btn_AgregarModelo.IconColor = Color.Black;
            btn_AgregarModelo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_AgregarModelo.IconSize = 20;
            btn_AgregarModelo.ImageAlign = ContentAlignment.MiddleLeft;
            btn_AgregarModelo.Location = new Point(6, 14);
            btn_AgregarModelo.Name = "btn_AgregarModelo";
            btn_AgregarModelo.Size = new Size(121, 35);
            btn_AgregarModelo.TabIndex = 0;
            btn_AgregarModelo.Text = "AGREGAR";
            btn_AgregarModelo.UseVisualStyleBackColor = true;
            btn_AgregarModelo.Click += btn_AgregarModelo_Click;
            // 
            // pnl_dgv
            // 
            pnl_dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnl_dgv.Controls.Add(dgv_Modelos);
            pnl_dgv.Location = new Point(0, 0);
            pnl_dgv.Name = "pnl_dgv";
            pnl_dgv.Size = new Size(696, 407);
            pnl_dgv.TabIndex = 2;
            // 
            // dgv_Modelos
            // 
            dgv_Modelos.AllowUserToAddRows = false;
            dgv_Modelos.AllowUserToDeleteRows = false;
            dgv_Modelos.AllowUserToResizeColumns = false;
            dgv_Modelos.AllowUserToResizeRows = false;
            dgv_Modelos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgv_Modelos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Modelos.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv_Modelos.BorderStyle = BorderStyle.None;
            dgv_Modelos.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_Modelos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_Modelos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_Modelos.ColumnHeadersHeight = 40;
            dgv_Modelos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_Modelos.Columns.AddRange(new DataGridViewColumn[] { Id_Modelo, Codigo_Modelo, Nombre_Evaluacion, Materia_Periodo, Tipo_Evaluacion, Calificacion_Maxima, Peso_Porcentual });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle2.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_Modelos.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_Modelos.EnableHeadersVisualStyles = false;
            dgv_Modelos.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv_Modelos.GridColor = Color.FromArgb(255, 255, 255);
            dgv_Modelos.Location = new Point(0, 0);
            dgv_Modelos.MultiSelect = false;
            dgv_Modelos.Name = "dgv_Modelos";
            dgv_Modelos.ReadOnly = true;
            dgv_Modelos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_Modelos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_Modelos.RowHeadersVisible = false;
            dgv_Modelos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_Modelos.RowTemplate.Height = 36;
            dgv_Modelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Modelos.Size = new Size(696, 407);
            dgv_Modelos.TabIndex = 0;
            // 
            // Id_Modelo
            // 
            Id_Modelo.FillWeight = 5F;
            Id_Modelo.HeaderText = "ID";
            Id_Modelo.Name = "Id_Modelo";
            Id_Modelo.ReadOnly = true;
            // 
            // Codigo_Modelo
            // 
            Codigo_Modelo.FillWeight = 12F;
            Codigo_Modelo.HeaderText = "Código";
            Codigo_Modelo.Name = "Codigo_Modelo";
            Codigo_Modelo.ReadOnly = true;
            // 
            // Nombre_Evaluacion
            // 
            Nombre_Evaluacion.FillWeight = 25F;
            Nombre_Evaluacion.HeaderText = "Nombre Evaluación";
            Nombre_Evaluacion.Name = "Nombre_Evaluacion";
            Nombre_Evaluacion.ReadOnly = true;
            // 
            // Materia_Periodo
            // 
            Materia_Periodo.FillWeight = 22F;
            Materia_Periodo.HeaderText = "Materia Período";
            Materia_Periodo.Name = "Materia_Periodo";
            Materia_Periodo.ReadOnly = true;
            // 
            // Tipo_Evaluacion
            // 
            Tipo_Evaluacion.FillWeight = 16F;
            Tipo_Evaluacion.HeaderText = "Tipo Evaluación";
            Tipo_Evaluacion.Name = "Tipo_Evaluacion";
            Tipo_Evaluacion.ReadOnly = true;
            // 
            // Calificacion_Maxima
            // 
            Calificacion_Maxima.FillWeight = 12F;
            Calificacion_Maxima.HeaderText = "Calificación Máxima";
            Calificacion_Maxima.Name = "Calificacion_Maxima";
            Calificacion_Maxima.ReadOnly = true;
            // 
            // Peso_Porcentual
            // 
            Peso_Porcentual.FillWeight = 8F;
            Peso_Porcentual.HeaderText = "Peso %";
            Peso_Porcentual.Name = "Peso_Porcentual";
            Peso_Porcentual.ReadOnly = true;
            // 
            // umfEvaluacionesModelos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(826, 407);
            Controls.Add(pnl_dgv);
            Controls.Add(pnl_Acciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfEvaluacionesModelos";
            Text = "Modelos de Evaluación";
            Load += umfEvaluacionesModelos_Load;
            pnl_Acciones.ResumeLayout(false);
            pnl_dgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_Modelos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.NightPanel pnl_Acciones;
        private FontAwesome.Sharp.IconButton btn_AgregarModelo;
        private FontAwesome.Sharp.IconButton btn_ModificarModelo;
        private Panel pnl_dgv;
        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Modelos;
        private DataGridViewTextBoxColumn Id_Modelo;
        private DataGridViewTextBoxColumn Codigo_Modelo;
        private DataGridViewTextBoxColumn Nombre_Evaluacion;
        private DataGridViewTextBoxColumn Materia_Periodo;
        private DataGridViewTextBoxColumn Tipo_Evaluacion;
        private DataGridViewTextBoxColumn Calificacion_Maxima;
        private DataGridViewTextBoxColumn Peso_Porcentual;
    }
}

