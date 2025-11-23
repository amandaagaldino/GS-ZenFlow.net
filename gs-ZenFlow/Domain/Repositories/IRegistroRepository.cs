using gs_ZenFlow.Domain.Entities;

namespace gs_ZenFlow.Domain.Repositories;

public interface IRegistroRepository
{
    Task<Registro> AddAsync(Registro registro);
    Task<Registro?> GetByIdAsync(int id);
    Task<List<Registro>> GetAllAsync();
    Task<List<Registro>> GetByUsuarioIdAsync(int usuarioId);
    Task DeleteAsync(Registro registro);
    Task<Registro> UpdateAsync(Registro registro);
    Task<List<Registro>> GetByDateRangeAsync(DateTime dataInicio, DateTime dataFim);
    Task<List<Registro>> GetByUsuarioIdAndDateRangeAsync(int usuarioId, DateTime dataInicio, DateTime dataFim);
}