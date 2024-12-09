//using LosMontenegrosAPIWeb.Entidades;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LosMontenegrosAPIWeb.Repositories
//{
//    public class CategoriaRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public CategoriaRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // Crear
//        public async Task<Categorium> CrearCategoriaAsync(Categorium categoria)
//        {
//            _context.Categoriums.Add(categoria);
//            await _context.SaveChangesAsync();
//            return categoria;
//        }

//        // Leer todos
//        public async Task<List<Categorium>> ObtenerCategoriasAsync()
//        {
//            return await _context.Categoriums.ToListAsync();
//        }

//        // Leer por Id
//        public async Task<Categorium> ObtenerCategoriaPorIdAsync(int id)
//        {
//            return await _context.Categoriums
//                                 .Include(c => c.Habitacions)  // Si quieres incluir las habitaciones asociadas
//                                 .FirstOrDefaultAsync(c => c.Id == id);
//        }

//        // Actualizar
//        public async Task<bool> ActualizarCategoriaAsync(Categorium categoria)
//        {
//            _context.Categoriums.Update(categoria);
//            var resultado = await _context.SaveChangesAsync();
//            return resultado > 0;
//        }

//        // Eliminar
//        public async Task<bool> EliminarCategoriaAsync(int id)
//        {
//            var categoria = await _context.Categoriums.FindAsync(id);
//            if (categoria != null)
//            {
//                _context.Categoriums.Remove(categoria);
//                var resultado = await _context.SaveChangesAsync();
//                return resultado > 0;
//            }
//            return false;
//        }
//    }
//}
