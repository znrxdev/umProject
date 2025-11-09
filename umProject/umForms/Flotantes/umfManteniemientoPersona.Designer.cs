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
            gbx_Datos_Personales = new GroupBox();
            lb_FechaNacimiento = new Label();
            dtm_FechaNacimiento = new ReaLTaiizor.Controls.PoisonDateTime();
            lb_EstadoCivil = new Label();
            cmb_Estado_Civil = new ComboBox();
            lb_Estado = new Label();
            cmb_Estado = new ComboBox();
            btn_GuardarDatos = new Button();
            lb_Nacionalidad = new Label();
            cmb_Nacionalidades = new ComboBox();
            lb_Genero = new Label();
            cmb_Generos = new ComboBox();
            lb_TipoIdentificacion = new Label();
            cmb_TipoDocumento = new ComboBox();
            txt_NumeroIdentificacion = new TextBox();
            lb_NumeroIdentificacion = new Label();
            txt_SegundoApellido = new TextBox();
            lb_SegundoApellido = new Label();
            txt_PrimerApellido = new TextBox();
            lb_PrimerApellido = new Label();
            txt_SegundoNombre = new TextBox();
            lb_SegundoNombre = new Label();
            txt_PrimerNombre = new TextBox();
            lb_PrimerNombre = new Label();
            pnl_Arriba = new Panel();
            btn_CerrarFormulario = new FontAwesome.Sharp.IconButton();
            lb_TituloFormulario = new Label();
            gbx_Datos_Personales.SuspendLayout();
            pnl_Arriba.SuspendLayout();
            SuspendLayout();
            // 
            // gbx_Datos_Personales
            // 
            gbx_Datos_Personales.Controls.Add(lb_FechaNacimiento);
            gbx_Datos_Personales.Controls.Add(dtm_FechaNacimiento);
            gbx_Datos_Personales.Controls.Add(lb_EstadoCivil);
            gbx_Datos_Personales.Controls.Add(cmb_Estado_Civil);
            gbx_Datos_Personales.Controls.Add(lb_Estado);
            gbx_Datos_Personales.Controls.Add(cmb_Estado);
            gbx_Datos_Personales.Controls.Add(btn_GuardarDatos);
            gbx_Datos_Personales.Controls.Add(lb_Nacionalidad);
            gbx_Datos_Personales.Controls.Add(cmb_Nacionalidades);
            gbx_Datos_Personales.Controls.Add(lb_Genero);
            gbx_Datos_Personales.Controls.Add(cmb_Generos);
            gbx_Datos_Personales.Controls.Add(lb_TipoIdentificacion);
            gbx_Datos_Personales.Controls.Add(cmb_TipoDocumento);
            gbx_Datos_Personales.Controls.Add(txt_NumeroIdentificacion);
            gbx_Datos_Personales.Controls.Add(lb_NumeroIdentificacion);
            gbx_Datos_Personales.Controls.Add(txt_SegundoApellido);
            gbx_Datos_Personales.Controls.Add(lb_SegundoApellido);
            gbx_Datos_Personales.Controls.Add(txt_PrimerApellido);
            gbx_Datos_Personales.Controls.Add(lb_PrimerApellido);
            gbx_Datos_Personales.Controls.Add(txt_SegundoNombre);
            gbx_Datos_Personales.Controls.Add(lb_SegundoNombre);
            gbx_Datos_Personales.Controls.Add(txt_PrimerNombre);
            gbx_Datos_Personales.Controls.Add(lb_PrimerNombre);
            gbx_Datos_Personales.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gbx_Datos_Personales.Location = new Point(66, 88);
            gbx_Datos_Personales.Margin = new Padding(3, 2, 3, 2);
            gbx_Datos_Personales.Name = "gbx_Datos_Personales";
            gbx_Datos_Personales.Padding = new Padding(3, 2, 3, 2);
            gbx_Datos_Personales.Size = new Size(402, 697);
            gbx_Datos_Personales.TabIndex = 4;
            gbx_Datos_Personales.TabStop = false;
            gbx_Datos_Personales.Text = "Datos Personales:";
            // 
            // lb_FechaNacimiento
            // 
            lb_FechaNacimiento.AutoSize = true;
            lb_FechaNacimiento.Location = new Point(5, 373);
            lb_FechaNacimiento.Name = "lb_FechaNacimiento";
            lb_FechaNacimiento.Size = new Size(120, 16);
            lb_FechaNacimiento.TabIndex = 22;
            lb_FechaNacimiento.Text = "Fecha Nacimiento";
            // 
            // dtm_FechaNacimiento
            // 
            dtm_FechaNacimiento.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium;
            dtm_FechaNacimiento.Format = DateTimePickerFormat.Short;
            dtm_FechaNacimiento.Location = new Point(10, 395);
            dtm_FechaNacimiento.MinimumSize = new Size(0, 29);
            dtm_FechaNacimiento.Name = "dtm_FechaNacimiento";
            dtm_FechaNacimiento.Size = new Size(376, 29);
            dtm_FechaNacimiento.TabIndex = 21;
            // 
            // lb_EstadoCivil
            // 
            lb_EstadoCivil.AutoSize = true;
            lb_EstadoCivil.Location = new Point(4, 549);
            lb_EstadoCivil.Name = "lb_EstadoCivil";
            lb_EstadoCivil.Size = new Size(85, 16);
            lb_EstadoCivil.TabIndex = 20;
            lb_EstadoCivil.Text = "Estado Civil:";
            // 
            // cmb_Estado_Civil
            // 
            cmb_Estado_Civil.FormattingEnabled = true;
            cmb_Estado_Civil.Location = new Point(10, 572);
            cmb_Estado_Civil.Margin = new Padding(3, 2, 3, 2);
            cmb_Estado_Civil.Name = "cmb_Estado_Civil";
            cmb_Estado_Civil.Size = new Size(376, 24);
            cmb_Estado_Civil.TabIndex = 19;
            // 
            // lb_Estado
            // 
            lb_Estado.AutoSize = true;
            lb_Estado.Location = new Point(4, 606);
            lb_Estado.Name = "lb_Estado";
            lb_Estado.Size = new Size(55, 16);
            lb_Estado.TabIndex = 18;
            lb_Estado.Text = "Estado:";
            // 
            // cmb_Estado
            // 
            cmb_Estado.FormattingEnabled = true;
            cmb_Estado.Location = new Point(10, 628);
            cmb_Estado.Margin = new Padding(3, 2, 3, 2);
            cmb_Estado.Name = "cmb_Estado";
            cmb_Estado.Size = new Size(376, 24);
            cmb_Estado.TabIndex = 9;
            // 
            // btn_GuardarDatos
            // 
            btn_GuardarDatos.BackColor = Color.FromArgb(9, 46, 85);
            btn_GuardarDatos.FlatStyle = FlatStyle.Flat;
            btn_GuardarDatos.ForeColor = Color.White;
            btn_GuardarDatos.Location = new Point(124, 656);
            btn_GuardarDatos.Margin = new Padding(3, 2, 3, 2);
            btn_GuardarDatos.Name = "btn_GuardarDatos";
            btn_GuardarDatos.Size = new Size(147, 37);
            btn_GuardarDatos.TabIndex = 10;
            btn_GuardarDatos.Text = "GUARDAR";
            btn_GuardarDatos.UseVisualStyleBackColor = false;
            btn_GuardarDatos.Click += btn_GuardarDatos_Click;
            // 
            // lb_Nacionalidad
            // 
            lb_Nacionalidad.AutoSize = true;
            lb_Nacionalidad.Location = new Point(4, 489);
            lb_Nacionalidad.Name = "lb_Nacionalidad";
            lb_Nacionalidad.Size = new Size(92, 16);
            lb_Nacionalidad.TabIndex = 15;
            lb_Nacionalidad.Text = "Nacionalidad:";
            // 
            // cmb_Nacionalidades
            // 
            cmb_Nacionalidades.FormattingEnabled = true;
            cmb_Nacionalidades.Location = new Point(10, 512);
            cmb_Nacionalidades.Margin = new Padding(3, 2, 3, 2);
            cmb_Nacionalidades.Name = "cmb_Nacionalidades";
            cmb_Nacionalidades.Size = new Size(376, 24);
            cmb_Nacionalidades.TabIndex = 8;
            // 
            // lb_Genero
            // 
            lb_Genero.AutoSize = true;
            lb_Genero.Location = new Point(4, 429);
            lb_Genero.Name = "lb_Genero";
            lb_Genero.Size = new Size(59, 16);
            lb_Genero.TabIndex = 13;
            lb_Genero.Text = "Genero:";
            // 
            // cmb_Generos
            // 
            cmb_Generos.FormattingEnabled = true;
            cmb_Generos.Location = new Point(10, 452);
            cmb_Generos.Margin = new Padding(3, 2, 3, 2);
            cmb_Generos.Name = "cmb_Generos";
            cmb_Generos.Size = new Size(376, 24);
            cmb_Generos.TabIndex = 7;
            // 
            // lb_TipoIdentificacion
            // 
            lb_TipoIdentificacion.AutoSize = true;
            lb_TipoIdentificacion.Location = new Point(4, 92);
            lb_TipoIdentificacion.Name = "lb_TipoIdentificacion";
            lb_TipoIdentificacion.Size = new Size(124, 16);
            lb_TipoIdentificacion.TabIndex = 11;
            lb_TipoIdentificacion.Text = "Tipo Identificacion:";
            // 
            // cmb_TipoDocumento
            // 
            cmb_TipoDocumento.FormattingEnabled = true;
            cmb_TipoDocumento.Location = new Point(10, 115);
            cmb_TipoDocumento.Margin = new Padding(3, 2, 3, 2);
            cmb_TipoDocumento.Name = "cmb_TipoDocumento";
            cmb_TipoDocumento.Size = new Size(376, 24);
            cmb_TipoDocumento.TabIndex = 2;
            // 
            // txt_NumeroIdentificacion
            // 
            txt_NumeroIdentificacion.Location = new Point(10, 61);
            txt_NumeroIdentificacion.Margin = new Padding(3, 2, 3, 2);
            txt_NumeroIdentificacion.Name = "txt_NumeroIdentificacion";
            txt_NumeroIdentificacion.Size = new Size(376, 23);
            txt_NumeroIdentificacion.TabIndex = 1;
            // 
            // lb_NumeroIdentificacion
            // 
            lb_NumeroIdentificacion.AutoSize = true;
            lb_NumeroIdentificacion.Location = new Point(4, 36);
            lb_NumeroIdentificacion.Name = "lb_NumeroIdentificacion";
            lb_NumeroIdentificacion.Size = new Size(146, 16);
            lb_NumeroIdentificacion.TabIndex = 8;
            lb_NumeroIdentificacion.Text = "Numero Identificacion:";
            // 
            // txt_SegundoApellido
            // 
            txt_SegundoApellido.Location = new Point(10, 338);
            txt_SegundoApellido.Margin = new Padding(3, 2, 3, 2);
            txt_SegundoApellido.Name = "txt_SegundoApellido";
            txt_SegundoApellido.Size = new Size(376, 23);
            txt_SegundoApellido.TabIndex = 6;
            // 
            // lb_SegundoApellido
            // 
            lb_SegundoApellido.AutoSize = true;
            lb_SegundoApellido.Location = new Point(4, 313);
            lb_SegundoApellido.Name = "lb_SegundoApellido";
            lb_SegundoApellido.Size = new Size(121, 16);
            lb_SegundoApellido.TabIndex = 6;
            lb_SegundoApellido.Text = "Segundo apellido:";
            // 
            // txt_PrimerApellido
            // 
            txt_PrimerApellido.Location = new Point(10, 285);
            txt_PrimerApellido.Margin = new Padding(3, 2, 3, 2);
            txt_PrimerApellido.Name = "txt_PrimerApellido";
            txt_PrimerApellido.Size = new Size(376, 23);
            txt_PrimerApellido.TabIndex = 5;
            // 
            // lb_PrimerApellido
            // 
            lb_PrimerApellido.AutoSize = true;
            lb_PrimerApellido.Location = new Point(4, 260);
            lb_PrimerApellido.Name = "lb_PrimerApellido";
            lb_PrimerApellido.Size = new Size(105, 16);
            lb_PrimerApellido.TabIndex = 4;
            lb_PrimerApellido.Text = "Primer apellido:";
            // 
            // txt_SegundoNombre
            // 
            txt_SegundoNombre.Location = new Point(10, 224);
            txt_SegundoNombre.Margin = new Padding(3, 2, 3, 2);
            txt_SegundoNombre.Name = "txt_SegundoNombre";
            txt_SegundoNombre.Size = new Size(376, 23);
            txt_SegundoNombre.TabIndex = 4;
            // 
            // lb_SegundoNombre
            // 
            lb_SegundoNombre.AutoSize = true;
            lb_SegundoNombre.Location = new Point(4, 199);
            lb_SegundoNombre.Name = "lb_SegundoNombre";
            lb_SegundoNombre.Size = new Size(120, 16);
            lb_SegundoNombre.TabIndex = 2;
            lb_SegundoNombre.Text = "Segundo nombre:";
            // 
            // txt_PrimerNombre
            // 
            txt_PrimerNombre.Location = new Point(10, 171);
            txt_PrimerNombre.Margin = new Padding(3, 2, 3, 2);
            txt_PrimerNombre.Name = "txt_PrimerNombre";
            txt_PrimerNombre.Size = new Size(376, 23);
            txt_PrimerNombre.TabIndex = 3;
            // 
            // lb_PrimerNombre
            // 
            lb_PrimerNombre.AutoSize = true;
            lb_PrimerNombre.Location = new Point(4, 145);
            lb_PrimerNombre.Name = "lb_PrimerNombre";
            lb_PrimerNombre.Size = new Size(104, 16);
            lb_PrimerNombre.TabIndex = 0;
            lb_PrimerNombre.Text = "Primer nombre:";
            // 
            // pnl_Arriba
            // 
            pnl_Arriba.BackColor = Color.FromArgb(10, 24, 51);
            pnl_Arriba.Controls.Add(btn_CerrarFormulario);
            pnl_Arriba.Controls.Add(lb_TituloFormulario);
            pnl_Arriba.Dock = DockStyle.Top;
            pnl_Arriba.Location = new Point(0, 0);
            pnl_Arriba.Margin = new Padding(3, 2, 3, 2);
            pnl_Arriba.Name = "pnl_Arriba";
            pnl_Arriba.Size = new Size(525, 70);
            pnl_Arriba.TabIndex = 5;
            // 
            // btn_CerrarFormulario
            // 
            btn_CerrarFormulario.Cursor = Cursors.Hand;
            btn_CerrarFormulario.IconChar = FontAwesome.Sharp.IconChar.None;
            btn_CerrarFormulario.IconColor = Color.Black;
            btn_CerrarFormulario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btn_CerrarFormulario.Location = new Point(487, 3);
            btn_CerrarFormulario.Name = "btn_CerrarFormulario";
            btn_CerrarFormulario.Size = new Size(35, 29);
            btn_CerrarFormulario.TabIndex = 6;
            btn_CerrarFormulario.Text = "X";
            btn_CerrarFormulario.UseVisualStyleBackColor = true;
            btn_CerrarFormulario.Click += btn_CerrarFormulario_Click;
            // 
            // lb_TituloFormulario
            // 
            lb_TituloFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lb_TituloFormulario.AutoSize = true;
            lb_TituloFormulario.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_TituloFormulario.ForeColor = Color.White;
            lb_TituloFormulario.Location = new Point(154, 25);
            lb_TituloFormulario.Name = "lb_TituloFormulario";
            lb_TituloFormulario.Size = new Size(217, 19);
            lb_TituloFormulario.TabIndex = 1;
            lb_TituloFormulario.Text = "INFORMACION PERSONAL";
            // 
            // umfManteniemientoPersona
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 796);
            Controls.Add(pnl_Arriba);
            Controls.Add(gbx_Datos_Personales);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            MaximumSize = new Size(525, 796);
            MinimumSize = new Size(525, 796);
            Name = "umfManteniemientoPersona";
            StartPosition = FormStartPosition.CenterScreen;
            Load += umfManteniemientoPersona_Load;
            gbx_Datos_Personales.ResumeLayout(false);
            gbx_Datos_Personales.PerformLayout();
            pnl_Arriba.ResumeLayout(false);
            pnl_Arriba.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbx_Datos_Personales;
        private System.Windows.Forms.Label lb_Nacionalidad;
        private System.Windows.Forms.ComboBox cmb_Nacionalidades;
        private System.Windows.Forms.Label lb_Genero;
        private System.Windows.Forms.ComboBox cmb_Generos;
        private System.Windows.Forms.Label lb_TipoIdentificacion;
        private System.Windows.Forms.ComboBox cmb_TipoDocumento;
        private System.Windows.Forms.TextBox txt_NumeroIdentificacion;
        private System.Windows.Forms.Label lb_NumeroIdentificacion;
        private System.Windows.Forms.TextBox txt_SegundoApellido;
        private System.Windows.Forms.Label lb_SegundoApellido;
        private System.Windows.Forms.TextBox txt_PrimerApellido;
        private System.Windows.Forms.Label lb_PrimerApellido;
        private System.Windows.Forms.TextBox txt_SegundoNombre;
        private System.Windows.Forms.Label lb_SegundoNombre;
        private System.Windows.Forms.TextBox txt_PrimerNombre;
        private System.Windows.Forms.Label lb_PrimerNombre;
        private System.Windows.Forms.Panel pnl_Arriba;
        private System.Windows.Forms.Label lb_TituloFormulario;
        private System.Windows.Forms.Button btn_GuardarDatos;
        private System.Windows.Forms.Label lb_Estado;
        private System.Windows.Forms.ComboBox cmb_Estado;
        private System.Windows.Forms.Label lb_EstadoCivil;
        private System.Windows.Forms.ComboBox cmb_Estado_Civil;
        private FontAwesome.Sharp.IconButton btn_CerrarFormulario;
        private ReaLTaiizor.Controls.PoisonDateTime dtm_FechaNacimiento;
        private Label lb_FechaNacimiento;
    }
}