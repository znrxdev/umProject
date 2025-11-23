using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeEvaluacionesInstancias
    {
        private int? _idEvaluacionInstancia;
        private string? _codigoInstancia;
        private int? _idSeccion;
        private int? _idEvaluacionModelo;
        private int? _idPeriodo;
        private DateTime? _fechaProgramada;
        private DateTime? _fechaLimite;
        private bool? _requiereRevisionInterna;
        private int? _numeroVersion;
        private byte? _nivelAprobacionActual;
        private int? _idEstado;
        private int? _idEstadoPublicacion;
        private int? _idResponsableRevision;
        private DateTime? _fechaRevision;
        private int? _idResponsablePublicacion;
        private DateTime? _fechaPublicacion;
        private int? _idEvaluacionPadre;
        private string? _observacionesRevision;
        private string? _motivoRechazo;
        private DateTime? _fechaCreacion;
        private DateTime? _fechaModificacion;
        private int? _idCreador;
        private int? _idModificador;
        private int? _idTransaccion;
        // Propiedades adicionales de JOINs
        private string? _codigoSeccion;
        private string? _nombreMateria;
        private string? _nombreModeloEvaluacion;
        private string? _codigoModelo;
        private string? _nombrePeriodo;
        private string? _codigoPeriodo;
        private string? _nombreEstado;
        private string? _nombreEstadoPublicacion;

        public int? IdEvaluacionInstancia { get => _idEvaluacionInstancia; set => _idEvaluacionInstancia = value; }
        public string? CodigoInstancia { get => _codigoInstancia; set => _codigoInstancia = value; }
        public int? IdSeccion { get => _idSeccion; set => _idSeccion = value; }
        public int? IdEvaluacionModelo { get => _idEvaluacionModelo; set => _idEvaluacionModelo = value; }
        public int? IdPeriodo { get => _idPeriodo; set => _idPeriodo = value; }
        public DateTime? FechaProgramada { get => _fechaProgramada; set => _fechaProgramada = value; }
        public DateTime? FechaLimite { get => _fechaLimite; set => _fechaLimite = value; }
        public bool? RequiereRevisionInterna { get => _requiereRevisionInterna; set => _requiereRevisionInterna = value; }
        public int? NumeroVersion { get => _numeroVersion; set => _numeroVersion = value; }
        public byte? NivelAprobacionActual { get => _nivelAprobacionActual; set => _nivelAprobacionActual = value; }
        public int? IdEstado { get => _idEstado; set => _idEstado = value; }
        public int? IdEstadoPublicacion { get => _idEstadoPublicacion; set => _idEstadoPublicacion = value; }
        public int? IdResponsableRevision { get => _idResponsableRevision; set => _idResponsableRevision = value; }
        public DateTime? FechaRevision { get => _fechaRevision; set => _fechaRevision = value; }
        public int? IdResponsablePublicacion { get => _idResponsablePublicacion; set => _idResponsablePublicacion = value; }
        public DateTime? FechaPublicacion { get => _fechaPublicacion; set => _fechaPublicacion = value; }
        public int? IdEvaluacionPadre { get => _idEvaluacionPadre; set => _idEvaluacionPadre = value; }
        public string? ObservacionesRevision { get => _observacionesRevision; set => _observacionesRevision = value; }
        public string? MotivoRechazo { get => _motivoRechazo; set => _motivoRechazo = value; }
        public DateTime? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public int? IdCreador { get => _idCreador; set => _idCreador = value; }
        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
        // Propiedades adicionales de JOINs
        public string? CodigoSeccion { get => _codigoSeccion; set => _codigoSeccion = value; }
        public string? NombreMateria { get => _nombreMateria; set => _nombreMateria = value; }
        public string? NombreModeloEvaluacion { get => _nombreModeloEvaluacion; set => _nombreModeloEvaluacion = value; }
        public string? CodigoModelo { get => _codigoModelo; set => _codigoModelo = value; }
        public string? NombrePeriodo { get => _nombrePeriodo; set => _nombrePeriodo = value; }
        public string? CodigoPeriodo { get => _codigoPeriodo; set => _codigoPeriodo = value; }
        public string? NombreEstado { get => _nombreEstado; set => _nombreEstado = value; }
        public string? NombreEstadoPublicacion { get => _nombreEstadoPublicacion; set => _nombreEstadoPublicacion = value; }
    }
}

