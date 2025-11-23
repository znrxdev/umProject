using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeUsuarios
    {
        private int? _idUsuario;
        private int? _idPersona;
        private string? _usuario;
        private string? _contrasena;
        private string? _fechaCreacion;
        private string? _fechaModificacion;
        private string? _ultimaSesion;
        private string? _ultimoCambioContrasena;
        private int? _idCreador;
        private int? _idModificador;
        private int? _idTransaccion;
        private int? _idEstado;
        private string? _valorDocumento;
        private string? _nombreCompleto;

        public int? IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public int? IdPersona { get => _idPersona; set => _idPersona = value; }
        public string? Usuario { get => _usuario; set => _usuario = value; }
        public string? Contrasena { get => _contrasena; set => _contrasena = value; }
        public string? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public string? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public string? UltimaSesion { get => _ultimaSesion; set => _ultimaSesion = value; }
        public string? UltimoCambioContrasena { get => _ultimoCambioContrasena; set => _ultimoCambioContrasena = value; }
        public int? IdCreador { get => _idCreador; set => _idCreador = value; }
        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
        public int? IdEstado { get => _idEstado; set => _idEstado = value; }
        public string? ValorDocumento { get => _valorDocumento; set => _valorDocumento = value; }
        public string? NombreCompleto { get => _nombreCompleto; set => _nombreCompleto = value; }
    }
}
