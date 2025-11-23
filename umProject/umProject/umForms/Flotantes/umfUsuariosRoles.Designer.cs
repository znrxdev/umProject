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
            pnl_Encabezado = new Panel();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            lb_Titulo = new ReaLTaiizor.Controls.CrownLabel();
            pnl_Formulario = new Panel();
            btn_Limpiar = new Button();
            btn_Guardar = new Button();
            ckb_Activo = new CheckBox();
            cmb_Roles = new ComboBox();
            lb_Roles = new ReaLTaiizor.Controls.CrownLabel();
            dgv_UsuarioRol = new DataGridView();
            Id_Usuario_Rol = new DataGridViewTextBoxColumn();
            Usuario = new DataGridViewTextBoxColumn();
            Id_Rol = new DataGridViewTextBoxColumn();
            Rol = new DataGridViewTextBoxColumn();
            Fecha_Creacion = new DataGridViewTextBoxColumn();
            Fecha_Modificacion = new DataGridViewTextBoxColumn();
            ActivoBool = new DataGridViewTextBoxColumn();
            Activo = new DataGridViewTextBoxColumn();
            btn_Seleccionar = new Button();
            Btn_Finalizar = new Button();
            pnl_Encabezado.SuspendLayout();
            pnl_Formulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_UsuarioRol).BeginInit();
            SuspendLayout();
            // 
            // pnl_Encabezado
            // 
            pnl_Encabezado.BackColor = Color.FromArgb(20, 40, 70);
            pnl_Encabezado.Controls.Add(btn_CerrarFormulario);
            pnl_Encabezado.Controls.Add(lb_Titulo);
            pnl_Encabezado.Dock = DockStyle.Top;
            pnl_Encabezado.Location = new Point(0, 0);
            pnl_Encabezado.Margin = new Padding(3, 4, 3, 4);
            pnl_Encabezado.Name = "pnl_Encabezado";
            pnl_Encabezado.Size = new Size(710, 93);
            pnl_Encabezado.TabIndex = 6;
            // 
            // btn_CerrarFormulario
            // 
            btn_CerrarFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_CerrarFormulario.BackColor = Color.FromArgb(200, 70, 70);
            btn_CerrarFormulario.Cursor = Cursors.Hand;
            btn_CerrarFormulario.FlatAppearance.BorderSize = 0;
            btn_CerrarFormulario.FlatStyle = FlatStyle.Flat;
            btn_CerrarFormulario.IconChar = FontAwesome.Sharp.IconChar.Close;
            btn_CerrarFormulario.IconColor = Color.White;
            btn_CerrarFormulario.IconFont = FontAwesome.Sharp.IconFont.Solid;
            btn_CerrarFormulario.IconSize = 18;
            btn_CerrarFormulario.Location = new Point(664, 11);
            btn_CerrarFormulario.Margin = new Padding(3, 4, 3, 4);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(37, 43);
            btn_CerrarFormulario.TabIndex = 26;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_Titulo
            // 
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(34, 29);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(285, 30);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "INFORMACIÓN PERMISOS";
            // 
            // pnl_Formulario
            // 
            pnl_Formulario.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Formulario.Controls.Add(btn_Limpiar);
            pnl_Formulario.Controls.Add(btn_Guardar);
            pnl_Formulario.Controls.Add(ckb_Activo);
            pnl_Formulario.Controls.Add(cmb_Roles);
            pnl_Formulario.Controls.Add(lb_Roles);
            pnl_Formulario.Location = new Point(15, 100);
            pnl_Formulario.Margin = new Padding(3, 4, 3, 4);
            pnl_Formulario.Name = "pnl_Formulario";
            pnl_Formulario.Padding = new Padding(17, 20, 17, 20);
            pnl_Formulario.Size = new Size(680, 200);
            pnl_Formulario.TabIndex = 11;
            // 
            // btn_Limpiar
            // 
            btn_Limpiar.BackColor = Color.FromArgb(200, 100, 100);
            btn_Limpiar.Cursor = Cursors.Hand;
            btn_Limpiar.FlatAppearance.BorderSize = 0;
            btn_Limpiar.FlatStyle = FlatStyle.Flat;
            btn_Limpiar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Limpiar.ForeColor = Color.White;
            btn_Limpiar.Location = new Point(17, 87);
            btn_Limpiar.Margin = new Padding(3, 4, 3, 4);
            btn_Limpiar.Name = "btn_Limpiar";
            btn_Limpiar.Size = new Size(286, 51);
            btn_Limpiar.TabIndex = 26;
            btn_Limpiar.Text = "🗑️ LIMPIAR";
            btn_Limpiar.UseVisualStyleBackColor = false;
            btn_Limpiar.Visible = false;
            btn_Limpiar.Click += btn_Limpiar_Click;
            // 
            // btn_Guardar
            // 
            btn_Guardar.BackColor = Color.FromArgb(0, 120, 215);
            btn_Guardar.Cursor = Cursors.Hand;
            btn_Guardar.FlatAppearance.BorderSize = 0;
            btn_Guardar.FlatStyle = FlatStyle.Flat;
            btn_Guardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Guardar.ForeColor = Color.White;
            btn_Guardar.Location = new Point(320, 87);
            btn_Guardar.Margin = new Padding(3, 4, 3, 4);
            btn_Guardar.Name = "btn_Guardar";
            btn_Guardar.Size = new Size(337, 51);
            btn_Guardar.TabIndex = 6;
            btn_Guardar.Text = "➕ GUARDAR";
            btn_Guardar.UseVisualStyleBackColor = false;
            btn_Guardar.Click += btn_Guardar_Click;
            // 
            // ckb_Activo
            // 
            ckb_Activo.AutoSize = true;
            ckb_Activo.Font = new Font("Segoe UI", 10F);
            ckb_Activo.Location = new Point(17, 100);
            ckb_Activo.Margin = new Padding(3, 4, 3, 4);
            ckb_Activo.Name = "ckb_Activo";
            ckb_Activo.Size = new Size(95, 27);
            ckb_Activo.TabIndex = 23;
            ckb_Activo.Text = "¿Activo?";
            ckb_Activo.UseVisualStyleBackColor = true;
            // 
            // cmb_Roles
            // 
            cmb_Roles.BackColor = Color.White;
            cmb_Roles.ForeColor = Color.Black;
            cmb_Roles.FormattingEnabled = true;
            cmb_Roles.Location = new Point(17, 49);
            cmb_Roles.Margin = new Padding(3, 4, 3, 4);
            cmb_Roles.Name = "cmb_Roles";
            cmb_Roles.Size = new Size(639, 28);
            cmb_Roles.TabIndex = 2;
            // 
            // lb_Roles
            // 
            lb_Roles.AutoSize = true;
            lb_Roles.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Roles.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Roles.Location = new Point(17, 20);
            lb_Roles.Name = "lb_Roles";
            lb_Roles.Size = new Size(57, 23);
            lb_Roles.TabIndex = 15;
            lb_Roles.Text = "Roles:";
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
            dgv_UsuarioRol.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_UsuarioRol.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(20, 40, 70);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_UsuarioRol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_UsuarioRol.ColumnHeadersHeight = 40;
            dgv_UsuarioRol.Columns.AddRange(new DataGridViewColumn[] { Id_Usuario_Rol, Usuario, Id_Rol, Rol, Fecha_Creacion, Fecha_Modificacion, ActivoBool, Activo });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_UsuarioRol.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_UsuarioRol.EnableHeadersVisualStyles = false;
            dgv_UsuarioRol.GridColor = Color.FromArgb(220, 220, 220);
            dgv_UsuarioRol.Location = new Point(15, 313);
            dgv_UsuarioRol.Margin = new Padding(3, 4, 3, 4);
            dgv_UsuarioRol.MultiSelect = false;
            dgv_UsuarioRol.Name = "dgv_UsuarioRol";
            dgv_UsuarioRol.ReadOnly = true;
            dgv_UsuarioRol.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgv_UsuarioRol.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv_UsuarioRol.RowHeadersVisible = false;
            dgv_UsuarioRol.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dgv_UsuarioRol.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgv_UsuarioRol.RowTemplate.Height = 28;
            dgv_UsuarioRol.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_UsuarioRol.Size = new Size(680, 267);
            dgv_UsuarioRol.TabIndex = 13;
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
            // Id_Rol
            // 
            Id_Rol.HeaderText = "Id_Rol";
            Id_Rol.MinimumWidth = 6;
            Id_Rol.Name = "Id_Rol";
            Id_Rol.ReadOnly = true;
            Id_Rol.Visible = false;
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
            // btn_Seleccionar
            // 
            btn_Seleccionar.BackColor = Color.FromArgb(0, 150, 100);
            btn_Seleccionar.Cursor = Cursors.Hand;
            btn_Seleccionar.FlatAppearance.BorderSize = 0;
            btn_Seleccionar.FlatStyle = FlatStyle.Flat;
            btn_Seleccionar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_Seleccionar.ForeColor = Color.White;
            btn_Seleccionar.Location = new Point(15, 993);
            btn_Seleccionar.Margin = new Padding(3, 4, 3, 4);
            btn_Seleccionar.Name = "btn_Seleccionar";
            btn_Seleccionar.Size = new Size(320, 56);
            btn_Seleccionar.TabIndex = 25;
            btn_Seleccionar.Text = "✅ SELECCIONAR";
            btn_Seleccionar.UseVisualStyleBackColor = false;
            btn_Seleccionar.Click += btn_Seleccionar_Click;
            // 
            // Btn_Finalizar
            // 
            Btn_Finalizar.BackColor = Color.FromArgb(0, 120, 215);
            Btn_Finalizar.Cursor = Cursors.Hand;
            Btn_Finalizar.FlatAppearance.BorderSize = 0;
            Btn_Finalizar.FlatStyle = FlatStyle.Flat;
            Btn_Finalizar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Btn_Finalizar.ForeColor = Color.White;
            Btn_Finalizar.Location = new Point(375, 993);
            Btn_Finalizar.Margin = new Padding(3, 4, 3, 4);
            Btn_Finalizar.Name = "Btn_Finalizar";
            Btn_Finalizar.Size = new Size(320, 56);
            Btn_Finalizar.TabIndex = 24;
            Btn_Finalizar.Text = "🔒 FINALIZAR";
            Btn_Finalizar.UseVisualStyleBackColor = false;
            Btn_Finalizar.Click += Btn_Finalizar_Click;
            // 
            // umfUsuariosRoles
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(710, 1067);
            Controls.Add(btn_Seleccionar);
            Controls.Add(Btn_Finalizar);
            Controls.Add(dgv_UsuarioRol);
            Controls.Add(pnl_Formulario);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(710, 1067);
            MinimumSize = new Size(710, 1018);
            Name = "umfUsuariosRoles";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfUsuariosRoles_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Formulario.ResumeLayout(false);
            pnl_Formulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_UsuarioRol).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Encabezado;
        private System.Windows.Forms.Panel pnl_Formulario;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private ReaLTaiizor.Controls.CrownLabel lb_Roles;
        private System.Windows.Forms.ComboBox cmb_Roles;
        private System.Windows.Forms.CheckBox ckb_Activo;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.Button btn_Limpiar;
        private System.Windows.Forms.DataGridView dgv_UsuarioRol;
        private System.Windows.Forms.Button btn_Seleccionar;
        private System.Windows.Forms.Button Btn_Finalizar;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private DataGridViewTextBoxColumn Id_Usuario_Rol;
        private DataGridViewTextBoxColumn Usuario;
        private DataGridViewTextBoxColumn Id_Rol;
        private DataGridViewTextBoxColumn Rol;
        private DataGridViewTextBoxColumn Fecha_Creacion;
        private DataGridViewTextBoxColumn Fecha_Modificacion;
        private DataGridViewTextBoxColumn ActivoBool;
        private DataGridViewTextBoxColumn Activo;
    }
}