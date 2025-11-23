namespace umForms.Flotantes
{
    partial class umfDetallesSancion
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
            txt_ResultadoApelacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_ResultadoApelacion = new ReaLTaiizor.Controls.CrownLabel();
            txt_ObservacionesApelacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_ObservacionesApelacion = new ReaLTaiizor.Controls.CrownLabel();
            txt_DocumentoResolucion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_DocumentoResolucion = new ReaLTaiizor.Controls.CrownLabel();
            txt_RevisadoPor = new ReaLTaiizor.Controls.SmallTextBox();
            lb_RevisadoPor = new ReaLTaiizor.Controls.CrownLabel();
            txt_FechaResolucion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_FechaResolucion = new ReaLTaiizor.Controls.CrownLabel();
            txt_FechaApelacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_FechaApelacion = new ReaLTaiizor.Controls.CrownLabel();
            txt_EsApelable = new ReaLTaiizor.Controls.SmallTextBox();
            lb_EsApelable = new ReaLTaiizor.Controls.CrownLabel();
            txt_Motivo = new ReaLTaiizor.Controls.SmallTextBox();
            lb_Motivo = new ReaLTaiizor.Controls.CrownLabel();
            txt_FechaFin = new ReaLTaiizor.Controls.SmallTextBox();
            lb_FechaFin = new ReaLTaiizor.Controls.CrownLabel();
            txt_FechaRegistro = new ReaLTaiizor.Controls.SmallTextBox();
            lb_FechaRegistro = new ReaLTaiizor.Controls.CrownLabel();
            txt_Estado = new ReaLTaiizor.Controls.SmallTextBox();
            lb_Estado = new ReaLTaiizor.Controls.CrownLabel();
            txt_Severidad = new ReaLTaiizor.Controls.SmallTextBox();
            lb_Severidad = new ReaLTaiizor.Controls.CrownLabel();
            txt_TipoFalta = new ReaLTaiizor.Controls.SmallTextBox();
            lb_TipoFalta = new ReaLTaiizor.Controls.CrownLabel();
            txt_TipoSancion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_TipoSancion = new ReaLTaiizor.Controls.CrownLabel();
            txt_CodigoSancion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_CodigoSancion = new ReaLTaiizor.Controls.CrownLabel();
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
            pnl_Encabezado.Size = new Size(800, 70);
            pnl_Encabezado.TabIndex = 0;
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
            btn_CerrarFormulario.Location = new Point(760, 8);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(32, 32);
            btn_CerrarFormulario.TabIndex = 1;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_Titulo
            // 
            lb_Titulo.AutoSize = true;
            lb_Titulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lb_Titulo.ForeColor = Color.White;
            lb_Titulo.Location = new Point(20, 20);
            lb_Titulo.Name = "lb_Titulo";
            lb_Titulo.Size = new Size(200, 28);
            lb_Titulo.TabIndex = 0;
            lb_Titulo.Text = "DETALLES DE SANCIÓN";
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Contenedor.Controls.Add(scrollPanel);
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Location = new Point(0, 70);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Padding = new Padding(20);
            pnl_Contenedor.Size = new Size(800, 630);
            pnl_Contenedor.TabIndex = 1;
            // 
            // scrollPanel
            // 
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(txt_ResultadoApelacion);
            scrollPanel.Controls.Add(lb_ResultadoApelacion);
            scrollPanel.Controls.Add(txt_ObservacionesApelacion);
            scrollPanel.Controls.Add(lb_ObservacionesApelacion);
            scrollPanel.Controls.Add(txt_DocumentoResolucion);
            scrollPanel.Controls.Add(lb_DocumentoResolucion);
            scrollPanel.Controls.Add(txt_RevisadoPor);
            scrollPanel.Controls.Add(lb_RevisadoPor);
            scrollPanel.Controls.Add(txt_FechaResolucion);
            scrollPanel.Controls.Add(lb_FechaResolucion);
            scrollPanel.Controls.Add(txt_FechaApelacion);
            scrollPanel.Controls.Add(lb_FechaApelacion);
            scrollPanel.Controls.Add(txt_EsApelable);
            scrollPanel.Controls.Add(lb_EsApelable);
            scrollPanel.Controls.Add(txt_Motivo);
            scrollPanel.Controls.Add(lb_Motivo);
            scrollPanel.Controls.Add(txt_FechaFin);
            scrollPanel.Controls.Add(lb_FechaFin);
            scrollPanel.Controls.Add(txt_FechaRegistro);
            scrollPanel.Controls.Add(lb_FechaRegistro);
            scrollPanel.Controls.Add(txt_Estado);
            scrollPanel.Controls.Add(lb_Estado);
            scrollPanel.Controls.Add(txt_Severidad);
            scrollPanel.Controls.Add(lb_Severidad);
            scrollPanel.Controls.Add(txt_TipoFalta);
            scrollPanel.Controls.Add(lb_TipoFalta);
            scrollPanel.Controls.Add(txt_TipoSancion);
            scrollPanel.Controls.Add(lb_TipoSancion);
            scrollPanel.Controls.Add(txt_CodigoSancion);
            scrollPanel.Controls.Add(lb_CodigoSancion);
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.Location = new Point(20, 20);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new Size(760, 590);
            scrollPanel.TabIndex = 0;
            // 
            // txt_ResultadoApelacion
            // 
            txt_ResultadoApelacion.BackColor = Color.FromArgb(245, 245, 245);
            txt_ResultadoApelacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_ResultadoApelacion.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_ResultadoApelacion.Font = new Font("Segoe UI", 9F);
            txt_ResultadoApelacion.ForeColor = Color.Black;
            txt_ResultadoApelacion.Location = new Point(20, 900);
            txt_ResultadoApelacion.MaxLength = 200;
            txt_ResultadoApelacion.Multiline = true;
            txt_ResultadoApelacion.Name = "txt_ResultadoApelacion";
            txt_ResultadoApelacion.ReadOnly = true;
            txt_ResultadoApelacion.Size = new Size(720, 60);
            txt_ResultadoApelacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_ResultadoApelacion.TabIndex = 27;
            txt_ResultadoApelacion.Text = "";
            txt_ResultadoApelacion.TextAlignment = HorizontalAlignment.Left;
            txt_ResultadoApelacion.UseSystemPasswordChar = false;
            // 
            // lb_ResultadoApelacion
            // 
            lb_ResultadoApelacion.AutoSize = true;
            lb_ResultadoApelacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_ResultadoApelacion.ForeColor = Color.Black;
            lb_ResultadoApelacion.Location = new Point(20, 875);
            lb_ResultadoApelacion.Name = "lb_ResultadoApelacion";
            lb_ResultadoApelacion.Size = new Size(180, 19);
            lb_ResultadoApelacion.TabIndex = 26;
            lb_ResultadoApelacion.Text = "Resultado de la Apelación:";
            // 
            // txt_ObservacionesApelacion
            // 
            txt_ObservacionesApelacion.BackColor = Color.FromArgb(245, 245, 245);
            txt_ObservacionesApelacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_ObservacionesApelacion.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_ObservacionesApelacion.Font = new Font("Segoe UI", 9F);
            txt_ObservacionesApelacion.ForeColor = Color.Black;
            txt_ObservacionesApelacion.Location = new Point(20, 800);
            txt_ObservacionesApelacion.MaxLength = 500;
            txt_ObservacionesApelacion.Multiline = true;
            txt_ObservacionesApelacion.Name = "txt_ObservacionesApelacion";
            txt_ObservacionesApelacion.ReadOnly = true;
            txt_ObservacionesApelacion.Size = new Size(720, 60);
            txt_ObservacionesApelacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_ObservacionesApelacion.TabIndex = 25;
            txt_ObservacionesApelacion.Text = "";
            txt_ObservacionesApelacion.TextAlignment = HorizontalAlignment.Left;
            txt_ObservacionesApelacion.UseSystemPasswordChar = false;
            // 
            // lb_ObservacionesApelacion
            // 
            lb_ObservacionesApelacion.AutoSize = true;
            lb_ObservacionesApelacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_ObservacionesApelacion.ForeColor = Color.Black;
            lb_ObservacionesApelacion.Location = new Point(20, 775);
            lb_ObservacionesApelacion.Name = "lb_ObservacionesApelacion";
            lb_ObservacionesApelacion.Size = new Size(200, 19);
            lb_ObservacionesApelacion.TabIndex = 24;
            lb_ObservacionesApelacion.Text = "Observaciones de Apelación:";
            // 
            // txt_DocumentoResolucion
            // 
            txt_DocumentoResolucion.BackColor = Color.FromArgb(245, 245, 245);
            txt_DocumentoResolucion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_DocumentoResolucion.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_DocumentoResolucion.Font = new Font("Segoe UI", 9F);
            txt_DocumentoResolucion.ForeColor = Color.Black;
            txt_DocumentoResolucion.Location = new Point(20, 720);
            txt_DocumentoResolucion.MaxLength = 255;
            txt_DocumentoResolucion.Multiline = false;
            txt_DocumentoResolucion.Name = "txt_DocumentoResolucion";
            txt_DocumentoResolucion.ReadOnly = true;
            txt_DocumentoResolucion.Size = new Size(720, 30);
            txt_DocumentoResolucion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_DocumentoResolucion.TabIndex = 23;
            txt_DocumentoResolucion.Text = "";
            txt_DocumentoResolucion.TextAlignment = HorizontalAlignment.Left;
            txt_DocumentoResolucion.UseSystemPasswordChar = false;
            // 
            // lb_DocumentoResolucion
            // 
            lb_DocumentoResolucion.AutoSize = true;
            lb_DocumentoResolucion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_DocumentoResolucion.ForeColor = Color.Black;
            lb_DocumentoResolucion.Location = new Point(20, 695);
            lb_DocumentoResolucion.Name = "lb_DocumentoResolucion";
            lb_DocumentoResolucion.Size = new Size(180, 19);
            lb_DocumentoResolucion.TabIndex = 22;
            lb_DocumentoResolucion.Text = "Documento de Resolución:";
            // 
            // txt_RevisadoPor
            // 
            txt_RevisadoPor.BackColor = Color.FromArgb(245, 245, 245);
            txt_RevisadoPor.BorderColor = Color.FromArgb(180, 180, 180);
            txt_RevisadoPor.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_RevisadoPor.Font = new Font("Segoe UI", 9F);
            txt_RevisadoPor.ForeColor = Color.Black;
            txt_RevisadoPor.Location = new Point(20, 640);
            txt_RevisadoPor.MaxLength = 100;
            txt_RevisadoPor.Multiline = false;
            txt_RevisadoPor.Name = "txt_RevisadoPor";
            txt_RevisadoPor.ReadOnly = true;
            txt_RevisadoPor.Size = new Size(720, 30);
            txt_RevisadoPor.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_RevisadoPor.TabIndex = 21;
            txt_RevisadoPor.Text = "";
            txt_RevisadoPor.TextAlignment = HorizontalAlignment.Left;
            txt_RevisadoPor.UseSystemPasswordChar = false;
            // 
            // lb_RevisadoPor
            // 
            lb_RevisadoPor.AutoSize = true;
            lb_RevisadoPor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_RevisadoPor.ForeColor = Color.Black;
            lb_RevisadoPor.Location = new Point(20, 615);
            lb_RevisadoPor.Name = "lb_RevisadoPor";
            lb_RevisadoPor.Size = new Size(100, 19);
            lb_RevisadoPor.TabIndex = 20;
            lb_RevisadoPor.Text = "Revisado Por:";
            // 
            // txt_FechaResolucion
            // 
            txt_FechaResolucion.BackColor = Color.FromArgb(245, 245, 245);
            txt_FechaResolucion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_FechaResolucion.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_FechaResolucion.Font = new Font("Segoe UI", 9F);
            txt_FechaResolucion.ForeColor = Color.Black;
            txt_FechaResolucion.Location = new Point(20, 560);
            txt_FechaResolucion.MaxLength = 50;
            txt_FechaResolucion.Multiline = false;
            txt_FechaResolucion.Name = "txt_FechaResolucion";
            txt_FechaResolucion.ReadOnly = true;
            txt_FechaResolucion.Size = new Size(720, 30);
            txt_FechaResolucion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_FechaResolucion.TabIndex = 19;
            txt_FechaResolucion.Text = "";
            txt_FechaResolucion.TextAlignment = HorizontalAlignment.Left;
            txt_FechaResolucion.UseSystemPasswordChar = false;
            // 
            // lb_FechaResolucion
            // 
            lb_FechaResolucion.AutoSize = true;
            lb_FechaResolucion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaResolucion.ForeColor = Color.Black;
            lb_FechaResolucion.Location = new Point(20, 535);
            lb_FechaResolucion.Name = "lb_FechaResolucion";
            lb_FechaResolucion.Size = new Size(150, 19);
            lb_FechaResolucion.TabIndex = 18;
            lb_FechaResolucion.Text = "Fecha de Resolución:";
            // 
            // txt_FechaApelacion
            // 
            txt_FechaApelacion.BackColor = Color.FromArgb(245, 245, 245);
            txt_FechaApelacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_FechaApelacion.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_FechaApelacion.Font = new Font("Segoe UI", 9F);
            txt_FechaApelacion.ForeColor = Color.Black;
            txt_FechaApelacion.Location = new Point(20, 480);
            txt_FechaApelacion.MaxLength = 50;
            txt_FechaApelacion.Multiline = false;
            txt_FechaApelacion.Name = "txt_FechaApelacion";
            txt_FechaApelacion.ReadOnly = true;
            txt_FechaApelacion.Size = new Size(720, 30);
            txt_FechaApelacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_FechaApelacion.TabIndex = 17;
            txt_FechaApelacion.Text = "";
            txt_FechaApelacion.TextAlignment = HorizontalAlignment.Left;
            txt_FechaApelacion.UseSystemPasswordChar = false;
            // 
            // lb_FechaApelacion
            // 
            lb_FechaApelacion.AutoSize = true;
            lb_FechaApelacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaApelacion.ForeColor = Color.Black;
            lb_FechaApelacion.Location = new Point(20, 455);
            lb_FechaApelacion.Name = "lb_FechaApelacion";
            lb_FechaApelacion.Size = new Size(140, 19);
            lb_FechaApelacion.TabIndex = 16;
            lb_FechaApelacion.Text = "Fecha de Apelación:";
            // 
            // txt_EsApelable
            // 
            txt_EsApelable.BackColor = Color.FromArgb(245, 245, 245);
            txt_EsApelable.BorderColor = Color.FromArgb(180, 180, 180);
            txt_EsApelable.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_EsApelable.Font = new Font("Segoe UI", 9F);
            txt_EsApelable.ForeColor = Color.Black;
            txt_EsApelable.Location = new Point(20, 400);
            txt_EsApelable.MaxLength = 10;
            txt_EsApelable.Multiline = false;
            txt_EsApelable.Name = "txt_EsApelable";
            txt_EsApelable.ReadOnly = true;
            txt_EsApelable.Size = new Size(720, 30);
            txt_EsApelable.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_EsApelable.TabIndex = 15;
            txt_EsApelable.Text = "";
            txt_EsApelable.TextAlignment = HorizontalAlignment.Left;
            txt_EsApelable.UseSystemPasswordChar = false;
            // 
            // lb_EsApelable
            // 
            lb_EsApelable.AutoSize = true;
            lb_EsApelable.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_EsApelable.ForeColor = Color.Black;
            lb_EsApelable.Location = new Point(20, 375);
            lb_EsApelable.Name = "lb_EsApelable";
            lb_EsApelable.Size = new Size(80, 19);
            lb_EsApelable.TabIndex = 14;
            lb_EsApelable.Text = "Apelable:";
            // 
            // txt_Motivo
            // 
            txt_Motivo.BackColor = Color.FromArgb(245, 245, 245);
            txt_Motivo.BorderColor = Color.FromArgb(180, 180, 180);
            txt_Motivo.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_Motivo.Font = new Font("Segoe UI", 9F);
            txt_Motivo.ForeColor = Color.Black;
            txt_Motivo.Location = new Point(20, 320);
            txt_Motivo.MaxLength = 300;
            txt_Motivo.Multiline = true;
            txt_Motivo.Name = "txt_Motivo";
            txt_Motivo.ReadOnly = true;
            txt_Motivo.Size = new Size(720, 40);
            txt_Motivo.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_Motivo.TabIndex = 13;
            txt_Motivo.Text = "";
            txt_Motivo.TextAlignment = HorizontalAlignment.Left;
            txt_Motivo.UseSystemPasswordChar = false;
            // 
            // lb_Motivo
            // 
            lb_Motivo.AutoSize = true;
            lb_Motivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Motivo.ForeColor = Color.Black;
            lb_Motivo.Location = new Point(20, 295);
            lb_Motivo.Name = "lb_Motivo";
            lb_Motivo.Size = new Size(60, 19);
            lb_Motivo.TabIndex = 12;
            lb_Motivo.Text = "Motivo:";
            // 
            // txt_FechaFin
            // 
            txt_FechaFin.BackColor = Color.FromArgb(245, 245, 245);
            txt_FechaFin.BorderColor = Color.FromArgb(180, 180, 180);
            txt_FechaFin.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_FechaFin.Font = new Font("Segoe UI", 9F);
            txt_FechaFin.ForeColor = Color.Black;
            txt_FechaFin.Location = new Point(20, 240);
            txt_FechaFin.MaxLength = 50;
            txt_FechaFin.Multiline = false;
            txt_FechaFin.Name = "txt_FechaFin";
            txt_FechaFin.ReadOnly = true;
            txt_FechaFin.Size = new Size(720, 30);
            txt_FechaFin.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_FechaFin.TabIndex = 11;
            txt_FechaFin.Text = "";
            txt_FechaFin.TextAlignment = HorizontalAlignment.Left;
            txt_FechaFin.UseSystemPasswordChar = false;
            // 
            // lb_FechaFin
            // 
            lb_FechaFin.AutoSize = true;
            lb_FechaFin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaFin.ForeColor = Color.Black;
            lb_FechaFin.Location = new Point(20, 215);
            lb_FechaFin.Name = "lb_FechaFin";
            lb_FechaFin.Size = new Size(80, 19);
            lb_FechaFin.TabIndex = 10;
            lb_FechaFin.Text = "Fecha Fin:";
            // 
            // txt_FechaRegistro
            // 
            txt_FechaRegistro.BackColor = Color.FromArgb(245, 245, 245);
            txt_FechaRegistro.BorderColor = Color.FromArgb(180, 180, 180);
            txt_FechaRegistro.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_FechaRegistro.Font = new Font("Segoe UI", 9F);
            txt_FechaRegistro.ForeColor = Color.Black;
            txt_FechaRegistro.Location = new Point(20, 160);
            txt_FechaRegistro.MaxLength = 50;
            txt_FechaRegistro.Multiline = false;
            txt_FechaRegistro.Name = "txt_FechaRegistro";
            txt_FechaRegistro.ReadOnly = true;
            txt_FechaRegistro.Size = new Size(720, 30);
            txt_FechaRegistro.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_FechaRegistro.TabIndex = 9;
            txt_FechaRegistro.Text = "";
            txt_FechaRegistro.TextAlignment = HorizontalAlignment.Left;
            txt_FechaRegistro.UseSystemPasswordChar = false;
            // 
            // lb_FechaRegistro
            // 
            lb_FechaRegistro.AutoSize = true;
            lb_FechaRegistro.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaRegistro.ForeColor = Color.Black;
            lb_FechaRegistro.Location = new Point(20, 135);
            lb_FechaRegistro.Name = "lb_FechaRegistro";
            lb_FechaRegistro.Size = new Size(120, 19);
            lb_FechaRegistro.TabIndex = 8;
            lb_FechaRegistro.Text = "Fecha Registro:";
            // 
            // txt_Estado
            // 
            txt_Estado.BackColor = Color.FromArgb(245, 245, 245);
            txt_Estado.BorderColor = Color.FromArgb(180, 180, 180);
            txt_Estado.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_Estado.Font = new Font("Segoe UI", 9F);
            txt_Estado.ForeColor = Color.Black;
            txt_Estado.Location = new Point(400, 80);
            txt_Estado.MaxLength = 100;
            txt_Estado.Multiline = false;
            txt_Estado.Name = "txt_Estado";
            txt_Estado.ReadOnly = true;
            txt_Estado.Size = new Size(340, 30);
            txt_Estado.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_Estado.TabIndex = 7;
            txt_Estado.Text = "";
            txt_Estado.TextAlignment = HorizontalAlignment.Left;
            txt_Estado.UseSystemPasswordChar = false;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Estado.ForeColor = Color.Black;
            lb_Estado.Location = new Point(400, 55);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(60, 19);
            lb_Estado.TabIndex = 6;
            lb_Estado.Text = "Estado:";
            // 
            // txt_Severidad
            // 
            txt_Severidad.BackColor = Color.FromArgb(245, 245, 245);
            txt_Severidad.BorderColor = Color.FromArgb(180, 180, 180);
            txt_Severidad.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_Severidad.Font = new Font("Segoe UI", 9F);
            txt_Severidad.ForeColor = Color.Black;
            txt_Severidad.Location = new Point(20, 80);
            txt_Severidad.MaxLength = 100;
            txt_Severidad.Multiline = false;
            txt_Severidad.Name = "txt_Severidad";
            txt_Severidad.ReadOnly = true;
            txt_Severidad.Size = new Size(340, 30);
            txt_Severidad.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_Severidad.TabIndex = 5;
            txt_Severidad.Text = "";
            txt_Severidad.TextAlignment = HorizontalAlignment.Left;
            txt_Severidad.UseSystemPasswordChar = false;
            // 
            // lb_Severidad
            // 
            lb_Severidad.AutoSize = true;
            lb_Severidad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Severidad.ForeColor = Color.Black;
            lb_Severidad.Location = new Point(20, 55);
            lb_Severidad.Name = "lb_Severidad";
            lb_Severidad.Size = new Size(80, 19);
            lb_Severidad.TabIndex = 4;
            lb_Severidad.Text = "Severidad:";
            // 
            // txt_TipoFalta
            // 
            txt_TipoFalta.BackColor = Color.FromArgb(245, 245, 245);
            txt_TipoFalta.BorderColor = Color.FromArgb(180, 180, 180);
            txt_TipoFalta.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_TipoFalta.Font = new Font("Segoe UI", 9F);
            txt_TipoFalta.ForeColor = Color.Black;
            txt_TipoFalta.Location = new Point(20, 115);
            txt_TipoFalta.MaxLength = 100;
            txt_TipoFalta.Multiline = false;
            txt_TipoFalta.Name = "txt_TipoFalta";
            txt_TipoFalta.ReadOnly = true;
            txt_TipoFalta.Size = new Size(340, 30);
            txt_TipoFalta.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_TipoFalta.TabIndex = 3;
            txt_TipoFalta.Text = "";
            txt_TipoFalta.TextAlignment = HorizontalAlignment.Left;
            txt_TipoFalta.UseSystemPasswordChar = false;
            // 
            // lb_CodigoSancion
            // 
            lb_CodigoSancion.AutoSize = true;
            lb_CodigoSancion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_CodigoSancion.ForeColor = Color.Black;
            lb_CodigoSancion.Location = new Point(20, 15);
            lb_CodigoSancion.Name = "lb_CodigoSancion";
            lb_CodigoSancion.Size = new Size(120, 19);
            lb_CodigoSancion.TabIndex = 29;
            lb_CodigoSancion.Text = "Código Sanción:";
            // 
            // txt_CodigoSancion
            // 
            txt_CodigoSancion.BackColor = Color.FromArgb(245, 245, 245);
            txt_CodigoSancion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_CodigoSancion.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_CodigoSancion.Font = new Font("Segoe UI", 9F);
            txt_CodigoSancion.ForeColor = Color.Black;
            txt_CodigoSancion.Location = new Point(20, 40);
            txt_CodigoSancion.MaxLength = 30;
            txt_CodigoSancion.Multiline = false;
            txt_CodigoSancion.Name = "txt_CodigoSancion";
            txt_CodigoSancion.ReadOnly = true;
            txt_CodigoSancion.Size = new Size(340, 30);
            txt_CodigoSancion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_CodigoSancion.TabIndex = 28;
            txt_CodigoSancion.Text = "";
            txt_CodigoSancion.TextAlignment = HorizontalAlignment.Left;
            txt_CodigoSancion.UseSystemPasswordChar = false;
            // 
            // lb_TipoSancion
            // 
            lb_TipoSancion.AutoSize = true;
            lb_TipoSancion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_TipoSancion.ForeColor = Color.Black;
            lb_TipoSancion.Location = new Point(400, 15);
            lb_TipoSancion.Name = "lb_TipoSancion";
            lb_TipoSancion.Size = new Size(100, 19);
            lb_TipoSancion.TabIndex = 0;
            lb_TipoSancion.Text = "Tipo Sanción:";
            // 
            // txt_TipoSancion
            // 
            txt_TipoSancion.BackColor = Color.FromArgb(245, 245, 245);
            txt_TipoSancion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_TipoSancion.CustomBGColor = Color.FromArgb(245, 245, 245);
            txt_TipoSancion.Font = new Font("Segoe UI", 9F);
            txt_TipoSancion.ForeColor = Color.Black;
            txt_TipoSancion.Location = new Point(400, 40);
            txt_TipoSancion.MaxLength = 100;
            txt_TipoSancion.Multiline = false;
            txt_TipoSancion.Name = "txt_TipoSancion";
            txt_TipoSancion.ReadOnly = true;
            txt_TipoSancion.Size = new Size(340, 30);
            txt_TipoSancion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_TipoSancion.TabIndex = 1;
            txt_TipoSancion.Text = "";
            txt_TipoSancion.TextAlignment = HorizontalAlignment.Left;
            txt_TipoSancion.UseSystemPasswordChar = false;
            // 
            // lb_TipoFalta
            // 
            lb_TipoFalta.AutoSize = true;
            lb_TipoFalta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_TipoFalta.ForeColor = Color.Black;
            lb_TipoFalta.Location = new Point(20, 90);
            lb_TipoFalta.Name = "lb_TipoFalta";
            lb_TipoFalta.Size = new Size(80, 19);
            lb_TipoFalta.TabIndex = 2;
            lb_TipoFalta.Text = "Tipo Falta:";
            // 
            // umfDetallesSancion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(800, 700);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            Name = "umfDetallesSancion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Detalles de Sanción Académica";
            Load += umfDetallesSancion_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            scrollPanel.ResumeLayout(false);
            scrollPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnl_Encabezado;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private ReaLTaiizor.Controls.CrownLabel lb_Titulo;
        private Panel pnl_Contenedor;
        private Panel scrollPanel;
        private ReaLTaiizor.Controls.CrownLabel lb_CodigoSancion;
        private ReaLTaiizor.Controls.SmallTextBox txt_CodigoSancion;
        private ReaLTaiizor.Controls.CrownLabel lb_TipoSancion;
        private ReaLTaiizor.Controls.SmallTextBox txt_TipoSancion;
        private ReaLTaiizor.Controls.CrownLabel lb_TipoFalta;
        private ReaLTaiizor.Controls.SmallTextBox txt_TipoFalta;
        private ReaLTaiizor.Controls.CrownLabel lb_Severidad;
        private ReaLTaiizor.Controls.SmallTextBox txt_Severidad;
        private ReaLTaiizor.Controls.CrownLabel lb_Estado;
        private ReaLTaiizor.Controls.SmallTextBox txt_Estado;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaRegistro;
        private ReaLTaiizor.Controls.SmallTextBox txt_FechaRegistro;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaFin;
        private ReaLTaiizor.Controls.SmallTextBox txt_FechaFin;
        private ReaLTaiizor.Controls.CrownLabel lb_Motivo;
        private ReaLTaiizor.Controls.SmallTextBox txt_Motivo;
        private ReaLTaiizor.Controls.CrownLabel lb_EsApelable;
        private ReaLTaiizor.Controls.SmallTextBox txt_EsApelable;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaApelacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_FechaApelacion;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaResolucion;
        private ReaLTaiizor.Controls.SmallTextBox txt_FechaResolucion;
        private ReaLTaiizor.Controls.CrownLabel lb_RevisadoPor;
        private ReaLTaiizor.Controls.SmallTextBox txt_RevisadoPor;
        private ReaLTaiizor.Controls.CrownLabel lb_DocumentoResolucion;
        private ReaLTaiizor.Controls.SmallTextBox txt_DocumentoResolucion;
        private ReaLTaiizor.Controls.CrownLabel lb_ObservacionesApelacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_ObservacionesApelacion;
        private ReaLTaiizor.Controls.CrownLabel lb_ResultadoApelacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_ResultadoApelacion;
    }
}

