using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeReporteUsuario
    {
        private int? _idUsuario;
        private string? _usuario;
        private int? _idPersona;
        private string? _primerNombre;
        private string? _segundoNombre;
        private string? _primerApellido;
        private string? _segundoApellido;
        private string? _nombreCompleto;
        private string? _valorDocumento;
        private string? _tipoDocumento;
        private string? _fechaNacimiento;
        private string? _genero;
        private string? _nacionalidad;
        private string? _estadoCivil;
        private string? _fechaCreacionUsuario;
        private string? _fechaModificacionUsuario;
        private string? _ultimaSesion;
        private string? _ultimoCambioContrasena;
        private string? _estadoUsuario;
        private string? _fechaCreacionPersona;
        private string? _fechaModificacionPersona;

        public int? IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string? Usuario { get => _usuario; set => _usuario = value; }
        public int? IdPersona { get => _idPersona; set => _idPersona = value; }
        public string? PrimerNombre { get => _primerNombre; set => _primerNombre = value; }
        public string? SegundoNombre { get => _segundoNombre; set => _segundoNombre = value; }
        public string? PrimerApellido { get => _primerApellido; set => _primerApellido = value; }
        public string? SegundoApellido { get => _segundoApellido; set => _segundoApellido = value; }
        public string? NombreCompleto { get => _nombreCompleto; set => _nombreCompleto = value; }
        public string? ValorDocumento { get => _valorDocumento; set => _valorDocumento = value; }
        public string? TipoDocumento { get => _tipoDocumento; set => _tipoDocumento = value; }
        public string? FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public string? Genero { get => _genero; set => _genero = value; }
        public string? Nacionalidad { get => _nacionalidad; set => _nacionalidad = value; }
        public string? EstadoCivil { get => _estadoCivil; set => _estadoCivil = value; }
        public string? FechaCreacionUsuario { get => _fechaCreacionUsuario; set => _fechaCreacionUsuario = value; }
        public string? FechaModificacionUsuario { get => _fechaModificacionUsuario; set => _fechaModificacionUsuario = value; }
        public string? UltimaSesion { get => _ultimaSesion; set => _ultimaSesion = value; }
        public string? UltimoCambioContrasena { get => _ultimoCambioContrasena; set => _ultimoCambioContrasena = value; }
        public string? EstadoUsuario { get => _estadoUsuario; set => _estadoUsuario = value; }
        public string? FechaCreacionPersona { get => _fechaCreacionPersona; set => _fechaCreacionPersona = value; }
        public string? FechaModificacionPersona { get => _fechaModificacionPersona; set => _fechaModificacionPersona = value; }
    }
}

