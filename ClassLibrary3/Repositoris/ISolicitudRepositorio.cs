using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public interface ISolicitudRepositorio
    {
        Task AgregarAsync(Solicitud solicitud);
        Task<Solicitud> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Solicitud>> ObtenerTodasAsync();
        Task ActualizarAsync(Solicitud solicitud);
        Task EliminarAsync(int id);

    }
}
