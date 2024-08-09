using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AgregarUsuarioAsync(Usuario usuario)
        {
            // Asignar un rol por defecto, por ejemplo, el rol con ID 1
            var rolPorDefecto = await _context.Roles.FindAsync(1);
            if (rolPorDefecto == null)
            {
                throw new InvalidOperationException("Rol por defecto no encontrado");
            }
            usuario.Rol = rolPorDefecto;
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> ObtenerTodosUsuariosAsync()
        {
            return await _context.Usuarios.Include(u => u.Rol).ToListAsync();
        }

        public async Task<List<Usuario>> FiltrarUsuariosAsync(int? id = null, string? nombre = null, string? apellido = null, int? rolId = null)
        {
            var query = _context.Usuarios.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(u => u.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(u => u.Nombre.Contains(nombre));
            }

            if (!string.IsNullOrEmpty(apellido))
            {
                query = query.Where(u => u.Apellido.Contains(apellido));
            }

            if (rolId.HasValue)
            {
                query = query.Where(u => u.RolId == rolId.Value);
            }

            return await query.ToListAsync();
        }
    }
}

