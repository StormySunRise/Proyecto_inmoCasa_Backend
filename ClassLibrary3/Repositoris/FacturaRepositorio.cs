using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteReyesSalas.Repositorio
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        private readonly ApplicationDbContext _context;

        public FacturaRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Factura>> ObtenerTodasLasFacturasAsync()
        {
            return await _context.Facturas
                .Include(f => f.Proveedor)  // Incluye el proveedor relacionado
                .ToListAsync();
        }

        public async Task<Factura> ObtenerFacturaPorIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.Proveedor)  // Incluye el proveedor relacionado
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<bool> EliminarFacturaPorIdAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null)
            {
                return false; // La factura no existe
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return true; // La factura fue eliminada exitosamente
        }
        public async Task<Factura> AgregarAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return factura; // Retorna la factura agregada con su Id asignado
        }
    }
}


