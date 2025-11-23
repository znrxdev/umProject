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

namespace umForms.Principales.Hijos
{
    public partial class umfAuditoriaSistema : Form
    {
        CnTransacciones cnTransacciones = new CnTransacciones();

        public umfAuditoriaSistema()
        {
            InitializeComponent();
        }

        private void umfAuditoriaSistema_Load(object sender, EventArgs e)
        {
            CargarAuditoriaSistema();
        }

        private void CargarAuditoriaSistema()
        {
            try
            {
                var listaTransacciones = cnTransacciones.ListarAuditoriaSistema(out int oNum, out string oMsg);
                
                if (oNum == -1)
                {
                    MessageBox.Show($"Error al cargar la auditoría del sistema:\n{oMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LlenarDgvAuditoria(listaTransacciones);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al cargar la auditoría:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarDgvAuditoria(List<CeTransacciones> listaTransacciones)
        {
            try
            {
                dgv_Auditoria.Rows.Clear();

                if (listaTransacciones == null || listaTransacciones.Count == 0)
                {
                    return;
                }

                foreach (var transaccion in listaTransacciones)
                {
                    dgv_Auditoria.Rows.Add(
                        transaccion.IdTransaccion ?? 0,
                        transaccion.NombreTipoTransaccion ?? "N/A",
                        transaccion.Concepto ?? "N/A",
                        transaccion.NombrePersona ?? "N/A",
                        transaccion.NombreUsuario ?? "N/A",
                        transaccion.NombreAutor ?? "N/A",
                        transaccion.TipoEntidad ?? "N/A",
                        transaccion.FechaCreacion ?? "N/A",
                        transaccion.Completado == true ? "Sí" : "No"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al llenar la tabla de auditoría:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

