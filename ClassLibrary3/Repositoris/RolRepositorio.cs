using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly ApplicationDbContext _context;

        public RolRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Agregar(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
        }
        public async Task<Rol?> ObtenerPorId(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<List<Rol>> ObtenerTodos()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task Actualizar(Rol rol)
        {
            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();
        }
        public async Task Eliminar(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Rol> ObtenerPorIdAsync(int id)
        {
            // Buscar el rol por ID
            var rol = await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rol == null)
            {
                // Manejo del caso cuando no se encuentra el rol
                throw new KeyNotFoundException($"Rol con ID {id} no encontrado.");
            }

            return rol;
        }

        public async Task ActualizarRol(Rol rol)
        {
            // Verificar si el rol existe antes de actualizar
            var rolExistente = await _context.Roles.FindAsync(rol.Id);
            if (rolExistente == null)
            {
                throw new KeyNotFoundException($"Rol con ID {rol.Id} no encontrado.");
            }

            _context.Entry(rolExistente).CurrentValues.SetValues(rol);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarPorId(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> RolExistsAsync(int rolId)
        {
            return await _context.Roles.AnyAsync(r => r.Id == rolId);
        }
    }
}
