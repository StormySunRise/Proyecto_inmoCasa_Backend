using ClassLibrary3.Entidades;
using ClassLibrary3.Repositoris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        // GET: api/cliente/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteRepositorio.ObtenerClientePorIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // GET: api/cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteRepositorio.ObtenerTodosClientesAsync();
            return Ok(clientes);
        }

        // POST: api/cliente
        [HttpPost]
        public async Task<ActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }

            await _clienteRepositorio.CrearClienteAsync(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }



        // PUT: api/cliente/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            var clienteExistente = await _clienteRepositorio.ObtenerClientePorIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound();
            }

            await _clienteRepositorio.ActualizarClienteAsync(cliente);
            return NoContent();
        }



        // DELETE: api/cliente/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            var cliente = await _clienteRepositorio.ObtenerClientePorIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteRepositorio.EliminarClienteAsync(id);
            return NoContent();
        }


    }
}
