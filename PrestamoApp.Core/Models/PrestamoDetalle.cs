using System;
using System.ComponentModel.DataAnnotations;

namespace PrestamoApp.Models
{
    public class PrestamoDetalle
    {
        [Key]
        public int IdPrestamoDetalle { get; set; }

        public DateTime FechaPago { get; set; }

        public int NroCuota { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal MontoCuota { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = null!;

        public DateTime? FechaPagado { get; set; }  // Nullable, en caso de que aún no se haya pagado
    }
}
