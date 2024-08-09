using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteReyesSalas.Repositorio
{
    public class OrdenCompraRepositorio : IOrdenCompraRepositorio
    {
        private readonly ApplicationDbContext _context;

        public OrdenCompraRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdenCompra>> ObtenerTodasLasOrdenesCompraAsync()
        {
            return await _context.OrdenesCompra
                .Include(oc => oc.Proveedor) // Incluye el proveedor relacionado
                .ToListAsync();
        }

        public async Task<OrdenCompra> AgregarOrdenCompraAsync(OrdenCompra ordenCompra)
        {
            // Asegúrate de incluir el proveedor en la orden de compra
            // Esto puede ser necesario si estás trabajando con relaciones en Entity Framework
            // y quieres incluir los datos del proveedor en la respuesta
            _context.OrdenesCompra.Add(ordenCompra);
            await _context.SaveChangesAsync();

            // Cargar el proveedor relacionado
            var ordenCompraConProveedor = await _context.OrdenesCompra
                .Include(o => o.Proveedor)
                .SingleOrDefaultAsync(o => o.Id == ordenCompra.Id);

            return ordenCompraConProveedor!;
        }



        public async Task<OrdenCompra> ObtenerOrdenCompraPorIdAsync(int id)
        {
            return await _context.OrdenesCompra
                .Include(oc => oc.Proveedor) // Incluye el proveedor relacionado
                .Where(oc => oc.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrdenCompra>> ObtenerOrdenesCompraFiltradasAsync(
        string numeroOrden,
        string nombreProveedor,
        string observaciones)
        {
            var query = _context.OrdenesCompra.AsQueryable();

            if (!string.IsNullOrEmpty(numeroOrden))
            {
                query = query.Where(oc => oc.NumeroOrden.Contains(numeroOrden));
            }

            if (!string.IsNullOrEmpty(nombreProveedor))
            {
                query = query.Where(oc => oc.Proveedor.Nombre.Contains(nombreProveedor));
            }

            if (!string.IsNullOrEmpty(observaciones))
            {
                query = query.Where(oc => oc.Observaciones.Contains(observaciones));
            }

            return await query
                .Include(oc => oc.Proveedor) // Incluye el proveedor relacionado
                .ToListAsync();
        }
    }
}
