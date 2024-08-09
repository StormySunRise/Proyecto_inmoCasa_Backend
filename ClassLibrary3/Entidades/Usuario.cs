using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary3.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int RolId { get; set; }

        [JsonIgnore]
        public Rol? Rol { get; set; }
    }
}
