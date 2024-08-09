using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteReyesSalas.Repositorio
{
    public class VerificacionFacturaRepositorio : IVerificacionFacturaRepositorio
    {
        private readonly ApplicationDbContext _context;

        public VerificacionFacturaRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<VerificacionFactura>> ObtenerTodasLasVerificacionesAsync()
        {
            return await _context.Verificaciones
                .Include(vf => vf.Factura) // Incluye la factura asociada
                .ToListAsync();
        }

        public async Task<VerificacionFactura> ObtenerVerificacionPorIdAsync(int id)
        {
            return await _context.Verificaciones
                .Include(vf => vf.Factura) // Incluye la factura asociada
                .FirstOrDefaultAsync(vf => vf.Id == id);
        }

        public async Task<VerificacionFactura> AgregarVerificacionAsync(VerificacionFactura verificacion)
        {
            _context.Verificaciones.Add(verificacion);
            await _context.SaveChangesAsync();
            return verificacion;
        }

        public async Task<VerificacionFactura> ActualizarVerificacionAsync(VerificacionFactura verificacion)
        {
            var verificacionExistente = await _context.Verificaciones
                .FirstOrDefaultAsync(vf => vf.Id == verificacion.Id);

            if (verificacionExistente == null)
            {
                return null;
            }

            _context.Entry(verificacionExistente).CurrentValues.SetValues(verificacion);
            await _context.SaveChangesAsync();

            return verificacionExistente;
        }

        public async Task<bool> EliminarVerificacionAsync(int id)
        {
            var verificacionExistente = await _context.Verificaciones
                .FirstOrDefaultAsync(vf => vf.Id == id);

            if (verificacionExistente == null)
            {
                return false;
            }

            _context.Verificaciones.Remove(verificacionExistente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
