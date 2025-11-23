using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDADES
{
    public class CeEvaluacionesModelos
    {
        private int? _idEvaluacionModelo;
        private int? _idMateriaPeriodo;
        private int? _idTipoEvaluacion;
        private string? _codigoModelo;
        private string? _nombreEvaluacion;
        private string? _concepto;
        private decimal? _calificacionMaxima;
        private decimal? _pesoPorcentual;
        private int? _orden;
        private bool? _requiereAprobacion;
        private int? _versionConfiguracion;
        private int? _idMetodoCalculo;
        private string? _rubricaDetalle;
        private decimal? _porcentajeMinimoAprobacion;
        private byte? _nivelesRevision;
        private int? _idRolAprobador;
        private bool? _permiteRecalculo;
        private int? _tiempoMaximoCarga;
        private DateTime? _fechaInicio;
        private DateTime? _fechaFin;
        private bool? _activo;
        private DateTime? _fechaCreacion;
        private DateTime? _fechaModificacion;
        private int? _idCreador;
        private int? _idModificador;
        private int? _idTransaccion;
        private string? _nombreMateria;
        private string? _nombrePeriodo;
        private string? _nombreTipoEvaluacion;

        public int? IdEvaluacionModelo { get => _idEvaluacionModelo; set => _idEvaluacionModelo = value; }
        public int? IdMateriaPeriodo { get => _idMateriaPeriodo; set => _idMateriaPeriodo = value; }
        public int? IdTipoEvaluacion { get => _idTipoEvaluacion; set => _idTipoEvaluacion = value; }
        public string? CodigoModelo { get => _codigoModelo; set => _codigoModelo = value; }
        public string? NombreEvaluacion { get => _nombreEvaluacion; set => _nombreEvaluacion = value; }
        public string? Concepto { get => _concepto; set => _concepto = value; }
        public decimal? CalificacionMaxima { get => _calificacionMaxima; set => _calificacionMaxima = value; }
        public decimal? PesoPorcentual { get => _pesoPorcentual; set => _pesoPorcentual = value; }
        public int? Orden { get => _orden; set => _orden = value; }
        public bool? RequiereAprobacion { get => _requiereAprobacion; set => _requiereAprobacion = value; }
        public int? VersionConfiguracion { get => _versionConfiguracion; set => _versionConfiguracion = value; }
        public int? IdMetodoCalculo { get => _idMetodoCalculo; set => _idMetodoCalculo = value; }
        public string? RubricaDetalle { get => _rubricaDetalle; set => _rubricaDetalle = value; }
        public decimal? PorcentajeMinimoAprobacion { get => _porcentajeMinimoAprobacion; set => _porcentajeMinimoAprobacion = value; }
        public byte? NivelesRevision { get => _nivelesRevision; set => _nivelesRevision = value; }
        public int? IdRolAprobador { get => _idRolAprobador; set => _idRolAprobador = value; }
        public bool? PermiteRecalculo { get => _permiteRecalculo; set => _permiteRecalculo = value; }
        public int? TiempoMaximoCarga { get => _tiempoMaximoCarga; set => _tiempoMaximoCarga = value; }
        public DateTime? FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime? FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public bool? Activo { get => _activo; set => _activo = value; }
        public DateTime? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
        public int? IdCreador { get => _idCreador; set => _idCreador = value; }
        public int? IdModificador { get => _idModificador; set => _idModificador = value; }
        public int? IdTransaccion { get => _idTransaccion; set => _idTransaccion = value; }
        public string? NombreMateria { get => _nombreMateria; set => _nombreMateria = value; }
        public string? NombrePeriodo { get => _nombrePeriodo; set => _nombrePeriodo = value; }
        public string? NombreTipoEvaluacion { get => _nombreTipoEvaluacion; set => _nombreTipoEvaluacion = value; }
    }
}

