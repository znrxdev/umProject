namespace umForms.Flotantes
{
    partial class umfMantenimientoEvaluacionAlumno
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
            cmb_Inscripcion = new ComboBox();
            lb_Inscripcion = new ReaLTaiizor.Controls.CrownLabel();
            cmb_InstanciaEvaluacion = new ComboBox();
            lb_InstanciaEvaluacion = new ReaLTaiizor.Controls.CrownLabel();
            chk_FirmadoPorEstudiante = new CheckBox();
            chk_FechaPublicacion = new CheckBox();
            dtp_FechaPublicacion = new DateTimePicker();
            lb_FechaPublicacion = new ReaLTaiizor.Controls.CrownLabel();
            chk_FechaValidacion = new CheckBox();
            dtp_FechaValidacion = new DateTimePicker();
            lb_FechaValidacion = new ReaLTaiizor.Controls.CrownLabel();
            txt_MotivoAjuste = new ReaLTaiizor.Controls.SmallTextBox();
            lb_MotivoAjuste = new ReaLTaiizor.Controls.CrownLabel();
            num_NumeroRecalculo = new NumericUpDown();
            lb_NumeroRecalculo = new ReaLTaiizor.Controls.CrownLabel();
            chk_EsRecalculo = new CheckBox();
            num_PorcentajeLogrado = new NumericUpDown();
            lb_PorcentajeLogrado = new ReaLTaiizor.Controls.CrownLabel();
            num_PuntajeObtenido = new NumericUpDown();
            lb_PuntajeObtenido = new ReaLTaiizor.Controls.CrownLabel();
            txt_Observaciones = new ReaLTaiizor.Controls.SmallTextBox();
            lb_Observaciones = new ReaLTaiizor.Controls.CrownLabel();
            cmb_EstadoPublicacion = new ComboBox();
            lb_EstadoPublicacion = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Estado = new ComboBox();
            lb_Estado = new ReaLTaiizor.Controls.CrownLabel();
            txt_CodigoRegistro = new ReaLTaiizor.Controls.SmallTextBox();
            lb_CodigoRegistro = new ReaLTaiizor.Controls.CrownLabel();
            btn_GuardarDatos = new Button();
            pnl_Encabezado.SuspendLayout();
            pnl_Contenedor.SuspendLayout();
            scrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_NumeroRecalculo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_PorcentajeLogrado).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_PuntajeObtenido).BeginInit();
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
            lb_Titulo.Size = new Size(270, 25);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "INFORMACIÓN CALIFICACIÓN";
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
            scrollPanel.Controls.Add(cmb_Inscripcion);
            scrollPanel.Controls.Add(lb_Inscripcion);
            scrollPanel.Controls.Add(cmb_InstanciaEvaluacion);
            scrollPanel.Controls.Add(lb_InstanciaEvaluacion);
            scrollPanel.Controls.Add(chk_FirmadoPorEstudiante);
            scrollPanel.Controls.Add(chk_FechaPublicacion);
            scrollPanel.Controls.Add(dtp_FechaPublicacion);
            scrollPanel.Controls.Add(lb_FechaPublicacion);
            scrollPanel.Controls.Add(chk_FechaValidacion);
            scrollPanel.Controls.Add(dtp_FechaValidacion);
            scrollPanel.Controls.Add(lb_FechaValidacion);
            scrollPanel.Controls.Add(txt_MotivoAjuste);
            scrollPanel.Controls.Add(lb_MotivoAjuste);
            scrollPanel.Controls.Add(num_NumeroRecalculo);
            scrollPanel.Controls.Add(lb_NumeroRecalculo);
            scrollPanel.Controls.Add(chk_EsRecalculo);
            scrollPanel.Controls.Add(num_PorcentajeLogrado);
            scrollPanel.Controls.Add(lb_PorcentajeLogrado);
            scrollPanel.Controls.Add(num_PuntajeObtenido);
            scrollPanel.Controls.Add(lb_PuntajeObtenido);
            scrollPanel.Controls.Add(txt_Observaciones);
            scrollPanel.Controls.Add(lb_Observaciones);
            scrollPanel.Controls.Add(cmb_EstadoPublicacion);
            scrollPanel.Controls.Add(lb_EstadoPublicacion);
            scrollPanel.Controls.Add(cmb_Estado);
            scrollPanel.Controls.Add(lb_Estado);
            scrollPanel.Controls.Add(txt_CodigoRegistro);
            scrollPanel.Controls.Add(lb_CodigoRegistro);
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.Location = new Point(30, 30);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new Size(840, 495);
            scrollPanel.TabIndex = 18;
            // 
            // cmb_Inscripcion
            // 
            cmb_Inscripcion.BackColor = Color.White;
            cmb_Inscripcion.ForeColor = Color.Black;
            cmb_Inscripcion.FormattingEnabled = true;
            cmb_Inscripcion.Location = new Point(22, 130);
            cmb_Inscripcion.Name = "cmb_Inscripcion";
            cmb_Inscripcion.Size = new Size(380, 23);
            cmb_Inscripcion.TabIndex = 2;
            // 
            // lb_Inscripcion
            // 
            lb_Inscripcion.AutoSize = true;
            lb_Inscripcion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Inscripcion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Inscripcion.Location = new Point(22, 108);
            lb_Inscripcion.Name = "lb_Inscripcion";
            lb_Inscripcion.Size = new Size(85, 19);
            lb_Inscripcion.TabIndex = 0;
            lb_Inscripcion.Text = "Inscripción:";
            // 
            // cmb_InstanciaEvaluacion
            // 
            cmb_InstanciaEvaluacion.BackColor = Color.White;
            cmb_InstanciaEvaluacion.ForeColor = Color.Black;
            cmb_InstanciaEvaluacion.FormattingEnabled = true;
            cmb_InstanciaEvaluacion.Location = new Point(22, 75);
            cmb_InstanciaEvaluacion.Name = "cmb_InstanciaEvaluacion";
            cmb_InstanciaEvaluacion.Size = new Size(380, 23);
            cmb_InstanciaEvaluacion.TabIndex = 1;
            cmb_InstanciaEvaluacion.SelectedIndexChanged += cmb_InstanciaEvaluacion_SelectedIndexChanged;
            // 
            // lb_InstanciaEvaluacion
            // 
            lb_InstanciaEvaluacion.AutoSize = true;
            lb_InstanciaEvaluacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_InstanciaEvaluacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_InstanciaEvaluacion.Location = new Point(22, 53);
            lb_InstanciaEvaluacion.Name = "lb_InstanciaEvaluacion";
            lb_InstanciaEvaluacion.Size = new Size(146, 19);
            lb_InstanciaEvaluacion.TabIndex = 0;
            lb_InstanciaEvaluacion.Text = "Instancia Evaluación:";
            // 
            // chk_FirmadoPorEstudiante
            // 
            chk_FirmadoPorEstudiante.AutoSize = true;
            chk_FirmadoPorEstudiante.Font = new Font("Segoe UI", 10F);
            chk_FirmadoPorEstudiante.Location = new Point(22, 437);
            chk_FirmadoPorEstudiante.Name = "chk_FirmadoPorEstudiante";
            chk_FirmadoPorEstudiante.Size = new Size(170, 23);
            chk_FirmadoPorEstudiante.TabIndex = 17;
            chk_FirmadoPorEstudiante.Text = "Firmado Por Estudiante";
            chk_FirmadoPorEstudiante.UseVisualStyleBackColor = true;
            // 
            // chk_FechaPublicacion
            // 
            chk_FechaPublicacion.AutoSize = true;
            chk_FechaPublicacion.Font = new Font("Segoe UI", 10F);
            chk_FechaPublicacion.Location = new Point(527, 397);
            chk_FechaPublicacion.Name = "chk_FechaPublicacion";
            chk_FechaPublicacion.Size = new Size(15, 14);
            chk_FechaPublicacion.TabIndex = 16;
            chk_FechaPublicacion.UseVisualStyleBackColor = true;
            chk_FechaPublicacion.CheckedChanged += chk_FechaPublicacion_CheckedChanged;
            // 
            // dtp_FechaPublicacion
            // 
            dtp_FechaPublicacion.Enabled = false;
            dtp_FechaPublicacion.Font = new Font("Segoe UI", 9F);
            dtp_FechaPublicacion.Format = DateTimePickerFormat.Short;
            dtp_FechaPublicacion.Location = new Point(548, 392);
            dtp_FechaPublicacion.Name = "dtp_FechaPublicacion";
            dtp_FechaPublicacion.Size = new Size(270, 23);
            dtp_FechaPublicacion.TabIndex = 15;
            // 
            // lb_FechaPublicacion
            // 
            lb_FechaPublicacion.AutoSize = true;
            lb_FechaPublicacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaPublicacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaPublicacion.Location = new Point(527, 370);
            lb_FechaPublicacion.Name = "lb_FechaPublicacion";
            lb_FechaPublicacion.Size = new Size(132, 19);
            lb_FechaPublicacion.TabIndex = 14;
            lb_FechaPublicacion.Text = "Fecha Publicación:";
            // 
            // chk_FechaValidacion
            // 
            chk_FechaValidacion.AutoSize = true;
            chk_FechaValidacion.Font = new Font("Segoe UI", 10F);
            chk_FechaValidacion.Location = new Point(22, 397);
            chk_FechaValidacion.Name = "chk_FechaValidacion";
            chk_FechaValidacion.Size = new Size(15, 14);
            chk_FechaValidacion.TabIndex = 13;
            chk_FechaValidacion.UseVisualStyleBackColor = true;
            chk_FechaValidacion.CheckedChanged += chk_FechaValidacion_CheckedChanged;
            // 
            // dtp_FechaValidacion
            // 
            dtp_FechaValidacion.Enabled = false;
            dtp_FechaValidacion.Font = new Font("Segoe UI", 9F);
            dtp_FechaValidacion.Format = DateTimePickerFormat.Short;
            dtp_FechaValidacion.Location = new Point(43, 392);
            dtp_FechaValidacion.Name = "dtp_FechaValidacion";
            dtp_FechaValidacion.Size = new Size(270, 23);
            dtp_FechaValidacion.TabIndex = 12;
            // 
            // lb_FechaValidacion
            // 
            lb_FechaValidacion.AutoSize = true;
            lb_FechaValidacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaValidacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaValidacion.Location = new Point(22, 370);
            lb_FechaValidacion.Name = "lb_FechaValidacion";
            lb_FechaValidacion.Size = new Size(124, 19);
            lb_FechaValidacion.TabIndex = 11;
            lb_FechaValidacion.Text = "Fecha Validación:";
            // 
            // txt_MotivoAjuste
            // 
            txt_MotivoAjuste.BackColor = Color.White;
            txt_MotivoAjuste.BorderColor = Color.FromArgb(180, 180, 180);
            txt_MotivoAjuste.CustomBGColor = Color.White;
            txt_MotivoAjuste.Font = new Font("Segoe UI", 9F);
            txt_MotivoAjuste.ForeColor = Color.Black;
            txt_MotivoAjuste.Location = new Point(22, 337);
            txt_MotivoAjuste.MaxLength = 500;
            txt_MotivoAjuste.Multiline = true;
            txt_MotivoAjuste.Name = "txt_MotivoAjuste";
            txt_MotivoAjuste.ReadOnly = false;
            txt_MotivoAjuste.Size = new Size(775, 30);
            txt_MotivoAjuste.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_MotivoAjuste.TabIndex = 10;
            txt_MotivoAjuste.TextAlignment = HorizontalAlignment.Left;
            txt_MotivoAjuste.UseSystemPasswordChar = false;
            // 
            // lb_MotivoAjuste
            // 
            lb_MotivoAjuste.AutoSize = true;
            lb_MotivoAjuste.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_MotivoAjuste.ForeColor = Color.FromArgb(40, 40, 40);
            lb_MotivoAjuste.Location = new Point(22, 315);
            lb_MotivoAjuste.Name = "lb_MotivoAjuste";
            lb_MotivoAjuste.Size = new Size(106, 19);
            lb_MotivoAjuste.TabIndex = 9;
            lb_MotivoAjuste.Text = "Motivo Ajuste:";
            // 
            // num_NumeroRecalculo
            // 
            num_NumeroRecalculo.Font = new Font("Segoe UI", 9F);
            num_NumeroRecalculo.Location = new Point(129, 277);
            num_NumeroRecalculo.Name = "num_NumeroRecalculo";
            num_NumeroRecalculo.Size = new Size(270, 23);
            num_NumeroRecalculo.TabIndex = 8;
            // 
            // lb_NumeroRecalculo
            // 
            lb_NumeroRecalculo.AutoSize = true;
            lb_NumeroRecalculo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_NumeroRecalculo.ForeColor = Color.FromArgb(40, 40, 40);
            lb_NumeroRecalculo.Location = new Point(129, 255);
            lb_NumeroRecalculo.Name = "lb_NumeroRecalculo";
            lb_NumeroRecalculo.Size = new Size(136, 19);
            lb_NumeroRecalculo.TabIndex = 7;
            lb_NumeroRecalculo.Text = "Número Recalculo:";
            // 
            // chk_EsRecalculo
            // 
            chk_EsRecalculo.AutoSize = true;
            chk_EsRecalculo.Font = new Font("Segoe UI", 10F);
            chk_EsRecalculo.Location = new Point(22, 277);
            chk_EsRecalculo.Name = "chk_EsRecalculo";
            chk_EsRecalculo.Size = new Size(101, 23);
            chk_EsRecalculo.TabIndex = 6;
            chk_EsRecalculo.Text = "Es Recalculo";
            chk_EsRecalculo.UseVisualStyleBackColor = true;
            // 
            // num_PorcentajeLogrado
            // 
            num_PorcentajeLogrado.DecimalPlaces = 2;
            num_PorcentajeLogrado.Font = new Font("Segoe UI", 9F);
            num_PorcentajeLogrado.Location = new Point(527, 130);
            num_PorcentajeLogrado.Name = "num_PorcentajeLogrado";
            num_PorcentajeLogrado.Size = new Size(270, 23);
            num_PorcentajeLogrado.TabIndex = 5;
            // 
            // lb_PorcentajeLogrado
            // 
            lb_PorcentajeLogrado.AutoSize = true;
            lb_PorcentajeLogrado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_PorcentajeLogrado.ForeColor = Color.FromArgb(40, 40, 40);
            lb_PorcentajeLogrado.Location = new Point(527, 108);
            lb_PorcentajeLogrado.Name = "lb_PorcentajeLogrado";
            lb_PorcentajeLogrado.Size = new Size(146, 19);
            lb_PorcentajeLogrado.TabIndex = 4;
            lb_PorcentajeLogrado.Text = "Porcentaje Logrado:";
            // 
            // num_PuntajeObtenido
            // 
            num_PuntajeObtenido.DecimalPlaces = 2;
            num_PuntajeObtenido.Font = new Font("Segoe UI", 9F);
            num_PuntajeObtenido.Location = new Point(22, 181);
            num_PuntajeObtenido.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            num_PuntajeObtenido.Name = "num_PuntajeObtenido";
            num_PuntajeObtenido.Size = new Size(270, 23);
            num_PuntajeObtenido.TabIndex = 3;
            // 
            // lb_PuntajeObtenido
            // 
            lb_PuntajeObtenido.AutoSize = true;
            lb_PuntajeObtenido.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_PuntajeObtenido.ForeColor = Color.FromArgb(40, 40, 40);
            lb_PuntajeObtenido.Location = new Point(14, 159);
            lb_PuntajeObtenido.Name = "lb_PuntajeObtenido";
            lb_PuntajeObtenido.Size = new Size(130, 19);
            lb_PuntajeObtenido.TabIndex = 2;
            lb_PuntajeObtenido.Text = "Puntaje Obtenido:";
            // 
            // txt_Observaciones
            // 
            txt_Observaciones.BackColor = Color.White;
            txt_Observaciones.BorderColor = Color.FromArgb(180, 180, 180);
            txt_Observaciones.CustomBGColor = Color.White;
            txt_Observaciones.Font = new Font("Segoe UI", 9F);
            txt_Observaciones.ForeColor = Color.Black;
            txt_Observaciones.Location = new Point(22, 226);
            txt_Observaciones.MaxLength = 255;
            txt_Observaciones.Multiline = false;
            txt_Observaciones.Name = "txt_Observaciones";
            txt_Observaciones.ReadOnly = false;
            txt_Observaciones.Size = new Size(775, 26);
            txt_Observaciones.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_Observaciones.TabIndex = 1;
            txt_Observaciones.TextAlignment = HorizontalAlignment.Left;
            txt_Observaciones.UseSystemPasswordChar = false;
            // 
            // lb_Observaciones
            // 
            lb_Observaciones.AutoSize = true;
            lb_Observaciones.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Observaciones.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Observaciones.Location = new Point(22, 204);
            lb_Observaciones.Name = "lb_Observaciones";
            lb_Observaciones.Size = new Size(112, 19);
            lb_Observaciones.TabIndex = 0;
            lb_Observaciones.Text = "Observaciones:";
            // 
            // cmb_EstadoPublicacion
            // 
            cmb_EstadoPublicacion.BackColor = Color.White;
            cmb_EstadoPublicacion.ForeColor = Color.Black;
            cmb_EstadoPublicacion.FormattingEnabled = true;
            cmb_EstadoPublicacion.Location = new Point(527, 28);
            cmb_EstadoPublicacion.Name = "cmb_EstadoPublicacion";
            cmb_EstadoPublicacion.Size = new Size(270, 23);
            cmb_EstadoPublicacion.TabIndex = 1;
            // 
            // lb_EstadoPublicacion
            // 
            lb_EstadoPublicacion.AutoSize = true;
            lb_EstadoPublicacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_EstadoPublicacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_EstadoPublicacion.Location = new Point(525, 6);
            lb_EstadoPublicacion.Name = "lb_EstadoPublicacion";
            lb_EstadoPublicacion.Size = new Size(138, 19);
            lb_EstadoPublicacion.TabIndex = 0;
            lb_EstadoPublicacion.Text = "Estado Publicación:";
            // 
            // cmb_Estado
            // 
            cmb_Estado.BackColor = Color.White;
            cmb_Estado.ForeColor = Color.Black;
            cmb_Estado.FormattingEnabled = true;
            cmb_Estado.Location = new Point(527, 75);
            cmb_Estado.Name = "cmb_Estado";
            cmb_Estado.Size = new Size(270, 23);
            cmb_Estado.TabIndex = 1;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Estado.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Estado.Location = new Point(527, 53);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(57, 19);
            lb_Estado.TabIndex = 0;
            lb_Estado.Text = "Estado:";
            // 
            // txt_CodigoRegistro
            // 
            txt_CodigoRegistro.BackColor = Color.White;
            txt_CodigoRegistro.BorderColor = Color.FromArgb(180, 180, 180);
            txt_CodigoRegistro.CustomBGColor = Color.White;
            txt_CodigoRegistro.Font = new Font("Segoe UI", 9F);
            txt_CodigoRegistro.ForeColor = Color.Black;
            txt_CodigoRegistro.Location = new Point(22, 25);
            txt_CodigoRegistro.MaxLength = 30;
            txt_CodigoRegistro.Multiline = false;
            txt_CodigoRegistro.Name = "txt_CodigoRegistro";
            txt_CodigoRegistro.ReadOnly = false;
            txt_CodigoRegistro.Size = new Size(380, 26);
            txt_CodigoRegistro.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_CodigoRegistro.TabIndex = 1;
            txt_CodigoRegistro.TextAlignment = HorizontalAlignment.Left;
            txt_CodigoRegistro.UseSystemPasswordChar = false;
            // 
            // lb_CodigoRegistro
            // 
            lb_CodigoRegistro.AutoSize = true;
            lb_CodigoRegistro.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_CodigoRegistro.ForeColor = Color.FromArgb(40, 40, 40);
            lb_CodigoRegistro.Location = new Point(22, 3);
            lb_CodigoRegistro.Name = "lb_CodigoRegistro";
            lb_CodigoRegistro.Size = new Size(122, 19);
            lb_CodigoRegistro.TabIndex = 0;
            lb_CodigoRegistro.Text = "Código Registro:";
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
            // umfMantenimientoEvaluacionAlumno
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
            Name = "umfMantenimientoEvaluacionAlumno";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfMantenimientoEvaluacionAlumno_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            scrollPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_NumeroRecalculo).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_PorcentajeLogrado).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_PuntajeObtenido).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnl_Encabezado;
        private Panel pnl_Contenedor;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private ReaLTaiizor.Controls.CrownLabel lb_CodigoRegistro;
        private ReaLTaiizor.Controls.CrownLabel lb_Estado;
        private ReaLTaiizor.Controls.CrownLabel lb_EstadoPublicacion;
        private ReaLTaiizor.Controls.CrownLabel lb_Observaciones;
        private ReaLTaiizor.Controls.CrownLabel lb_PuntajeObtenido;
        private ReaLTaiizor.Controls.CrownLabel lb_PorcentajeLogrado;
        private ReaLTaiizor.Controls.CrownLabel lb_NumeroRecalculo;
        private ReaLTaiizor.Controls.CrownLabel lb_MotivoAjuste;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaValidacion;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaPublicacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_CodigoRegistro;
        private ComboBox cmb_Estado;
        private ComboBox cmb_EstadoPublicacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_Observaciones;
        private NumericUpDown num_PuntajeObtenido;
        private NumericUpDown num_PorcentajeLogrado;
        private CheckBox chk_EsRecalculo;
        private NumericUpDown num_NumeroRecalculo;
        private ReaLTaiizor.Controls.SmallTextBox txt_MotivoAjuste;
        private DateTimePicker dtp_FechaValidacion;
        private CheckBox chk_FechaValidacion;
        private DateTimePicker dtp_FechaPublicacion;
        private CheckBox chk_FechaPublicacion;
        private CheckBox chk_FirmadoPorEstudiante;
        private Button btn_GuardarDatos;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private Panel scrollPanel;
        private ComboBox cmb_InstanciaEvaluacion;
        private ReaLTaiizor.Controls.CrownLabel lb_InstanciaEvaluacion;
        private ComboBox cmb_Inscripcion;
        private ReaLTaiizor.Controls.CrownLabel lb_Inscripcion;
    }
}

