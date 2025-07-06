using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoApp.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; } = null!;

        [Required]
        [StringLength(256)] // Para almacenar hash o contraseña cifrada
        public string Clave { get; set; } = null!;
        public string? Token { get; set; }
        public DateTime? TokenExpira { get; set; }
    }
}
