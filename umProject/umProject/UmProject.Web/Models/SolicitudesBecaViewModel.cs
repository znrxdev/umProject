using UmProject.Entities;
using System.Collections.Generic;

namespace UmProject.Web.Models
{
    public class SolicitudesBecaViewModel
    {
        public List<BecaPrograma> ProgramasDisponibles { get; set; } = new();
        public List<EstudianteSolicitudBeca> MisSolicitudes { get; set; } = new();
        public List<EstudianteSolicitudBecaHistorial> HistorialSolicitudes { get; set; } = new();
        public string? MensajeResultado { get; set; }
    }
}

