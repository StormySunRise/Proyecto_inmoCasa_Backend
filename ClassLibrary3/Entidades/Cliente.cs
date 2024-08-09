using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Apellido { get; set; } = null!;

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string FechaIngreso { get; set; } = null!;

        public int NumPropiedades { get; set; }

        public bool Seleccionado { get; set; }

        // Inicializar las colecciones como opcionales
        public ICollection<Propiedad> Propiedades { get; set; } = new List<Propiedad>();
        public ICollection<Solicitud> Solicitudes { get; set; } = new List<Solicitud>();
    }
}
