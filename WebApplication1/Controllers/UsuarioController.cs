using ClassLibrary3.Entidades;
using ClassLibrary3.Repositoris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _usuarioRepositorio.AgregarUsuarioAsync(usuario);
                return CreatedAtAction(nameof(AgregarUsuario), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar la excepción
                return StatusCode(500, "Ocurrió un error al agregar el usuario.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosUsuarios()
        {
            try
            {
                var usuarios = await _usuarioRepositorio.ObtenerTodosUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar la excepción
                return StatusCode(500, "Ocurrió un error al obtener los usuarios.");
            }
        }

        [HttpGet("filtrar")]
        public async Task<IActionResult> FiltrarUsuarios([FromQuery] int? id = null, [FromQuery] string? nombre = null, [FromQuery] string? apellido = null, [FromQuery] int? rolId = null)
        {
            try
            {
                var usuarios = await _usuarioRepositorio.FiltrarUsuariosAsync(id, nombre, apellido, rolId);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar la excepción
                return StatusCode(500, "Ocurrió un error al filtrar los usuarios.");
            }
        }
    }
}

    
