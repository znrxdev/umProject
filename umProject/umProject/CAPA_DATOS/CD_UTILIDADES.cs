using System;
using System.Text;

namespace CAPA_DATOS
{
    public static class CdUtilidades
    {
        public static string HashearContrasena(string plainTextPassword)
        {
            string hashString = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
            return hashString;
        }
        public static bool VerificarContrasena(string passwordAttempt, string storedHashString)
        {
            try
            {
                if (string.IsNullOrEmpty(storedHashString))
                    return false;

                return BCrypt.Net.BCrypt.Verify(passwordAttempt, storedHashString);
            }
            catch
            {
                return false;
            }
        }
    }
}
