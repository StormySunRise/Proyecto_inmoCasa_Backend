using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class Documento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Url { get; set; } = null!;

        [Required]
        public string Tipo { get; set; } = null!;

        [Required]
        public int SolicitudId { get; set; }
        [ForeignKey(nameof(SolicitudId))]
        public Solicitud Solicitud { get; set; } = null!;
    }
}
