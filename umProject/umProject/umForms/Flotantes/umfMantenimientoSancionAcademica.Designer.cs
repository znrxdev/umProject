namespace umForms.Flotantes
{
    partial class umfMantenimientoSancionAcademica
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
            pnl_Encabezado = new Panel();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            lb_Titulo = new ReaLTaiizor.Controls.CrownLabel();
            pnl_Contenedor = new Panel();
            scrollPanel = new Panel();
            txt_DocumentoResolucion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_DocumentoResolucion = new ReaLTaiizor.Controls.CrownLabel();
            txt_ObservacionesApelacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_ObservacionesApelacion = new ReaLTaiizor.Controls.CrownLabel();
            txt_ResultadoApelacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_ResultadoApelacion = new ReaLTaiizor.Controls.CrownLabel();
            chk_FechaApelacion = new CheckBox();
            dtp_FechaApelacion = new DateTimePicker();
            lb_FechaApelacion = new ReaLTaiizor.Controls.CrownLabel();
            chk_EsApelable = new CheckBox();
            txt_Motivo = new ReaLTaiizor.Controls.SmallTextBox();
            lb_Motivo = new ReaLTaiizor.Controls.CrownLabel();
            chk_FechaFin = new CheckBox();
            dtp_FechaFin = new DateTimePicker();
            lb_FechaFin = new ReaLTaiizor.Controls.CrownLabel();
            dtp_FechaRegistro = new DateTimePicker();
            lb_FechaRegistro = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Estado = new ComboBox();
            lb_Estado = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Severidad = new ComboBox();
            lb_Severidad = new ReaLTaiizor.Controls.CrownLabel();
            cmb_TipoFalta = new ComboBox();
            lb_TipoFalta = new ReaLTaiizor.Controls.CrownLabel();
            cmb_TipoSancion = new ComboBox();
            lb_TipoSancion = new ReaLTaiizor.Controls.CrownLabel();
            txt_NumeroDocumento = new ReaLTaiizor.Controls.SmallTextBox();
            lb_NumeroDocumento = new ReaLTaiizor.Controls.CrownLabel();
            lb_NombreEstudiante = new ReaLTaiizor.Controls.CrownLabel();
            txt_CodigoSancion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_CodigoSancion = new ReaLTaiizor.Controls.CrownLabel();
            btn_GuardarDatos = new Button();
            pnl_Encabezado.SuspendLayout();
            pnl_Contenedor.SuspendLayout();
            scrollPanel.SuspendLayout();
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
            pnl_Encabezado.Size = new Size(1050, 70);
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
            btn_CerrarFormulario.Location = new Point(1010, 8);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(32, 32);
            btn_CerrarFormulario.TabIndex = 12;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_Titulo
            // 
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(35, 22);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(341, 25);
            lb_Titulo.TabIndex = 1;
            lb_Titulo.Text = "INFORMACIÓN SANCIÓN ACADÉMICA";
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
            pnl_Contenedor.Size = new Size(1050, 642);
            pnl_Contenedor.TabIndex = 2;
            // 
            // scrollPanel
            // 
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(txt_DocumentoResolucion);
            scrollPanel.Controls.Add(lb_DocumentoResolucion);
            scrollPanel.Controls.Add(txt_ObservacionesApelacion);
            scrollPanel.Controls.Add(lb_ObservacionesApelacion);
            scrollPanel.Controls.Add(txt_ResultadoApelacion);
            scrollPanel.Controls.Add(lb_ResultadoApelacion);
            scrollPanel.Controls.Add(chk_FechaApelacion);
            scrollPanel.Controls.Add(dtp_FechaApelacion);
            scrollPanel.Controls.Add(lb_FechaApelacion);
            scrollPanel.Controls.Add(chk_EsApelable);
            scrollPanel.Controls.Add(txt_Motivo);
            scrollPanel.Controls.Add(lb_Motivo);
            scrollPanel.Controls.Add(chk_FechaFin);
            scrollPanel.Controls.Add(dtp_FechaFin);
            scrollPanel.Controls.Add(lb_FechaFin);
            scrollPanel.Controls.Add(dtp_FechaRegistro);
            scrollPanel.Controls.Add(lb_FechaRegistro);
            scrollPanel.Controls.Add(cmb_Estado);
            scrollPanel.Controls.Add(lb_Estado);
            scrollPanel.Controls.Add(cmb_Severidad);
            scrollPanel.Controls.Add(lb_Severidad);
            scrollPanel.Controls.Add(cmb_TipoFalta);
            scrollPanel.Controls.Add(lb_TipoFalta);
            scrollPanel.Controls.Add(cmb_TipoSancion);
            scrollPanel.Controls.Add(lb_TipoSancion);
            scrollPanel.Controls.Add(txt_NumeroDocumento);
            scrollPanel.Controls.Add(lb_NumeroDocumento);
            scrollPanel.Controls.Add(lb_NombreEstudiante);
            scrollPanel.Controls.Add(txt_CodigoSancion);
            scrollPanel.Controls.Add(lb_CodigoSancion);
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.Location = new Point(30, 30);
            scrollPanel.Margin = new Padding(3, 2, 3, 2);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new Size(990, 537);
            scrollPanel.TabIndex = 18;
            // 
            // txt_DocumentoResolucion
            // 
            txt_DocumentoResolucion.BackColor = Color.White;
            txt_DocumentoResolucion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_DocumentoResolucion.CustomBGColor = Color.White;
            txt_DocumentoResolucion.Font = new Font("Segoe UI", 9F);
            txt_DocumentoResolucion.ForeColor = Color.Black;
            txt_DocumentoResolucion.Location = new Point(22, 500);
            txt_DocumentoResolucion.MaxLength = 255;
            txt_DocumentoResolucion.Multiline = false;
            txt_DocumentoResolucion.Name = "txt_DocumentoResolucion";
            txt_DocumentoResolucion.ReadOnly = false;
            txt_DocumentoResolucion.Size = new Size(939, 26);
            txt_DocumentoResolucion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_DocumentoResolucion.TabIndex = 17;
            txt_DocumentoResolucion.TextAlignment = HorizontalAlignment.Left;
            txt_DocumentoResolucion.UseSystemPasswordChar = false;
            // 
            // lb_DocumentoResolucion
            // 
            lb_DocumentoResolucion.AutoSize = true;
            lb_DocumentoResolucion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_DocumentoResolucion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_DocumentoResolucion.Location = new Point(22, 478);
            lb_DocumentoResolucion.Name = "lb_DocumentoResolucion";
            lb_DocumentoResolucion.Size = new Size(166, 19);
            lb_DocumentoResolucion.TabIndex = 16;
            lb_DocumentoResolucion.Text = "Documento Resolución:";
            // 
            // txt_ObservacionesApelacion
            // 
            txt_ObservacionesApelacion.BackColor = Color.White;
            txt_ObservacionesApelacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_ObservacionesApelacion.CustomBGColor = Color.White;
            txt_ObservacionesApelacion.Font = new Font("Segoe UI", 9F);
            txt_ObservacionesApelacion.ForeColor = Color.Black;
            txt_ObservacionesApelacion.Location = new Point(22, 433);
            txt_ObservacionesApelacion.MaxLength = 500;
            txt_ObservacionesApelacion.Multiline = true;
            txt_ObservacionesApelacion.Name = "txt_ObservacionesApelacion";
            txt_ObservacionesApelacion.ReadOnly = false;
            txt_ObservacionesApelacion.Size = new Size(939, 30);
            txt_ObservacionesApelacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_ObservacionesApelacion.TabIndex = 15;
            txt_ObservacionesApelacion.TextAlignment = HorizontalAlignment.Left;
            txt_ObservacionesApelacion.UseSystemPasswordChar = false;
            // 
            // lb_ObservacionesApelacion
            // 
            lb_ObservacionesApelacion.AutoSize = true;
            lb_ObservacionesApelacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_ObservacionesApelacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_ObservacionesApelacion.Location = new Point(22, 411);
            lb_ObservacionesApelacion.Name = "lb_ObservacionesApelacion";
            lb_ObservacionesApelacion.Size = new Size(183, 19);
            lb_ObservacionesApelacion.TabIndex = 14;
            lb_ObservacionesApelacion.Text = "Observaciones Apelación:";
            // 
            // txt_ResultadoApelacion
            // 
            txt_ResultadoApelacion.BackColor = Color.White;
            txt_ResultadoApelacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_ResultadoApelacion.CustomBGColor = Color.White;
            txt_ResultadoApelacion.Font = new Font("Segoe UI", 9F);
            txt_ResultadoApelacion.ForeColor = Color.Black;
            txt_ResultadoApelacion.Location = new Point(22, 371);
            txt_ResultadoApelacion.MaxLength = 200;
            txt_ResultadoApelacion.Multiline = false;
            txt_ResultadoApelacion.Name = "txt_ResultadoApelacion";
            txt_ResultadoApelacion.ReadOnly = false;
            txt_ResultadoApelacion.Size = new Size(939, 26);
            txt_ResultadoApelacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_ResultadoApelacion.TabIndex = 13;
            txt_ResultadoApelacion.TextAlignment = HorizontalAlignment.Left;
            txt_ResultadoApelacion.UseSystemPasswordChar = false;
            // 
            // lb_ResultadoApelacion
            // 
            lb_ResultadoApelacion.AutoSize = true;
            lb_ResultadoApelacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_ResultadoApelacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_ResultadoApelacion.Location = new Point(22, 349);
            lb_ResultadoApelacion.Name = "lb_ResultadoApelacion";
            lb_ResultadoApelacion.Size = new Size(150, 19);
            lb_ResultadoApelacion.TabIndex = 12;
            lb_ResultadoApelacion.Text = "Resultado Apelación:";
            // 
            // chk_FechaApelacion
            // 
            chk_FechaApelacion.AutoSize = true;
            chk_FechaApelacion.Location = new Point(147, 297);
            chk_FechaApelacion.Margin = new Padding(3, 2, 3, 2);
            chk_FechaApelacion.Name = "chk_FechaApelacion";
            chk_FechaApelacion.Size = new Size(15, 14);
            chk_FechaApelacion.TabIndex = 11;
            chk_FechaApelacion.UseVisualStyleBackColor = true;
            chk_FechaApelacion.CheckedChanged += chk_FechaApelacion_CheckedChanged;
            // 
            // dtp_FechaApelacion
            // 
            dtp_FechaApelacion.Enabled = false;
            dtp_FechaApelacion.Format = DateTimePickerFormat.Short;
            dtp_FechaApelacion.Location = new Point(296, 292);
            dtp_FechaApelacion.Margin = new Padding(3, 2, 3, 2);
            dtp_FechaApelacion.Name = "dtp_FechaApelacion";
            dtp_FechaApelacion.Size = new Size(176, 23);
            dtp_FechaApelacion.TabIndex = 10;
            // 
            // lb_FechaApelacion
            // 
            lb_FechaApelacion.AutoSize = true;
            lb_FechaApelacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaApelacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaApelacion.Location = new Point(168, 294);
            lb_FechaApelacion.Name = "lb_FechaApelacion";
            lb_FechaApelacion.Size = new Size(122, 19);
            lb_FechaApelacion.TabIndex = 9;
            lb_FechaApelacion.Text = "Fecha Apelación:";
            // 
            // chk_EsApelable
            // 
            chk_EsApelable.AutoSize = true;
            chk_EsApelable.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            chk_EsApelable.ForeColor = Color.FromArgb(40, 40, 40);
            chk_EsApelable.Location = new Point(26, 292);
            chk_EsApelable.Margin = new Padding(3, 2, 3, 2);
            chk_EsApelable.Name = "chk_EsApelable";
            chk_EsApelable.Size = new Size(105, 23);
            chk_EsApelable.TabIndex = 8;
            chk_EsApelable.Text = "Es Apelable";
            chk_EsApelable.UseVisualStyleBackColor = true;
            // 
            // txt_Motivo
            // 
            txt_Motivo.BackColor = Color.White;
            txt_Motivo.BorderColor = Color.FromArgb(180, 180, 180);
            txt_Motivo.CustomBGColor = Color.White;
            txt_Motivo.Font = new Font("Segoe UI", 9F);
            txt_Motivo.ForeColor = Color.Black;
            txt_Motivo.Location = new Point(22, 243);
            txt_Motivo.MaxLength = 300;
            txt_Motivo.Multiline = true;
            txt_Motivo.Name = "txt_Motivo";
            txt_Motivo.ReadOnly = false;
            txt_Motivo.Size = new Size(939, 38);
            txt_Motivo.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_Motivo.TabIndex = 7;
            txt_Motivo.TextAlignment = HorizontalAlignment.Left;
            txt_Motivo.UseSystemPasswordChar = false;
            // 
            // lb_Motivo
            // 
            lb_Motivo.AutoSize = true;
            lb_Motivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Motivo.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Motivo.Location = new Point(22, 221);
            lb_Motivo.Name = "lb_Motivo";
            lb_Motivo.Size = new Size(61, 19);
            lb_Motivo.TabIndex = 6;
            lb_Motivo.Text = "Motivo:";
            // 
            // chk_FechaFin
            // 
            chk_FechaFin.AutoSize = true;
            chk_FechaFin.Location = new Point(261, 170);
            chk_FechaFin.Margin = new Padding(3, 2, 3, 2);
            chk_FechaFin.Name = "chk_FechaFin";
            chk_FechaFin.Size = new Size(15, 14);
            chk_FechaFin.TabIndex = 5;
            chk_FechaFin.UseVisualStyleBackColor = true;
            chk_FechaFin.CheckedChanged += chk_FechaFin_CheckedChanged;
            // 
            // dtp_FechaFin
            // 
            dtp_FechaFin.Enabled = false;
            dtp_FechaFin.Format = DateTimePickerFormat.Short;
            dtp_FechaFin.Location = new Point(277, 186);
            dtp_FechaFin.Margin = new Padding(3, 2, 3, 2);
            dtp_FechaFin.Name = "dtp_FechaFin";
            dtp_FechaFin.Size = new Size(176, 23);
            dtp_FechaFin.TabIndex = 4;
            // 
            // lb_FechaFin
            // 
            lb_FechaFin.AutoSize = true;
            lb_FechaFin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaFin.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaFin.Location = new Point(277, 165);
            lb_FechaFin.Name = "lb_FechaFin";
            lb_FechaFin.Size = new Size(74, 19);
            lb_FechaFin.TabIndex = 3;
            lb_FechaFin.Text = "Fecha Fin:";
            // 
            // dtp_FechaRegistro
            // 
            dtp_FechaRegistro.Format = DateTimePickerFormat.Short;
            dtp_FechaRegistro.Location = new Point(22, 186);
            dtp_FechaRegistro.Margin = new Padding(3, 2, 3, 2);
            dtp_FechaRegistro.Name = "dtp_FechaRegistro";
            dtp_FechaRegistro.Size = new Size(176, 23);
            dtp_FechaRegistro.TabIndex = 2;
            // 
            // lb_FechaRegistro
            // 
            lb_FechaRegistro.AutoSize = true;
            lb_FechaRegistro.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaRegistro.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaRegistro.Location = new Point(22, 161);
            lb_FechaRegistro.Name = "lb_FechaRegistro";
            lb_FechaRegistro.Size = new Size(111, 19);
            lb_FechaRegistro.TabIndex = 1;
            lb_FechaRegistro.Text = "Fecha Registro:";
            // 
            // cmb_Estado
            // 
            cmb_Estado.BackColor = Color.White;
            cmb_Estado.ForeColor = Color.Black;
            cmb_Estado.FormattingEnabled = true;
            cmb_Estado.Location = new Point(714, 135);
            cmb_Estado.Name = "cmb_Estado";
            cmb_Estado.Size = new Size(170, 23);
            cmb_Estado.TabIndex = 1;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Estado.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Estado.Location = new Point(714, 110);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(57, 19);
            lb_Estado.TabIndex = 0;
            lb_Estado.Text = "Estado:";
            // 
            // cmb_Severidad
            // 
            cmb_Severidad.BackColor = Color.White;
            cmb_Severidad.ForeColor = Color.Black;
            cmb_Severidad.FormattingEnabled = true;
            cmb_Severidad.Location = new Point(22, 135);
            cmb_Severidad.Name = "cmb_Severidad";
            cmb_Severidad.Size = new Size(482, 23);
            cmb_Severidad.TabIndex = 1;
            // 
            // lb_Severidad
            // 
            lb_Severidad.AutoSize = true;
            lb_Severidad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Severidad.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Severidad.Location = new Point(22, 113);
            lb_Severidad.Name = "lb_Severidad";
            lb_Severidad.Size = new Size(81, 19);
            lb_Severidad.TabIndex = 0;
            lb_Severidad.Text = "Severidad:";
            // 
            // cmb_TipoFalta
            // 
            cmb_TipoFalta.BackColor = Color.White;
            cmb_TipoFalta.ForeColor = Color.Black;
            cmb_TipoFalta.FormattingEnabled = true;
            cmb_TipoFalta.Location = new Point(527, 135);
            cmb_TipoFalta.Name = "cmb_TipoFalta";
            cmb_TipoFalta.Size = new Size(170, 23);
            cmb_TipoFalta.TabIndex = 1;
            // 
            // lb_TipoFalta
            // 
            lb_TipoFalta.AutoSize = true;
            lb_TipoFalta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_TipoFalta.ForeColor = Color.FromArgb(40, 40, 40);
            lb_TipoFalta.Location = new Point(527, 110);
            lb_TipoFalta.Name = "lb_TipoFalta";
            lb_TipoFalta.Size = new Size(79, 19);
            lb_TipoFalta.TabIndex = 0;
            lb_TipoFalta.Text = "Tipo Falta:";
            // 
            // cmb_TipoSancion
            // 
            cmb_TipoSancion.BackColor = Color.White;
            cmb_TipoSancion.ForeColor = Color.Black;
            cmb_TipoSancion.FormattingEnabled = true;
            cmb_TipoSancion.Location = new Point(22, 87);
            cmb_TipoSancion.Name = "cmb_TipoSancion";
            cmb_TipoSancion.Size = new Size(482, 23);
            cmb_TipoSancion.TabIndex = 1;
            // 
            // lb_TipoSancion
            // 
            lb_TipoSancion.AutoSize = true;
            lb_TipoSancion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_TipoSancion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_TipoSancion.Location = new Point(22, 59);
            lb_TipoSancion.Name = "lb_TipoSancion";
            lb_TipoSancion.Size = new Size(99, 19);
            lb_TipoSancion.TabIndex = 0;
            lb_TipoSancion.Text = "Tipo Sanción:";
            // 
            // txt_NumeroDocumento
            // 
            txt_NumeroDocumento.BackColor = Color.White;
            txt_NumeroDocumento.BorderColor = Color.FromArgb(180, 180, 180);
            txt_NumeroDocumento.CustomBGColor = Color.White;
            txt_NumeroDocumento.Font = new Font("Segoe UI", 9F);
            txt_NumeroDocumento.ForeColor = Color.Black;
            txt_NumeroDocumento.Location = new Point(527, 30);
            txt_NumeroDocumento.MaxLength = 50;
            txt_NumeroDocumento.Multiline = false;
            txt_NumeroDocumento.Name = "txt_NumeroDocumento";
            txt_NumeroDocumento.ReadOnly = false;
            txt_NumeroDocumento.Size = new Size(460, 26);
            txt_NumeroDocumento.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_NumeroDocumento.TabIndex = 1;
            txt_NumeroDocumento.TextAlignment = HorizontalAlignment.Left;
            txt_NumeroDocumento.UseSystemPasswordChar = false;
            txt_NumeroDocumento.KeyDown += txt_NumeroDocumento_KeyDown;
            txt_NumeroDocumento.Leave += txt_NumeroDocumento_Leave;
            // 
            // lb_NumeroDocumento
            // 
            lb_NumeroDocumento.AutoSize = true;
            lb_NumeroDocumento.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_NumeroDocumento.ForeColor = Color.FromArgb(40, 40, 40);
            lb_NumeroDocumento.Location = new Point(527, 5);
            lb_NumeroDocumento.Name = "lb_NumeroDocumento";
            lb_NumeroDocumento.Size = new Size(170, 19);
            lb_NumeroDocumento.TabIndex = 0;
            lb_NumeroDocumento.Text = "Número de Documento:";
            // 
            // lb_NombreEstudiante
            // 
            lb_NombreEstudiante.AutoSize = true;
            lb_NombreEstudiante.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lb_NombreEstudiante.ForeColor = Color.FromArgb(0, 100, 200);
            lb_NombreEstudiante.Location = new Point(508, 56);
            lb_NombreEstudiante.Name = "lb_NombreEstudiante";
            lb_NombreEstudiante.Size = new Size(0, 15);
            lb_NombreEstudiante.TabIndex = 0;
            // 
            // txt_CodigoSancion
            // 
            txt_CodigoSancion.BackColor = Color.White;
            txt_CodigoSancion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_CodigoSancion.CustomBGColor = Color.White;
            txt_CodigoSancion.Font = new Font("Segoe UI", 9F);
            txt_CodigoSancion.ForeColor = Color.Black;
            txt_CodigoSancion.Location = new Point(22, 30);
            txt_CodigoSancion.MaxLength = 30;
            txt_CodigoSancion.Multiline = false;
            txt_CodigoSancion.Name = "txt_CodigoSancion";
            txt_CodigoSancion.ReadOnly = false;
            txt_CodigoSancion.Size = new Size(481, 26);
            txt_CodigoSancion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_CodigoSancion.TabIndex = 1;
            txt_CodigoSancion.TextAlignment = HorizontalAlignment.Left;
            txt_CodigoSancion.UseSystemPasswordChar = false;
            // 
            // lb_CodigoSancion
            // 
            lb_CodigoSancion.AutoSize = true;
            lb_CodigoSancion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_CodigoSancion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_CodigoSancion.Location = new Point(22, 5);
            lb_CodigoSancion.Name = "lb_CodigoSancion";
            lb_CodigoSancion.Size = new Size(118, 19);
            lb_CodigoSancion.TabIndex = 0;
            lb_CodigoSancion.Text = "Código Sanción:";
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
            btn_GuardarDatos.Location = new Point(30, 567);
            btn_GuardarDatos.Name = "btn_GuardarDatos";
            btn_GuardarDatos.Size = new Size(990, 45);
            btn_GuardarDatos.TabIndex = 17;
            btn_GuardarDatos.Text = "💾 GUARDAR";
            btn_GuardarDatos.UseVisualStyleBackColor = false;
            btn_GuardarDatos.Click += btn_GuardarDatos_Click;
            // 
            // umfMantenimientoSancionAcademica
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(1050, 712);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(1050, 712);
            MinimumSize = new Size(1050, 712);
            Name = "umfMantenimientoSancionAcademica";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfMantenimientoSancionAcademica_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            scrollPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel pnl_Encabezado;
        private System.Windows.Forms.Panel pnl_Contenedor;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private ReaLTaiizor.Controls.CrownLabel lb_CodigoSancion;
        private ReaLTaiizor.Controls.CrownLabel lb_NumeroDocumento;
        private ReaLTaiizor.Controls.CrownLabel lb_NombreEstudiante;
        private ReaLTaiizor.Controls.CrownLabel lb_TipoSancion;
        private ReaLTaiizor.Controls.CrownLabel lb_TipoFalta;
        private ReaLTaiizor.Controls.CrownLabel lb_Severidad;
        private ReaLTaiizor.Controls.CrownLabel lb_Estado;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaRegistro;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaFin;
        private ReaLTaiizor.Controls.CrownLabel lb_Motivo;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaApelacion;
        private ReaLTaiizor.Controls.CrownLabel lb_ResultadoApelacion;
        private ReaLTaiizor.Controls.CrownLabel lb_ObservacionesApelacion;
        private ReaLTaiizor.Controls.CrownLabel lb_DocumentoResolucion;
        private ReaLTaiizor.Controls.SmallTextBox txt_NumeroDocumento;
        private System.Windows.Forms.ComboBox cmb_TipoSancion;
        private System.Windows.Forms.ComboBox cmb_TipoFalta;
        private System.Windows.Forms.ComboBox cmb_Severidad;
        private System.Windows.Forms.ComboBox cmb_Estado;
        private System.Windows.Forms.DateTimePicker dtp_FechaRegistro;
        private System.Windows.Forms.DateTimePicker dtp_FechaFin;
        private System.Windows.Forms.DateTimePicker dtp_FechaApelacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_CodigoSancion;
        private ReaLTaiizor.Controls.SmallTextBox txt_Motivo;
        private ReaLTaiizor.Controls.SmallTextBox txt_ResultadoApelacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_ObservacionesApelacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_DocumentoResolucion;
        private System.Windows.Forms.CheckBox chk_EsApelable;
        private System.Windows.Forms.CheckBox chk_FechaFin;
        private System.Windows.Forms.CheckBox chk_FechaApelacion;
        private System.Windows.Forms.Button btn_GuardarDatos;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private System.Windows.Forms.Panel scrollPanel;
    }
}

