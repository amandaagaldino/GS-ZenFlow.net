using gs_ZenFlow.Application.DTOs.Registro;
using gs_ZenFlow.Domain.Entities;
using gs_ZenFlow.Domain.Repositories;

namespace gs_ZenFlow.Application.UseCase;

public class RegistroUseCase : IRegistroUseCase
{
    private readonly IRegistroRepository _registroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    
    public RegistroUseCase(IRegistroRepository registroRepository, IUsuarioRepository usuarioRepository)
    {
        _registroRepository = registroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<RegistroResponseDto> CreateRegistroAsync(int usuarioId, CreateRegistroDto dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado");

        var registro = new Registro(usuarioId, dto.NivelEstresse, dto.Observacoes);

        var registroCriado = await _registroRepository.AddAsync(registro);

        return new RegistroResponseDto
        {
            Id = registroCriado.Id,
            UsuarioId = registroCriado.UsuarioId,
            UsuarioNome = usuario.NomeCompleto,
            NivelEstresse = registroCriado.NivelEstresse,
            Observacoes = registroCriado.Observacoes,
            Data = registroCriado.Data
        };    
    }

    public async Task<RegistroResponseDto?> GetRegistroByIdAsync(int id)
    {
        var registro = await _registroRepository.GetByIdAsync(id);
        
        if (registro == null)
            return null;

        var usuario = await _usuarioRepository.GetByIdAsync(registro.UsuarioId);

        return new RegistroResponseDto
        {
            Id = registro.Id,
            UsuarioId = registro.UsuarioId,
            UsuarioNome = usuario?.NomeCompleto ?? string.Empty,
            NivelEstresse = registro.NivelEstresse,
            Observacoes = registro.Observacoes,
            Data = registro.Data
        };
        
    }

    public async Task<List<RegistroResponseDto>> GetAllRegistrosAsync()
    {
        var registros = await _registroRepository.GetAllAsync();
        var usuarios = await _usuarioRepository.GetAllAsync();
        var usuarioDict = usuarios.ToDictionary(u => u.Id);

        return registros.Select(registro => new RegistroResponseDto
        {
            Id = registro.Id,
            UsuarioId = registro.UsuarioId,
            UsuarioNome = usuarioDict.ContainsKey(registro.UsuarioId) ? usuarioDict[registro.UsuarioId].NomeCompleto : string.Empty,
            NivelEstresse = registro.NivelEstresse,
            Observacoes = registro.Observacoes,
            Data = registro.Data,
            
        }).ToList();
        
    }

    public async Task<List<RegistroResponseDto>> GetRegistrosByUsuarioIdAsync(int usuarioId)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado");

        var registros = await _registroRepository.GetByUsuarioIdAsync(usuarioId);

        return registros.Select(registro => new RegistroResponseDto
        {
            Id = registro.Id,
            UsuarioId = registro.UsuarioId,
            UsuarioNome = usuario.NomeCompleto,
            NivelEstresse = registro.NivelEstresse,
            Observacoes = registro.Observacoes,
            Data = registro.Data
        }).ToList();
    }

    public async Task DeleteRegistroAsync(int id, int usuarioId)
    {
        var registro = await _registroRepository.GetByIdAsync(id);
        if (registro == null)
            throw new InvalidOperationException("Registro não encontrado");

        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado");

        // Gestores podem deletar qualquer registro, usuários comuns só os próprios
        if (!usuario.IsGestor && registro.UsuarioId != usuarioId)
            throw new UnauthorizedAccessException("Você não tem permissão para deletar este registro");

        await _registroRepository.DeleteAsync(registro);
    }    
}