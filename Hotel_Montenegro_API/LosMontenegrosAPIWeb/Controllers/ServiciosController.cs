using Microsoft.AspNetCore.Mvc;
using LosMontenegrosAPIWeb.Entidades;
using LosMontenegrosAPIWeb.Repositories;

namespace LosMontenegrosAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly ServicioRepository _servicioRepository;

        public ServiciosController(ServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        // GET: api/Servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicios()
        {
            var servicios = await _servicioRepository.ObtenerServiciosAsync();
            return Ok(servicios);
        }

        // GET: api/Servicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> GetServicio(int id)
        {
            var servicio = await _servicioRepository.ObtenerServicioPorIdAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return Ok(servicio);
        }

        // PUT: api/Servicios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, Servicio servicio)
        {
            if (id != servicio.Id)
            {
                return BadRequest();
            }

            var actualizado = await _servicioRepository.ActualizarServicioAsync(servicio);

            if (!actualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Servicios
        [HttpPost]
        public async Task<ActionResult<Servicio>> PostServicio(Servicio servicio)
        {
            var nuevoServicio = await _servicioRepository.CrearServicioAsync(servicio);
            return CreatedAtAction(nameof(GetServicio), new { id = nuevoServicio.Id }, nuevoServicio);
        }

        // DELETE: api/Servicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var eliminado = await _servicioRepository.EliminarServicioAsync(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
