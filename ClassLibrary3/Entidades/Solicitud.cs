using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class Solicitud
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ClienteId))]

        public Cliente? Cliente { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }
        [Required]
        public string Estado { get; set; } = null!;
        [Required]
        public string Tipo { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
        [JsonIgnore]
        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
