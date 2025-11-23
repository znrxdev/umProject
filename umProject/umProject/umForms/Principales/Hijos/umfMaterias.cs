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
using umForms.Flotantes;
using umForms.Utilidades;

namespace umForms.Principales.Hijos
{
    public partial class umfMaterias : Form
    {
        CeMaterias ceMaterias = new CeMaterias();
        CnMaterias cnMaterias = new CnMaterias();
        UtFormularios utFormularios = new UtFormularios();
        public umfMaterias()
        {
            InitializeComponent();
        }

        private void umfMaterias_Load(object sender, EventArgs e)
        {
            CargarMaterias();
        }

        private void CargarMaterias()
        {
            string textoBusqueda = txt_Buscar.Text;
            
            // Si el texto es "Buscar Materia" (placeholder) o está vacío, usar "%" para buscar todas
            if (string.IsNullOrWhiteSpace(textoBusqueda) || textoBusqueda.Trim().Equals("Buscar Materia", StringComparison.OrdinalIgnoreCase))
            {
                textoBusqueda = "%";
            }
            
            var listaMaterias = cnMaterias.FiltrarMateriasPorNombre(new CeMaterias { NombreMateria = textoBusqueda }, out int oNum, out string oMsg);
            if (oNum == -1)
            {
                MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LlenarDgvMaterias(listaMaterias);
        }

        private void LlenarDgvMaterias(List<CeMaterias> listaMaterias)
        {
            dgv_Materias.Rows.Clear();
            UtFormularios util = new UtFormularios();

            foreach (var materia in listaMaterias)
            {
                dgv_Materias.Rows.Add(
                    materia.IdMateria,
                    materia.CodigoMateria,
                    materia.NombreMateria,
                    util.ObtenerDatosUsuarioId(Convert.ToInt32(materia.IdCreador)).FirstOrDefault()?.Usuario ?? "N/A",
                    util.ObtenerDatosUsuarioId(Convert.ToInt32(materia.IdModificador)).FirstOrDefault()?.Usuario ?? "N/A",
                    materia.IdTransaccion,
                    materia.Activo == true ? "Activo" : "Inactivo"
                );
            }
        }

        private void btn_AgregarMateria_Click(object sender, EventArgs e)
        {
            umfMantenimientoMateria umfMantenimientoMateria = new umfMantenimientoMateria(new CeMaterias { IdMateria = 0 });
            umfMantenimientoMateria.ShowDialog();
            CargarMaterias();
        }

        private void txt_Buscar_TextChanged(object sender, EventArgs e)
        {
            CargarMaterias();
        }

        private void btn_ModificarMateria_Click(object sender, EventArgs e)
        {
            if (dgv_Materias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Para modificar necesita seleccionar una materia de la lista", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idMateria = Convert.ToInt32(dgv_Materias.CurrentRow.Cells[0].Value);
                var materia = cnMaterias.FiltrarMateriasPorId(new CeMaterias { IdMateria = idMateria }, out int oNum, out string oMsg);
                if (oNum == 0 && materia.Count > 0)
                {
                    umfMantenimientoMateria umfMantenimientoMateria = new umfMantenimientoMateria(materia.First());
                    umfMantenimientoMateria.ShowDialog();
                    CargarMaterias();
                }
                else
                {
                    MessageBox.Show(oMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

