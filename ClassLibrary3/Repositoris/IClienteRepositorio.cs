using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public interface IClienteRepositorio
    {
        Task<Cliente> ObtenerClientePorIdAsync(int id);
        Task<IEnumerable<Cliente>> ObtenerTodosClientesAsync();
        Task CrearClienteAsync(Cliente cliente);
        Task ActualizarClienteAsync(Cliente cliente);
        Task EliminarClienteAsync(int id);

    }
}
