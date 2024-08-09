using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public interface IPropiedadRepositorio
    {
        Task<IEnumerable<Propiedad>> GetAllPropiedadesAsync();
        Task<Propiedad> GetPropiedadByIdAsync(int id);
        Task<Propiedad> CreatePropiedadAsync(Propiedad propiedad);
        Task<bool> PropietarioExistsAsync(int propietarioId);      // Método para verificar si existe el propietario

        Task<Propiedad> UpdatePropiedadAsync(Propiedad propiedad);  // Añadido para actualizar

        Task<Propiedad> GetPropiedadAsync(int id);
        Task<bool> DeletePropiedadAsync(int id); // Añadir el método para eliminar


    }
}
