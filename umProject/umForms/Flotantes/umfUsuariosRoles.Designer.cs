namespace umForms.Flotantes
{
    partial class umfUsuariosRoles
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pnl_Arriba = new Panel();
            lb_Titulo = new Label();
            gbx_Permisos = new GroupBox();
            btn_Limpiar = new Button();
            ckb_Activo = new CheckBox();
            btn_Guardar = new Button();
            lb_Roles = new Label();
            cmb_Roles = new ComboBox();
            dgv_UsuarioRol = new DataGridView();
            Btn_Finalizar = new Button();
            btn_Seleccionar = new Button();
            Id_Usuario_Rol = new DataGridViewTextBoxColumn();
            Usuario = new DataGridViewTextBoxColumn();
            Rol = new DataGridViewTextBoxColumn();
            Fecha_Creacion = new DataGridViewTextBoxColumn();
            Fecha_Modificacion = new DataGridViewTextBoxColumn();
            ActivoBool = new DataGridViewTextBoxColumn();
            Activo = new DataGridViewTextBoxColumn();
            pnl_Arriba.SuspendLayout();
            gbx_Permisos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_UsuarioRol).BeginInit();
            SuspendLayout();
            // 
            // pnl_Arriba
            // 
            pnl_Arriba.BackColor = Color.FromArgb(10, 24, 51);
            pnl_Arriba.Controls.Add(lb_Titulo);
            pnl_Arriba.Dock = DockStyle.Top;
            pnl_Arriba.Location = new Point(0, 0);
            pnl_Arriba.Margin = new Padding(2, 3, 2, 3);
            pnl_Arriba.Name = "pnl_Arriba";
            pnl_Arriba.Size = new Size(621, 104);
            pnl_Arriba.TabIndex = 6;
            // 
            // lb_Titulo
            // 
            lb_Titulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(190, 40);
            lb_Titulo.Margin = new Padding(2, 0, 2, 0);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(260, 24);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "INFORMACION PERMISOS";
            // 
            // gbx_Permisos
            // 
            gbx_Permisos.Controls.Add(btn_Limpiar);
            gbx_Permisos.Controls.Add(ckb_Activo);
            gbx_Permisos.Controls.Add(btn_Guardar);
            gbx_Permisos.Controls.Add(lb_Roles);
            gbx_Permisos.Controls.Add(cmb_Roles);
            gbx_Permisos.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gbx_Permisos.Location = new Point(13, 112);
            gbx_Permisos.Margin = new Padding(2, 3, 2, 3);
            gbx_Permisos.Name = "gbx_Permisos";
            gbx_Permisos.Padding = new Padding(2, 3, 2, 3);
            gbx_Permisos.Size = new Size(595, 284);
            gbx_Permisos.TabIndex = 11;
            gbx_Permisos.TabStop = false;
            gbx_Permisos.Text = "Datos de Permisos:";
            // 
            // btn_Limpiar
            // 
            btn_Limpiar.BackColor = Color.FromArgb(9, 46, 85);
            btn_Limpiar.FlatStyle = FlatStyle.Flat;
            btn_Limpiar.Font = new Font("Arial", 10.2F);
            btn_Limpiar.ForeColor = Color.White;
            btn_Limpiar.Location = new Point(219, 229);
            btn_Limpiar.Margin = new Padding(2, 3, 2, 3);
            btn_Limpiar.Name = "btn_Limpiar";
            btn_Limpiar.Size = new Size(146, 49);
            btn_Limpiar.TabIndex = 26;
            btn_Limpiar.Text = "LIMPIAR";
            btn_Limpiar.UseVisualStyleBackColor = false;
            btn_Limpiar.Visible = false;
            btn_Limpiar.Click += btn_Limpiar_Click;
            // 
            // ckb_Activo
            // 
            ckb_Activo.AutoSize = true;
            ckb_Activo.Location = new Point(73, 133);
            ckb_Activo.Margin = new Padding(2, 3, 2, 3);
            ckb_Activo.Name = "ckb_Activo";
            ckb_Activo.Size = new Size(94, 23);
            ckb_Activo.TabIndex = 23;
            ckb_Activo.Text = "¿Activo?";
            ckb_Activo.UseVisualStyleBackColor = true;
            // 
            // btn_Guardar
            // 
            btn_Guardar.BackColor = Color.FromArgb(9, 46, 85);
            btn_Guardar.FlatStyle = FlatStyle.Flat;
            btn_Guardar.ForeColor = Color.White;
            btn_Guardar.Location = new Point(73, 165);
            btn_Guardar.Margin = new Padding(2, 3, 2, 3);
            btn_Guardar.Name = "btn_Guardar";
            btn_Guardar.Size = new Size(438, 49);
            btn_Guardar.TabIndex = 6;
            btn_Guardar.Text = "GUARDAR";
            btn_Guardar.UseVisualStyleBackColor = false;
            btn_Guardar.Click += btn_Guardar_Click;
            // 
            // lb_Roles
            // 
            lb_Roles.AutoSize = true;
            lb_Roles.Location = new Point(73, 60);
            lb_Roles.Margin = new Padding(2, 0, 2, 0);
            lb_Roles.Name = "lb_Roles";
            lb_Roles.Size = new Size(54, 19);
            lb_Roles.TabIndex = 15;
            lb_Roles.Text = "Roles:";
            // 
            // cmb_Roles
            // 
            cmb_Roles.FormattingEnabled = true;
            cmb_Roles.Location = new Point(73, 96);
            cmb_Roles.Margin = new Padding(2, 3, 2, 3);
            cmb_Roles.Name = "cmb_Roles";
            cmb_Roles.Size = new Size(437, 27);
            cmb_Roles.TabIndex = 2;
            // 
            // dgv_UsuarioRol
            // 
            dgv_UsuarioRol.AllowUserToAddRows = false;
            dgv_UsuarioRol.AllowUserToDeleteRows = false;
            dgv_UsuarioRol.AllowUserToResizeColumns = false;
            dgv_UsuarioRol.AllowUserToResizeRows = false;
            dgv_UsuarioRol.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_UsuarioRol.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_UsuarioRol.BackgroundColor = Color.White;
            dgv_UsuarioRol.BorderStyle = BorderStyle.None;
            dgv_UsuarioRol.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dgv_UsuarioRol.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(10, 24, 51);
            dataGridViewCellStyle1.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(9, 46, 85);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_UsuarioRol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_UsuarioRol.ColumnHeadersHeight = 50;
            dgv_UsuarioRol.Columns.AddRange(new DataGridViewColumn[] { Id_Usuario_Rol, Usuario, Rol, Fecha_Creacion, Fecha_Modificacion, ActivoBool, Activo });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(9, 46, 85);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_UsuarioRol.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_UsuarioRol.EnableHeadersVisualStyles = false;
            dgv_UsuarioRol.GridColor = Color.FromArgb(10, 24, 51);
            dgv_UsuarioRol.Location = new Point(13, 403);
            dgv_UsuarioRol.Margin = new Padding(2, 3, 2, 3);
            dgv_UsuarioRol.MultiSelect = false;
            dgv_UsuarioRol.Name = "dgv_UsuarioRol";
            dgv_UsuarioRol.ReadOnly = true;
            dgv_UsuarioRol.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(10, 24, 51);
            dataGridViewCellStyle3.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(9, 46, 85);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_UsuarioRol.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_UsuarioRol.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(9, 46, 85);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dgv_UsuarioRol.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgv_UsuarioRol.RowTemplate.Height = 24;
            dgv_UsuarioRol.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_UsuarioRol.Size = new Size(595, 240);
            dgv_UsuarioRol.TabIndex = 13;
            // 
            // Btn_Finalizar
            // 
            Btn_Finalizar.BackColor = Color.FromArgb(9, 46, 85);
            Btn_Finalizar.FlatStyle = FlatStyle.Flat;
            Btn_Finalizar.Font = new Font("Arial", 10.2F);
            Btn_Finalizar.ForeColor = Color.White;
            Btn_Finalizar.Location = new Point(462, 677);
            Btn_Finalizar.Margin = new Padding(2, 3, 2, 3);
            Btn_Finalizar.Name = "Btn_Finalizar";
            Btn_Finalizar.Size = new Size(146, 49);
            Btn_Finalizar.TabIndex = 24;
            Btn_Finalizar.Text = "FINALIZAR";
            Btn_Finalizar.UseVisualStyleBackColor = false;
            Btn_Finalizar.Click += Btn_Finalizar_Click;
            // 
            // btn_Seleccionar
            // 
            btn_Seleccionar.BackColor = Color.FromArgb(9, 46, 85);
            btn_Seleccionar.FlatStyle = FlatStyle.Flat;
            btn_Seleccionar.Font = new Font("Arial", 10.2F);
            btn_Seleccionar.ForeColor = Color.White;
            btn_Seleccionar.Location = new Point(13, 677);
            btn_Seleccionar.Margin = new Padding(2, 3, 2, 3);
            btn_Seleccionar.Name = "btn_Seleccionar";
            btn_Seleccionar.Size = new Size(170, 49);
            btn_Seleccionar.TabIndex = 25;
            btn_Seleccionar.Text = "SELECCIONAR";
            btn_Seleccionar.UseVisualStyleBackColor = false;
            btn_Seleccionar.Click += btn_Seleccionar_Click;
            // 
            // Id_Usuario_Rol
            // 
            Id_Usuario_Rol.HeaderText = "Id_Usuario_Rol";
            Id_Usuario_Rol.MinimumWidth = 6;
            Id_Usuario_Rol.Name = "Id_Usuario_Rol";
            Id_Usuario_Rol.ReadOnly = true;
            Id_Usuario_Rol.Visible = false;
            // 
            // Usuario
            // 
            Usuario.HeaderText = "Usuario";
            Usuario.MinimumWidth = 6;
            Usuario.Name = "Usuario";
            Usuario.ReadOnly = true;
            // 
            // Rol
            // 
            Rol.HeaderText = "Rol";
            Rol.MinimumWidth = 6;
            Rol.Name = "Rol";
            Rol.ReadOnly = true;
            // 
            // Fecha_Creacion
            // 
            Fecha_Creacion.HeaderText = "Fecha Creacion";
            Fecha_Creacion.MinimumWidth = 6;
            Fecha_Creacion.Name = "Fecha_Creacion";
            Fecha_Creacion.ReadOnly = true;
            // 
            // Fecha_Modificacion
            // 
            Fecha_Modificacion.HeaderText = "Fecha Modificacion";
            Fecha_Modificacion.MinimumWidth = 6;
            Fecha_Modificacion.Name = "Fecha_Modificacion";
            Fecha_Modificacion.ReadOnly = true;
            // 
            // ActivoBool
            // 
            ActivoBool.HeaderText = "ActivoBool";
            ActivoBool.MinimumWidth = 6;
            ActivoBool.Name = "ActivoBool";
            ActivoBool.ReadOnly = true;
            ActivoBool.Visible = false;
            // 
            // Activo
            // 
            Activo.HeaderText = "Activo";
            Activo.MinimumWidth = 6;
            Activo.Name = "Activo";
            Activo.ReadOnly = true;
            // 
            // umfUsuariosRoles
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(621, 800);
            Controls.Add(btn_Seleccionar);
            Controls.Add(Btn_Finalizar);
            Controls.Add(dgv_UsuarioRol);
            Controls.Add(gbx_Permisos);
            Controls.Add(pnl_Arriba);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2, 3, 2, 3);
            MaximumSize = new Size(621, 800);
            MinimumSize = new Size(621, 800);
            Name = "umfUsuariosRoles";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfUsuariosRoles_Load;
            pnl_Arriba.ResumeLayout(false);
            pnl_Arriba.PerformLayout();
            gbx_Permisos.ResumeLayout(false);
            gbx_Permisos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_UsuarioRol).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Arriba;
        private System.Windows.Forms.Label lb_Titulo;
        private System.Windows.Forms.GroupBox gbx_Permisos;
        private System.Windows.Forms.CheckBox ckb_Activo;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.Label lb_Roles;
        private System.Windows.Forms.ComboBox cmb_Roles;
        private System.Windows.Forms.DataGridView dgv_UsuarioRol;
        private System.Windows.Forms.Button Btn_Finalizar;
        private System.Windows.Forms.Button btn_Seleccionar;
        private System.Windows.Forms.Button btn_Limpiar;
        private DataGridViewTextBoxColumn Id_Usuario_Rol;
        private DataGridViewTextBoxColumn Usuario;
        private DataGridViewTextBoxColumn Rol;
        private DataGridViewTextBoxColumn Fecha_Creacion;
        private DataGridViewTextBoxColumn Fecha_Modificacion;
        private DataGridViewTextBoxColumn ActivoBool;
        private DataGridViewTextBoxColumn Activo;
    }
}