using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteReyesSalas.Repositorio
{
    public interface IOrdenCompraRepositorio
    {
        Task<IEnumerable<OrdenCompra>> ObtenerTodasLasOrdenesCompraAsync();
        Task<OrdenCompra> AgregarOrdenCompraAsync(OrdenCompra ordenCompra);

        Task<OrdenCompra> ObtenerOrdenCompraPorIdAsync(int id);

        Task<IEnumerable<OrdenCompra>> ObtenerOrdenesCompraFiltradasAsync(
               string numeroOrden,
               string nombreProveedor,
               string observaciones);

    }
}
