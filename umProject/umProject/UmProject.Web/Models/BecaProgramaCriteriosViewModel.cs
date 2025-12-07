using UmProject.Entities;
using System.Collections.Generic;

namespace UmProject.Web.Models
{
    public class BecaProgramaCriteriosViewModel
    {
        public BecaPrograma? Programa { get; set; }
        public List<BecaCriterio> Criterios { get; set; } = new();
        public BecaCriterio FormCriterio { get; set; } = new();
        public bool EsEdicion => FormCriterio?.IdBecaCriterio != null;
    }
}

