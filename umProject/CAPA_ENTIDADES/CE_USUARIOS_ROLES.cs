using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_USUARIOS_ROLES
    {
        private int? _IdUsuarioRol;
        private int? _IdUsuario;
        private int? _IdRol;
        private string? _FechaCreacion;
        private string? _FechaModificacion;
        private int? _IdCreador;
        private int? _IdModificador;
        private int? _IdTransaccion;
        private bool? _Activo;

        public int? Id_Usuario_Rol { get => _IdUsuarioRol; set => _IdUsuarioRol = value; }
        public int? Id_Usuario { get => _IdUsuario; set => _IdUsuario = value; }
        public int? Id_Rol { get => _IdRol; set => _IdRol = value; }
        public string? Fecha_Creacion { get => _FechaCreacion; set => _FechaCreacion = value; }
        public string? Fecha_Modificacion { get => _FechaModificacion; set => _FechaModificacion = value; }
        public int? Id_Creador { get => _IdCreador; set => _IdCreador = value; }
        public int? Id_Modificador { get => _IdModificador; set => _IdModificador = value; }
        public int? Id_Transaccion { get => _IdTransaccion; set => _IdTransaccion = value; }
        public bool? Activo { get => _Activo; set => _Activo = value; }
    }
}
