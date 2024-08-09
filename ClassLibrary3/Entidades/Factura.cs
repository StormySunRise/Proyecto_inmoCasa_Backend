using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public int IdentificadorFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; } = null!;
        public ICollection<VerificacionFactura> Verificaciones { get; set; } = new List<VerificacionFactura>();
    }
}
