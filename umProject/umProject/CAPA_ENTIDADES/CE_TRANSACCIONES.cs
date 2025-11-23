using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeTransacciones
    {
        private int? _idTransaccion;
        private int? _idTipoTransaccion;
        private string? _nombreTipoTransaccion;
        private string? _concepto;
        private int? _idPersona;
        private string? _nombrePersona;
        private int? _idUsuario;
        private string? _nombreUsuario;
        private int? _idContacto;
        private int? _idEvaluacion;
        private int? _idSolicitudBeca;
        private int? _idInscripcion;
        private int? _idAutor;
        private string? _nombreAutor;
        private string? _fechaCreacion;
        private bool? _completado;
        private string? _tipoEntidad;

        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
        public int? IdTipoTransaccion { get => _idTipoTransaccion; set => _idTipoTransaccion = value; }
        public string? NombreTipoTransaccion { get => _nombreTipoTransaccion; set => _nombreTipoTransaccion = value; }
        public string? Concepto { get => _concepto; set => _concepto = value; }
        public int? IdPersona { get => _idPersona; set => _idPersona = value; }
        public string? NombrePersona { get => _nombrePersona; set => _nombrePersona = value; }
        public int? IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string? NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public int? IdContacto { get => _idContacto; set => _idContacto = value; }
        public int? IdEvaluacion { get => _idEvaluacion; set => _idEvaluacion = value; }
        public int? IdSolicitudBeca { get => _idSolicitudBeca; set => _idSolicitudBeca = value; }
        public int? IdInscripcion { get => _idInscripcion; set => _idInscripcion = value; }
        public int? IdAutor { get => _idAutor; set => _idAutor = value; }
        public string? NombreAutor { get => _nombreAutor; set => _nombreAutor = value; }
        public string? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public bool? Completado { get => _completado; set => _completado = value; }
        public string? TipoEntidad { get => _tipoEntidad; set => _tipoEntidad = value; }
    }
}

