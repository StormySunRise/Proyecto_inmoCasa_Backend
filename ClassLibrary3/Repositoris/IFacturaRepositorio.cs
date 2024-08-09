using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteReyesSalas.Repositorio
{
    public interface IFacturaRepositorio
    {
        
            Task<List<Factura>> ObtenerTodasLasFacturasAsync();

            Task<Factura> ObtenerFacturaPorIdAsync(int id); // Método para buscar por ID
            Task<bool> EliminarFacturaPorIdAsync(int id); // Método para eliminar por ID
            Task<Factura> AgregarAsync(Factura factura); // Método para agregar una nueva factura

        
    }
}
