using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public class PropiedadRepositorio : IPropiedadRepositorio
    {
        private readonly ApplicationDbContext _context;

        public PropiedadRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Propiedad>> GetAllPropiedadesAsync()
        {
            return await _context.Propiedades.ToListAsync();
        }

        public async Task<Propiedad> GetPropiedadByIdAsync(int id)
        {
            return await _context.Propiedades.FindAsync(id);
        }

        public async Task<Propiedad> CreatePropiedadAsync(Propiedad propiedad)
        {
            if (propiedad == null)
            {
                throw new ArgumentNullException(nameof(propiedad));
            }

            // Asegurarse de que no estamos tratando de establecer el Propietario directamente
            propiedad.Propietario = null;

            // No es necesario crear una nueva instancia de Propiedad
            _context.Propiedades.Add(propiedad);
            await _context.SaveChangesAsync();
            return propiedad;
        }



        public async Task<bool> PropietarioExistsAsync(int propietarioId)  // Implementación del método
        {
            return await _context.Clientes.AnyAsync(c => c.Id == propietarioId);
        }
        public async Task<Propiedad> GetPropiedadAsync(int id)
        {
            return await _context.Propiedades.FindAsync(id);
        }

        public async Task<Propiedad> UpdatePropiedadAsync(Propiedad propiedad)
        {
            if (propiedad == null)
            {
                throw new ArgumentNullException(nameof(propiedad));
            }

            var existingPropiedad = await _context.Propiedades.FindAsync(propiedad.Id);
            if (existingPropiedad == null)
            {
                throw new ArgumentException("Propiedad no encontrada.");
            }

            // Actualizar las propiedades
            existingPropiedad.Direccion = propiedad.Direccion;
            existingPropiedad.Ciudad = propiedad.Ciudad;
            existingPropiedad.Tipo = propiedad.Tipo;
            existingPropiedad.Precio = propiedad.Precio;
            existingPropiedad.PropietarioId = propiedad.PropietarioId;
            existingPropiedad.Disponible = propiedad.Disponible;
            existingPropiedad.Seleccionada = propiedad.Seleccionada;

            await _context.SaveChangesAsync();
            return existingPropiedad;
        }

        public async Task<bool> DeletePropiedadAsync(int id)
        {
            var propiedad = await _context.Propiedades.FindAsync(id);
            if (propiedad == null)
            {
                return false;
            }

            _context.Propiedades.Remove(propiedad);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
