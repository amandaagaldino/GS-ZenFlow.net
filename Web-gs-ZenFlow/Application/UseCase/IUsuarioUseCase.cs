using Web_gs_ZenFlow.Application.DTOs.Usuario;

namespace Web_gs_ZenFlow.Application.UseCase;

public interface IUsuarioUseCase
{
    Task<UsuarioResponseDto> CreateUsuarioAsync(CreateUsuarioDto dto);
    Task<UsuarioResponseDto?> GetUsuarioByIdAsync(int id);
    Task<List<UsuarioResponseDto>> GetAllUsuariosAsync();
    Task<UsuarioResponseDto?> LoginAsync(LoginDto dto);
    Task<UsuarioResponseDto> AlterarEmailUsuarioAsync(int id, AlterarEmailDto dto);
    Task<UsuarioResponseDto> AlterarSenhaUsuarioAsync(int id, AlterarSenhaDto dto);
    Task DeleteUsuarioAsync(int id);
}

