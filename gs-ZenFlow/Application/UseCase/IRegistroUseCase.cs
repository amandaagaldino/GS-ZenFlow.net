using gs_ZenFlow.Application.DTOs.Registro;

namespace gs_ZenFlow.Application.UseCase;

public interface IRegistroUseCase
{
    Task<RegistroResponseDto> CreateRegistroAsync(int usuarioId, CreateRegistroDto dto);
    Task<RegistroResponseDto?> GetRegistroByIdAsync(int id);
    Task<List<RegistroResponseDto>> GetAllRegistrosAsync();
    Task<List<RegistroResponseDto>> GetRegistrosByUsuarioIdAsync(int usuarioId);
    Task DeleteRegistroAsync(int id, int usuarioId);
}
