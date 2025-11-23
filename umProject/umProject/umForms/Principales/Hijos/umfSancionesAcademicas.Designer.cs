namespace umForms.Principales.Hijos
{
    partial class umfSancionesAcademicas
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
            btn_ResponderApelacion = new FontAwesome.Sharp.IconButton();
            btn_ModificarSancion = new FontAwesome.Sharp.IconButton();
            btn_AgregarSancion = new FontAwesome.Sharp.IconButton();
            pnl_Busqueda = new ReaLTaiizor.Controls.NightPanel();
            ckb_FiltrarApelaciones = new CheckBox();
            cmb_Estado = new ComboBox();
            lb_Estado = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Estudiante = new ComboBox();
            lb_Estudiante = new ReaLTaiizor.Controls.CrownLabel();
            pnl_dgv = new Panel();
            dgv_SancionesAcademicas = new ReaLTaiizor.Controls.PoisonDataGridView();
            Id_Sancion = new DataGridViewTextBoxColumn();
            Codigo_Sancion = new DataGridViewTextBoxColumn();
            Estudiante = new DataGridViewTextBoxColumn();
            Tipo_Sancion = new DataGridViewTextBoxColumn();
            Severidad = new DataGridViewTextBoxColumn();
            Fecha_Registro = new DataGridViewTextBoxColumn();
            Fecha_Fin = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            Motivo = new DataGridViewTextBoxColumn();
            pnl_Acciones.SuspendLayout();
            pnl_Busqueda.SuspendLayout();
            pnl_dgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_SancionesAcademicas).BeginInit();
            SuspendLayout();
            // 
            // pnl_Acciones
            // 
            pnl_Acciones.Controls.Add(btn_ResponderApelacion);
            pnl_Acciones.Controls.Add(btn_ModificarSancion);
            pnl_Acciones.Controls.Add(btn_AgregarSancion);
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
            // btn_ResponderApelacion
            // 
            btn_ResponderApelacion.ForeColor = Color.Black;
            btn_ResponderApelacion.IconChar = FontAwesome.Sharp.IconChar.Gavel;
            btn_ResponderApelacion.IconColor = Color.Black;
            btn_ResponderApelacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_ResponderApelacion.IconSize = 20;
            btn_ResponderApelacion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ResponderApelacion.Location = new Point(6, 164);
            btn_ResponderApelacion.Name = "btn_ResponderApelacion";
            btn_ResponderApelacion.Size = new Size(121, 35);
            btn_ResponderApelacion.TabIndex = 2;
            btn_ResponderApelacion.Text = "RESPONDER APELACIÓN";
            btn_ResponderApelacion.UseVisualStyleBackColor = true;
            btn_ResponderApelacion.Click += btn_ResponderApelacion_Click;
            // 
            // btn_ModificarSancion
            // 
            btn_ModificarSancion.ForeColor = Color.Black;
            btn_ModificarSancion.IconChar = FontAwesome.Sharp.IconChar.Pen;
            btn_ModificarSancion.IconColor = Color.Black;
            btn_ModificarSancion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_ModificarSancion.IconSize = 20;
            btn_ModificarSancion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ModificarSancion.Location = new Point(6, 114);
            btn_ModificarSancion.Name = "btn_ModificarSancion";
            btn_ModificarSancion.Size = new Size(121, 35);
            btn_ModificarSancion.TabIndex = 1;
            btn_ModificarSancion.Text = "MODIFICAR";
            btn_ModificarSancion.UseVisualStyleBackColor = true;
            btn_ModificarSancion.Click += btn_ModificarSancion_Click;
            // 
            // btn_AgregarSancion
            // 
            btn_AgregarSancion.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_AgregarSancion.ForeColor = Color.Black;
            btn_AgregarSancion.IconChar = FontAwesome.Sharp.IconChar.Add;
            btn_AgregarSancion.IconColor = Color.Black;
            btn_AgregarSancion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_AgregarSancion.IconSize = 20;
            btn_AgregarSancion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_AgregarSancion.Location = new Point(6, 64);
            btn_AgregarSancion.Name = "btn_AgregarSancion";
            btn_AgregarSancion.Size = new Size(121, 35);
            btn_AgregarSancion.TabIndex = 0;
            btn_AgregarSancion.Text = "AGREGAR";
            btn_AgregarSancion.UseVisualStyleBackColor = true;
            btn_AgregarSancion.Click += btn_AgregarSancion_Click;
            // 
            // pnl_Busqueda
            // 
            pnl_Busqueda.Controls.Add(ckb_FiltrarApelaciones);
            pnl_Busqueda.Controls.Add(cmb_Estado);
            pnl_Busqueda.Controls.Add(lb_Estado);
            pnl_Busqueda.Controls.Add(cmb_Estudiante);
            pnl_Busqueda.Controls.Add(lb_Estudiante);
            pnl_Busqueda.Dock = DockStyle.Top;
            pnl_Busqueda.ForeColor = Color.FromArgb(250, 250, 250);
            pnl_Busqueda.LeftSideColor = Color.FromArgb(10, 28, 51);
            pnl_Busqueda.Location = new Point(0, 0);
            pnl_Busqueda.Name = "pnl_Busqueda";
            pnl_Busqueda.RightSideColor = Color.FromArgb(10, 28, 51);
            pnl_Busqueda.Side = ReaLTaiizor.Controls.NightPanel.PanelSide.Left;
            pnl_Busqueda.Size = new Size(696, 99);
            pnl_Busqueda.TabIndex = 4;
            // 
            // ckb_FiltrarApelaciones
            // 
            ckb_FiltrarApelaciones.AutoSize = true;
            ckb_FiltrarApelaciones.Font = new Font("Segoe UI", 10F);
            ckb_FiltrarApelaciones.ForeColor = Color.White;
            ckb_FiltrarApelaciones.Location = new Point(12, 57);
            ckb_FiltrarApelaciones.Name = "ckb_FiltrarApelaciones";
            ckb_FiltrarApelaciones.Size = new Size(243, 23);
            ckb_FiltrarApelaciones.TabIndex = 4;
            ckb_FiltrarApelaciones.Text = "Mostrar solo apelaciones en espera";
            ckb_FiltrarApelaciones.UseVisualStyleBackColor = true;
            ckb_FiltrarApelaciones.CheckedChanged += ckb_FiltrarApelaciones_CheckedChanged;
            // 
            // cmb_Estado
            // 
            cmb_Estado.BackColor = Color.White;
            cmb_Estado.ForeColor = Color.Black;
            cmb_Estado.FormattingEnabled = true;
            cmb_Estado.Location = new Point(330, 28);
            cmb_Estado.Name = "cmb_Estado";
            cmb_Estado.Size = new Size(300, 23);
            cmb_Estado.TabIndex = 3;
            cmb_Estado.Visible = false;
            cmb_Estado.SelectedIndexChanged += cmb_Estado_SelectedIndexChanged;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Estado.ForeColor = Color.White;
            lb_Estado.Location = new Point(330, 5);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(57, 19);
            lb_Estado.TabIndex = 2;
            lb_Estado.Text = "Estado:";
            lb_Estado.Visible = false;
            // 
            // cmb_Estudiante
            // 
            cmb_Estudiante.BackColor = Color.White;
            cmb_Estudiante.ForeColor = Color.Black;
            cmb_Estudiante.FormattingEnabled = true;
            cmb_Estudiante.Location = new Point(12, 28);
            cmb_Estudiante.Name = "cmb_Estudiante";
            cmb_Estudiante.Size = new Size(300, 23);
            cmb_Estudiante.TabIndex = 1;
            cmb_Estudiante.SelectedIndexChanged += cmb_Estudiante_SelectedIndexChanged;
            // 
            // lb_Estudiante
            // 
            lb_Estudiante.AutoSize = true;
            lb_Estudiante.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Estudiante.ForeColor = Color.White;
            lb_Estudiante.Location = new Point(12, 5);
            lb_Estudiante.Name = "lb_Estudiante";
            lb_Estudiante.Size = new Size(81, 19);
            lb_Estudiante.TabIndex = 0;
            lb_Estudiante.Text = "Estudiante:";
            // 
            // pnl_dgv
            // 
            pnl_dgv.Controls.Add(dgv_SancionesAcademicas);
            pnl_dgv.Dock = DockStyle.Fill;
            pnl_dgv.Location = new Point(0, 99);
            pnl_dgv.Name = "pnl_dgv";
            pnl_dgv.Size = new Size(696, 308);
            pnl_dgv.TabIndex = 5;
            // 
            // dgv_SancionesAcademicas
            // 
            dgv_SancionesAcademicas.AllowUserToAddRows = false;
            dgv_SancionesAcademicas.AllowUserToDeleteRows = false;
            dgv_SancionesAcademicas.AllowUserToResizeRows = false;
            dgv_SancionesAcademicas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_SancionesAcademicas.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv_SancionesAcademicas.BorderStyle = BorderStyle.None;
            dgv_SancionesAcademicas.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_SancionesAcademicas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_SancionesAcademicas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_SancionesAcademicas.ColumnHeadersHeight = 40;
            dgv_SancionesAcademicas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_SancionesAcademicas.Columns.AddRange(new DataGridViewColumn[] { Id_Sancion, Codigo_Sancion, Estudiante, Tipo_Sancion, Severidad, Fecha_Registro, Fecha_Fin, Estado, Motivo });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle2.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_SancionesAcademicas.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_SancionesAcademicas.Dock = DockStyle.Fill;
            dgv_SancionesAcademicas.EnableHeadersVisualStyles = false;
            dgv_SancionesAcademicas.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv_SancionesAcademicas.GridColor = Color.FromArgb(255, 255, 255);
            dgv_SancionesAcademicas.Location = new Point(0, 0);
            dgv_SancionesAcademicas.MultiSelect = false;
            dgv_SancionesAcademicas.Name = "dgv_SancionesAcademicas";
            dgv_SancionesAcademicas.ReadOnly = true;
            dgv_SancionesAcademicas.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_SancionesAcademicas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_SancionesAcademicas.RowHeadersVisible = false;
            dgv_SancionesAcademicas.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_SancionesAcademicas.RowTemplate.Height = 36;
            dgv_SancionesAcademicas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_SancionesAcademicas.Size = new Size(696, 308);
            dgv_SancionesAcademicas.TabIndex = 0;
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
            // Estudiante
            // 
            Estudiante.HeaderText = "Estudiante";
            Estudiante.Name = "Estudiante";
            Estudiante.ReadOnly = true;
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
            // umfSancionesAcademicas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 407);
            Controls.Add(pnl_dgv);
            Controls.Add(pnl_Busqueda);
            Controls.Add(pnl_Acciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfSancionesAcademicas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "umfSancionesAcademicas";
            Load += umfSancionesAcademicas_Load;
            pnl_Acciones.ResumeLayout(false);
            pnl_Busqueda.ResumeLayout(false);
            pnl_Busqueda.PerformLayout();
            pnl_dgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_SancionesAcademicas).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ReaLTaiizor.Controls.NightPanel pnl_Acciones;
        private ReaLTaiizor.Controls.NightPanel pnl_Busqueda;
        private Panel pnl_dgv;
        private ReaLTaiizor.Controls.PoisonDataGridView dgv_SancionesAcademicas;
        private DataGridViewTextBoxColumn Id_Sancion;
        private DataGridViewTextBoxColumn Codigo_Sancion;
        private DataGridViewTextBoxColumn Estudiante;
        private DataGridViewTextBoxColumn Tipo_Sancion;
        private DataGridViewTextBoxColumn Severidad;
        private DataGridViewTextBoxColumn Fecha_Registro;
        private DataGridViewTextBoxColumn Fecha_Fin;
        private DataGridViewTextBoxColumn Estado;
        private DataGridViewTextBoxColumn Motivo;
        private FontAwesome.Sharp.IconButton btn_AgregarSancion;
        private ComboBox cmb_Estudiante;
        private ReaLTaiizor.Controls.CrownLabel lb_Estudiante;
        private FontAwesome.Sharp.IconButton btn_ModificarSancion;
        private ComboBox cmb_Estado;
        private ReaLTaiizor.Controls.CrownLabel lb_Estado;
        private FontAwesome.Sharp.IconButton btn_ResponderApelacion;
        private CheckBox ckb_FiltrarApelaciones;
    }
}

