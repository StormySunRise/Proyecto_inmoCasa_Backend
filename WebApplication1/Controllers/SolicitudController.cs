using ClassLibrary3.Entidades;
using ClassLibrary3.Repositoris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudRepositorio _solicitudRepository;
        private readonly IClienteRepositorio _clienteRepository;

        public SolicitudController(ISolicitudRepositorio solicitudRepository, IClienteRepositorio clienteRepository)
        {
            _solicitudRepository = solicitudRepository;
            _clienteRepository = clienteRepository;
        }


        // GET: api/solicitud
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitudes()
        {
            var solicitudes = await _solicitudRepository.ObtenerTodasAsync();
            return Ok(solicitudes);
        }



        // GET: api/solicitud/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud>> GetSolicitud(int id)
        {
            var solicitud = await _solicitudRepository.ObtenerPorIdAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            return Ok(solicitud);
        }




        [HttpPost]
        public async Task<ActionResult<Solicitud>> PostSolicitud([FromBody] Solicitud solicitud)
        {
            if (solicitud == null)
            {
                return BadRequest("El objeto solicitud no puede ser nulo.");
            }

            // Verificar si el cliente asociado existe
            var cliente = await _clienteRepository.ObtenerClientePorIdAsync(solicitud.ClienteId);
            if (cliente == null)
            {
                return BadRequest($"Cliente con ID {solicitud.ClienteId} no existe.");
            }

            // Asignar la fecha actual si no se proporciona
            if (solicitud.FechaSolicitud == default)
            {
                solicitud.FechaSolicitud = DateTime.Now;
            }

            // Agregar la solicitud a la base de datos
            await _solicitudRepository.AgregarAsync(solicitud);

            // Devolver la solicitud creada con su nuevo ID
            return CreatedAtAction(nameof(GetSolicitud), new { id = solicitud.Id }, solicitud);
        }




        // PUT: api/solicitud/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitud(int id, [FromBody] Solicitud solicitud)
        {
            if (id != solicitud.Id)
            {
                return BadRequest(new { message = "El ID en la ruta no coincide con el ID en el cuerpo de la solicitud." });
            }

            var solicitudExistente = await _solicitudRepository.ObtenerPorIdAsync(id);
            if (solicitudExistente == null)
            {
                return NotFound(new { message = $"No se encontró una solicitud con el ID {id}." });
            }

            try
            {
                // Actualizar los campos de la solicitud existente
                solicitudExistente.ClienteId = solicitud.ClienteId;
                solicitudExistente.FechaSolicitud = solicitud.FechaSolicitud;
                solicitudExistente.Estado = solicitud.Estado;
                solicitudExistente.Tipo = solicitud.Tipo;
                solicitudExistente.Descripcion = solicitud.Descripcion;

                // Llamar al método de actualización del repositorio
                await _solicitudRepository.ActualizarAsync(solicitudExistente);

                return Ok(new { message = "Solicitud actualizada con éxito." });
            }
            catch (Exception ex)
            {
                // Loguear la excepción
                return StatusCode(500, new { message = "Ocurrió un error al actualizar la solicitud.", error = ex.Message });
            }
        }





        // DELETE: api/solicitud/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSolicitud(int id)
        {
            var solicitud = await _solicitudRepository.ObtenerPorIdAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            await _solicitudRepository.EliminarAsync(id);
            return NoContent();
        }
    }
}
