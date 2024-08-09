using ClassLibrary3.Entidades;
using ClassLibrary3.Repositoris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoRepositorio _contratoRepositorio;

        public ContratoController(IContratoRepositorio contratoRepositorio)
        {
            _contratoRepositorio = contratoRepositorio;
        }

        [HttpPost]
        public async Task<ActionResult<Contrato>> CreateContrato([FromBody] Contrato contrato)
        {
            var createdContrato = await _contratoRepositorio.CreateAsync(contrato);
            return CreatedAtAction(nameof(CreateContrato), new { id = createdContrato.Id }, createdContrato);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratos()
        {
            var contratos = await _contratoRepositorio.GetAllAsync();
            return Ok(contratos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contrato>> GetContratoById(int id)
        {
            var contrato = await _contratoRepositorio.GetByIdAsync(id);

            if (contrato == null)
            {
                return NotFound();
            }

            return Ok(contrato);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var contrato = await _contratoRepositorio.GetByIdAsync(id);

            if (contrato == null)
            {
                return NotFound();
            }

            await _contratoRepositorio.DeleteAsync(id);
            return NoContent();
        }
    }

}