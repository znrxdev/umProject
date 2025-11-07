using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CE_USUARIOS
    {
        private int? _IdUsuario;
        private int? _IdPersona;
        private string? _Usuario;
        private string? _Contrasena;
        private string? _FechaCreacion;
        private string? _FechaModificacion;
        private string? _UltimaSesion;
        private string? _UltimoCambioContrasena;
        private int? _IdCreador;
        private int? _IdModificador;
        private int? _IdTransaccion;
        private int? _IdEstado;

        public int? Id_Usuario { get => _IdUsuario; set => _IdUsuario = value; }
        public int? Id_Persona { get => _IdPersona; set => _IdPersona = value; }
        public string? Usuario { get => _Usuario; set => _Usuario = value; }
        public string? Contrasena { get => _Contrasena; set => _Contrasena = value; }
        public string? Fecha_Creacion { get => _FechaCreacion; set => _FechaCreacion = value; }
        public string? Fecha_Modificacion { get => _FechaModificacion; set => _FechaModificacion = value; }
        public string? Ultima_Sesion { get => _UltimaSesion; set => _UltimaSesion = value; }
        public string? Ultimo_Cambio_Contrasena { get => _UltimoCambioContrasena; set => _UltimoCambioContrasena = value; }
        public int? Id_Creador { get => _IdCreador; set => _IdCreador = value; }
        public int? Id_Modificador { get => _IdModificador; set => _IdModificador = value; }
        public int? Id_Transaccion { get => _IdTransaccion; set => _IdTransaccion = value; }
        public int? Id_Estado { get => _IdEstado; set => _IdEstado = value; }
    }
}
