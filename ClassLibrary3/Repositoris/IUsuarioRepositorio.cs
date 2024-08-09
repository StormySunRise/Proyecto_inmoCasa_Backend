using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public interface IUsuarioRepositorio
    {
        Task AgregarUsuarioAsync(Usuario usuario);
        Task<List<Usuario>> ObtenerTodosUsuariosAsync();
        Task<List<Usuario>> FiltrarUsuariosAsync(int? id = null, string? nombre = null, string? apellido = null, int? rolId = null);

    }
}
