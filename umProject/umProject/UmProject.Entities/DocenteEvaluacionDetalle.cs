namespace UmProject.Entities
{
    public class DocenteEvaluacionDetalle
    {
        // Datos del Alumno
        public int IdEvaluacionAlumno { get; set; }
        public string? CodigoRegistro { get; set; }
        public string? NombreEstudiante { get; set; }
        public string? UsuarioEstudiante { get; set; }
        public string? ValorDocumentoEstudiante { get; set; }

        // Datos de la Instancia de Evaluación
        public int? IdEvaluacionInstancia { get; set; }
        public string? CodigoInstancia { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaLimite { get; set; }

        // Datos del Modelo de Evaluación
        public int? IdEvaluacionModelo { get; set; }
        public string? CodigoModelo { get; set; }
        public string? NombreEvaluacion { get; set; }
        public string? Concepto { get; set; }
        public string? TipoEvaluacion { get; set; }
        public decimal CalificacionMaxima { get; set; }

        // Datos de la Materia y Sección
        public string? NombreMateria { get; set; }
        public string? CodigoMateria { get; set; }
        public string? CodigoSeccion { get; set; }
        public string? NombrePeriodo { get; set; }
        public string? CodigoPeriodo { get; set; }

        // Resultado del Alumno
        public decimal PuntajeObtenido { get; set; }
        public decimal? PorcentajeLogrado { get; set; }
        public decimal? PuntajeNormalizado { get; set; }
        public bool EsRecalculo { get; set; }
        public int NumeroRecalculo { get; set; }
        public string? MotivoAjuste { get; set; }
        public string? Observaciones { get; set; }

        // Usuarios involucrados
        public int? IdUsuarioEvaluador { get; set; }
        public string? UsuarioEvaluador { get; set; }
        public string? NombreEvaluador { get; set; }
        public int? IdUsuarioValidador { get; set; }
        public string? UsuarioValidador { get; set; }
        public string? NombreValidador { get; set; }
        public DateTime? FechaValidacion { get; set; }

        // Estados
        public string? EstadoEvaluacion { get; set; }
        public string? EstadoPublicacion { get; set; }

        // Firma
        public bool FirmadoPorEstudiante { get; set; }
        public string? FirmaDigital { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public DateTime? FechaPublicacionResultado { get; set; }

        // Fechas de auditoría
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}

