using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using umForms.Utilidades;

namespace umForms.Flotantes
{
    public partial class umfDetallesSancion : Form
    {
        private CeSancionesAcademicas datosSancion;
        private UtFormularios utFormularios = new UtFormularios();
        private CnUsuarios cnUsuarios = new CnUsuarios();

        public umfDetallesSancion(CeSancionesAcademicas datosSancion)
        {
            InitializeComponent();
            this.datosSancion = datosSancion;
            lb_Titulo.Text = $"DETALLES DE SANCIÓN: {datosSancion.CodigoSancion ?? "N/A"}";
        }

        private void umfDetallesSancion_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                // Código de Sanción
                txt_CodigoSancion.Text = datosSancion.CodigoSancion ?? "N/A";

                // Tipo de Sanción
                if (datosSancion.IdTipoSancion.HasValue && datosSancion.IdTipoSancion.Value > 0)
                {
                    txt_TipoSancion.Text = utFormularios.ObtenerNombreCatalogo(datosSancion.IdTipoSancion.Value);
                }
                else
                {
                    txt_TipoSancion.Text = "N/A";
                }

                // Tipo de Falta
                if (datosSancion.IdTipoFalta.HasValue && datosSancion.IdTipoFalta.Value > 0)
                {
                    txt_TipoFalta.Text = utFormularios.ObtenerNombreCatalogo(datosSancion.IdTipoFalta.Value);
                }
                else
                {
                    txt_TipoFalta.Text = "N/A";
                }

                // Severidad
                if (datosSancion.IdSeveridad.HasValue && datosSancion.IdSeveridad.Value > 0)
                {
                    txt_Severidad.Text = utFormularios.ObtenerNombreCatalogo(datosSancion.IdSeveridad.Value);
                }
                else
                {
                    txt_Severidad.Text = "N/A";
                }

                // Estado
                if (datosSancion.IdEstado.HasValue && datosSancion.IdEstado.Value > 0)
                {
                    txt_Estado.Text = utFormularios.ObtenerNombreEstado(datosSancion.IdEstado.Value);
                }
                else
                {
                    txt_Estado.Text = "N/A";
                }

                // Fechas
                txt_FechaRegistro.Text = datosSancion.FechaRegistro?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
                txt_FechaFin.Text = datosSancion.FechaFin?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";

                // Motivo
                txt_Motivo.Text = datosSancion.Motivo ?? "N/A";

                // Es Apelable
                txt_EsApelable.Text = datosSancion.EsApelable == true ? "SI" : "NO";

                // Información de Apelación
                if (datosSancion.FechaApelacion.HasValue)
                {
                    txt_FechaApelacion.Text = datosSancion.FechaApelacion.Value.ToString("dd/MM/yyyy HH:mm");
                }
                else
                {
                    txt_FechaApelacion.Text = "N/A";
                }

                txt_ObservacionesApelacion.Text = datosSancion.ObservacionesApelacion ?? "N/A";

                // Información de Resolución
                if (datosSancion.FechaResolucion.HasValue)
                {
                    txt_FechaResolucion.Text = datosSancion.FechaResolucion.Value.ToString("dd/MM/yyyy HH:mm");
                }
                else
                {
                    txt_FechaResolucion.Text = "N/A";
                }

                // Revisado Por
                if (datosSancion.IdUsuarioResolucion.HasValue && datosSancion.IdUsuarioResolucion.Value > 0)
                {
                    var usuarioRevisor = cnUsuarios.FiltrarUsuariosPorId(
                        new CeUsuarios { IdUsuario = datosSancion.IdUsuarioResolucion.Value },
                        out int oNum,
                        out string oMsg);
                    if (oNum != -1 && usuarioRevisor.Count > 0)
                    {
                        txt_RevisadoPor.Text = usuarioRevisor[0].Usuario ?? "N/A";
                    }
                    else
                    {
                        txt_RevisadoPor.Text = "N/A";
                    }
                }
                else
                {
                    txt_RevisadoPor.Text = "N/A";
                }

                txt_DocumentoResolucion.Text = datosSancion.DocumentoResolucion ?? "N/A";
                txt_ResultadoApelacion.Text = datosSancion.ResultadoApelacion ?? "N/A";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los detalles de la sanción:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_CerrarFormulario_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

