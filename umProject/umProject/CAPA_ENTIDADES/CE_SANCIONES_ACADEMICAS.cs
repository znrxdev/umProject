using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeSancionesAcademicas
    {
        private int? _idSancion;
        private string? _codigoSancion;
        private int? _idEstudiante;
        private int? _idTipoSancion;
        private int? _idTipoFalta;
        private int? _idSeveridad;
        private int? _idEstado;
        private DateTime? _fechaRegistro;
        private DateTime? _fechaFin;
        private string? _motivo;
        private bool? _esApelable;
        private DateTime? _fechaApelacion;
        private string? _resultadoApelacion;
        private string? _observacionesApelacion;
        private string? _documentoResolucion;
        private int? _idUsuarioResolucion;
        private DateTime? _fechaResolucion;
        private int? _idSancionOrigen;
        private string? _fechaCreacion;
        private string? _fechaModificacion;
        private int? _idCreador;
        private int? _idModificador;
        private int? _idTransaccion;

        public int? IdSancion { get => _idSancion; set => _idSancion = value; }
        public string? CodigoSancion { get => _codigoSancion; set => _codigoSancion = value; }
        public int? IdEstudiante { get => _idEstudiante; set => _idEstudiante = value; }
        public int? IdTipoSancion { get => _idTipoSancion; set => _idTipoSancion = value; }
        public int? IdTipoFalta { get => _idTipoFalta; set => _idTipoFalta = value; }
        public int? IdSeveridad { get => _idSeveridad; set => _idSeveridad = value; }
        public int? IdEstado { get => _idEstado; set => _idEstado = value; }
        public DateTime? FechaRegistro { get => _fechaRegistro; set => _fechaRegistro = value; }
        public DateTime? FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public string? Motivo { get => _motivo; set => _motivo = value; }
        public bool? EsApelable { get => _esApelable; set => _esApelable = value; }
        public DateTime? FechaApelacion { get => _fechaApelacion; set => _fechaApelacion = value; }
        public string? ResultadoApelacion { get => _resultadoApelacion; set => _resultadoApelacion = value; }
        public string? ObservacionesApelacion { get => _observacionesApelacion; set => _observacionesApelacion = value; }
        public string? DocumentoResolucion { get => _documentoResolucion; set => _documentoResolucion = value; }
        public int? IdUsuarioResolucion { get => _idUsuarioResolucion; set => _idUsuarioResolucion = value; }
        public DateTime? FechaResolucion { get => _fechaResolucion; set => _fechaResolucion = value; }
        public int? IdSancionOrigen { get => _idSancionOrigen; set => _idSancionOrigen = value; }
        public string? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public string? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public int? IdCreador { get => _idCreador; set => _idCreador = value; }
        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
    }
}

