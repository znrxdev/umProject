namespace UmProject.Data
{
    public static class Utilidades
    {
        public static string HashearContrasena(string plainTextPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
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

