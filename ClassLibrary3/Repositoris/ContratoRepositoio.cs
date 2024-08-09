using ClassLibrary3.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3.Repositoris
{
    public class ContratoRepositoio : IContratoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ContratoRepositoio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contrato> CreateAsync(Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();
            return contrato;
        }
        public async Task<IEnumerable<Contrato>> GetAllAsync()
        {
            return await _context.Contratos.ToListAsync();
        }
        public async Task<Contrato> GetByIdAsync(int id)
        {
            return await _context.Contratos.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato != null)
            {
                _context.Contratos.Remove(contrato);
                await _context.SaveChangesAsync();
            }
        }

    }
}
