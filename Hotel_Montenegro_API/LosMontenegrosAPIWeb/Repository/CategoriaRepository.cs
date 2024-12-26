using LosMontenegrosAPIWeb.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosMontenegrosAPIWeb.Repositories
{
    public class CategoriaRepository
    {
        private readonly HotelBDContext _context;

        public CategoriaRepository(HotelBDContext context)
        {
            _context = context;
        }

        // Crear
        public async Task<Categorium> CrearCategoriaAsync(Categorium categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        // Leer todos
        public async Task<List<Categorium>> ObtenerCategoriasAsync()
        {
            return await _context.Categoria.ToListAsync();
        }

        // Leer por Id
        public async Task<Categorium> ObtenerCategoriaPorIdAsync(int id)
        {
            return await _context.Categoria
                                 .Include(c => c.Habitacions)  // Si quieres incluir las habitaciones asociadas
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Actualizar
        public async Task<bool> ActualizarCategoriaAsync(Categorium categoria)
        {
            _context.Categoria.Update(categoria);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Eliminar
        public async Task<bool> EliminarCategoriaAsync(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria != null)
            {
                _context.Categoria.Remove(categoria);
                var resultado = await _context.SaveChangesAsync();
                return resultado > 0;
            }
            return false;
        }
    }
}
