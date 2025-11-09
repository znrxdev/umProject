using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_CATALOGOS
    {
        private int? _IdCatalogo;
        private int? _IdTipoCatalogo;
        private string? _NombreCatalogo;
        private string? _FechaCreacion;
        private string? _FechaModificacion;
        private int? _IdCreador;
        private int? _IdModificador;
        private int? _IdTransaccion;
        private bool? _Activo;

        public int? Id_Catalogo { get => _IdCatalogo; set => _IdCatalogo = value; }
        public int? Id_Tipo_Catalogo { get => _IdTipoCatalogo; set => _IdTipoCatalogo = value; }
        public string? Nombre_Catalogo { get => _NombreCatalogo; set => _NombreCatalogo = value; }
        public string? Fecha_Creacion { get => _FechaCreacion; set => _FechaCreacion = value; }
        public string? Fecha_Modificacion { get => _FechaModificacion; set => _FechaModificacion = value; }
        public int? Id_Creador { get => _IdCreador; set => _IdCreador = value; }
        public int? Id_Modificador { get => _IdModificador; set => _IdModificador = value; }
        public int? Id_Transaccion { get => _IdTransaccion; set => _IdTransaccion = value; }
        public bool? Activo { get => _Activo; set => _Activo = value; }
    }
}