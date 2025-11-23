using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeEvaluacionesAlumnos
    {
        private int? _idEvaluacionAlumno;
        private string? _codigoRegistro;
        private int? _idEvaluacionInstancia;
        private int? _idInscripcion;
        private decimal? _puntajeObtenido;
        private decimal? _porcentajeLogrado;
        private decimal? _puntajeNormalizado;
        private bool? _esRecalculo;
        private int? _numeroRecalculo;
        private string? _motivoAjuste;
        private string? _observaciones;
        private int? _idUsuarioEvaluador;
        private int? _idUsuarioValidador;
        private DateTime? _fechaValidacion;
        private int? _idEstado;
        private int? _idEstadoPublicacion;
        private int? _idEvaluacionReemplazada;
        private bool? _firmadoPorEstudiante;
        private string? _firmaDigital;
        private DateTime? _fechaNotificacion;
        private DateTime? _fechaPublicacion;
        private DateTime? _fechaCreacion;
        private DateTime? _fechaModificacion;
        private int? _idCreador;
        private int? _idModificador;
        private int? _idTransaccion;
        // Propiedades adicionales de JOINs
        private string? _codigoInstancia;
        private string? _codigoSeccion;
        private string? _nombreMateria;
        private string? _nombreModeloEvaluacion;
        private string? _nombrePeriodo;
        private string? _codigoPeriodo;
        private string? _codigoInscripcion;
        private string? _usuarioEstudiante;
        private string? _nombreEstudiante;
        private string? _nombreEstado;
        private string? _nombreEstadoPublicacion;

        public int? IdEvaluacionAlumno { get => _idEvaluacionAlumno; set => _idEvaluacionAlumno = value; }
        public string? CodigoRegistro { get => _codigoRegistro; set => _codigoRegistro = value; }
        public int? IdEvaluacionInstancia { get => _idEvaluacionInstancia; set => _idEvaluacionInstancia = value; }
        public int? IdInscripcion { get => _idInscripcion; set => _idInscripcion = value; }
        public decimal? PuntajeObtenido { get => _puntajeObtenido; set => _puntajeObtenido = value; }
        public decimal? PorcentajeLogrado { get => _porcentajeLogrado; set => _porcentajeLogrado = value; }
        public decimal? PuntajeNormalizado { get => _puntajeNormalizado; set => _puntajeNormalizado = value; }
        public bool? EsRecalculo { get => _esRecalculo; set => _esRecalculo = value; }
        public int? NumeroRecalculo { get => _numeroRecalculo; set => _numeroRecalculo = value; }
        public string? MotivoAjuste { get => _motivoAjuste; set => _motivoAjuste = value; }
        public string? Observaciones { get => _observaciones; set => _observaciones = value; }
        public int? IdUsuarioEvaluador { get => _idUsuarioEvaluador; set => _idUsuarioEvaluador = value; }
        public int? IdUsuarioValidador { get => _idUsuarioValidador; set => _idUsuarioValidador = value; }
        public DateTime? FechaValidacion { get => _fechaValidacion; set => _fechaValidacion = value; }
        public int? IdEstado { get => _idEstado; set => _idEstado = value; }
        public int? IdEstadoPublicacion { get => _idEstadoPublicacion; set => _idEstadoPublicacion = value; }
        public int? IdEvaluacionReemplazada { get => _idEvaluacionReemplazada; set => _idEvaluacionReemplazada = value; }
        public bool? FirmadoPorEstudiante { get => _firmadoPorEstudiante; set => _firmadoPorEstudiante = value; }
        public string? FirmaDigital { get => _firmaDigital; set => _firmaDigital = value; }
        public DateTime? FechaNotificacion { get => _fechaNotificacion; set => _fechaNotificacion = value; }
        public DateTime? FechaPublicacion { get => _fechaPublicacion; set => _fechaPublicacion = value; }
        public DateTime? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public int? IdCreador { get => _idCreador; set => _idCreador = value; }
        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
        // Propiedades adicionales de JOINs
        public string? CodigoInstancia { get => _codigoInstancia; set => _codigoInstancia = value; }
        public string? CodigoSeccion { get => _codigoSeccion; set => _codigoSeccion = value; }
        public string? NombreMateria { get => _nombreMateria; set => _nombreMateria = value; }
        public string? NombreModeloEvaluacion { get => _nombreModeloEvaluacion; set => _nombreModeloEvaluacion = value; }
        public string? NombrePeriodo { get => _nombrePeriodo; set => _nombrePeriodo = value; }
        public string? CodigoPeriodo { get => _codigoPeriodo; set => _codigoPeriodo = value; }
        public string? CodigoInscripcion { get => _codigoInscripcion; set => _codigoInscripcion = value; }
        public string? UsuarioEstudiante { get => _usuarioEstudiante; set => _usuarioEstudiante = value; }
        public string? NombreEstudiante { get => _nombreEstudiante; set => _nombreEstudiante = value; }
        public string? NombreEstado { get => _nombreEstado; set => _nombreEstado = value; }
        public string? NombreEstadoPublicacion { get => _nombreEstadoPublicacion; set => _nombreEstadoPublicacion = value; }
    }
}

