using LosMontenegrosAPIWeb.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LosMontenegrosAPIWeb.Repositories
{
    public class ServicioRepository
    {
        private readonly HotelBDContext _context;

        public ServicioRepository(HotelBDContext context)
        {
            _context = context;
        }

        // Crear
        public async Task<Servicio> CrearServicioAsync(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();
            return servicio;
        }

        // Leer todos
        public async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            return await _context.Servicios.ToListAsync();
        }

        // Leer por Id
        public async Task<Servicio?> ObtenerServicioPorIdAsync(int id)
        {
            return await _context.Servicios.FirstOrDefaultAsync(s => s.Id == id);
        }

        // Actualizar
        public async Task<bool> ActualizarServicioAsync(Servicio servicio)
        {
            _context.Servicios.Update(servicio);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Eliminar
        public async Task<bool> EliminarServicioAsync(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                var resultado = await _context.SaveChangesAsync();
                return resultado > 0;
            }
            return false;
        }
    }
}
