namespace umForms.Principales.Hijos
{
    partial class umfUsuarios
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            pnl_Acciones = new ReaLTaiizor.Controls.NightPanel();
            btn_AgregarUsuario = new FontAwesome.Sharp.IconButton();
            pnl_Busqueda = new ReaLTaiizor.Controls.NightPanel();
            txt_Usuario = new ReaLTaiizor.Controls.BigTextBox();
            pnl_dgv = new Panel();
            dgv_Usuarios = new ReaLTaiizor.Controls.PoisonDataGridView();
            Id_Usuario = new DataGridViewTextBoxColumn();
            Id_Persona = new DataGridViewTextBoxColumn();
            Usuario = new DataGridViewTextBoxColumn();
            Ultima_Sesion = new DataGridViewTextBoxColumn();
            CreadoPor = new DataGridViewTextBoxColumn();
            ModificadoPor = new DataGridViewTextBoxColumn();
            Id_Transaccion = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            btn_ModificarUsuario = new FontAwesome.Sharp.IconButton();
            pnl_Acciones.SuspendLayout();
            pnl_Busqueda.SuspendLayout();
            pnl_dgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Usuarios).BeginInit();
            SuspendLayout();
            // 
            // pnl_Acciones
            // 
            pnl_Acciones.Controls.Add(btn_ModificarUsuario);
            pnl_Acciones.Controls.Add(btn_AgregarUsuario);
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
            // btn_AgregarUsuario
            // 
            btn_AgregarUsuario.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_AgregarUsuario.ForeColor = Color.Black;
            btn_AgregarUsuario.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            btn_AgregarUsuario.IconColor = Color.Black;
            btn_AgregarUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_AgregarUsuario.IconSize = 20;
            btn_AgregarUsuario.ImageAlign = ContentAlignment.MiddleLeft;
            btn_AgregarUsuario.Location = new Point(6, 64);
            btn_AgregarUsuario.Name = "btn_AgregarUsuario";
            btn_AgregarUsuario.Size = new Size(121, 35);
            btn_AgregarUsuario.TabIndex = 0;
            btn_AgregarUsuario.Text = "PERSONA / USUARIO";
            btn_AgregarUsuario.UseVisualStyleBackColor = true;
            btn_AgregarUsuario.Click += btn_AgregarUsuario_Click;
            // 
            // pnl_Busqueda
            // 
            pnl_Busqueda.Controls.Add(txt_Usuario);
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
            // txt_Usuario
            // 
            txt_Usuario.BackColor = Color.Transparent;
            txt_Usuario.Font = new Font("Tahoma", 11F);
            txt_Usuario.ForeColor = Color.DimGray;
            txt_Usuario.Image = null;
            txt_Usuario.Location = new Point(12, 12);
            txt_Usuario.MaxLength = 32767;
            txt_Usuario.Multiline = false;
            txt_Usuario.Name = "txt_Usuario";
            txt_Usuario.ReadOnly = false;
            txt_Usuario.Size = new Size(164, 41);
            txt_Usuario.TabIndex = 0;
            txt_Usuario.Text = "Buscar Usuario";
            txt_Usuario.TextAlignment = HorizontalAlignment.Center;
            txt_Usuario.UseSystemPasswordChar = false;
            txt_Usuario.TextChanged += txt_Usuario_TextChanged;
            // 
            // pnl_dgv
            // 
            pnl_dgv.Controls.Add(dgv_Usuarios);
            pnl_dgv.Dock = DockStyle.Fill;
            pnl_dgv.Location = new Point(0, 64);
            pnl_dgv.Name = "pnl_dgv";
            pnl_dgv.Size = new Size(696, 343);
            pnl_dgv.TabIndex = 5;
            // 
            // dgv_Usuarios
            // 
            dgv_Usuarios.AllowUserToAddRows = false;
            dgv_Usuarios.AllowUserToDeleteRows = false;
            dgv_Usuarios.AllowUserToResizeRows = false;
            dgv_Usuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Usuarios.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv_Usuarios.BorderStyle = BorderStyle.None;
            dgv_Usuarios.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv_Usuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgv_Usuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgv_Usuarios.ColumnHeadersHeight = 40;
            dgv_Usuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_Usuarios.Columns.AddRange(new DataGridViewColumn[] { Id_Usuario, Id_Persona, Usuario, Ultima_Sesion, CreadoPor, ModificadoPor, Id_Transaccion, Estado });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = Color.Black;
            dataGridViewCellStyle5.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle5.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgv_Usuarios.DefaultCellStyle = dataGridViewCellStyle5;
            dgv_Usuarios.Dock = DockStyle.Fill;
            dgv_Usuarios.EnableHeadersVisualStyles = false;
            dgv_Usuarios.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv_Usuarios.GridColor = Color.FromArgb(255, 255, 255);
            dgv_Usuarios.Location = new Point(0, 0);
            dgv_Usuarios.MultiSelect = false;
            dgv_Usuarios.Name = "dgv_Usuarios";
            dgv_Usuarios.ReadOnly = true;
            dgv_Usuarios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgv_Usuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgv_Usuarios.RowHeadersVisible = false;
            dgv_Usuarios.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv_Usuarios.RowTemplate.Height = 36;
            dgv_Usuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Usuarios.Size = new Size(696, 343);
            dgv_Usuarios.TabIndex = 0;
            // 
            // Id_Usuario
            // 
            Id_Usuario.HeaderText = "No. Usuario";
            Id_Usuario.Name = "Id_Usuario";
            Id_Usuario.ReadOnly = true;
            // 
            // Id_Persona
            // 
            Id_Persona.HeaderText = "Id_Persona";
            Id_Persona.Name = "Id_Persona";
            Id_Persona.ReadOnly = true;
            Id_Persona.Visible = false;
            // 
            // Usuario
            // 
            Usuario.HeaderText = "Usuario";
            Usuario.Name = "Usuario";
            Usuario.ReadOnly = true;
            // 
            // Ultima_Sesion
            // 
            Ultima_Sesion.HeaderText = "Ultima Sesion";
            Ultima_Sesion.Name = "Ultima_Sesion";
            Ultima_Sesion.ReadOnly = true;
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
            // btn_ModificarUsuario
            // 
            btn_ModificarUsuario.ForeColor = Color.Black;
            btn_ModificarUsuario.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            btn_ModificarUsuario.IconColor = Color.Black;
            btn_ModificarUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_ModificarUsuario.IconSize = 20;
            btn_ModificarUsuario.ImageAlign = ContentAlignment.MiddleLeft;
            btn_ModificarUsuario.Location = new Point(6, 114);
            btn_ModificarUsuario.Name = "btn_ModificarUsuario";
            btn_ModificarUsuario.Size = new Size(121, 35);
            btn_ModificarUsuario.TabIndex = 1;
            btn_ModificarUsuario.Text = "USUARIO";
            btn_ModificarUsuario.UseVisualStyleBackColor = true;
            btn_ModificarUsuario.Click += btn_ModificarUsuario_Click;
            // 
            // umfUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 407);
            Controls.Add(pnl_dgv);
            Controls.Add(pnl_Busqueda);
            Controls.Add(pnl_Acciones);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "umfUsuarios";
            Load += umfUsuarios_Load;
            pnl_Acciones.ResumeLayout(false);
            pnl_Busqueda.ResumeLayout(false);
            pnl_dgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_Usuarios).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ReaLTaiizor.Controls.NightPanel pnl_Acciones;
        private ReaLTaiizor.Controls.NightPanel pnl_Busqueda;
        private Panel pnl_dgv;
        private ReaLTaiizor.Controls.PoisonDataGridView dgv_Usuarios;
        private DataGridViewTextBoxColumn Id_Usuario;
        private DataGridViewTextBoxColumn Id_Persona;
        private DataGridViewTextBoxColumn Usuario;
        private DataGridViewTextBoxColumn Ultima_Sesion;
        private DataGridViewTextBoxColumn CreadoPor;
        private DataGridViewTextBoxColumn ModificadoPor;
        private DataGridViewTextBoxColumn Id_Transaccion;
        private DataGridViewTextBoxColumn Estado;
        private FontAwesome.Sharp.IconButton btn_AgregarUsuario;
        private ReaLTaiizor.Controls.BigTextBox txt_Usuario;
        private FontAwesome.Sharp.IconButton btn_ModificarUsuario;
    }
}