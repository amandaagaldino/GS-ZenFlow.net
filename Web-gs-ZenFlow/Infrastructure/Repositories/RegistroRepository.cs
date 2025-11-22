using Web_gs_ZenFlow.Domain.Entities;
using Web_gs_ZenFlow.Domain.Repositories;
using Web_gs_ZenFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Web_gs_ZenFlow.Infrastructure.Repositories;

public class RegistroRepository : IRegistroRepository
{
    private readonly ApplicationDbContext _context;

    public RegistroRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Registro> AddAsync(Registro registro)
    {
        await _context.Registros.AddAsync(registro);
        await _context.SaveChangesAsync();
        return registro;
    }

    public async Task<Registro?> GetByIdAsync(int id)
    {
        return await _context.Registros
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(r => r.Id == id && r.Ativo == true);
    }
    public async Task<List<Registro>> GetAllAsync()
    {
        return await _context.Registros
            .Where(r => r.Ativo == true)
            .Include(r => r.Usuario)
            .OrderByDescending(r => r.Data)
            .ToListAsync();
    }

    public async Task<List<Registro>> GetByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Registros
            .Where(r => r.UsuarioId == usuarioId && r.Ativo == true)
            .Include(r => r.Usuario)
            .OrderByDescending(r => r.Data)
            .ToListAsync();
    }

    public async Task DeleteAsync(Registro registro)
    {
        registro.Desativar();
        _context.Registros.Update(registro);
        await _context.SaveChangesAsync();
        
    }
    
    public async Task<List<Registro>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _context.Registros
            .Where(r => r.Data >= dataInicio && r.Data <= dataFim && r.Ativo == true)
            .Include(r => r.Usuario)
            .OrderByDescending(r => r.Data)
            .ToListAsync();
    }

    public async Task<List<Registro>> GetByUsuarioIdAndDateRangeAsync(int usuarioId, DateTime dataInicio, DateTime dataFim)
    {
        return await _context.Registros
            .Where(r => r.UsuarioId == usuarioId && r.Data >= dataInicio && r.Data <= dataFim && r.Ativo == true)
            .Include(r => r.Usuario)
            .OrderByDescending(r => r.Data)
            .ToListAsync();
    }
}

