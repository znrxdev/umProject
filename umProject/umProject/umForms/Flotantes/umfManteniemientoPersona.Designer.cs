namespace umForms.Flotantes
{
    partial class umfManteniemientoPersona
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
            lb_TituloFormulario = new ReaLTaiizor.Controls.CrownLabel();
            pnl_Contenedor = new Panel();
            flp_Campos = new FlowLayoutPanel();
            lb_PrimerNombre = new ReaLTaiizor.Controls.CrownLabel();
            txt_PrimerNombre = new ReaLTaiizor.Controls.SmallTextBox();
            lb_SegundoNombre = new ReaLTaiizor.Controls.CrownLabel();
            txt_SegundoNombre = new ReaLTaiizor.Controls.SmallTextBox();
            lb_PrimerApellido = new ReaLTaiizor.Controls.CrownLabel();
            txt_PrimerApellido = new ReaLTaiizor.Controls.SmallTextBox();
            lb_SegundoApellido = new ReaLTaiizor.Controls.CrownLabel();
            txt_SegundoApellido = new ReaLTaiizor.Controls.SmallTextBox();
            lb_NumeroIdentificacion = new ReaLTaiizor.Controls.CrownLabel();
            txt_NumeroIdentificacion = new ReaLTaiizor.Controls.SmallTextBox();
            lb_TipoIdentificacion = new ReaLTaiizor.Controls.CrownLabel();
            cmb_TipoDocumento = new ComboBox();
            lb_Genero = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Generos = new ComboBox();
            lb_FechaNacimiento = new ReaLTaiizor.Controls.CrownLabel();
            dtm_FechaNacimiento = new ReaLTaiizor.Controls.PoisonDateTime();
            lb_Nacionalidad = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Nacionalidades = new ComboBox();
            lb_EstadoCivil = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Estado_Civil = new ComboBox();
            lb_Estado = new ReaLTaiizor.Controls.CrownLabel();
            cmb_Estado = new ComboBox();
            btn_GuardarDatos = new Button();
            pnl_Encabezado.SuspendLayout();
            pnl_Contenedor.SuspendLayout();
            flp_Campos.SuspendLayout();
            SuspendLayout();
            // 
            // pnl_Encabezado
            // 
            pnl_Encabezado.BackColor = Color.FromArgb(20, 40, 70);
            pnl_Encabezado.Controls.Add(btn_CerrarFormulario);
            pnl_Encabezado.Controls.Add(lb_TituloFormulario);
            pnl_Encabezado.Dock = DockStyle.Top;
            pnl_Encabezado.Location = new Point(0, 0);
            pnl_Encabezado.Margin = new Padding(3, 4, 3, 4);
            pnl_Encabezado.Name = "pnl_Encabezado";
            pnl_Encabezado.Size = new Size(600, 93);
            pnl_Encabezado.TabIndex = 5;
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
            btn_CerrarFormulario.Location = new Point(554, 11);
            btn_CerrarFormulario.Margin = new Padding(3, 4, 3, 4);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(37, 43);
            btn_CerrarFormulario.TabIndex = 6;
            btn_CerrarFormulario.UseVisualStyleBackColor = false;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_TituloFormulario
            // 
            lb_TituloFormulario.AutoSize = true;
            lb_TituloFormulario.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_TituloFormulario.ForeColor = Color.White;
            lb_TituloFormulario.Location = new Point(34, 29);
            lb_TituloFormulario.Name = "lb_TituloFormulario";
            lb_TituloFormulario.Size = new Size(288, 30);
            lb_TituloFormulario.TabIndex = 1;
            lb_TituloFormulario.Text = "INFORMACIÓN PERSONAL";
            // 
            // pnl_Contenedor
            // 
            pnl_Contenedor.BackColor = Color.FromArgb(240, 242, 245);
            pnl_Contenedor.Controls.Add(flp_Campos);
            pnl_Contenedor.Dock = DockStyle.Fill;
            pnl_Contenedor.Location = new Point(0, 93);
            pnl_Contenedor.Margin = new Padding(3, 4, 3, 4);
            pnl_Contenedor.Name = "pnl_Contenedor";
            pnl_Contenedor.Padding = new Padding(23, 27, 23, 27);
            pnl_Contenedor.Size = new Size(600, 968);
            pnl_Contenedor.TabIndex = 6;
            // 
            // flp_Campos
            // 
            flp_Campos.AutoSize = true;
            flp_Campos.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flp_Campos.BackColor = Color.FromArgb(240, 242, 245);
            flp_Campos.Controls.Add(lb_PrimerNombre);
            flp_Campos.Controls.Add(txt_PrimerNombre);
            flp_Campos.Controls.Add(lb_SegundoNombre);
            flp_Campos.Controls.Add(txt_SegundoNombre);
            flp_Campos.Controls.Add(lb_PrimerApellido);
            flp_Campos.Controls.Add(txt_PrimerApellido);
            flp_Campos.Controls.Add(lb_SegundoApellido);
            flp_Campos.Controls.Add(txt_SegundoApellido);
            flp_Campos.Controls.Add(lb_NumeroIdentificacion);
            flp_Campos.Controls.Add(txt_NumeroIdentificacion);
            flp_Campos.Controls.Add(lb_TipoIdentificacion);
            flp_Campos.Controls.Add(cmb_TipoDocumento);
            flp_Campos.Controls.Add(lb_Genero);
            flp_Campos.Controls.Add(cmb_Generos);
            flp_Campos.Controls.Add(lb_FechaNacimiento);
            flp_Campos.Controls.Add(dtm_FechaNacimiento);
            flp_Campos.Controls.Add(lb_Nacionalidad);
            flp_Campos.Controls.Add(cmb_Nacionalidades);
            flp_Campos.Controls.Add(lb_EstadoCivil);
            flp_Campos.Controls.Add(cmb_Estado_Civil);
            flp_Campos.Controls.Add(lb_Estado);
            flp_Campos.Controls.Add(cmb_Estado);
            flp_Campos.Controls.Add(btn_GuardarDatos);
            flp_Campos.Dock = DockStyle.Top;
            flp_Campos.FlowDirection = FlowDirection.TopDown;
            flp_Campos.Location = new Point(23, 27);
            flp_Campos.Margin = new Padding(3, 4, 3, 4);
            flp_Campos.Name = "flp_Campos";
            flp_Campos.Size = new Size(554, 746);
            flp_Campos.TabIndex = 0;
            flp_Campos.WrapContents = false;
            // 
            // lb_PrimerNombre
            // 
            lb_PrimerNombre.AutoSize = true;
            lb_PrimerNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_PrimerNombre.ForeColor = Color.FromArgb(40, 40, 40);
            lb_PrimerNombre.Location = new Point(3, 0);
            lb_PrimerNombre.Name = "lb_PrimerNombre";
            lb_PrimerNombre.Size = new Size(137, 23);
            lb_PrimerNombre.TabIndex = 0;
            lb_PrimerNombre.Text = "Primer nombre:";
            // 
            // txt_PrimerNombre
            // 
            txt_PrimerNombre.BackColor = Color.White;
            txt_PrimerNombre.BorderColor = Color.FromArgb(180, 180, 180);
            txt_PrimerNombre.CustomBGColor = Color.White;
            txt_PrimerNombre.Font = new Font("Segoe UI", 9F);
            txt_PrimerNombre.ForeColor = Color.FromArgb(124, 133, 142);
            txt_PrimerNombre.Location = new Point(3, 28);
            txt_PrimerNombre.Margin = new Padding(3, 5, 3, 5);
            txt_PrimerNombre.MaxLength = 50;
            txt_PrimerNombre.Multiline = false;
            txt_PrimerNombre.Name = "txt_PrimerNombre";
            txt_PrimerNombre.ReadOnly = false;
            txt_PrimerNombre.Size = new Size(554, 30);
            txt_PrimerNombre.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_PrimerNombre.TabIndex = 3;
            txt_PrimerNombre.TextAlignment = HorizontalAlignment.Left;
            txt_PrimerNombre.UseSystemPasswordChar = false;
            // 
            // lb_SegundoNombre
            // 
            lb_SegundoNombre.AutoSize = true;
            lb_SegundoNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_SegundoNombre.ForeColor = Color.FromArgb(40, 40, 40);
            lb_SegundoNombre.Location = new Point(3, 63);
            lb_SegundoNombre.Name = "lb_SegundoNombre";
            lb_SegundoNombre.Size = new Size(154, 23);
            lb_SegundoNombre.TabIndex = 2;
            lb_SegundoNombre.Text = "Segundo nombre:";
            // 
            // txt_SegundoNombre
            // 
            txt_SegundoNombre.BackColor = Color.White;
            txt_SegundoNombre.BorderColor = Color.FromArgb(180, 180, 180);
            txt_SegundoNombre.CustomBGColor = Color.White;
            txt_SegundoNombre.Font = new Font("Segoe UI", 9F);
            txt_SegundoNombre.ForeColor = Color.FromArgb(124, 133, 142);
            txt_SegundoNombre.Location = new Point(3, 91);
            txt_SegundoNombre.Margin = new Padding(3, 5, 3, 5);
            txt_SegundoNombre.MaxLength = 50;
            txt_SegundoNombre.Multiline = false;
            txt_SegundoNombre.Name = "txt_SegundoNombre";
            txt_SegundoNombre.ReadOnly = false;
            txt_SegundoNombre.Size = new Size(554, 30);
            txt_SegundoNombre.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_SegundoNombre.TabIndex = 4;
            txt_SegundoNombre.TextAlignment = HorizontalAlignment.Left;
            txt_SegundoNombre.UseSystemPasswordChar = false;
            // 
            // lb_PrimerApellido
            // 
            lb_PrimerApellido.AutoSize = true;
            lb_PrimerApellido.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_PrimerApellido.ForeColor = Color.FromArgb(40, 40, 40);
            lb_PrimerApellido.Location = new Point(3, 126);
            lb_PrimerApellido.Name = "lb_PrimerApellido";
            lb_PrimerApellido.Size = new Size(139, 23);
            lb_PrimerApellido.TabIndex = 4;
            lb_PrimerApellido.Text = "Primer apellido:";
            // 
            // txt_PrimerApellido
            // 
            txt_PrimerApellido.BackColor = Color.White;
            txt_PrimerApellido.BorderColor = Color.FromArgb(180, 180, 180);
            txt_PrimerApellido.CustomBGColor = Color.White;
            txt_PrimerApellido.Font = new Font("Segoe UI", 9F);
            txt_PrimerApellido.ForeColor = Color.FromArgb(124, 133, 142);
            txt_PrimerApellido.Location = new Point(3, 154);
            txt_PrimerApellido.Margin = new Padding(3, 5, 3, 5);
            txt_PrimerApellido.MaxLength = 50;
            txt_PrimerApellido.Multiline = false;
            txt_PrimerApellido.Name = "txt_PrimerApellido";
            txt_PrimerApellido.ReadOnly = false;
            txt_PrimerApellido.Size = new Size(554, 30);
            txt_PrimerApellido.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_PrimerApellido.TabIndex = 5;
            txt_PrimerApellido.TextAlignment = HorizontalAlignment.Left;
            txt_PrimerApellido.UseSystemPasswordChar = false;
            // 
            // lb_SegundoApellido
            // 
            lb_SegundoApellido.AutoSize = true;
            lb_SegundoApellido.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_SegundoApellido.ForeColor = Color.FromArgb(40, 40, 40);
            lb_SegundoApellido.Location = new Point(3, 189);
            lb_SegundoApellido.Name = "lb_SegundoApellido";
            lb_SegundoApellido.Size = new Size(156, 23);
            lb_SegundoApellido.TabIndex = 6;
            lb_SegundoApellido.Text = "Segundo apellido:";
            // 
            // txt_SegundoApellido
            // 
            txt_SegundoApellido.BackColor = Color.White;
            txt_SegundoApellido.BorderColor = Color.FromArgb(180, 180, 180);
            txt_SegundoApellido.CustomBGColor = Color.White;
            txt_SegundoApellido.Font = new Font("Segoe UI", 9F);
            txt_SegundoApellido.ForeColor = Color.FromArgb(124, 133, 142);
            txt_SegundoApellido.Location = new Point(3, 217);
            txt_SegundoApellido.Margin = new Padding(3, 5, 3, 5);
            txt_SegundoApellido.MaxLength = 50;
            txt_SegundoApellido.Multiline = false;
            txt_SegundoApellido.Name = "txt_SegundoApellido";
            txt_SegundoApellido.ReadOnly = false;
            txt_SegundoApellido.Size = new Size(554, 30);
            txt_SegundoApellido.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_SegundoApellido.TabIndex = 6;
            txt_SegundoApellido.TextAlignment = HorizontalAlignment.Left;
            txt_SegundoApellido.UseSystemPasswordChar = false;
            // 
            // lb_NumeroIdentificacion
            // 
            lb_NumeroIdentificacion.AutoSize = true;
            lb_NumeroIdentificacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_NumeroIdentificacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_NumeroIdentificacion.Location = new Point(3, 252);
            lb_NumeroIdentificacion.Name = "lb_NumeroIdentificacion";
            lb_NumeroIdentificacion.Size = new Size(194, 23);
            lb_NumeroIdentificacion.TabIndex = 8;
            lb_NumeroIdentificacion.Text = "Número identificación:";
            // 
            // txt_NumeroIdentificacion
            // 
            txt_NumeroIdentificacion.BackColor = Color.White;
            txt_NumeroIdentificacion.BorderColor = Color.FromArgb(180, 180, 180);
            txt_NumeroIdentificacion.CustomBGColor = Color.White;
            txt_NumeroIdentificacion.Font = new Font("Segoe UI", 9F);
            txt_NumeroIdentificacion.ForeColor = Color.FromArgb(124, 133, 142);
            txt_NumeroIdentificacion.Location = new Point(3, 280);
            txt_NumeroIdentificacion.Margin = new Padding(3, 5, 3, 5);
            txt_NumeroIdentificacion.MaxLength = 14;
            txt_NumeroIdentificacion.Multiline = false;
            txt_NumeroIdentificacion.Name = "txt_NumeroIdentificacion";
            txt_NumeroIdentificacion.ReadOnly = false;
            txt_NumeroIdentificacion.Size = new Size(554, 30);
            txt_NumeroIdentificacion.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txt_NumeroIdentificacion.TabIndex = 1;
            txt_NumeroIdentificacion.TextAlignment = HorizontalAlignment.Left;
            txt_NumeroIdentificacion.UseSystemPasswordChar = false;
            txt_NumeroIdentificacion.KeyPress += txt_NumeroIdentificacion_KeyPress;
            // 
            // lb_TipoIdentificacion
            // 
            lb_TipoIdentificacion.AutoSize = true;
            lb_TipoIdentificacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_TipoIdentificacion.ForeColor = Color.FromArgb(40, 40, 40);
            lb_TipoIdentificacion.Location = new Point(3, 315);
            lb_TipoIdentificacion.Name = "lb_TipoIdentificacion";
            lb_TipoIdentificacion.Size = new Size(165, 23);
            lb_TipoIdentificacion.TabIndex = 11;
            lb_TipoIdentificacion.Text = "Tipo identificación:";
            // 
            // cmb_TipoDocumento
            // 
            cmb_TipoDocumento.BackColor = Color.White;
            cmb_TipoDocumento.ForeColor = Color.Black;
            cmb_TipoDocumento.FormattingEnabled = true;
            cmb_TipoDocumento.Location = new Point(3, 342);
            cmb_TipoDocumento.Margin = new Padding(3, 4, 3, 4);
            cmb_TipoDocumento.Name = "cmb_TipoDocumento";
            cmb_TipoDocumento.Size = new Size(554, 28);
            cmb_TipoDocumento.TabIndex = 2;
            // 
            // lb_Genero
            // 
            lb_Genero.AutoSize = true;
            lb_Genero.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Genero.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Genero.Location = new Point(3, 374);
            lb_Genero.Name = "lb_Genero";
            lb_Genero.Size = new Size(72, 23);
            lb_Genero.TabIndex = 13;
            lb_Genero.Text = "Género:";
            // 
            // cmb_Generos
            // 
            cmb_Generos.BackColor = Color.White;
            cmb_Generos.ForeColor = Color.Black;
            cmb_Generos.FormattingEnabled = true;
            cmb_Generos.Location = new Point(3, 401);
            cmb_Generos.Margin = new Padding(3, 4, 3, 4);
            cmb_Generos.Name = "cmb_Generos";
            cmb_Generos.Size = new Size(554, 28);
            cmb_Generos.TabIndex = 7;
            // 
            // lb_FechaNacimiento
            // 
            lb_FechaNacimiento.AutoSize = true;
            lb_FechaNacimiento.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_FechaNacimiento.ForeColor = Color.FromArgb(40, 40, 40);
            lb_FechaNacimiento.Location = new Point(3, 433);
            lb_FechaNacimiento.Name = "lb_FechaNacimiento";
            lb_FechaNacimiento.Size = new Size(154, 23);
            lb_FechaNacimiento.TabIndex = 22;
            lb_FechaNacimiento.Text = "Fecha nacimiento:";
            // 
            // dtm_FechaNacimiento
            // 
            dtm_FechaNacimiento.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium;
            dtm_FechaNacimiento.Format = DateTimePickerFormat.Short;
            dtm_FechaNacimiento.Location = new Point(3, 460);
            dtm_FechaNacimiento.Margin = new Padding(3, 4, 3, 4);
            dtm_FechaNacimiento.MaxDate = new DateTime(2025, 11, 14, 0, 0, 0, 0);
            dtm_FechaNacimiento.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dtm_FechaNacimiento.MinimumSize = new Size(0, 30);
            dtm_FechaNacimiento.Name = "dtm_FechaNacimiento";
            dtm_FechaNacimiento.Size = new Size(554, 30);
            dtm_FechaNacimiento.TabIndex = 21;
            dtm_FechaNacimiento.Value = new DateTime(2025, 11, 14, 0, 0, 0, 0);
            // 
            // lb_Nacionalidad
            // 
            lb_Nacionalidad.AutoSize = true;
            lb_Nacionalidad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Nacionalidad.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Nacionalidad.Location = new Point(3, 494);
            lb_Nacionalidad.Name = "lb_Nacionalidad";
            lb_Nacionalidad.Size = new Size(120, 23);
            lb_Nacionalidad.TabIndex = 15;
            lb_Nacionalidad.Text = "Nacionalidad:";
            // 
            // cmb_Nacionalidades
            // 
            cmb_Nacionalidades.BackColor = Color.White;
            cmb_Nacionalidades.ForeColor = Color.Black;
            cmb_Nacionalidades.FormattingEnabled = true;
            cmb_Nacionalidades.Location = new Point(3, 521);
            cmb_Nacionalidades.Margin = new Padding(3, 4, 3, 4);
            cmb_Nacionalidades.Name = "cmb_Nacionalidades";
            cmb_Nacionalidades.Size = new Size(554, 28);
            cmb_Nacionalidades.TabIndex = 8;
            // 
            // lb_EstadoCivil
            // 
            lb_EstadoCivil.AutoSize = true;
            lb_EstadoCivil.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_EstadoCivil.ForeColor = Color.FromArgb(40, 40, 40);
            lb_EstadoCivil.Location = new Point(3, 553);
            lb_EstadoCivil.Name = "lb_EstadoCivil";
            lb_EstadoCivil.Size = new Size(105, 23);
            lb_EstadoCivil.TabIndex = 20;
            lb_EstadoCivil.Text = "Estado civil:";
            // 
            // cmb_Estado_Civil
            // 
            cmb_Estado_Civil.BackColor = Color.White;
            cmb_Estado_Civil.ForeColor = Color.Black;
            cmb_Estado_Civil.FormattingEnabled = true;
            cmb_Estado_Civil.Location = new Point(3, 580);
            cmb_Estado_Civil.Margin = new Padding(3, 4, 3, 4);
            cmb_Estado_Civil.Name = "cmb_Estado_Civil";
            cmb_Estado_Civil.Size = new Size(554, 28);
            cmb_Estado_Civil.TabIndex = 19;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_Estado.ForeColor = Color.FromArgb(40, 40, 40);
            lb_Estado.Location = new Point(3, 612);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(68, 23);
            lb_Estado.TabIndex = 18;
            lb_Estado.Text = "Estado:";
            // 
            // cmb_Estado
            // 
            cmb_Estado.BackColor = Color.White;
            cmb_Estado.ForeColor = Color.Black;
            cmb_Estado.FormattingEnabled = true;
            cmb_Estado.Location = new Point(3, 639);
            cmb_Estado.Margin = new Padding(3, 4, 3, 4);
            cmb_Estado.Name = "cmb_Estado";
            cmb_Estado.Size = new Size(554, 28);
            cmb_Estado.TabIndex = 9;
            // 
            // btn_GuardarDatos
            // 
            btn_GuardarDatos.BackColor = Color.FromArgb(0, 150, 100);
            btn_GuardarDatos.Cursor = Cursors.Hand;
            btn_GuardarDatos.FlatAppearance.BorderSize = 0;
            btn_GuardarDatos.FlatStyle = FlatStyle.Flat;
            btn_GuardarDatos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn_GuardarDatos.ForeColor = Color.White;
            btn_GuardarDatos.Location = new Point(3, 675);
            btn_GuardarDatos.Margin = new Padding(3, 4, 3, 4);
            btn_GuardarDatos.Name = "btn_GuardarDatos";
            btn_GuardarDatos.Size = new Size(554, 67);
            btn_GuardarDatos.TabIndex = 10;
            btn_GuardarDatos.Text = "💾 GUARDAR DATOS";
            btn_GuardarDatos.UseVisualStyleBackColor = false;
            btn_GuardarDatos.Click += btn_GuardarDatos_Click;
            // 
            // umfManteniemientoPersona
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(600, 1061);
            Controls.Add(pnl_Contenedor);
            Controls.Add(pnl_Encabezado);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(600, 1061);
            MinimumSize = new Size(600, 1018);
            Name = "umfManteniemientoPersona";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfManteniemientoPersona_Load;
            pnl_Encabezado.ResumeLayout(false);
            pnl_Encabezado.PerformLayout();
            pnl_Contenedor.ResumeLayout(false);
            pnl_Contenedor.PerformLayout();
            flp_Campos.ResumeLayout(false);
            flp_Campos.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Encabezado;
        private System.Windows.Forms.Panel pnl_Contenedor;
        private System.Windows.Forms.FlowLayoutPanel flp_Campos;
        private ReaLTaiizor.Controls.CrownLabel lb_TituloFormulario;
        private ReaLTaiizor.Controls.CrownLabel lb_PrimerNombre;
        private ReaLTaiizor.Controls.SmallTextBox txt_PrimerNombre;
        private ReaLTaiizor.Controls.CrownLabel lb_SegundoNombre;
        private ReaLTaiizor.Controls.SmallTextBox txt_SegundoNombre;
        private ReaLTaiizor.Controls.CrownLabel lb_PrimerApellido;
        private ReaLTaiizor.Controls.SmallTextBox txt_PrimerApellido;
        private ReaLTaiizor.Controls.CrownLabel lb_SegundoApellido;
        private ReaLTaiizor.Controls.SmallTextBox txt_SegundoApellido;
        private ReaLTaiizor.Controls.CrownLabel lb_NumeroIdentificacion;
        private ReaLTaiizor.Controls.SmallTextBox txt_NumeroIdentificacion;
        private ReaLTaiizor.Controls.CrownLabel lb_TipoIdentificacion;
        private System.Windows.Forms.ComboBox cmb_TipoDocumento;
        private ReaLTaiizor.Controls.CrownLabel lb_Genero;
        private System.Windows.Forms.ComboBox cmb_Generos;
        private ReaLTaiizor.Controls.CrownLabel lb_FechaNacimiento;
        private ReaLTaiizor.Controls.PoisonDateTime dtm_FechaNacimiento;
        private ReaLTaiizor.Controls.CrownLabel lb_Nacionalidad;
        private System.Windows.Forms.ComboBox cmb_Nacionalidades;
        private ReaLTaiizor.Controls.CrownLabel lb_EstadoCivil;
        private System.Windows.Forms.ComboBox cmb_Estado_Civil;
        private ReaLTaiizor.Controls.CrownLabel lb_Estado;
        private System.Windows.Forms.ComboBox cmb_Estado;
        private System.Windows.Forms.Button btn_GuardarDatos;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
    }
}
