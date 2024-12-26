using Microsoft.AspNetCore.Mvc;
using LosMontenegrosAPIWeb.Entidades;
using LosMontenegrosAPIWeb.Repositories;

namespace LosMontenegrosAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionUsuariosController : ControllerBase
    {
        private readonly DireccionUsuarioRepository _direccionUsuarioRepository;

        public DireccionUsuariosController(DireccionUsuarioRepository direccionUsuarioRepository)
        {
            _direccionUsuarioRepository = direccionUsuarioRepository;
        }

        // GET: api/DireccionUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DireccionUsuario>>> GetDireccionUsuarios()
        {
            var direcciones = await _direccionUsuarioRepository.ObtenerDireccionesAsync();
            return Ok(direcciones);
        }

        // GET: api/DireccionUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DireccionUsuario>> GetDireccionUsuario(int id)
        {
            var direccionUsuario = await _direccionUsuarioRepository.ObtenerDireccionPorIdAsync(id);

            if (direccionUsuario == null)
            {
                return NotFound();
            }

            return Ok(direccionUsuario);
        }

        // PUT: api/DireccionUsuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDireccionUsuario(int id, DireccionUsuario direccionUsuario)
        {
            if (id != direccionUsuario.Id)
            {
                return BadRequest("El ID proporcionado no coincide con el de la dirección.");
            }

            var actualizado = await _direccionUsuarioRepository.ActualizarDireccionAsync(direccionUsuario);

            if (!actualizado)
            {
                return NotFound("No se encontró la dirección para actualizar.");
            }

            return NoContent();
        }

        // POST: api/DireccionUsuarios
        [HttpPost]
        public async Task<ActionResult<DireccionUsuario>> PostDireccionUsuario(DireccionUsuario direccionUsuario)
        {
            var nuevaDireccion = await _direccionUsuarioRepository.CrearDireccionAsync(direccionUsuario);
            return CreatedAtAction(nameof(GetDireccionUsuario), new { id = nuevaDireccion.Id }, nuevaDireccion);
        }

        // DELETE: api/DireccionUsuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDireccionUsuario(int id)
        {
            var eliminado = await _direccionUsuarioRepository.EliminarDireccionAsync(id);

            if (!eliminado)
            {
                return NotFound("No se encontró la dirección para eliminar.");
            }

            return NoContent();
        }
    }
}
