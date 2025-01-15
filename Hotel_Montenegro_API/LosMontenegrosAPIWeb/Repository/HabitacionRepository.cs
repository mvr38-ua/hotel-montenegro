using LosMontenegrosAPIWeb.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosMontenegrosAPIWeb.Repositories
{
    public class HabitacionRepository
    {
        private readonly HotelBDContext _context;

        public HabitacionRepository(HotelBDContext context)
        {
            _context = context;
        }

        // Crear
        public async Task<Habitacion> CrearHabitacionAsync(Habitacion habitacion)
        {
            _context.Habitacions.Add(habitacion);
            await _context.SaveChangesAsync();
            return habitacion;
        }

        // Leer todos
        public async Task<List<Habitacion>> ObtenerHabitacionesAsync()
        {
            return await _context.Habitacions.Include(h => h.Categoria)
                .Select(h => new Habitacion
            {
                Id = h.Id,
                Numero = h.Numero,
                PrecioBase = h.PrecioBase,
                Capacidad = h.Capacidad,
                Bloqueada = h.Bloqueada,
                Adaptada = h.Adaptada,
                CategoriaId = h.CategoriaId,
                Categoria = new Categorium
                {
                    Id = h.Categoria.Id,
                    Nombre = h.Categoria.Nombre
                }
                }).ToListAsync();
        }

        // Leer por Id
        public async Task<Habitacion?> ObtenerHabitacionPorIdAsync(int id)
        {
            return await _context.Habitacions
                .Include(h => h.Categoria) // Carga las Categoria
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        // Actualizar
        public async Task<bool> ActualizarHabitacionAsync(Habitacion habitacion)
        {
            _context.Habitacions.Update(habitacion);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Eliminar
        public async Task<bool> EliminarHabitacionAsync(int id)
        {
            var habitacion = await _context.Habitacions.FindAsync(id);
            if (habitacion != null)
            {
                _context.Habitacions.Remove(habitacion);
                var resultado = await _context.SaveChangesAsync();
                return resultado > 0;
            }
            return false;
        }

        // Método para obtener habitaciones disponibles
        public async Task<List<Habitacion>> ObtenerHabitacionesDisponiblesAsync(
            DateTime fechaInicio,
            DateTime fechaFin,
            int? capacidadMinima = null,
            int? categoriaId = null)
        {
            return await _context.Habitacions
                .Where(h =>
                    !h.Bloqueada &&                                                     // Excluir habitaciones bloqueadas
                    (capacidadMinima == null || h.Capacidad >= capacidadMinima) &&      // Filtrar por capacidad mínima si se especifica
                    (categoriaId == null || h.CategoriaId == categoriaId) &&            // Filtrar por categoría si se especifica
                    !h.Reservas.Any(r =>
                        (fechaInicio < r.FechaFinal && fechaFin > r.FechaInicio)        // Validar cruce de fechas
                    ))
                .ToListAsync();
        }

        // Método para bloquear una habitación
        public async Task<Habitacion> BloquearHabitacionAsync(int id)
        {
            var habitacion = await _context.Habitacions.FindAsync(id);

            if (habitacion == null)
            {
                return null; // Si no se encuentra la habitación, Devuelve null
            }

            habitacion.Bloqueada = true;
            await _context.SaveChangesAsync();

            return habitacion; // Devuelve la habitación bloqueada
        }

        // Método para desbloquear una habitación
        public async Task<Habitacion> DesbloquearHabitacionAsync(int id)
        {
            var habitacion = await _context.Habitacions.FindAsync(id);

            if (habitacion == null)
            {
                return null; // Si no se encuentra la habitación, Devuelve null
            }

            habitacion.Bloqueada = false;
            await _context.SaveChangesAsync();

            return habitacion; // Devuelve la habitación desbloqueada
        }

    }
}
