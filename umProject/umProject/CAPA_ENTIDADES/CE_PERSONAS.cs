using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CePersonas
    {
        private int? _idPersona;
        private string? _primerNombre;
        private string? _segundoNombre;
        private string? _primerApellido;
        private string? _segundoApellido;
        private int? _idTipoDocumento;
        private string? _valorDocumento;
        private int? _idGeneroPersona;
        private string? _fechaNacimiento;   
        private int? _idNacionalidad;
        private int? _idEstadoCivil;
        private string? _fechaCreacion;
        private string? _fechaModificacion;
        private int? _idCreador;
        private int? _idModificador;
        private int? _idTransaccion;
        private int? _idEstado;

        public int? IdPersona { get => _idPersona; set => _idPersona = value; }
        public string? PrimerNombre { get => _primerNombre; set => _primerNombre = value; }
        public string? SegundoNombre { get => _segundoNombre; set => _segundoNombre = value; }
        public string? PrimerApellido { get => _primerApellido; set => _primerApellido = value; }
        public string? SegundoApellido { get => _segundoApellido; set => _segundoApellido = value; }
        public int? IdTipoDocumento { get => _idTipoDocumento; set => _idTipoDocumento = value; }
        public string? ValorDocumento { get => _valorDocumento; set => _valorDocumento = value; }
        public int? IdGeneroPersona { get => _idGeneroPersona; set => _idGeneroPersona = value; }
        public int? IdNacionalidad { get => _idNacionalidad; set => _idNacionalidad = value; }
        public string? FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public int? IdEstadoCivil { get => _idEstadoCivil; set => _idEstadoCivil = value; }
        public string? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public string? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public int? IdCreador { get => _idCreador; set => _idCreador = value; }
        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
        public int? IdEstado { get => _idEstado; set => _idEstado = value; }
    }
}