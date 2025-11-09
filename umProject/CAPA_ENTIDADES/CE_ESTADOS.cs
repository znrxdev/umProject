using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_ESTADOS
    {
        private int? _IdEstado;
        private int? _IdTipoTransaccion;
        private string _NombreEstado;
        private string _FechaCreacion;
        private string _FechaModificacion;
        private int? _IdTransaccion;
        private int? _IdCreador;
        private int? _IdModificador;
        private bool _Activo;

        public int? Id_Estado { get => _IdEstado; set => _IdEstado = value; }
        public int? Id_Tipo_Transaccion { get => _IdTipoTransaccion; set => _IdTipoTransaccion = value; }
        public string Nombre_Estado { get => _NombreEstado; set => _NombreEstado  = value; }
        public string Fecha_Creacion { get => _FechaCreacion; set => _FechaCreacion = value; }
        public string Fecha_Modificacion { get => _FechaModificacion; set => _FechaModificacion = value; }
        public int? Id_Transaccion { get => _IdTransaccion; set => _IdTransaccion = value; }

        public int? Id_Creador { get => _IdCreador; set => _IdCreador = value; }

        public int? Id_Modificador { get => _IdModificador; set => _IdModificador = value; }
        public bool Activo { get => _Activo; set => _Activo = value; }
    }
}
