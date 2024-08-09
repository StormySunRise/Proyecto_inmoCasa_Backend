using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public interface IRolRepositorio
    {
        Task Agregar(Rol rol);
        Task<List<Rol>> ObtenerTodos(); // Nuevo método para obtener todos los roles
        Task<Rol> ObtenerPorIdAsync(int id); // Método para obtener un rol por ID
        Task ActualizarRol(Rol rol); // Método para actualizar un rol
        Task EliminarPorId(int id); // Renombrado a EliminarPorId

        Task<bool> RolExistsAsync(int rolId);

    }
}
