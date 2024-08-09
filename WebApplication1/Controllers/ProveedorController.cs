using ClassLibrary3.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParteReyesSalas.Repositorio;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;

        public ProveedorController(IProveedorRepositorio proveedorRepositorio)
        {
            _proveedorRepositorio = proveedorRepositorio;
        }



        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            var proveedores = await _proveedorRepositorio.ObtenerTodosLosProveedoresAsync();
            return Ok(proveedores);
        }



        [HttpPost]
        public async Task<IActionResult> AgregarProveedor([FromBody] Proveedor proveedor)
        {
            if (proveedor == null)
            {
                return BadRequest("El proveedor no puede ser nulo.");
            }

            var nuevoProveedor = await _proveedorRepositorio.AgregarProveedorAsync(proveedor);

            if (nuevoProveedor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al agregar el proveedor.");
            }

            return CreatedAtAction(nameof(GetProveedores), new { id = nuevoProveedor.Id }, nuevoProveedor);
        }



        [HttpGet("filtrados")]
        public async Task<IActionResult> GetProveedoresFiltrados([FromQuery] string nombre = "", [FromQuery] string tipo = "", [FromQuery] string email = "")
        {
            var proveedores = await _proveedorRepositorio.ObtenerProveedoresFiltradosAsync(nombre, tipo, email);
            return Ok(proveedores);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            var eliminado = await _proveedorRepositorio.EliminarProveedorAsync(id);

            if (!eliminado)
            {
                return NotFound($"Proveedor con ID {id} no encontrado.");
            }

            return NoContent(); // Respuesta 204 No Content
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProveedorPorId(int id)
        {
            try
            {
                var proveedor = await _proveedorRepositorio.ObtenerProveedorPorIdAsync(id);

                if (proveedor == null)
                {
                    return NotFound($"Proveedor con ID {id} no encontrado.");
                }

                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones y registro de errores
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditarProveedor(int id, [FromBody] Proveedor proveedor)
        {
            // Verificar que el ID en la URL coincida con el ID del proveedor en el cuerpo de la solicitud
            if (id != proveedor.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del proveedor en el cuerpo de la solicitud.");
            }

            try
            {
                // Intentar actualizar el proveedor usando el repositorio
                var proveedorActualizado = await _proveedorRepositorio.EditarProveedorAsync(proveedor);
                return Ok(proveedorActualizado);
            }
            catch (KeyNotFoundException ex)
            {
                // Devolver NotFound si el proveedor no se encuentra
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Devolver error interno del servidor en caso de excepciones generales
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
