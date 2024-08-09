using ClassLibrary3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public interface IContratoRepositorio
    {
        Task<Contrato> CreateAsync(Contrato contrato);
        Task<IEnumerable<Contrato>> GetAllAsync(); // Nuevo método para obtener todos los contratos
        Task<Contrato> GetByIdAsync(int id); // Método para obtener un contrato por ID
        Task DeleteAsync(int id); // Nuevo método para eliminar un contrato por ID

    }
}