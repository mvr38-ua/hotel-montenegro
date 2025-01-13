using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LosMontenegrosAPIWeb.Entidades;

namespace LosMontenegrosAPIWeb.Repositories
{
    public class UsuarioRepository
    {
        private readonly HotelBDContext _context;

        public UsuarioRepository(HotelBDContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Contraseña)
                .Include(u => u.Direccion)
                .Include(u => u.TipoUsuario)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios
                .Select( u => new Usuario
                {
                    Id = u.Id,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Apellidos = u.Apellidos,
                    Dni = u.Dni,
                    Movil = u.Movil,
                    Telefono = u.Telefono,
                    Activo = u.Activo,
                    FechaNac = u.FechaNac,
                    Direccion = new DireccionUsuario
                    {
                        Direccion = u.Direccion.Direccion
                    },
                    TipoUsuario = new TipoUsuario
                    {
                        Tipo = u.TipoUsuario.Tipo
                    }
                }).ToListAsync();

        }

        // Update
        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
