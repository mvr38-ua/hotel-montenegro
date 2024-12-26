using Microsoft.AspNetCore.Mvc;
using LosMontenegrosAPIWeb.Entidades;
using LosMontenegrosAPIWeb.Repositories;

namespace LosMontenegrosAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContraseñasUsuariosController : ControllerBase
    {
        private readonly ContraseñaUsuarioRepository _contraseñaUsuarioRepository;

        public ContraseñasUsuariosController(ContraseñaUsuarioRepository contraseñaUsuarioRepository)
        {
            _contraseñaUsuarioRepository = contraseñaUsuarioRepository;
        }

        // GET: api/ContraseñasUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContraseñaUsuario>>> GetContraseñasUsuarios()
        {
            var contraseñasUsuarios = await _contraseñaUsuarioRepository.ObtenerContraseñasUsuariosAsync();
            return Ok(contraseñasUsuarios);
        }

        // GET: api/ContraseñasUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContraseñaUsuario>> GetContraseñaUsuario(int id)
        {
            var contraseñaUsuario = await _contraseñaUsuarioRepository.ObtenerContraseñaUsuarioPorIdAsync(id);

            if (contraseñaUsuario == null)
            {
                return NotFound();
            }

            return Ok(contraseñaUsuario);
        }

        // PUT: api/ContraseñasUsuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContraseñaUsuario(int id, ContraseñaUsuario contraseñaUsuario)
        {
            if (id != contraseñaUsuario.Id)
            {
                return BadRequest();
            }

            var actualizado = await _contraseñaUsuarioRepository.ActualizarContraseñaUsuarioAsync(contraseñaUsuario);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/ContraseñasUsuarios
        [HttpPost]
        public async Task<ActionResult<ContraseñaUsuario>> PostContraseñaUsuario(ContraseñaUsuario contraseñaUsuario)
        {
            var nuevaContraseñaUsuario = await _contraseñaUsuarioRepository.CrearContraseñaUsuarioAsync(contraseñaUsuario);
            return CreatedAtAction(nameof(GetContraseñaUsuario), new { id = nuevaContraseñaUsuario.Id }, nuevaContraseñaUsuario);
        }

        // DELETE: api/ContraseñasUsuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContraseñaUsuario(int id)
        {
            var eliminado = await _contraseñaUsuarioRepository.EliminarContraseñaUsuarioAsync(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
