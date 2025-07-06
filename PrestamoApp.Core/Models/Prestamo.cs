using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoApp.Models
{
    public class Prestamo
    {
        [Key]
        public int PrestamoId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set; } = null!;

        [Required]
        public int MonedaId { get; set; }

        [ForeignKey(nameof(MonedaId))]
        public Moneda Moneda { get; set; } = null!;

        public DateTime FechaInicioPago { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoPrestamo { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal InteresPorcentaje { get; set; }

        public int NroCuotas { get; set; }

        [Required]
        [StringLength(20)]
        public string FormaDePago { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorPorCuota { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorInteres { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = null!;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public List<PrestamoDetalle> PrestamoDetalle { get; set; } = new();
    }
}
