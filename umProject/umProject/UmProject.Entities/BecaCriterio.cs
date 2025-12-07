using System;
using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class BecaCriterio
    {
        public int? IdBecaCriterio { get; set; }

        [Required]
        public int? IdPrograma { get; set; }

        [Required]
        [StringLength(50)]
        public string? Codigo { get; set; }

        [Required]
        [StringLength(150)]
        public string? NombreCriterio { get; set; }

        [Required]
        [StringLength(100)]
        public string? ClaveCriterio { get; set; }

        public string? ValorCriterio { get; set; }

        [Required]
        public string? TipoDatoValor { get; set; }

        [Required]
        public int? IdTipoCriterio { get; set; }

        [Required]
        public string? OperadorComparacion { get; set; }

        public decimal? ValorNumericoMinimo { get; set; }
        public decimal? ValorNumericoMaximo { get; set; }
        public string? ValorTexto { get; set; }
        public bool ValorBooleano { get; set; } = false;
        public string? FuenteValidacion { get; set; }
        public string? ExpresionValidacion { get; set; }
        public bool RequiereSoporte { get; set; } = false;
        public string? Observaciones { get; set; }
        public bool Activo { get; set; } = true;

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
    }
}

