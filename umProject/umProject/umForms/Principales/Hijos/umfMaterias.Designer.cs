namespace umForms.Principales.Hijos
{
    partial class umfMaterias
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnl_Acciones = new ReaLTaiizor.Controls.NightPanel();
            btn_ModificarMateria = new FontAwesome.Sharp.IconButton();
            btn_AgregarMateria = new FontAwesome.Sharp.IconButton();
            pnl_Busqueda = new ReaLTaiizor.Controls.NightPanel();
            txt_Buscar = new ReaLTaiizor.Controls.BigTextBox();
            pnl_dgv = new Panel();
            dgv_Materias = new ReaLTaiizor.Controls.PoisonDataGridView();
            Id_Materia = new DataGridViewTextBoxColumn();
            Codigo_Materia = new DataGridViewTextBoxColumn();
            Nombre_Materia = new DataGridViewTextBoxColumn();
            CreadoPor = new DataGridViewTextBoxColumn();
            ModificadoPor = new DataGridViewTextBoxColumn();
            Id_Transaccion = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            pnl_Acciones.SuspendLayout();
            pnl_Busqueda.SuspendLayout();
            pnl_dgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Materias).BeginInit();
            SuspendLayout();
            // 
            // pnl_Acciones
            // 
            pnl_Acciones.Controls.Add(btn_ModificarMateria);
            pnl_Acciones.Controls.Add(btn_AgregarMateria);
            pnl_Acciones.Dock = DockStyle.Right;
            pnl_Acciones.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Acciones.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Acciones.Location = new Point(696, 0);
            pnl_Acciones.Name = "pnl_Acciones";
            pnl_Acciones.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Acciones.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Acciones.Size = new Size(130, 407);
            pnl_Acciones.TabIndex = 3;
            // 
            // btn_ModificarMateria
            // 
            btn_ModificarMateria.ForeColor = Color.Black;
            btn_ModificarMateria.IconChar = FontAwesome.Sharp.IconChar.Pen;
            btn_ModificarMateria.IconColor = Color.Black;
            btn_ModificarMateria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_ModificarMateria.IconSize = 20;
            btn_ModificarMateria.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ModificarMateria.Location = new Point(6, 114);
            btn_ModificarMateria.Name = "btn_ModificarMateria";
            btn_ModificarMateria.Size = new Size(121, 35);
            btn_ModificarMateria.TabIndex = 1;
            btn_ModificarMateria.Text = "MODIFICAR";
            btn_ModificarMateria.UseVisualStyleBackColor = true;
            btn_ModificarMateria.Click += btn_ModificarMateria_Click;
            // 
            // btn_AgregarMateria
            // 
            btn_AgregarMateria.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_AgregarMateria.ForeColor = Color.Black;
            btn_AgregarMateria.IconChar = FontAwesome.Sharp.IconChar.Plus;
            btn_AgregarMateria.IconColor = Color.Black;
            btn_AgregarMateria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_AgregarMateria.IconSize = 20;
            btn_AgregarMateria.ImageAlign = ContentAlignment.MiddleLeft;
            btn_AgregarMateria.Location = new Point(6, 64);
            btn_AgregarMateria.Name = "btn_AgregarMateria";
            btn_AgregarMateria.Size = new Size(121, 35);
            btn_AgregarMateria.TabIndex = 0;
            btn_AgregarMateria.Text = "AGREGAR";
            btn_AgregarMateria.UseVisualStyleBackColor = true;
            btn_AgregarMateria.Click += btn_AgregarMateria_Click;
            // 
            // pnl_Busqueda
            // 
            pnl_Busqueda.Controls.Add(txt_Buscar);
            pnl_Busqueda.Dock = DockStyle.Top;
            pnl_Busqueda.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Busqueda.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Busqueda.Location = new Point(0, 0);
            pnl_Busqueda.Name = "pnl_Busqueda";
            pnl_Busqueda.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Busqueda.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Busqueda.Size = new Size(696, 64);
            pnl_Busqueda.TabIndex = 4;
            // 
            // txt_Buscar
            // 
            txt_Buscar.BackColor = Color.Transparent;
            txt_Buscar.Font = new Font("Tahoma", 11F);
            txt_Buscar.ForeColor = Color.DimGray;
            txt_Buscar.Image = null;
            txt_Buscar.Location = new Point(12, 12);
            txt_Buscar.MaxLength = 32767;
            txt_Buscar.Multiline = false;
            txt_Buscar.Name = "txt_Buscar";
            txt_Buscar.ReadOnly = false;
            txt_Buscar.Size = new Size(164, 41);
            txt_Buscar.TabIndex = 0;
            txt_Buscar.Text = "Buscar Materia";
            txt_Buscar.TextAlignment = HorizontalAlignment.Center;
            txt_Buscar.UseSystemPasswordChar = false;
            txt_Buscar.TextChanged += txt_Buscar_TextChanged;
            // 
            // pnl_dgv
            // 
            pnl_dgv.Controls.Add(dgv_Materias);
            pnl_dgv.Dock = DockStyle.Fill;
            pnl_dgv.Location = new Point(0, 64);
            pnl_dgv.Name = "pnl_dgv";
            pnl_dgv.Size = new Size(696, 343);
            pnl_dgv.TabIndex = 5;
            // 
            // dgv_Materias
            // 
            dgv_Materias.AllowUserToAddRows = false;
            dgv_Materias.AllowUserToDeleteRows = false;
            dgv_Materias.AllowUserToResizeRows = false;
            dgv_Materias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Materias.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv_Materias.BorderStyle = BorderStyle.None;
            dgv_Materias.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_Materias.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_Materias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_Materias.ColumnHeadersHeight = 40;
            dgv_Materias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_Materias.Columns.AddRange(new DataGridViewColumn[] { Id_Materia, Codigo_Materia, Nombre_Materia, CreadoPor, ModificadoPor, Id_Transaccion, Estado });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle2.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_Materias.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_Materias.Dock = DockStyle.Fill;
            dgv_Materias.EnableHeadersVisualStyles = false;
            dgv_Materias.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv_Materias.GridColor = Color.FromArgb(255, 255, 255);
            dgv_Materias.Location = new Point(0, 0);
            dgv_Materias.MultiSelect = false;
            dgv_Materias.Name = "dgv_Materias";
            dgv_Materias.ReadOnly = true;
            dgv_Materias.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_Materias.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_Materias.RowHeadersVisible = false;
            dgv_Materias.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_Materias.RowTemplate.Height = 36;
            dgv_Materias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Materias.Size = new Size(696, 343);
            dgv_Materias.TabIndex = 0;
            // 
            // Id_Materia
            // 
            Id_Materia.HeaderText = "No. Materia";
            Id_Materia.Name = "Id_Materia";
            Id_Materia.ReadOnly = true;
            // 
            // Codigo_Materia
            // 
            Codigo_Materia.HeaderText = "Código";
            Codigo_Materia.Name = "Codigo_Materia";
            Codigo_Materia.ReadOnly = true;
            // 
            // Nombre_Materia
            // 
            Nombre_Materia.HeaderText = "Nombre Materia";
            Nombre_Materia.Name = "Nombre_Materia";
            Nombre_Materia.ReadOnly = true;
            // 
            // CreadoPor
            // 
            CreadoPor.HeaderText = "Creado Por";
            CreadoPor.Name = "CreadoPor";
            CreadoPor.ReadOnly = true;
            // 
            // ModificadoPor
            // 
            ModificadoPor.HeaderText = "Modificado Por";
            ModificadoPor.Name = "ModificadoPor";
            ModificadoPor.ReadOnly = true;
            // 
            // Id_Transaccion
            // 
            Id_Transaccion.HeaderText = "No. Ultimo Cambio";
            Id_Transaccion.Name = "Id_Transaccion";
            Id_Transaccion.ReadOnly = true;
            Id_Transaccion.Resizable = DataGridViewTriState.True;
            Id_Transaccion.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // Estado
            // 
            Estado.HeaderText = "Estado";
            Estado.Name = "Estado";
            Estado.ReadOnly = true;
            Estado.Resizable = DataGridViewTriState.True;
            Estado.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // umfMaterias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 407);
            Controls.Add(pnl_dgv);
            Controls.Add(pnl_Busqueda);
            Controls.Add(pnl_Acciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfMaterias";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "umfMaterias";
            Load += umfMaterias_Load;
            pnl_Acciones.ResumeLayout(false);
            pnl_Busqueda.ResumeLayout(false);
            pnl_dgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_Materias).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ReaLTaiizor.Controls.NightPanel pnl_Acciones;
        private ReaLTaiizor.Controls.NightPanel pnl_Busqueda;
        private Panel pnl_dgv;
        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Materias;
        private DataGridViewTextBoxColumn Id_Materia;
        private DataGridViewTextBoxColumn Codigo_Materia;
        private DataGridViewTextBoxColumn Nombre_Materia;
        private DataGridViewTextBoxColumn CreadoPor;
        private DataGridViewTextBoxColumn ModificadoPor;
        private DataGridViewTextBoxColumn Id_Transaccion;
        private DataGridViewTextBoxColumn Estado;
        private FontAwesome.Sharp.IconButton btn_AgregarMateria;
        private ReaLTaiizor.Controls.BigTextBox txt_Buscar;
        private FontAwesome.Sharp.IconButton btn_ModificarMateria;
    }
}

