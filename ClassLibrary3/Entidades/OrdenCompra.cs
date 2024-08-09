using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class OrdenCompra
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; } = null!;
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; } = null!;
        public decimal MontoTotal { get; set; }
        public DateTime FechaOrden { get; set; }
        public string? Observaciones { get; set; }
    }
}
