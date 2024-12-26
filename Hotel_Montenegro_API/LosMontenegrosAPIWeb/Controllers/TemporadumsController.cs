using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LosMontenegrosAPIWeb.Entidades;

namespace LosMontenegrosAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporadumsController : ControllerBase
    {
        private readonly TemporadaRepository _repository;

        public TemporadumsController(TemporadaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Temporadums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temporadum>>> GetTemporada()
        {
            var temporadas = await _repository.GetAllAsync();
            return Ok(temporadas);
        }

        // GET: api/Temporadums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Temporadum>> GetTemporadum(int id)
        {
            var temporadum = await _repository.GetByIdAsync(id);

            if (temporadum == null)
            {
                return NotFound();
            }

            return Ok(temporadum);
        }

        // PUT: api/Temporadums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporadum(int id, Temporadum temporadum)
        {
            if (id != temporadum.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID de la entidad.");
            }

            try
            {
                await _repository.UpdateAsync(temporadum);
            }
            catch
            {
                return NotFound($"No se pudo encontrar la temporada con ID {id}.");
            }

            return NoContent();
        }

        // POST: api/Temporadums
        [HttpPost]
        public async Task<ActionResult<Temporadum>> PostTemporadum(Temporadum temporadum)
        {
            await _repository.AddAsync(temporadum);
            return CreatedAtAction(nameof(GetTemporadum), new { id = temporadum.Id }, temporadum);
        }

        // DELETE: api/Temporadums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemporadum(int id)
        {
            var temporadum = await _repository.GetByIdAsync(id);

            if (temporadum == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
