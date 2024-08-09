using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> ObtenerClientePorIdAsync(int id)
        {
            return await _context.Clientes
                                 .Include(c => c.Solicitudes) // Incluye solicitudes relacionadas
                                 .Include(c => c.Propiedades) // Incluye propiedades relacionadas
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosClientesAsync()
        {
            return await _context.Clientes
                                 .Include(c => c.Solicitudes) // Incluye solicitudes relacionadas
                                 .Include(c => c.Propiedades) // Incluye propiedades relacionadas
                                 .ToListAsync();
        }

        public async Task CrearClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarClienteAsync(Cliente cliente)
        {
            var clienteExistente = await _context.Clientes.FindAsync(cliente.Id);
            if (clienteExistente != null)
            {
                clienteExistente.Nombre = cliente.Nombre;
                clienteExistente.Apellido = cliente.Apellido;
                // Actualiza otras propiedades según sea necesario
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarClienteAsync(int id)
        {
            var cliente = await _context.Clientes
                                        .Include(c => c.Solicitudes) // Incluye solicitudes relacionadas
                                        .Include(c => c.Propiedades) // Incluye propiedades relacionadas
                                        .FirstOrDefaultAsync(c => c.Id == id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExisteClienteAsync(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.Id == id);
        }

    }
}

