using LosMontenegrosAPIWeb.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosMontenegrosAPIWeb.Repositories
{
    public class DireccionUsuarioRepository
    {
        private readonly HotelBDContext _context;

        public DireccionUsuarioRepository(HotelBDContext context)
        {
            _context = context;
        }

        // Crear una nueva dirección de usuario
        public async Task<DireccionUsuario> CrearDireccionAsync(DireccionUsuario direccionUsuario)
        {
            _context.DireccionUsuarios.Add(direccionUsuario);
            await _context.SaveChangesAsync();
            return direccionUsuario;
        }

        // Obtener todas las direcciones de usuarios
        public async Task<List<DireccionUsuario>> ObtenerDireccionesAsync()
        {
            return await _context.DireccionUsuarios
                                 .Include(d => d.Usuarios) // Incluir usuarios relacionados si es necesario
                                 .ToListAsync();
        }

        // Obtener una dirección por ID
        public async Task<DireccionUsuario?> ObtenerDireccionPorIdAsync(int id)
        {
            return await _context.DireccionUsuarios
                                 .Include(d => d.Usuarios) // Incluir usuarios relacionados si es necesario
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        // Actualizar una dirección
        public async Task<bool> ActualizarDireccionAsync(DireccionUsuario direccionUsuario)
        {
            var existente = await _context.DireccionUsuarios.FindAsync(direccionUsuario.Id);
            if (existente == null)
            {
                return false; // La dirección no existe
            }

            existente.Direccion = direccionUsuario.Direccion;
            existente.CodPostal = direccionUsuario.CodPostal;
            existente.Municipio = direccionUsuario.Municipio;
            existente.Provincia = direccionUsuario.Provincia;
            existente.Pais = direccionUsuario.Pais;

            _context.DireccionUsuarios.Update(existente);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Eliminar una dirección por ID
        public async Task<bool> EliminarDireccionAsync(int id)
        {
            var direccionUsuario = await _context.DireccionUsuarios.FindAsync(id);
            if (direccionUsuario == null)
            {
                return false; // La dirección no existe
            }

            _context.DireccionUsuarios.Remove(direccionUsuario);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }
    }
}
