using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoApp.Models
{
    public class Resumen
    {
        public int TotalClientes { get; set; }
        public int PrestamosPendientes { get; set; }
        public int PrestamosCancelados { get; set; }
        public decimal InteresAcumulado { get; set; }
    }
}
