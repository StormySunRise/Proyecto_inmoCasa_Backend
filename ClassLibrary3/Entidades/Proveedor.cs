using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string? Descripcion { get; set; }

        public ICollection<OrdenCompra> OrdenesCompra { get; set; } = new List<OrdenCompra>();
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}
