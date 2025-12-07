using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UmProject.Entities
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public bool Required { get; set; } = false;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Si el valor es null o vacío
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                // Si es requerido, retornar error
                if (Required)
                {
                    return new ValidationResult("La contraseña es obligatoria.");
                }
                // Si no es requerido (edición), permitir
                return ValidationResult.Success;
            }

            var password = value.ToString()!;

            // Validar longitud mínima de 8 caracteres
            if (password.Length < 8)
            {
                return new ValidationResult("La contraseña debe tener al menos 8 caracteres.");
            }

            // Validar que tenga al menos una minúscula
            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                return new ValidationResult("La contraseña debe contener al menos una letra minúscula.");
            }

            // Validar que tenga al menos una mayúscula
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return new ValidationResult("La contraseña debe contener al menos una letra mayúscula.");
            }

            // Validar que tenga al menos un carácter especial
            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            {
                return new ValidationResult("La contraseña debe contener al menos un carácter especial (!@#$%^&*()_+-=[]{}|;:,.<>/?).");
            }

            return ValidationResult.Success;
        }
    }
}

