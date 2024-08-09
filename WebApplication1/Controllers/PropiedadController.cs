using ClassLibrary3.Entidades;
using ClassLibrary3.Repositoris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadController : ControllerBase
    {
        private readonly IPropiedadRepositorio _propiedadRepository;

        public PropiedadController(IPropiedadRepositorio propiedadRepository)
        {
            _propiedadRepository = propiedadRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Propiedad>> CreatePropiedad(Propiedad propiedad)
        {
            try
            {
                var nuevaPropiedad = await _propiedadRepository.CreatePropiedadAsync(propiedad);
                return CreatedAtAction(nameof(GetPropiedad), new { id = nuevaPropiedad.Id }, nuevaPropiedad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al crear la propiedad: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Propiedad>> GetPropiedad(int id)
        {
            var propiedad = await _propiedadRepository.GetPropiedadAsync(id);

            if (propiedad == null)
            {
                return NotFound();
            }

            return propiedad;
        }


        // GET: api/propiedad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Propiedad>>> GetAllPropiedades()
        {
            try
            {
                var propiedades = await _propiedadRepository.GetAllPropiedadesAsync();
                return Ok(propiedades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al obtener las propiedades: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePropiedad(int id, Propiedad propiedad)
        {
            if (id != propiedad.Id)
            {
                return BadRequest("ID de propiedad no coincide.");
            }

            try
            {
                var propiedadExistente = await _propiedadRepository.GetPropiedadByIdAsync(id);
                if (propiedadExistente == null)
                {
                    return NotFound();
                }

                var propiedadActualizada = await _propiedadRepository.UpdatePropiedadAsync(propiedad);
                return Ok(propiedadActualizada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al actualizar la propiedad: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropiedad(int id)
        {
            try
            {
                var propiedadExistente = await _propiedadRepository.GetPropiedadByIdAsync(id);
                if (propiedadExistente == null)
                {
                    return NotFound();
                }

                var resultado = await _propiedadRepository.DeletePropiedadAsync(id);
                if (!resultado)
                {
                    return StatusCode(500, "Error interno del servidor al eliminar la propiedad.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al eliminar la propiedad: {ex.Message}");
            }
        }
    }

}
