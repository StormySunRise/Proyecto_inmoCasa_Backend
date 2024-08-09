using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class Propiedad
    {
        public int Id { get; set; }
        [Required]
        public string Direccion { get; set; } = null!;
        [Required]
        public string Ciudad { get; set; } = null!;
        [Required]
        public string Tipo { get; set; } = null!;
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int PropietarioId { get; set; }

        [JsonIgnore]
        public Cliente? Propietario { get; set; } // Cambiado a nullable

        public bool Disponible { get; set; }
        public bool Seleccionada { get; set; }

        [JsonIgnore]
        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
