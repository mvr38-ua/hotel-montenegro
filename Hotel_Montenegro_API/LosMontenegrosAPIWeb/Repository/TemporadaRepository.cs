using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LosMontenegrosAPIWeb.Entidades;

public class TemporadaRepository
{
    private readonly HotelBDContext _context;

    public TemporadaRepository(HotelBDContext context)
    {
        _context = context;
    }

    // Obtener todas las temporadas
    public async Task<List<Temporadum>> GetAllAsync()
    {
        return await _context.Temporada.ToListAsync();
    }

    // Obtener una temporada por ID
    public async Task<Temporadum> GetByIdAsync(int id)
    {
        return await _context.Temporada
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    // Agregar una nueva temporada
    public async Task AddAsync(Temporadum temporada)
    {
        await _context.Temporada.AddAsync(temporada);
        await _context.SaveChangesAsync();
    }

    // Actualizar una temporada existente
    public async Task UpdateAsync(Temporadum temporada)
    {
        _context.Temporada.Update(temporada);
        await _context.SaveChangesAsync();
    }

    // Eliminar una temporada por ID
    public async Task DeleteAsync(int id)
    {
        var temporada = await GetByIdAsync(id);
        if (temporada != null)
        {
            _context.Temporada.Remove(temporada);
            await _context.SaveChangesAsync();
        }
    }
}
