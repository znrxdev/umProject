using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeMaterias
    {
        private int? _idMateria;
        private string? _codigoMateria;
        private string? _nombreMateria;
        private string? _fechaCreacion;
        private string? _fechaModificacion;
        private int? _idCreador;
        private int? _idModificador;
        private int? _idTransaccion;
        private bool? _activo;

        public int? IdMateria { get => _idMateria; set => _idMateria = value; }
        public string? CodigoMateria { get => _codigoMateria; set => _codigoMateria = value; }
        public string? NombreMateria { get => _nombreMateria; set => _nombreMateria = value; }
        public string? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public string? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public int? IdCreador { get => _idCreador; set => _idCreador = value; }
        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
        public bool? Activo { get => _activo; set => _activo = value; }
    }
}

