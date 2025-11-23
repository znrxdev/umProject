namespace umForms.Principales.Hijos
{
    partial class umfEvaluacionesInstancias
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
            btn_ModificarInstancia = new FontAwesome.Sharp.IconButton();
            btn_AgregarInstancia = new FontAwesome.Sharp.IconButton();
            pnl_dgv = new Panel();
            dgv_Instancias = new ReaLTaiizor.Controls.PoisonDataGridView();
            Id_Instancia = new DataGridViewTextBoxColumn();
            Codigo_Instancia = new DataGridViewTextBoxColumn();
            Seccion = new DataGridViewTextBoxColumn();
            Modelo_Evaluacion = new DataGridViewTextBoxColumn();
            Fecha_Programada = new DataGridViewTextBoxColumn();
            Fecha_Limite = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            pnl_Acciones.SuspendLayout();
            pnl_dgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Instancias).BeginInit();
            SuspendLayout();
            // 
            // pnl_Acciones
            // 
            pnl_Acciones.Controls.Add(btn_ModificarInstancia);
            pnl_Acciones.Controls.Add(btn_AgregarInstancia);
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
            // btn_ModificarInstancia
            // 
            btn_ModificarInstancia.ForeColor = Color.Black;
            btn_ModificarInstancia.IconChar = FontAwesome.Sharp.IconChar.Pen;
            btn_ModificarInstancia.IconColor = Color.Black;
            btn_ModificarInstancia.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_ModificarInstancia.IconSize = 20;
            btn_ModificarInstancia.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ModificarInstancia.Location = new Point(6, 64);
            btn_ModificarInstancia.Name = "btn_ModificarInstancia";
            btn_ModificarInstancia.Size = new Size(121, 35);
            btn_ModificarInstancia.TabIndex = 1;
            btn_ModificarInstancia.Text = "MODIFICAR";
            btn_ModificarInstancia.UseVisualStyleBackColor = true;
            btn_ModificarInstancia.Click += btn_ModificarInstancia_Click;
            // 
            // btn_AgregarInstancia
            // 
            btn_AgregarInstancia.ForeColor = Color.Black;
            btn_AgregarInstancia.IconChar = FontAwesome.Sharp.IconChar.Add;
            btn_AgregarInstancia.IconColor = Color.Black;
            btn_AgregarInstancia.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_AgregarInstancia.IconSize = 20;
            btn_AgregarInstancia.ImageAlign = ContentAlignment.MiddleLeft;
            btn_AgregarInstancia.Location = new Point(6, 14);
            btn_AgregarInstancia.Name = "btn_AgregarInstancia";
            btn_AgregarInstancia.Size = new Size(121, 35);
            btn_AgregarInstancia.TabIndex = 0;
            btn_AgregarInstancia.Text = "AGREGAR";
            btn_AgregarInstancia.UseVisualStyleBackColor = true;
            btn_AgregarInstancia.Click += btn_AgregarInstancia_Click;
            // 
            // pnl_dgv
            // 
            pnl_dgv.Controls.Add(dgv_Instancias);
            pnl_dgv.Dock = DockStyle.Fill;
            pnl_dgv.Location = new Point(0, 0);
            pnl_dgv.Name = "pnl_dgv";
            pnl_dgv.Size = new Size(696, 407);
            pnl_dgv.TabIndex = 2;
            // 
            // dgv_Instancias
            // 
            dgv_Instancias.AllowUserToAddRows = false;
            dgv_Instancias.AllowUserToDeleteRows = false;
            dgv_Instancias.AllowUserToResizeRows = false;
            dgv_Instancias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Instancias.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv_Instancias.BorderStyle = BorderStyle.None;
            dgv_Instancias.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_Instancias.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_Instancias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_Instancias.ColumnHeadersHeight = 40;
            dgv_Instancias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_Instancias.Columns.AddRange(new DataGridViewColumn[] { Id_Instancia, Codigo_Instancia, Seccion, Modelo_Evaluacion, Fecha_Programada, Fecha_Limite, Estado });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle2.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_Instancias.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_Instancias.Dock = DockStyle.Fill;
            dgv_Instancias.EnableHeadersVisualStyles = false;
            dgv_Instancias.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv_Instancias.GridColor = Color.FromArgb(255, 255, 255);
            dgv_Instancias.Location = new Point(0, 0);
            dgv_Instancias.MultiSelect = false;
            dgv_Instancias.Name = "dgv_Instancias";
            dgv_Instancias.ReadOnly = true;
            dgv_Instancias.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_Instancias.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_Instancias.RowHeadersVisible = false;
            dgv_Instancias.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_Instancias.RowTemplate.Height = 36;
            dgv_Instancias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Instancias.Size = new Size(696, 407);
            dgv_Instancias.TabIndex = 0;
            // 
            // Id_Instancia
            // 
            Id_Instancia.HeaderText = "ID";
            Id_Instancia.Name = "Id_Instancia";
            Id_Instancia.ReadOnly = true;
            // 
            // Codigo_Instancia
            // 
            Codigo_Instancia.HeaderText = "Código";
            Codigo_Instancia.Name = "Codigo_Instancia";
            Codigo_Instancia.ReadOnly = true;
            // 
            // Seccion
            // 
            Seccion.HeaderText = "Sección";
            Seccion.Name = "Seccion";
            Seccion.ReadOnly = true;
            // 
            // Modelo_Evaluacion
            // 
            Modelo_Evaluacion.HeaderText = "Modelo Evaluación";
            Modelo_Evaluacion.Name = "Modelo_Evaluacion";
            Modelo_Evaluacion.ReadOnly = true;
            // 
            // Fecha_Programada
            // 
            Fecha_Programada.HeaderText = "Fecha Programada";
            Fecha_Programada.Name = "Fecha_Programada";
            Fecha_Programada.ReadOnly = true;
            // 
            // Fecha_Limite
            // 
            Fecha_Limite.HeaderText = "Fecha Límite";
            Fecha_Limite.Name = "Fecha_Limite";
            Fecha_Limite.ReadOnly = true;
            // 
            // Estado
            // 
            Estado.HeaderText = "Estado";
            Estado.Name = "Estado";
            Estado.ReadOnly = true;
            // 
            // umfEvaluacionesInstancias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(826, 407);
            Controls.Add(pnl_dgv);
            Controls.Add(pnl_Acciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfEvaluacionesInstancias";
            Text = "Instancias de Evaluación";
            Load += umfEvaluacionesInstancias_Load;
            pnl_Acciones.ResumeLayout(false);
            pnl_dgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_Instancias).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.NightPanel pnl_Acciones;
        private FontAwesome.Sharp.IconButton btn_AgregarInstancia;
        private FontAwesome.Sharp.IconButton btn_ModificarInstancia;
        private Panel pnl_dgv;
        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Instancias;
        private DataGridViewTextBoxColumn Id_Instancia;
        private DataGridViewTextBoxColumn Codigo_Instancia;
        private DataGridViewTextBoxColumn Seccion;
        private DataGridViewTextBoxColumn Modelo_Evaluacion;
        private DataGridViewTextBoxColumn Fecha_Programada;
        private DataGridViewTextBoxColumn Fecha_Limite;
        private DataGridViewTextBoxColumn Estado;
    }
}

