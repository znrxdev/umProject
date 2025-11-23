namespace umForms.Flotantes
{
    partial class umfMantenimientoEvaluacionModelo
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
            chk_Activo = new CheckBox();
            chk_FechaFin = new CheckBox();
            dtp_FechaFin = new DateTimePicker();
            lb_FechaFin = new ReaLTaiizor.Controls.CrownLabel();
            chk_FechaInicio = new CheckBox();
            dtp_FechaInicio = new DateTimePicker();
            lb_FechaInicio = new ReaLTaiizor.Controls.CrownLabel();
            num_PorcentajeMinimoAprobacion = new NumericUpDown();
            lb_PorcentajeMinimoAprobacion = new ReaLTaiizor.Controls.CrownLabel();
            num_VersionConfiguracion = new NumericUpDown();
            lb_VersionConfiguracion = new ReaLTaiizor.Controls.CrownLabel();
            chk_RequiereAprobacion = new CheckBox();
            num_Orden = new NumericUpDown();
            lb_Orden = new ReaLTaiizor.Controls.CrownLabel();
            num_PesoPorcentual = new NumericUpDown();
            lb_PesoPorcentual = new ReaLTaiizor.Controls.CrownLabel();
            num_CalificacionMaxima = new NumericUpDown();
            lb_CalificacionMaxima = new ReaLTaiizor.Controls.CrownLabel();
            txt_Concepto = new ReaLTaiizor.Controls.SmallTextBox();
            lb_Concepto = new ReaLTaiizor.Controls.CrownLabel();
            txt_NombreEvaluacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_NombreEvaluacion = new ReaLTaiizor.Controls.CrownLabel();
            cmb_TipoEvaluacion = new ComboBox();
            lb_TipoEvaluacion = new ReaLTaiizor.Controls.CrownLabel();
            cmb_MateriaPeriodo = new ComboBox();
            lb_MateriaPeriodo = new ReaLTaiizor.Controls.CrownLabel();
            txt_CodigoModelo = new ReaLTaiizor.Controls.SmallTextBox();
            lb_CodigoModelo = new ReaLTaiizor.Controls.CrownLabel();
            btn_GuardarDatos = new Button();
            pnl_Encabezado.SuspendLayout();
            pnl_Contenedor.SuspendLayout();
            scrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_PorcentajeMinimoAprobacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_VersionConfiguracion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_Orden).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_PesoPorcentual).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_CalificacionMaxima).BeginInit();
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
            lb_Titulo.Size = new Size(371, 25);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "INFORMACIÓN MODELO DE EVALUACIÓN";
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
            pnl_Contenedor.Size = new Size(900, 600);
            pnl_Contenedor.TabIndex = 2;
            // 
            // scrollPanel
            // 
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(chk_Activo);
            scrollPanel.Controls.Add(chk_FechaFin);
            scrollPanel.Controls.Add(dtp_FechaFin);
            scrollPanel.Controls.Add(lb_FechaFin);
            scrollPanel.Controls.Add(chk_FechaInicio);
            scrollPanel.Controls.Add(dtp_FechaInicio);
            scrollPanel.Controls.Add(lb_FechaInicio);
            scrollPanel.Controls.Add(num_PorcentajeMinimoAprobacion);
            scrollPanel.Controls.Add(lb_PorcentajeMinimoAprobacion);
            scrollPanel.Controls.Add(num_VersionConfiguracion);
            scrollPanel.Controls.Add(lb_VersionConfiguracion);
            scrollPanel.Controls.Add(chk_RequiereAprobacion);
            scrollPanel.Controls.Add(num_Orden);
            scrollPanel.Controls.Add(lb_Orden);
            scrollPanel.Controls.Add(num_PesoPorcentual);
            scrollPanel.Controls.Add(lb_PesoPorcentual);
            scrollPanel.Controls.Add(num_CalificacionMaxima);
            scrollPanel.Controls.Add(lb_CalificacionMaxima);
            scrollPanel.Controls.Add(txt_Concepto);
            scrollPanel.Controls.Add(lb_Concepto);
            scrollPanel.Controls.Add(txt_NombreEvaluacion);
            scrollPanel.Controls.Add(lb_NombreEvaluacion);
            scrollPanel.Controls.Add(cmb_TipoEvaluacion);
            scrollPanel.Controls.Add(lb_TipoEvaluacion);
            scrollPanel.Controls.Add(cmb_MateriaPeriodo);
            scrollPanel.Controls.Add(lb_MateriaPeriodo);
            scrollPanel.Controls.Add(txt_CodigoModelo);
            scrollPanel.Controls.Add(lb_CodigoModelo);
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.Location = new Point(30, 30);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new Size(840, 495);
            scrollPanel.TabIndex = 18;
            // 
            // chk_Activo
            // 
            chk_Activo.AutoSize = true;
            chk_Activo.Checked = true;
            chk_Activo.CheckState = CheckState.Checked;
            chk_Activo.Font = new Font("Segoe UI", 10F);
            chk_Activo.Location = new Point(22, 380);
            chk_Activo.Name = "chk_Activo";
            chk_Activo.Size = new Size(66, 23);
            chk_Activo.TabIndex = 17;
            chk_Activo.Text = "Activo";
            chk_Activo.UseVisualStyleBackColor = true;
            // 
            // chk_FechaFin
            // 
            chk_FechaFin.AutoSize = true;
            chk_FechaFin.Font = new Font("Segoe UI", 10F);
            chk_FechaFin.Location = new Point(527, 340);
            chk_FechaFin.Name = "chk_FechaFin";
            chk_FechaFin.Size = new Size(15, 14);
            chk_FechaFin.TabIndex = 16;
            chk_FechaFin.UseVisualStyleBackColor = true;
            chk_FechaFin.CheckedChanged += chk_FechaFin_CheckedChanged;
            // 
            // dtp_FechaFin
            // 
            dtp_FechaFin.Enabled = false;
            dtp_FechaFin.Font = new Font("Segoe UI", 9F);
            dtp_FechaFin.Format = DateTimePickerFormat.Short;
            dtp_FechaFin.Location = new Point(548, 335);
            dtp_FechaFin.Name = "dtp_FechaFin";
            dtp_FechaFin.Size = new Size(270, 23);
            dtp_FechaFin.TabIndex = 15;
            // 
            // lb_FechaFin
            // 
            lb_FechaFin.AutoSize = true;
            lb_FechaFin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaFin.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaFin.Location = new Point(527, 313);
            lb_FechaFin.Name = "lb_FechaFin";
            lb_FechaFin.Size = new Size(74, 19);
            lb_FechaFin.TabIndex = 14;
            lb_FechaFin.Text = "Fecha Fin:";
            // 
            // chk_FechaInicio
            // 
            chk_FechaInicio.AutoSize = true;
            chk_FechaInicio.Font = new Font("Segoe UI", 10F);
            chk_FechaInicio.Location = new Point(22, 340);
            chk_FechaInicio.Name = "chk_FechaInicio";
            chk_FechaInicio.Size = new Size(15, 14);
            chk_FechaInicio.TabIndex = 13;
            chk_FechaInicio.UseVisualStyleBackColor = true;
            chk_FechaInicio.CheckedChanged += chk_FechaInicio_CheckedChanged;
            // 
            // dtp_FechaInicio
            // 
            dtp_FechaInicio.Enabled = false;
            dtp_FechaInicio.Font = new Font("Segoe UI", 9F);
            dtp_FechaInicio.Format = DateTimePickerFormat.Short;
            dtp_FechaInicio.Location = new Point(43, 335);
            dtp_FechaInicio.Name = "dtp_FechaInicio";
            dtp_FechaInicio.Size = new Size(270, 23);
            dtp_FechaInicio.TabIndex = 12;
            // 
            // lb_FechaInicio
            // 
            lb_FechaInicio.AutoSize = true;
            lb_FechaInicio.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaInicio.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaInicio.Location = new Point(22, 313);
            lb_FechaInicio.Name = "lb_FechaInicio";
            lb_FechaInicio.Size = new Size(91, 19);
            lb_FechaInicio.TabIndex = 11;
            lb_FechaInicio.Text = "Fecha Inicio:";
            // 
            // num_PorcentajeMinimoAprobacion
            // 
            num_PorcentajeMinimoAprobacion.DecimalPlaces = 2;
            num_PorcentajeMinimoAprobacion.Font = new Font("Segoe UI", 9F);
            num_PorcentajeMinimoAprobacion.Location = new Point(527, 280);
            num_PorcentajeMinimoAprobacion.Name = "num_PorcentajeMinimoAprobacion";
            num_PorcentajeMinimoAprobacion.Size = new Size(270, 23);
            num_PorcentajeMinimoAprobacion.TabIndex = 10;
            // 
            // lb_PorcentajeMinimoAprobacion
            // 
            lb_PorcentajeMinimoAprobacion.AutoSize = true;
            lb_PorcentajeMinimoAprobacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_PorcentajeMinimoAprobacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_PorcentajeMinimoAprobacion.Location = new Point(527, 258);
            lb_PorcentajeMinimoAprobacion.Name = "lb_PorcentajeMinimoAprobacion";
            lb_PorcentajeMinimoAprobacion.Size = new Size(223, 19);
            lb_PorcentajeMinimoAprobacion.TabIndex = 9;
            lb_PorcentajeMinimoAprobacion.Text = "Porcentaje Mínimo Aprobación:";
            // 
            // num_VersionConfiguracion
            // 
            num_VersionConfiguracion.Font = new Font("Segoe UI", 9F);
            num_VersionConfiguracion.Location = new Point(22, 280);
            num_VersionConfiguracion.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_VersionConfiguracion.Name = "num_VersionConfiguracion";
            num_VersionConfiguracion.Size = new Size(270, 23);
            num_VersionConfiguracion.TabIndex = 8;
            num_VersionConfiguracion.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lb_VersionConfiguracion
            // 
            lb_VersionConfiguracion.AutoSize = true;
            lb_VersionConfiguracion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_VersionConfiguracion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_VersionConfiguracion.Location = new Point(22, 258);
            lb_VersionConfiguracion.Name = "lb_VersionConfiguracion";
            lb_VersionConfiguracion.Size = new Size(160, 19);
            lb_VersionConfiguracion.TabIndex = 7;
            lb_VersionConfiguracion.Text = "Versión Configuración:";
            // 
            // chk_RequiereAprobacion
            // 
            chk_RequiereAprobacion.AutoSize = true;
            chk_RequiereAprobacion.Font = new Font("Segoe UI", 10F);
            chk_RequiereAprobacion.Location = new Point(527, 230);
            chk_RequiereAprobacion.Name = "chk_RequiereAprobacion";
            chk_RequiereAprobacion.Size = new Size(155, 23);
            chk_RequiereAprobacion.TabIndex = 6;
            chk_RequiereAprobacion.Text = "Requiere Aprobación";
            chk_RequiereAprobacion.UseVisualStyleBackColor = true;
            // 
            // num_Orden
            // 
            num_Orden.Font = new Font("Segoe UI", 9F);
            num_Orden.Location = new Point(527, 205);
            num_Orden.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_Orden.Name = "num_Orden";
            num_Orden.Size = new Size(270, 23);
            num_Orden.TabIndex = 5;
            num_Orden.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lb_Orden
            // 
            lb_Orden.AutoSize = true;
            lb_Orden.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Orden.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Orden.Location = new Point(527, 183);
            lb_Orden.Name = "lb_Orden";
            lb_Orden.Size = new Size(55, 19);
            lb_Orden.TabIndex = 4;
            lb_Orden.Text = "Orden:";
            // 
            // num_PesoPorcentual
            // 
            num_PesoPorcentual.DecimalPlaces = 2;
            num_PesoPorcentual.Font = new Font("Segoe UI", 9F);
            num_PesoPorcentual.Location = new Point(527, 155);
            num_PesoPorcentual.Name = "num_PesoPorcentual";
            num_PesoPorcentual.Size = new Size(270, 23);
            num_PesoPorcentual.TabIndex = 3;
            // 
            // lb_PesoPorcentual
            // 
            lb_PesoPorcentual.AutoSize = true;
            lb_PesoPorcentual.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_PesoPorcentual.ForeColor = Color.FromArgb(40, 40, 40);
            lb_PesoPorcentual.Location = new Point(527, 133);
            lb_PesoPorcentual.Name = "lb_PesoPorcentual";
            lb_PesoPorcentual.Size = new Size(121, 19);
            lb_PesoPorcentual.TabIndex = 2;
            lb_PesoPorcentual.Text = "Peso Porcentual:";
            // 
            // num_CalificacionMaxima
            // 
            num_CalificacionMaxima.DecimalPlaces = 2;
            num_CalificacionMaxima.Font = new Font("Segoe UI", 9F);
            num_CalificacionMaxima.Location = new Point(22, 155);
            num_CalificacionMaxima.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_CalificacionMaxima.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_CalificacionMaxima.Name = "num_CalificacionMaxima";
            num_CalificacionMaxima.Size = new Size(270, 23);
            num_CalificacionMaxima.TabIndex = 1;
            num_CalificacionMaxima.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // lb_CalificacionMaxima
            // 
            lb_CalificacionMaxima.AutoSize = true;
            lb_CalificacionMaxima.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_CalificacionMaxima.ForeColor = Color.FromArgb(40, 40, 40);
            lb_CalificacionMaxima.Location = new Point(22, 133);
            lb_CalificacionMaxima.Name = "lb_CalificacionMaxima";
            lb_CalificacionMaxima.Size = new Size(148, 19);
            lb_CalificacionMaxima.TabIndex = 0;
            lb_CalificacionMaxima.Text = "Calificación Máxima:";
            // 
            // txt_Concepto
            // 
            txt_Concepto.BackColor = Color.White;
            txt_Concepto.BorderColor = Color.FromArgb(180, 180, 180);
            txt_Concepto.CustomBGColor = Color.White;
            txt_Concepto.Font = new Font("Segoe UI", 9F);
            txt_Concepto.ForeColor = Color.Black;
            txt_Concepto.Location = new Point(22, 105);
            txt_Concepto.MaxLength = 255;
            txt_Concepto.Multiline = false;
            txt_Concepto.Name = "txt_Concepto";
            txt_Concepto.ReadOnly = false;
            txt_Concepto.Size = new Size(775, 26);
            txt_Concepto.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_Concepto.TabIndex = 1;
            txt_Concepto.TextAlignment = HorizontalAlignment.Left;
            txt_Concepto.UseSystemPasswordChar = false;
            // 
            // lb_Concepto
            // 
            lb_Concepto.AutoSize = true;
            lb_Concepto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Concepto.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Concepto.Location = new Point(22, 83);
            lb_Concepto.Name = "lb_Concepto";
            lb_Concepto.Size = new Size(77, 19);
            lb_Concepto.TabIndex = 0;
            lb_Concepto.Text = "Concepto:";
            // 
            // txt_NombreEvaluacion
            // 
            txt_NombreEvaluacion.BackColor = Color.White;
            txt_NombreEvaluacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_NombreEvaluacion.CustomBGColor = Color.White;
            txt_NombreEvaluacion.Font = new Font("Segoe UI", 9F);
            txt_NombreEvaluacion.ForeColor = Color.Black;
            txt_NombreEvaluacion.Location = new Point(527, 52);
            txt_NombreEvaluacion.MaxLength = 100;
            txt_NombreEvaluacion.Multiline = false;
            txt_NombreEvaluacion.Name = "txt_NombreEvaluacion";
            txt_NombreEvaluacion.ReadOnly = false;
            txt_NombreEvaluacion.Size = new Size(270, 26);
            txt_NombreEvaluacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_NombreEvaluacion.TabIndex = 1;
            txt_NombreEvaluacion.TextAlignment = HorizontalAlignment.Left;
            txt_NombreEvaluacion.UseSystemPasswordChar = false;
            // 
            // lb_NombreEvaluacion
            // 
            lb_NombreEvaluacion.AutoSize = true;
            lb_NombreEvaluacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_NombreEvaluacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_NombreEvaluacion.Location = new Point(527, 30);
            lb_NombreEvaluacion.Name = "lb_NombreEvaluacion";
            lb_NombreEvaluacion.Size = new Size(144, 19);
            lb_NombreEvaluacion.TabIndex = 0;
            lb_NombreEvaluacion.Text = "Nombre Evaluación:";
            // 
            // cmb_TipoEvaluacion
            // 
            cmb_TipoEvaluacion.BackColor = Color.White;
            cmb_TipoEvaluacion.ForeColor = Color.Black;
            cmb_TipoEvaluacion.FormattingEnabled = true;
            cmb_TipoEvaluacion.Location = new Point(22, 55);
            cmb_TipoEvaluacion.Name = "cmb_TipoEvaluacion";
            cmb_TipoEvaluacion.Size = new Size(270, 23);
            cmb_TipoEvaluacion.TabIndex = 1;
            // 
            // lb_TipoEvaluacion
            // 
            lb_TipoEvaluacion.AutoSize = true;
            lb_TipoEvaluacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_TipoEvaluacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_TipoEvaluacion.Location = new Point(22, 33);
            lb_TipoEvaluacion.Name = "lb_TipoEvaluacion";
            lb_TipoEvaluacion.Size = new Size(118, 19);
            lb_TipoEvaluacion.TabIndex = 0;
            lb_TipoEvaluacion.Text = "Tipo Evaluación:";
            // 
            // cmb_MateriaPeriodo
            // 
            cmb_MateriaPeriodo.BackColor = Color.White;
            cmb_MateriaPeriodo.ForeColor = Color.Black;
            cmb_MateriaPeriodo.FormattingEnabled = true;
            cmb_MateriaPeriodo.Location = new Point(22, 230);
            cmb_MateriaPeriodo.Name = "cmb_MateriaPeriodo";
            cmb_MateriaPeriodo.Size = new Size(270, 23);
            cmb_MateriaPeriodo.TabIndex = 1;
            // 
            // lb_MateriaPeriodo
            // 
            lb_MateriaPeriodo.AutoSize = true;
            lb_MateriaPeriodo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_MateriaPeriodo.ForeColor = Color.FromArgb(40, 40, 40);
            lb_MateriaPeriodo.Location = new Point(22, 205);
            lb_MateriaPeriodo.Name = "lb_MateriaPeriodo";
            lb_MateriaPeriodo.Size = new Size(123, 19);
            lb_MateriaPeriodo.TabIndex = 0;
            lb_MateriaPeriodo.Text = "Materia Período:";
            // 
            // txt_CodigoModelo
            // 
            txt_CodigoModelo.BackColor = Color.White;
            txt_CodigoModelo.BorderColor = Color.FromArgb(180, 180, 180);
            txt_CodigoModelo.CustomBGColor = Color.White;
            txt_CodigoModelo.Font = new Font("Segoe UI", 9F);
            txt_CodigoModelo.ForeColor = Color.Black;
            txt_CodigoModelo.Location = new Point(298, 52);
            txt_CodigoModelo.MaxLength = 30;
            txt_CodigoModelo.Multiline = false;
            txt_CodigoModelo.Name = "txt_CodigoModelo";
            txt_CodigoModelo.ReadOnly = false;
            txt_CodigoModelo.Size = new Size(223, 26);
            txt_CodigoModelo.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_CodigoModelo.TabIndex = 1;
            txt_CodigoModelo.TextAlignment = HorizontalAlignment.Left;
            txt_CodigoModelo.UseSystemPasswordChar = false;
            // 
            // lb_CodigoModelo
            // 
            lb_CodigoModelo.AutoSize = true;
            lb_CodigoModelo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_CodigoModelo.ForeColor = Color.FromArgb(40, 40, 40);
            lb_CodigoModelo.Location = new Point(298, 30);
            lb_CodigoModelo.Name = "lb_CodigoModelo";
            lb_CodigoModelo.Size = new Size(118, 19);
            lb_CodigoModelo.TabIndex = 0;
            lb_CodigoModelo.Text = "Código Modelo:";
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
            btn_GuardarDatos.Location = new Point(30, 525);
            btn_GuardarDatos.Name = "btn_GuardarDatos";
            btn_GuardarDatos.Size = new Size(840, 45);
            btn_GuardarDatos.TabIndex = 17;
            btn_GuardarDatos.Text = "💾 GUARDAR";
            btn_GuardarDatos.UseVisualStyleBackColor = false;
            btn_GuardarDatos.Click += btn_GuardarDatos_Click;
            // 
            // umfMantenimientoEvaluacionModelo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(900, 670);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(900, 670);
            MinimumSize = new Size(900, 670);
            Name = "umfMantenimientoEvaluacionModelo";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfMantenimientoEvaluacionModelo_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            scrollPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_PorcentajeMinimoAprobacion).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_VersionConfiguracion).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_Orden).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_PesoPorcentual).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_CalificacionMaxima).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnl_Encabezado;
        private Panel pnl_Contenedor;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private ReaLTaiizor.Controls.CrownLabel lb_CodigoModelo;
        private ReaLTaiizor.Controls.CrownLabel lb_TipoEvaluacion;
        private ReaLTaiizor.Controls.CrownLabel lb_NombreEvaluacion;
        private ReaLTaiizor.Controls.CrownLabel lb_Concepto;
        private ReaLTaiizor.Controls.CrownLabel lb_CalificacionMaxima;
        private ReaLTaiizor.Controls.CrownLabel lb_PesoPorcentual;
        private ReaLTaiizor.Controls.CrownLabel lb_Orden;
        private ReaLTaiizor.Controls.CrownLabel lb_VersionConfiguracion;
        private ReaLTaiizor.Controls.CrownLabel lb_PorcentajeMinimoAprobacion;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaInicio;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaFin;
        private ReaLTaiizor.Controls.SmallTextBox txt_CodigoModelo;
        private ComboBox cmb_TipoEvaluacion;
        private ComboBox cmb_MateriaPeriodo;
        private ReaLTaiizor.Controls.CrownLabel lb_MateriaPeriodo;
        private ReaLTaiizor.Controls.SmallTextBox txt_NombreEvaluacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_Concepto;
        private NumericUpDown num_CalificacionMaxima;
        private NumericUpDown num_PesoPorcentual;
        private NumericUpDown num_Orden;
        private CheckBox chk_RequiereAprobacion;
        private NumericUpDown num_VersionConfiguracion;
        private NumericUpDown num_PorcentajeMinimoAprobacion;
        private DateTimePicker dtp_FechaInicio;
        private CheckBox chk_FechaInicio;
        private DateTimePicker dtp_FechaFin;
        private CheckBox chk_FechaFin;
        private CheckBox chk_Activo;
        private Button btn_GuardarDatos;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private Panel scrollPanel;
    }
}

