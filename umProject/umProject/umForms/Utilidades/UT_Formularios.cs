using CAPA_DATOS;
using CAPA_ENTIDADES;
using CAPA_NEGOCIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace umForms.Utilidades
{
    public class UtFormularios
    {

        public List<CeUsuarios> ObtenerDatosUsuarioId(int idUsuario)
        {
            List<CeUsuarios> usuarioInfo = new List<CeUsuarios>();
            if (idUsuario != 0)
            {
                usuarioInfo = new CnUsuarios().FiltrarUsuariosPorId(new CeUsuarios { IdUsuario = idUsuario }, out int oNum, out string oMsg);
            }
            else
            {
                usuarioInfo.Add(new CeUsuarios { IdUsuario = 0 });
            }
            return usuarioInfo;
        }
        public List<CeUsuariosRoles> ObtenerPermisosUsuario(int? idUsuario)
        {
            List<CeUsuariosRoles> prmUsuario = new List<CeUsuariosRoles>();
            if (idUsuario != 0)
            {
                prmUsuario = new CnUsuariosRoles().ListarRolesDeUsuario(new CeUsuariosRoles { IdUsuario = idUsuario }, out int oNum, out string oMsg);
            }
            else
            {
                prmUsuario.Add(new CeUsuariosRoles { IdUsuarioRol = 0 });
            }
            return prmUsuario;
        }
        public List<CeUsuarios> ObtenerDatosUsuarioIdPersona(int? idPersona)
        {
            List<CeUsuarios> usuarioInfo = new List<CeUsuarios>();
            if (idPersona != 0)
            {
                usuarioInfo = new CnUsuarios().FiltrarUsuariosPorIdPersona(new CeUsuarios { IdPersona = idPersona }, out int oNum, out string oMsg);
            }
            else
            {
                usuarioInfo.Add(new CeUsuarios { IdUsuario = 0 });
            }
            return usuarioInfo;
        }
        public List<CePersonas> BuscarPersonaId(int idPersona, out int oNum, out string oMsg)
        {
            List<CePersonas> ltsPersonas = new CnPersonas().FiltrarPersonasPorId(new CePersonas { IdPersona = idPersona }, out oNum, out oMsg);
            if (oNum == 0 && ltsPersonas.Count > 0)
            {
                return ltsPersonas;
            }
            else
            {
                CePersonas objVacio = new CePersonas { IdPersona = 0 };
                ltsPersonas.Add(objVacio);
                return ltsPersonas;
            }
        }

        public List<CeRoles> FiltrarRolesId(int idRol)
        {
            List<CeRoles> ltsRoles = new CnRoles().FiltrarRolesId(new CeRoles { IdRol = idRol }, out int oNum, out string oMsg);
            if (oNum == 0 && ltsRoles.Count > 0)
            {
                return ltsRoles;
            }
            else
            {
                CeRoles objVacio = new CeRoles { IdRol = 0 };
                ltsRoles.Add(objVacio);
                return ltsRoles;
            }
        }
        public string ObtenerNombreEstado(int idEstado)
        {
            string nombreEstado = string.Empty;
            if (idEstado != 0)
            {
                List<CeEstados> estadoInfo = new CnEstados().FiltrarEstadosPorId(new CeEstados() { IdEstado = idEstado }, out int oNum, out string oMsg);
                foreach (var estado in estadoInfo)
                {
                    nombreEstado = estado.NombreEstado;
                }
            }
            else
            {
                nombreEstado = "N/A";
            }
            return nombreEstado;
        }

        public string ObtenerNombreCatalogo(int idCatalogo)
        {
            string nombreCatalogo = string.Empty;
            if (idCatalogo != 0)
            {
                List<CeCatalogos> catalogoInfo = new CnCatalogos().FiltrarCatalogoId(new CeCatalogos() { IdCatalogo = idCatalogo }, out int oNum, out string oMsg);
                foreach (var catalogo in catalogoInfo)
                {
                    nombreCatalogo = catalogo.NombreCatalogo;
                }
            }
            else
            {
                nombreCatalogo = "N/A";
            }
            return nombreCatalogo;
        }

        public List<CePersonas> BuscarPersonaDocumento(string valorDocumento, out int oNum, out string oMsg)
        {
            List<CePersonas> ltsPersonas = new CnPersonas().FiltrarPersonasPorNumeroDocumento(new CePersonas { ValorDocumento = valorDocumento }, out oNum, out oMsg);
            if (oNum == 0 && ltsPersonas.Count > 0)
            {
                return ltsPersonas;
            }
            else
            {
                CePersonas objVacio = new CePersonas { IdPersona = 0 };
                ltsPersonas.Add(objVacio);
                return ltsPersonas;
            }
        }

        public void CargarCmbEstado(int idTipoTransaccion, ComboBox cbo, out int oNum, out string oMsg)
        {
            cbo.DataSource = new CnEstados().FiltrarEstadosPorTipoTransaccion(new CeEstados { IdTipoTransaccion = idTipoTransaccion }, out oNum, out oMsg);
            cbo.ValueMember = "IdEstado";
            cbo.DisplayMember = "NombreEstado";
        }
        public void CargarCmbCatalogos(int idTipoCatalogo, ComboBox cbo, out int oNum, out string oMsg)
        {
            cbo.DataSource = new CnCatalogos().FiltrarCatalogosPorTipoCatalogo(new CeCatalogos { IdTipoCatalogo = idTipoCatalogo }, out oNum, out oMsg);
            cbo.ValueMember = "IdCatalogo";
            cbo.DisplayMember = "NombreCatalogo";
        }
        public void CargarCmbRoles(ComboBox cbo, out int oNum, out string oMsg)
        {
            cbo.DataSource = new CnRoles().ListarRoles(out oNum, out oMsg);
            cbo.ValueMember = "IdRol";
            cbo.DisplayMember = "NombreRol";
        }
        public void CargarCmbMateriasPeriodos(ComboBox cbo, out int oNum, out string oMsg)
        {
            var listaMateriasPeriodos = new CnMateriasPeriodos().ListarMateriasPeriodos(out oNum, out oMsg);
            if (oNum == 0)
            {
                // Agregar propiedad DisplayText a cada elemento usando una clase anónima
                var listaConDisplay = listaMateriasPeriodos.Select(mp => new
                {
                    IdMateriaPeriodo = mp.IdMateriaPeriodo,
                    DisplayText = !string.IsNullOrEmpty(mp.NombreMateria) && !string.IsNullOrEmpty(mp.NombrePeriodo)
                        ? $"{mp.NombreMateria} - {mp.NombrePeriodo}"
                        : mp.NombreMateria ?? "N/A"
                }).ToList();
                
                cbo.DataSource = listaConDisplay;
                cbo.ValueMember = "IdMateriaPeriodo";
                cbo.DisplayMember = "DisplayText";
            }
        }

        public void CargarCmbSeccionesPorMateriaPeriodo(int idMateriaPeriodo, ComboBox cbo, out int oNum, out string oMsg)
        {
            var listaSecciones = new CnSecciones().FiltrarSeccionesPorMateriaPeriodo(
                new CeSecciones { IdMateriaPeriodo = idMateriaPeriodo },
                out oNum,
                out oMsg);

            if (oNum == 0)
            {
                var listaConDisplay = listaSecciones.Select(s => new
                {
                    IdSeccion = s.IdSeccion,
                    DisplayText = !string.IsNullOrEmpty(s.CodigoSeccion)
                        ? $"{s.CodigoSeccion} - {s.HorarioDescripcion}"
                        : "SIN CÓDIGO"
                }).ToList();

                cbo.DataSource = listaConDisplay;
                cbo.ValueMember = "IdSeccion";
                cbo.DisplayMember = "DisplayText";
            }
        }

        public static void AplicarEstiloDataGridView(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.FromArgb(255, 255, 255);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Pixel),
                ForeColor = Color.Black,
                SelectionBackColor = Color.Gray,
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            var cellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(255, 255, 255),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Pixel),
                ForeColor = Color.Black,
                Padding = new Padding(8, 4, 8, 4),
                SelectionBackColor = Color.Gray,
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.False
            };
            dgv.DefaultCellStyle = cellStyle;
            dgv.EnableHeadersVisualStyles = false;
            dgv.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
            dgv.GridColor = Color.FromArgb(255, 255, 255);
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.RowTemplate.Height = 36;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
