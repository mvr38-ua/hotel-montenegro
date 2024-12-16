using Microsoft.AspNetCore.Mvc;
using LosMontenegrosAPIWeb.Entidades;
using LosMontenegrosAPIWeb.Repositories;

namespace LosMontenegrosAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionesController : ControllerBase
    {
        private readonly HabitacionRepository _habitacionRepository;

        public HabitacionesController(HabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }

        // GET: api/Habitaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitacion>>> GetHabitaciones()
        {
            var habitaciones = await _habitacionRepository.ObtenerHabitacionesAsync();
            return Ok(habitaciones);
        }

        // GET: api/Habitaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Habitacion>> GetHabitacion(int id)
        {
            var habitacion = await _habitacionRepository.ObtenerHabitacionPorIdAsync(id);

            if (habitacion == null)
            {
                return NotFound();
            }

            return Ok(habitacion);
        }

        // PUT: api/Habitaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitacion(int id, Habitacion habitacion)
        {
            if (id != habitacion.Id)
            {
                return BadRequest();
            }

            var actualizado = await _habitacionRepository.ActualizarHabitacionAsync(habitacion);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Habitaciones
        [HttpPost]
        public async Task<ActionResult<Habitacion>> PostHabitacion(Habitacion habitacion)
        {
            var nuevaHabitacion = await _habitacionRepository.CrearHabitacionAsync(habitacion);
            return CreatedAtAction(nameof(GetHabitacion), new { id = nuevaHabitacion.Id }, nuevaHabitacion);
        }

        // DELETE: api/Habitaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitacion(int id)
        {
            var eliminado = await _habitacionRepository.EliminarHabitacionAsync(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
