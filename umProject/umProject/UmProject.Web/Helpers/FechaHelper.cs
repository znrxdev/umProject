using System;
using System.Globalization;

namespace UmProject.Web.Helpers
{
    public static class FechaHelper
    {
        /// <summary>
        /// Parsea una fecha desde formato DD/MM/YYYY a DateTime
        /// </summary>
        public static bool TryParseFecha(string fechaString, out DateTime fecha)
        {
            fecha = DateTime.MinValue;
            
            if (string.IsNullOrWhiteSpace(fechaString))
                return false;

            // Intentar parsear en formato DD/MM/YYYY
            var formatos = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            
            foreach (var formato in formatos)
            {
                if (DateTime.TryParseExact(fechaString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    return true;
                }
            }

            // Si no funciona con formato específico, intentar parseo normal (por si viene en otro formato)
            return DateTime.TryParse(fechaString, CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
        }

        /// <summary>
        /// Convierte un DateTime a formato DD/MM/YYYY para mostrar en el frontend
        /// </summary>
        public static string ToFechaString(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convierte un DateTime a formato YYYY-MM-DD para enviar a SQL
        /// </summary>
        public static string ToFechaSql(DateTime fecha)
        {
            return fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convierte una fecha string en formato DD/MM/YYYY a formato YYYY-MM-DD para SQL
        /// </summary>
        public static string ConvertirFechaParaSql(string fechaString)
        {
            if (TryParseFecha(fechaString, out DateTime fecha))
            {
                return ToFechaSql(fecha);
            }
            return fechaString; // Si no se puede parsear, retornar el string original
        }
    }
}

