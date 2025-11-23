using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeEstados
    {
        private int? _idEstado;
        private int? _idTipoTransaccion;
        private string _nombreEstado;
        private string _fechaCreacion;
        private string _fechaModificacion;
        private int? _idTransaccion;
        private int? _idCreador;
        private int? _idModificador;
        private bool _activo;

        public int? IdEstado { get => _idEstado; set => _idEstado = value; }
        public int? IdTipoTransaccion { get => _idTipoTransaccion; set => _idTipoTransaccion = value; }
        public string NombreEstado { get => _nombreEstado; set => _nombreEstado  = value; }
        public string FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public string FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }

        public int? IdCreador { get => _idCreador; set => _idCreador = value; }

        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public bool Activo { get => _activo; set => _activo = value; }
    }
}
