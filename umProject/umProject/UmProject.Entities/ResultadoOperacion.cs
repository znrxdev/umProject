namespace UmProject.Entities
{
    public class ResultadoOperacion
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public bool Exitoso => Codigo != -1;
    }
}

