using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LosMontenegrosAPIWeb.Entidades;

public class TipoUsuarioRepository
{
    private readonly HotelBDContext _context;

    public TipoUsuarioRepository(HotelBDContext context)
    {
        _context = context;
    }

    public async Task<List<TipoUsuario>> GetAllAsync()
    {
        return await _context.TipoUsuarios.ToListAsync();
    }

    public async Task<TipoUsuario> GetByIdAsync(int id)
    {
        return await _context.TipoUsuarios
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(TipoUsuario tipoUsuario)
    {
        await _context.TipoUsuarios.AddAsync(tipoUsuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Update(tipoUsuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tipoUsuario = await GetByIdAsync(id);
        if (tipoUsuario != null)
        {
            _context.TipoUsuarios.Remove(tipoUsuario);
            await _context.SaveChangesAsync();
        }
    }
}
