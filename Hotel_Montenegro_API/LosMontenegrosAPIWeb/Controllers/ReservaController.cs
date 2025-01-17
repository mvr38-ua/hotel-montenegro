using Microsoft.AspNetCore.Mvc;
using LosMontenegrosAPIWeb.Repositories;
using LosMontenegrosAPIWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LosMontenegrosAPIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaRepository _reservaRepository;

        private readonly HabitacionRepository _habitacionRepository;

        public ReservaController(ReservaRepository reservaRepository, HabitacionRepository habitacionRepository)
        {
            _reservaRepository = reservaRepository;
            _habitacionRepository = habitacionRepository;
        }

        // GET: api/Reserva
        [HttpGet]
        public async Task<ActionResult<List<Reserva>>> GetReservas()
        {
            var reservas = await _reservaRepository.GetAllReservasAsync();
            return Ok(reservas);
        }

        // GET: api/Reserva/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _reservaRepository.GetReservaByIdAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }

        // GET: api/Reserva/Usuario/5
        [HttpGet("Usuario/{usuarioId}")]
        public async Task<ActionResult<List<Reserva>>> GetReservasByUsuario(int usuarioId)
        {
            var reservas = await _reservaRepository.GetReservasByUsuarioIdAsync(usuarioId);

            if (reservas == null || reservas.Count == 0)
            {
                return NotFound();
            }

            return Ok(reservas);
        }

        // GET: api/Reserva/Fecha
        [HttpGet("Fecha")]
        public async Task<ActionResult<List<Reserva>>> GetReservasByFechaRange(DateTime fechaInicio, DateTime fechaFinal)
        {
            var reservas = await _reservaRepository.GetReservasByFechaRangeAsync(fechaInicio, fechaFinal);

            if (reservas == null || reservas.Count == 0)
            {
                return NotFound();
            }

            return Ok(reservas);
        }

        // POST: api/Reserva
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            if (reserva == null)
            {
                return BadRequest();
            }

            await _reservaRepository.AddReservaAsync(reserva);
            return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
        }

        // PUT: api/Reserva/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }

            await _reservaRepository.UpdateReservaAsync(reserva);

            return NoContent();
        }

        // DELETE: api/Reserva/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _reservaRepository.GetReservaByIdAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            await _reservaRepository.DeleteReservaAsync(id);
            return NoContent();
        }


        // POST (para que acepte el JSON): api/Reserva/HabitacionesDisponibles
        [HttpPost("HabitacionesDisponibles")]
        public async Task<ActionResult<List<Habitacion>>> GetHabitacionesDisponibles([FromBody] FiltroHabitaciones filtro)
        {
            if (filtro.FechaInicio >= filtro.FechaFin)
            {
                return BadRequest("La fecha de inicio debe ser anterior a la fecha de fin.");
            }

            var habitacionesDisponibles = await _habitacionRepository.ObtenerHabitacionesDisponiblesAsync(
                filtro.FechaInicio,
                filtro.FechaFin,
                filtro.CapacidadMinima,
                filtro.CategoriaId
            );

            if (habitacionesDisponibles == null || habitacionesDisponibles.Count == 0)
            {
                return NotFound("No hay habitaciones disponibles que cumplan con los filtros proporcionados.");
            }

            return Ok(habitacionesDisponibles);
        }

    }
}
