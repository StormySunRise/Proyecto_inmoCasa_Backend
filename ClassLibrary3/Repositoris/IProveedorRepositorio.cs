using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteReyesSalas.Repositorio
{
    public interface IProveedorRepositorio
    {
        Task<Proveedor> AgregarProveedorAsync(Proveedor proveedor);
        Task<IEnumerable<Proveedor>> ObtenerTodosLosProveedoresAsync();
        Task<IEnumerable<Proveedor>> ObtenerProveedoresFiltradosAsync(string nombre, string tipo, string email);

        Task<bool> EliminarProveedorAsync(int id); // Método para eliminar un proveedor

        Task<Proveedor> ObtenerProveedorPorIdAsync(int id);//Provedor por ID

        Task<Proveedor> EditarProveedorAsync(Proveedor proveedor);


    }
}
