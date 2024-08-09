using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParteReyesSalas.Repositorio;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaRepositorio _facturaRepositorio;

        public FacturaController(IFacturaRepositorio facturaRepositorio)
        {
            _facturaRepositorio = facturaRepositorio;
        }



        // GET: api/factura
        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasFacturas()
        {
            try
            {
                var facturas = await _facturaRepositorio.ObtenerTodasLasFacturasAsync();

                if (facturas == null || !facturas.Any())
                {
                    return NotFound("No hay facturas disponibles.");
                }

                var response = facturas.Select(f => new
                {
                    f.Id,
                    f.IdentificadorFactura,
                    FechaEmision = f.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss"), // ISO 8601 format
                    FechaVencimiento = f.FechaVencimiento.ToString("yyyy-MM-ddTHH:mm:ss"), // ISO 8601 format
                    ProveedorNombre = f.Proveedor.Nombre, // Nombre del proveedor
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here) and return an error response
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las facturas");
            }
        }

        // GET: api/factura/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerFacturaPorId(int id)
        {
            try
            {
                var factura = await _facturaRepositorio.ObtenerFacturaPorIdAsync(id);

                if (factura == null)
                {
                    return NotFound($"No se encontró la factura con ID = {id}");
                }

                var response = new
                {
                    factura.Id,
                    factura.IdentificadorFactura,
                    FechaEmision = factura.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss"), // Formato ISO 8601
                    FechaVencimiento = factura.FechaVencimiento.ToString("yyyy-MM-ddTHH:mm:ss"), // Formato ISO 8601
                    ProveedorNombre = factura.Proveedor.Nombre, // Nombre del proveedor
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Registrar la excepción (no mostrado aquí) y devolver una respuesta de error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la factura");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarFactura(int id)
        {
            try
            {
                var resultado = await _facturaRepositorio.EliminarFacturaPorIdAsync(id);

                if (!resultado)
                {
                    return NotFound($"Factura con ID {id} no encontrada.");
                }

                return NoContent(); // Respuesta 204 No Content para indicar que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                // Log el error (no mostrado aquí) y retorna un error
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la factura");
            }
        }


    }
}

