using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParteReyesSalas.Repositorio
{
    public interface IVerificacionFacturaRepositorio
    {
        Task<List<VerificacionFactura>> ObtenerTodasLasVerificacionesAsync();
        Task<VerificacionFactura> ObtenerVerificacionPorIdAsync(int id);
        Task<VerificacionFactura> AgregarVerificacionAsync(VerificacionFactura verificacion);
        Task<VerificacionFactura> ActualizarVerificacionAsync(VerificacionFactura verificacion);
        Task<bool> EliminarVerificacionAsync(int id); // Nuevo método

    }
}
