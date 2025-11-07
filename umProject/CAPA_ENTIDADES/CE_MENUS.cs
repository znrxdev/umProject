using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_MENUS
    {
        private int? _IdMenu { get; set; }
        private string? _Menu { get; set; }
        private string? _Nombre_Boton { get; set; }
        private string? _FechaCreacion { get; set; }
        private string? _FechaModificacion { get; set; }
        private int? _IdCreador { get; set; }
        private int? _IdModificador { get; set; }
        private int? _IdTransaccion { get; set; }
        private bool? _Activo { get; set; }

        public int? Id_Menu { get => _IdMenu; set => _IdMenu = value; }
        public string? Menu { get => _Menu; set => _Menu = value; }
        public string? Nombre_Boton { get => _Nombre_Boton; set => _Nombre_Boton = value; }
        public string? Fecha_Creacion { get => _FechaCreacion; set => _FechaCreacion = value; }
        public string? Fecha_Modificacion { get => _FechaModificacion; set => _FechaModificacion = value; }
        public int? Id_Creador { get => _IdCreador; set => _IdCreador = value; }
        public int? Id_Modificador { get => _IdModificador; set => _IdModificador = value; }
        public int? Id_Transaccion { get => _IdTransaccion; set => _IdTransaccion = value; }
        public bool? Activo { get => _Activo; set => _Activo = value; }
    }
}
