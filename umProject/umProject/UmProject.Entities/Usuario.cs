using System.ComponentModel.DataAnnotations;

namespace UmProject.Entities
{
    public class Usuario
    {
        public int? IdUsuario { get; set; }
        public int? IdPersona { get; set; }
        public string? UsuarioNombre { get; set; }
        
        [PasswordValidation(Required = false, ErrorMessage = "La contraseña no cumple con los requisitos de seguridad.")]
        public string? Contrasena { get; set; }
        
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public string? UltimaSesion { get; set; }
        public string? UltimoCambioContrasena { get; set; }
        public int? IdCreador { get; set; }
        public int? IdModificador { get; set; }
        public int? IdTransaccion { get; set; }
        public int? IdEstado { get; set; }
        public string? ValorDocumento { get; set; }
        public string? NombreCompleto { get; set; }
    }
}

