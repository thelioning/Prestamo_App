using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;


namespace PrestamoApp.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres.")]
        public string NroDocumento { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        public string Apellido { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres.")]
        [EmailAddress(ErrorMessage = "El formato del email es inválido.")]
        public string? Correo { get; set; }
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres.")]
        public string? Telefono { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
