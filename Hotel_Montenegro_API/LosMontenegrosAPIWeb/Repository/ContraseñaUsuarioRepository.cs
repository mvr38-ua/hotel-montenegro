using LosMontenegrosAPIWeb.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosMontenegrosAPIWeb.Repositories
{
    public class ContraseñaUsuarioRepository
    {
        private readonly HotelBDContext _context;

        public ContraseñaUsuarioRepository(HotelBDContext context)
        {
            _context = context;
        }

        // Crear
        public async Task<ContraseñaUsuario> CrearContraseñaUsuarioAsync(ContraseñaUsuario contraseñaUsuario)
        {
            _context.ContraseñaUsuarios.Add(contraseñaUsuario);
            await _context.SaveChangesAsync();
            return contraseñaUsuario;
        }

        // Leer todos
        public async Task<List<ContraseñaUsuario>> ObtenerContraseñasUsuariosAsync()
        {
            return await _context.ContraseñaUsuarios.ToListAsync();
        }

        // Leer por Id
        public async Task<ContraseñaUsuario?> ObtenerContraseñaUsuarioPorIdAsync(int id)
        {
            return await _context.ContraseñaUsuarios
                .Include(cu => cu.Usuarios) // Carga los Usuarios
                .FirstOrDefaultAsync(cu => cu.Id == id);
        }

        // Actualizar
        public async Task<bool> ActualizarContraseñaUsuarioAsync(ContraseñaUsuario contraseñaUsuario)
        {
            _context.ContraseñaUsuarios.Update(contraseñaUsuario);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Eliminar
        public async Task<bool> EliminarContraseñaUsuarioAsync(int id)
        {
            var contraseñaUsuario = await _context.ContraseñaUsuarios.FindAsync(id);
            if (contraseñaUsuario != null)
            {
                _context.ContraseñaUsuarios.Remove(contraseñaUsuario);
                var resultado = await _context.SaveChangesAsync();
                return resultado > 0;
            }
            return false;
        }
    }
}
