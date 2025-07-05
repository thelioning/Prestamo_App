using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoApp.Models
{
    public class Moneda
    {
        [Key]
        public int MonedaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string Simbolo { get; set; } = null!;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
