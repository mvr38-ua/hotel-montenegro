using Microsoft.AspNetCore.Mvc;
using LosMontenegrosAPIWeb.Entidades;
using LosMontenegrosAPIWeb.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LosMontenegrosAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuariosController : ControllerBase
    {
        private readonly TipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuariosController(TipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        // GET: api/TipoUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoUsuario>>> GetTipoUsuarios()
        {
            var tipoUsuarios = await _tipoUsuarioRepository.GetAllAsync();
            return Ok(tipoUsuarios);
        }

        // GET: api/TipoUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> GetTipoUsuario(int id)
        {
            var tipoUsuario = await _tipoUsuarioRepository.GetByIdAsync(id);

            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return Ok(tipoUsuario);
        }

        // PUT: api/TipoUsuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUsuario(int id, TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.Id)
            {
                return BadRequest("El ID proporcionado no coincide con el del tipo de usuario.");
            }

            var existingTipoUsuario = await _tipoUsuarioRepository.GetByIdAsync(id);
            if (existingTipoUsuario == null)
            {
                return NotFound("No se encontró el tipo de usuario para actualizar.");
            }

            await _tipoUsuarioRepository.UpdateAsync(tipoUsuario);
            return NoContent();
        }

        // POST: api/TipoUsuarios
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> PostTipoUsuario(TipoUsuario tipoUsuario)
        {
            await _tipoUsuarioRepository.AddAsync(tipoUsuario);
            return CreatedAtAction(nameof(GetTipoUsuario), new { id = tipoUsuario.Id }, tipoUsuario);
        }

        // DELETE: api/TipoUsuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUsuario(int id)
        {
            var existingTipoUsuario = await _tipoUsuarioRepository.GetByIdAsync(id);
            if (existingTipoUsuario == null)
            {
                return NotFound("No se encontró el tipo de usuario para eliminar.");
            }

            await _tipoUsuarioRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
