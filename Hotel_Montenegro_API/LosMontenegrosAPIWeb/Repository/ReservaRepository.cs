using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LosMontenegrosAPIWeb.Entidades;

namespace LosMontenegrosAPIWeb.Repositories
{
    public class ReservaRepository
    {
        private readonly HotelBDContext _context;

        public ReservaRepository(HotelBDContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddReservaAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<Reserva?> GetReservaByIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Habitacion)
                .Include(r => r.Usuario)
                .Include(r => r.Servicios)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reserva>> GetAllReservasAsync()
        {
            return await _context.Reservas
             .Select(r => new Reserva
             {
                 Id = r.Id,
                 FechaInicio = r.FechaInicio,
                 FechaFinal = r.FechaFinal,
                 PrecioTotal = r.PrecioTotal,
                 UsuarioId = r.UsuarioId,
                 HabitacionId = r.HabitacionId, 
                 Usuario = new Usuario
                 {
                     Id = r.Usuario.Id,
                     Nombre = r.Usuario.Nombre,
                     Email = r.Usuario.Email
                 },
                 Habitacion = new Habitacion
                 {
                     Id = r.Habitacion.Id,
                     Numero = r.Habitacion.Numero
                 },
                 Servicios = r.Servicios.Select(s => new Servicio
                 {
                     Id = s.Id,
                     Nombre = s.Nombre,
                     PrecioServicio = s.PrecioServicio 
                 }).ToList()
             })
             .ToListAsync();
        }

        // Update
        public async Task UpdateReservaAsync(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteReservaAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
        }

        // Get Reservas by Usuario ID
        public async Task<List<Reserva>> GetReservasByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Reservas
                .Include(r => r.Habitacion)
                .Include(r => r.Servicios)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }

        // Get Reservas by Fecha Range
        public async Task<List<Reserva>> GetReservasByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFinal)
        {
            return await _context.Reservas
                .Include(r => r.Habitacion)
                .Include(r => r.Usuario)
                .Include(r => r.Servicios)
                .Where(r => r.FechaInicio >= fechaInicio && r.FechaFinal <= fechaFinal)
                .ToListAsync();
        }
    }
}
