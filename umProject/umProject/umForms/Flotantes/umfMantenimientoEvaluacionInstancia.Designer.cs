namespace umForms.Flotantes
{
    partial class umfMantenimientoEvaluacionInstancia
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
            pnl_Encabezado = new Panel();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            lb_Titulo = new ReaLTaiizor.Controls.CrownLabel();
            pnl_Contenedor = new Panel();
            scrollPanel = new Panel();
            cmb_ModeloEvaluacion = new ComboBox();
            lb_ModeloEvaluacion = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Seccion = new ComboBox();
            lb_Seccion = new ReaLTaiizor.Controls.CrownLabel();
            cmb_MateriaPeriodo = new ComboBox();
            lb_MateriaPeriodo = new ReaLTaiizor.Controls.CrownLabel();
            chk_RequiereRevisionInterna = new CheckBox();
            num_NumeroVersion = new NumericUpDown();
            lb_NumeroVersion = new ReaLTaiizor.Controls.CrownLabel();
            chk_FechaLimite = new CheckBox();
            dtp_FechaLimite = new DateTimePicker();
            lb_FechaLimite = new ReaLTaiizor.Controls.CrownLabel();
            dtp_FechaProgramada = new DateTimePicker();
            lb_FechaProgramada = new ReaLTaiizor.Controls.CrownLabel();
            cmb_EstadoPublicacion = new ComboBox();
            lb_EstadoPublicacion = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Estado = new ComboBox();
            lb_Estado = new ReaLTaiizor.Controls.CrownLabel();
            txt_CodigoInstancia = new ReaLTaiizor.Controls.SmallTextBox();
            lb_CodigoInstancia = new ReaLTaiizor.Controls.CrownLabel();
            btn_GuardarDatos = new Button();
            pnl_Encabezado.SuspendLayout();
            pnl_Contenedor.SuspendLayout();
            scrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_NumeroVersion).BeginInit();
            SuspendLayout();
            // 
            // pnl_Encabezado
            // 
            pnl_Encabezado.BackColor = Color.FromArgb(20, 40, 70);
            pnl_Encabezado.Controls.Add(btn_CerrarFormulario);
            pnl_Encabezado.Controls.Add(lb_Titulo);
            pnl_Encabezado.Dock = DockStyle.Top;
            pnl_Encabezado.Location = new Point(0, 0);
            pnl_Encabezado.Name = "pnl_Encabezado";
            pnl_Encabezado.Size = new Size(900, 70);
            pnl_Encabezado.TabIndex = 1;
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
            btn_CerrarFormulario.Location = new Point(860, 8);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(32, 32);
            btn_CerrarFormulario.TabIndex = 12;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_Titulo
            // 
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(35, 22);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(391, 25);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "INFORMACIÓN INSTANCIA DE EVALUACIÓN";
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Contenedor.Controls.Add(scrollPanel);
            pnl_Contenedor.Controls.Add(btn_GuardarDatos);
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Location = new Point(0, 70);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Padding = new Padding(30);
            pnl_Contenedor.Size = new Size(900, 500);
            pnl_Contenedor.TabIndex = 2;
            // 
            // scrollPanel
            // 
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(cmb_ModeloEvaluacion);
            scrollPanel.Controls.Add(lb_ModeloEvaluacion);
            scrollPanel.Controls.Add(cmb_Seccion);
            scrollPanel.Controls.Add(lb_Seccion);
            scrollPanel.Controls.Add(cmb_MateriaPeriodo);
            scrollPanel.Controls.Add(lb_MateriaPeriodo);
            scrollPanel.Controls.Add(chk_RequiereRevisionInterna);
            scrollPanel.Controls.Add(num_NumeroVersion);
            scrollPanel.Controls.Add(lb_NumeroVersion);
            scrollPanel.Controls.Add(chk_FechaLimite);
            scrollPanel.Controls.Add(dtp_FechaLimite);
            scrollPanel.Controls.Add(lb_FechaLimite);
            scrollPanel.Controls.Add(dtp_FechaProgramada);
            scrollPanel.Controls.Add(lb_FechaProgramada);
            scrollPanel.Controls.Add(cmb_EstadoPublicacion);
            scrollPanel.Controls.Add(lb_EstadoPublicacion);
            scrollPanel.Controls.Add(cmb_Estado);
            scrollPanel.Controls.Add(lb_Estado);
            scrollPanel.Controls.Add(txt_CodigoInstancia);
            scrollPanel.Controls.Add(lb_CodigoInstancia);
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.Location = new Point(30, 30);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new Size(840, 395);
            scrollPanel.TabIndex = 18;
            // 
            // cmb_ModeloEvaluacion
            // 
            cmb_ModeloEvaluacion.BackColor = Color.White;
            cmb_ModeloEvaluacion.ForeColor = Color.Black;
            cmb_ModeloEvaluacion.FormattingEnabled = true;
            cmb_ModeloEvaluacion.Location = new Point(22, 205);
            cmb_ModeloEvaluacion.Name = "cmb_ModeloEvaluacion";
            cmb_ModeloEvaluacion.Size = new Size(380, 23);
            cmb_ModeloEvaluacion.TabIndex = 3;
            // 
            // lb_ModeloEvaluacion
            // 
            lb_ModeloEvaluacion.AutoSize = true;
            lb_ModeloEvaluacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_ModeloEvaluacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_ModeloEvaluacion.Location = new Point(22, 183);
            lb_ModeloEvaluacion.Name = "lb_ModeloEvaluacion";
            lb_ModeloEvaluacion.Size = new Size(140, 19);
            lb_ModeloEvaluacion.TabIndex = 0;
            lb_ModeloEvaluacion.Text = "Modelo Evaluación:";
            // 
            // cmb_Seccion
            // 
            cmb_Seccion.BackColor = Color.White;
            cmb_Seccion.ForeColor = Color.Black;
            cmb_Seccion.FormattingEnabled = true;
            cmb_Seccion.Location = new Point(22, 150);
            cmb_Seccion.Name = "cmb_Seccion";
            cmb_Seccion.Size = new Size(380, 23);
            cmb_Seccion.TabIndex = 2;
            // 
            // lb_Seccion
            // 
            lb_Seccion.AutoSize = true;
            lb_Seccion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Seccion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Seccion.Location = new Point(22, 128);
            lb_Seccion.Name = "lb_Seccion";
            lb_Seccion.Size = new Size(64, 19);
            lb_Seccion.TabIndex = 0;
            lb_Seccion.Text = "Sección:";
            // 
            // cmb_MateriaPeriodo
            // 
            cmb_MateriaPeriodo.BackColor = Color.White;
            cmb_MateriaPeriodo.ForeColor = Color.Black;
            cmb_MateriaPeriodo.FormattingEnabled = true;
            cmb_MateriaPeriodo.Location = new Point(22, 95);
            cmb_MateriaPeriodo.Name = "cmb_MateriaPeriodo";
            cmb_MateriaPeriodo.Size = new Size(380, 23);
            cmb_MateriaPeriodo.TabIndex = 1;
            cmb_MateriaPeriodo.SelectedIndexChanged += cmb_MateriaPeriodo_SelectedIndexChanged;
            // 
            // lb_MateriaPeriodo
            // 
            lb_MateriaPeriodo.AutoSize = true;
            lb_MateriaPeriodo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_MateriaPeriodo.ForeColor = Color.FromArgb(40, 40, 40);
            lb_MateriaPeriodo.Location = new Point(22, 73);
            lb_MateriaPeriodo.Name = "lb_MateriaPeriodo";
            lb_MateriaPeriodo.Size = new Size(123, 19);
            lb_MateriaPeriodo.TabIndex = 0;
            lb_MateriaPeriodo.Text = "Materia Período:";
            // 
            // chk_RequiereRevisionInterna
            // 
            chk_RequiereRevisionInterna.AutoSize = true;
            chk_RequiereRevisionInterna.Font = new Font("Segoe UI", 10F);
            chk_RequiereRevisionInterna.Location = new Point(22, 245);
            chk_RequiereRevisionInterna.Name = "chk_RequiereRevisionInterna";
            chk_RequiereRevisionInterna.Size = new Size(183, 23);
            chk_RequiereRevisionInterna.TabIndex = 11;
            chk_RequiereRevisionInterna.Text = "Requiere Revisión Interna";
            chk_RequiereRevisionInterna.UseVisualStyleBackColor = true;
            // 
            // num_NumeroVersion
            // 
            num_NumeroVersion.Font = new Font("Segoe UI", 9F);
            num_NumeroVersion.Location = new Point(527, 205);
            num_NumeroVersion.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_NumeroVersion.Name = "num_NumeroVersion";
            num_NumeroVersion.Size = new Size(270, 23);
            num_NumeroVersion.TabIndex = 10;
            num_NumeroVersion.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lb_NumeroVersion
            // 
            lb_NumeroVersion.AutoSize = true;
            lb_NumeroVersion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_NumeroVersion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_NumeroVersion.Location = new Point(527, 183);
            lb_NumeroVersion.Name = "lb_NumeroVersion";
            lb_NumeroVersion.Size = new Size(121, 19);
            lb_NumeroVersion.TabIndex = 9;
            lb_NumeroVersion.Text = "Número Versión:";
            // 
            // chk_FechaLimite
            // 
            chk_FechaLimite.AutoSize = true;
            chk_FechaLimite.Font = new Font("Segoe UI", 10F);
            chk_FechaLimite.Location = new Point(527, 155);
            chk_FechaLimite.Name = "chk_FechaLimite";
            chk_FechaLimite.Size = new Size(15, 14);
            chk_FechaLimite.TabIndex = 8;
            chk_FechaLimite.UseVisualStyleBackColor = true;
            chk_FechaLimite.CheckedChanged += chk_FechaLimite_CheckedChanged;
            // 
            // dtp_FechaLimite
            // 
            dtp_FechaLimite.Enabled = false;
            dtp_FechaLimite.Font = new Font("Segoe UI", 9F);
            dtp_FechaLimite.Format = DateTimePickerFormat.Short;
            dtp_FechaLimite.Location = new Point(548, 150);
            dtp_FechaLimite.Name = "dtp_FechaLimite";
            dtp_FechaLimite.Size = new Size(270, 23);
            dtp_FechaLimite.TabIndex = 7;
            // 
            // lb_FechaLimite
            // 
            lb_FechaLimite.AutoSize = true;
            lb_FechaLimite.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaLimite.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaLimite.Location = new Point(527, 128);
            lb_FechaLimite.Name = "lb_FechaLimite";
            lb_FechaLimite.Size = new Size(96, 19);
            lb_FechaLimite.TabIndex = 6;
            lb_FechaLimite.Text = "Fecha Límite:";
            // 
            // dtp_FechaProgramada
            // 
            dtp_FechaProgramada.Font = new Font("Segoe UI", 9F);
            dtp_FechaProgramada.Format = DateTimePickerFormat.Short;
            dtp_FechaProgramada.Location = new Point(527, 45);
            dtp_FechaProgramada.Name = "dtp_FechaProgramada";
            dtp_FechaProgramada.Size = new Size(270, 23);
            dtp_FechaProgramada.TabIndex = 5;
            // 
            // lb_FechaProgramada
            // 
            lb_FechaProgramada.AutoSize = true;
            lb_FechaProgramada.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaProgramada.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaProgramada.Location = new Point(527, 23);
            lb_FechaProgramada.Name = "lb_FechaProgramada";
            lb_FechaProgramada.Size = new Size(140, 19);
            lb_FechaProgramada.TabIndex = 4;
            lb_FechaProgramada.Text = "Fecha Programada:";
            // 
            // cmb_EstadoPublicacion
            // 
            cmb_EstadoPublicacion.BackColor = Color.White;
            cmb_EstadoPublicacion.ForeColor = Color.Black;
            cmb_EstadoPublicacion.FormattingEnabled = true;
            cmb_EstadoPublicacion.Location = new Point(527, 95);
            cmb_EstadoPublicacion.Name = "cmb_EstadoPublicacion";
            cmb_EstadoPublicacion.Size = new Size(270, 23);
            cmb_EstadoPublicacion.TabIndex = 3;
            // 
            // lb_EstadoPublicacion
            // 
            lb_EstadoPublicacion.AutoSize = true;
            lb_EstadoPublicacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_EstadoPublicacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_EstadoPublicacion.Location = new Point(527, 73);
            lb_EstadoPublicacion.Name = "lb_EstadoPublicacion";
            lb_EstadoPublicacion.Size = new Size(138, 19);
            lb_EstadoPublicacion.TabIndex = 2;
            lb_EstadoPublicacion.Text = "Estado Publicación:";
            // 
            // cmb_Estado
            // 
            cmb_Estado.BackColor = Color.White;
            cmb_Estado.ForeColor = Color.Black;
            cmb_Estado.FormattingEnabled = true;
            cmb_Estado.Location = new Point(527, 295);
            cmb_Estado.Name = "cmb_Estado";
            cmb_Estado.Size = new Size(270, 23);
            cmb_Estado.TabIndex = 1;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Estado.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Estado.Location = new Point(527, 273);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(57, 19);
            lb_Estado.TabIndex = 0;
            lb_Estado.Text = "Estado:";
            // 
            // txt_CodigoInstancia
            // 
            txt_CodigoInstancia.BackColor = Color.White;
            txt_CodigoInstancia.BorderColor = Color.FromArgb(180, 180, 180);
            txt_CodigoInstancia.CustomBGColor = Color.White;
            txt_CodigoInstancia.Font = new Font("Segoe UI", 9F);
            txt_CodigoInstancia.ForeColor = Color.Black;
            txt_CodigoInstancia.Location = new Point(22, 45);
            txt_CodigoInstancia.MaxLength = 30;
            txt_CodigoInstancia.Multiline = false;
            txt_CodigoInstancia.Name = "txt_CodigoInstancia";
            txt_CodigoInstancia.ReadOnly = false;
            txt_CodigoInstancia.Size = new Size(380, 26);
            txt_CodigoInstancia.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_CodigoInstancia.TabIndex = 1;
            txt_CodigoInstancia.TextAlignment = HorizontalAlignment.Left;
            txt_CodigoInstancia.UseSystemPasswordChar = false;
            // 
            // lb_CodigoInstancia
            // 
            lb_CodigoInstancia.AutoSize = true;
            lb_CodigoInstancia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_CodigoInstancia.ForeColor = Color.FromArgb(40, 40, 40);
            lb_CodigoInstancia.Location = new Point(22, 23);
            lb_CodigoInstancia.Name = "lb_CodigoInstancia";
            lb_CodigoInstancia.Size = new Size(124, 19);
            lb_CodigoInstancia.TabIndex = 0;
            lb_CodigoInstancia.Text = "Código Instancia:";
            // 
            // btn_GuardarDatos
            // 
            btn_GuardarDatos.BackColor = Color.FromArgb(0, 150, 100);
            btn_GuardarDatos.Cursor = Cursors.Hand;
            btn_GuardarDatos.Dock = DockStyle.Bottom;
            btn_GuardarDatos.FlatAppearance.BorderSize = 0;
            btn_GuardarDatos.FlatStyle = FlatStyle.Flat;
            btn_GuardarDatos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn_GuardarDatos.ForeColor = Color.White;
            btn_GuardarDatos.Location = new Point(30, 425);
            btn_GuardarDatos.Name = "btn_GuardarDatos";
            btn_GuardarDatos.Size = new Size(840, 45);
            btn_GuardarDatos.TabIndex = 17;
            btn_GuardarDatos.Text = "💾 GUARDAR";
            btn_GuardarDatos.UseVisualStyleBackColor = false;
            btn_GuardarDatos.Click += btn_GuardarDatos_Click;
            // 
            // umfMantenimientoEvaluacionInstancia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(900, 570);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(900, 570);
            MinimumSize = new Size(900, 570);
            Name = "umfMantenimientoEvaluacionInstancia";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfMantenimientoEvaluacionInstancia_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            scrollPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_NumeroVersion).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnl_Encabezado;
        private Panel pnl_Contenedor;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private ReaLTaiizor.Controls.CrownLabel lb_CodigoInstancia;
        private ReaLTaiizor.Controls.CrownLabel lb_Estado;
        private ReaLTaiizor.Controls.CrownLabel lb_EstadoPublicacion;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaProgramada;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaLimite;
        private ReaLTaiizor.Controls.CrownLabel lb_NumeroVersion;
        private ReaLTaiizor.Controls.SmallTextBox txt_CodigoInstancia;
        private ComboBox cmb_Estado;
        private ComboBox cmb_EstadoPublicacion;
        private DateTimePicker dtp_FechaProgramada;
        private DateTimePicker dtp_FechaLimite;
        private CheckBox chk_FechaLimite;
        private NumericUpDown num_NumeroVersion;
        private CheckBox chk_RequiereRevisionInterna;
        private Button btn_GuardarDatos;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private Panel scrollPanel;
        private ComboBox cmb_MateriaPeriodo;
        private ReaLTaiizor.Controls.CrownLabel lb_MateriaPeriodo;
        private ComboBox cmb_Seccion;
        private ReaLTaiizor.Controls.CrownLabel lb_Seccion;
        private ComboBox cmb_ModeloEvaluacion;
        private ReaLTaiizor.Controls.CrownLabel lb_ModeloEvaluacion;
    }
}

