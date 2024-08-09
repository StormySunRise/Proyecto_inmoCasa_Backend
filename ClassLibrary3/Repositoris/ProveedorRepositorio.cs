using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteReyesSalas.Repositorio
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ProveedorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Proveedor> AgregarProveedorAsync(Proveedor proveedor)
        {

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;

        }

        public async Task<IEnumerable<Proveedor>> ObtenerTodosLosProveedoresAsync()
        {
            return await _context.Proveedores.ToListAsync();
        }

        public async Task<IEnumerable<Proveedor>> ObtenerProveedoresFiltradosAsync(string nombre, string tipo, string email)
        {
            var query = _context.Proveedores.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre));
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                query = query.Where(p => p.Tipo.Contains(tipo));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.Email.Contains(email));
            }

            return await query.ToListAsync();
        }
        public async Task<bool> EliminarProveedorAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return false; // Proveedor no encontrado
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Proveedor> ObtenerProveedorPorIdAsync(int id)
        {
            // Buscar el cliente por ID
            return await _context.Proveedores
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Proveedor> EditarProveedorAsync(Proveedor proveedor)
        {
            var proveedorExistente = await _context.Proveedores.FindAsync(proveedor.Id);
            if (proveedorExistente == null)
            {
                throw new KeyNotFoundException($"Proveedor con ID {proveedor.Id} no encontrado.");
            }

            // Actualizar las propiedades del proveedor existente
            proveedorExistente.Nombre = proveedor.Nombre;
            proveedorExistente.Email = proveedor.Email;
            proveedorExistente.Telefono = proveedor.Telefono;
            proveedorExistente.Direccion = proveedor.Direccion;
            proveedorExistente.Cedula = proveedor.Cedula;
            proveedorExistente.Tipo = proveedor.Tipo;
            proveedorExistente.Descripcion = proveedor.Descripcion;

            _context.Proveedores.Update(proveedorExistente);
            await _context.SaveChangesAsync();
            return proveedorExistente;
        }

    }
}

