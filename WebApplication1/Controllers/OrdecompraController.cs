using ClassLibrary3.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParteReyesSalas.Repositorio;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdecompraController : ControllerBase
    {
        private readonly IOrdenCompraRepositorio _ordenCompraRepositorio;
        private readonly IProveedorRepositorio _proveedorRepositorio;

        public OrdecompraController(IOrdenCompraRepositorio ordenCompraRepositorio, IProveedorRepositorio proveedorRepositorio)
        {
            _ordenCompraRepositorio = ordenCompraRepositorio;
            _proveedorRepositorio = proveedorRepositorio;
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasOrdenesDeCompra()
        {
            var ordenesCompra = await _ordenCompraRepositorio.ObtenerTodasLasOrdenesCompraAsync();
            if (ordenesCompra == null || !ordenesCompra.Any())
            {
                return NotFound("No hay órdenes de compra disponibles.");
            }
            var response = ordenesCompra.Select(oc => new
            {
                oc.Id,
                oc.NumeroOrden,
                nombreProveedor = oc.Proveedor.Nombre,
                oc.MontoTotal,
                FechaOrden = oc.FechaOrden.ToString("yyyy-MM-ddTHH:mm:ss"), // ISO 8601 format
                oc.Observaciones
            });
            return Ok(response);
        }






        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerOrdenCompraPorId(int id)
        {
            var ordenCompra = await _ordenCompraRepositorio.ObtenerOrdenCompraPorIdAsync(id);

            if (ordenCompra == null)
            {
                return NotFound();
            }

            var response = new
            {
                ordenCompra.Id,
                ordenCompra.NumeroOrden,
                NombreProveedor = ordenCompra.Proveedor.Nombre, // Solo el nombre del proveedor
                ordenCompra.MontoTotal,
                ordenCompra.FechaOrden,
                ordenCompra.Observaciones
            };

            return Ok(response);
        }

        [HttpGet("filtradas")]
        public async Task<IActionResult> ObtenerOrdenesCompraFiltradas(
          [FromQuery] string numeroOrden = null,
          [FromQuery] string nombreProveedor = null,
          [FromQuery] string observaciones = null)
        {
            try
            {
                var ordenesCompra = await _ordenCompraRepositorio.ObtenerOrdenesCompraFiltradasAsync(numeroOrden, nombreProveedor, observaciones);
                if (ordenesCompra == null || !ordenesCompra.Any())
                {
                    return NotFound("No se encontraron órdenes de compra que coincidan con los filtros.");
                }

                var response = ordenesCompra.Select(oc => new
                {
                    oc.Id,
                    oc.NumeroOrden,
                    nombreProveedor = oc.Proveedor.Nombre,
                    oc.MontoTotal,
                    FechaOrden = oc.FechaOrden.ToString("yyyy-MM-ddTHH:mm:ss"), // ISO 8601 format
                    oc.Observaciones
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error de forma más específica o registrar el error
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }



        [HttpPost]
        public async Task<IActionResult> AgregarOrdenCompra([FromBody] OrdenCompra ordenCompraDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Verificar si el proveedor existe
                var proveedor = await _proveedorRepositorio.ObtenerProveedorPorIdAsync(ordenCompraDTO.ProveedorId);
                if (proveedor == null)
                {
                    return BadRequest("El proveedor especificado no existe.");
                }

                // Crear una nueva instancia de OrdenCompra
                var nuevaOrdenCompra = new OrdenCompra
                {
                    NumeroOrden = ordenCompraDTO.NumeroOrden,
                    ProveedorId = ordenCompraDTO.ProveedorId,
                    MontoTotal = ordenCompraDTO.MontoTotal,
                    FechaOrden = ordenCompraDTO.FechaOrden,
                    Observaciones = ordenCompraDTO.Observaciones
                };

                // Agregar la orden de compra
                var ordenCompraAgregada = await _ordenCompraRepositorio.AgregarOrdenCompraAsync(nuevaOrdenCompra);

                // Crear la respuesta
                var response = new
                {
                    ordenCompraAgregada.Id,
                    ordenCompraAgregada.NumeroOrden,
                    NombreProveedor = ordenCompraAgregada.Proveedor.Nombre,
                    ordenCompraAgregada.MontoTotal,
                    FechaOrden = ordenCompraAgregada.FechaOrden.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ordenCompraAgregada.Observaciones
                };

                return CreatedAtAction(nameof(ObtenerOrdenCompraPorId), new { id = ordenCompraAgregada.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al agregar la orden de compra: {ex.Message}");
            }
        }



    }
}

