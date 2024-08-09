using ClassLibrary3.Entidades;
using ClassLibrary3.Repositoris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolRepositorio _rolRepositorio;

        public RolController(IRolRepositorio rolRepositorio)
        {
            _rolRepositorio = rolRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarRol([FromBody] Rol rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _rolRepositorio.Agregar(rol);
                return CreatedAtAction(nameof(AgregarRol), new { id = rol.Id }, rol);
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar la excepción
                return StatusCode(500, "Ocurrió un error al agregar el rol.");
            }
        }


        // Endpoint para obtener todos los roles
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosRoles()
        {
            try
            {
                var roles = await _rolRepositorio.ObtenerTodos();
                return Ok(roles); // Devuelve 200 con la lista de roles
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar la excepción
                return StatusCode(500, "Ocurrió un error al obtener los roles.");
            }
        }

        // Endpoint para obtener un rol por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRolPorId(int id)
        {
            try
            {
                var rol = await _rolRepositorio.ObtenerPorIdAsync(id);
                return Ok(rol); // Devuelve 200 con el rol encontrado
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Devuelve 404 si el rol no se encuentra
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar la excepción
                return StatusCode(500, "Ocurrió un error al obtener el rol.");
            }
        }

        // Endpoint para actualizar un rol
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest("El ID del rol no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _rolRepositorio.ActualizarRol(rol); // Actualizado a usar ActualizarRol
                return NoContent(); // Devuelve 204 si la actualización fue exitosa
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar la excepción
                return StatusCode(500, "Ocurrió un error al actualizar el rol.");
            }
        }

        // Endpoint para eliminar un rol
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            try
            {
                await _rolRepositorio.EliminarPorId(id); // Usar EliminarPorId
                return NoContent(); // Devuelve 204 si la eliminación fue exitosa
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Devuelve 404 si el rol no se encuentra
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar la excepción
                return StatusCode(500, "Ocurrió un error al eliminar el rol.");
            }
        }
    }
}
