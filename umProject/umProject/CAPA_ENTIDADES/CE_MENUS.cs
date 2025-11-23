using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeMenus
    {
        private int? _idMenu { get; set; }
        private string? _menu { get; set; }
        private string? _nombreBoton { get; set; }
        private string? _fechaCreacion { get; set; }
        private string? _fechaModificacion { get; set; }
        private int? _idCreador { get; set; }
        private int? _idModificador { get; set; }
        private int? _idTransaccion { get; set; }
        private bool? _activo { get; set; }

        public int? IdMenu { get => _idMenu; set => _idMenu = value; }
        public string? Menu { get => _menu; set => _menu = value; }
        public string? NombreBoton { get => _nombreBoton; set => _nombreBoton = value; }
        public string? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public string? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public int? IdCreador { get => _idCreador; set => _idCreador = value; }
        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
        public bool? Activo { get => _activo; set => _activo = value; }
    }
}
