using ClassLibrary3.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParteReyesSalas.Repositorio;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificacionController : ControllerBase
    {
        private readonly IVerificacionFacturaRepositorio _verificacionRepository;

        public VerificacionController(IVerificacionFacturaRepositorio verificacionRepository)
        {
            _verificacionRepository = verificacionRepository;
        }


        // GET: api/verificacion
        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasVerificaciones()
        {
            try
            {
                var verificaciones = await _verificacionRepository.ObtenerTodasLasVerificacionesAsync();
                return Ok(verificaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener las verificaciones: {ex.Message}");
            }


        }


        // POST: api/verificacion
        [HttpPost]
        public async Task<IActionResult> AgregarVerificacion([FromBody] VerificacionFactura verificacion)
        {
            if (verificacion == null)
            {
                return BadRequest("La verificación de factura no puede ser nula.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var nuevaVerificacion = await _verificacionRepository.AgregarVerificacionAsync(verificacion);
                return CreatedAtAction(nameof(AgregarVerificacion), new { id = nuevaVerificacion.Id }, nuevaVerificacion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al agregar la verificación: {ex.Message}");
            }
        }


        // GET: api/verificacion/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVerificacionPorId(int id)
        {
            try
            {
                var verificacion = await _verificacionRepository.ObtenerVerificacionPorIdAsync(id);
                if (verificacion == null)
                {
                    return NotFound();
                }
                return Ok(verificacion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener la verificación: {ex.Message}");
            }
        }

        // PUT: api/verificacion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVerificacion(int id, [FromBody] VerificacionFactura verificacion)
        {
            if (verificacion == null || verificacion.Id != id)
            {
                return BadRequest("La verificación de factura es inválida o los IDs no coinciden.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var verificacionActualizada = await _verificacionRepository.ActualizarVerificacionAsync(verificacion);
                if (verificacionActualizada == null)
                {
                    return NotFound();
                }
                return Ok(verificacionActualizada);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar la verificación: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVerificacion(int id)
        {
            try
            {
                var resultado = await _verificacionRepository.EliminarVerificacionAsync(id);
                if (!resultado)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar la verificación: {ex.Message}");
            }
        }
    }
}
