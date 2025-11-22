using Web_gs_ZenFlow.Domain.Entities;

namespace Web_gs_ZenFlow.Domain.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario?> GetByIdAsync(int id);
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario?> GetByCpfAsync(string cpf);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task DeleteAsync(Usuario usuario);
    Task<bool> ExistsAsync(int id);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> CpfExistsAsync(string cpf);
    Task<Usuario?> GetByEmailAndSenhaAsync(string email, string senha);
}

