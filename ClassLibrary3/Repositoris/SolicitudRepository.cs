using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public class SolicitudRepository : ISolicitudRepositorio
    {
        private readonly ApplicationDbContext _context;

        public SolicitudRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AgregarAsync(Solicitud solicitud)
        {
            _context.Solicitudes.Add(solicitud);
            await _context.SaveChangesAsync();
        }

        public async Task<Solicitud> ObtenerPorIdAsync(int id)
        {
            return await _context.Solicitudes
                .Include(s => s.Cliente) // Incluye el cliente relacionado
                .Include(s => s.Documentos) // Incluye los documentos relacionados
                .Include(s => s.Contratos) // Incluye los contratos relacionados
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Solicitud>> ObtenerTodasAsync()
        {
            return await _context.Solicitudes
                .Include(s => s.Cliente) // Incluye el cliente relacionado
                .Include(s => s.Documentos) // Incluye los documentos relacionados
                .Include(s => s.Contratos) // Incluye los contratos relacionados
                .ToListAsync();
        }

        public async Task ActualizarAsync(Solicitud solicitud)
        {
            _context.Solicitudes.Update(solicitud);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var solicitud = await ObtenerPorIdAsync(id);
            if (solicitud != null)
            {
                _context.Solicitudes.Remove(solicitud);
                await _context.SaveChangesAsync();
            }
        }
    }
}
