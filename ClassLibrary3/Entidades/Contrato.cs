using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class Contrato
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SolicitudId { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public string Estado { get; set; } = null!;
        [Required]
        public string Terminos { get; set; } = null!;
        [Required]
        public int PropiedadId { get; set; }

        // Ignora estas propiedades en la serialización
        [JsonIgnore]
        public Solicitud? Solicitud { get; set; }
        [JsonIgnore]
        public Propiedad? Propiedad { get; set; }
    }
}

