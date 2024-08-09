using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class VerificacionFactura
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        [JsonIgnore]
        public Factura? Factura { get; set; }
        public DateTime FechaVerificacion { get; set; }
        public string Resultado { get; set; } = null!;
    }
}
