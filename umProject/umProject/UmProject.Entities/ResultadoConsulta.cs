namespace UmProject.Entities
{
    /// <summary>
    /// Clase para encapsular resultados de consultas que devuelven datos junto con información de error
    /// </summary>
    /// <typeparam name="T">Tipo de datos devueltos (puede ser List<TEntity> o cualquier otro tipo)</typeparam>
    public class ResultadoConsulta<T>
    {
        public T? Datos { get; set; }
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public bool Exitoso => Codigo != -1;
        
        /// <summary>
        /// Indica si hay un error controlado desde la base de datos
        /// </summary>
        public bool HayError => Codigo == -1;
    }
}

