using gs_ZenFlow.Application.DTOs.Usuario;
using gs_ZenFlow.Domain.Entities;
using gs_ZenFlow.Domain.Repositories;

namespace gs_ZenFlow.Application.UseCase;

public class UsuarioUseCase : IUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioResponseDto> CreateUsuarioAsync(CreateUsuarioDto dto)
    {
        if (await _usuarioRepository.EmailExistsAsync(dto.Email))
        {
            throw new InvalidOperationException("Email já está em uso");
        }

        if (await _usuarioRepository.CpfExistsAsync(dto.Cpf))
        {
            throw new InvalidOperationException("CPF já está em uso");
        }

        var usuario = new Usuario(
            dto.NomeCompleto,
            dto.Email,
            dto.Senha,
            dto.DataNascimento,
            dto.Cpf,
            dto.IsGestor
        );
        
        var usuarioCriado = await _usuarioRepository.AddAsync(usuario);

        return new UsuarioResponseDto
        {
            Id = usuarioCriado.Id,
            NomeCompleto = usuarioCriado.NomeCompleto,
            Email = usuarioCriado.Email,
            DataNascimento = usuarioCriado.DataNascimento,
            Cpf = usuarioCriado.Cpf,
            IsGestor = usuarioCriado.IsGestor,
            DataCriacao = usuarioCriado.DataCriacao,
            DataAtualizacao = usuarioCriado.DataAtualizacao,
            Ativo = usuarioCriado.Ativo
        };
    }

    public async Task<UsuarioResponseDto?> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        
        if (usuario == null)
            return null;

        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            NomeCompleto = usuario.NomeCompleto,
            Email = usuario.Email,
            DataNascimento = usuario.DataNascimento,
            Cpf = usuario.Cpf,
            IsGestor = usuario.IsGestor,
            DataCriacao = usuario.DataCriacao,
            DataAtualizacao = usuario.DataAtualizacao,
            Ativo = usuario.Ativo
        };
    }

    public async Task<List<UsuarioResponseDto>> GetAllUsuariosAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();

        return usuarios.Select(usuario => new UsuarioResponseDto
        {
            Id = usuario.Id,
            NomeCompleto = usuario.NomeCompleto,
            Email = usuario.Email,
            DataNascimento = usuario.DataNascimento,
            Cpf = usuario.Cpf,
            IsGestor = usuario.IsGestor,
            DataCriacao = usuario.DataCriacao,
            DataAtualizacao = usuario.DataAtualizacao,
            Ativo = usuario.Ativo
        }).ToList();
    }

    public async Task<UsuarioResponseDto?> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.GetByEmailAndSenhaAsync(dto.Email, dto.Senha);
        
        if (usuario == null)
            return null;

        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            NomeCompleto = usuario.NomeCompleto,
            Email = usuario.Email,
            DataNascimento = usuario.DataNascimento,
            Cpf = usuario.Cpf,
            IsGestor = usuario.IsGestor,
            DataCriacao = usuario.DataCriacao,
            DataAtualizacao = usuario.DataAtualizacao,
            Ativo = usuario.Ativo
        };
    }

    public async Task<UsuarioResponseDto> AlterarEmailUsuarioAsync(int id, AlterarEmailDto dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado");

        var usuarioComEmail = await _usuarioRepository.GetByEmailAsync(dto.Email);
        if (usuarioComEmail != null && usuarioComEmail.Id != id)
        {
            throw new InvalidOperationException("Email já está em uso por outro usuário");
        }

        usuario.AlterarEmail(dto.Email);

        var usuarioAtualizado = await _usuarioRepository.UpdateAsync(usuario);

        return new UsuarioResponseDto
        {
            Id = usuarioAtualizado.Id,
            NomeCompleto = usuarioAtualizado.NomeCompleto,
            Email = usuarioAtualizado.Email,
            DataNascimento = usuarioAtualizado.DataNascimento,
            Cpf = usuarioAtualizado.Cpf,
            IsGestor = usuarioAtualizado.IsGestor,
            DataCriacao = usuarioAtualizado.DataCriacao,
            DataAtualizacao = usuarioAtualizado.DataAtualizacao,
            Ativo = usuarioAtualizado.Ativo
        };
    }

    public async Task<UsuarioResponseDto> AlterarSenhaUsuarioAsync(int id, AlterarSenhaDto dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado");

        if (usuario.Senha != dto.SenhaAtual)
            throw new InvalidOperationException("Senha atual incorreta");

        usuario.AlterarSenha(dto.NovaSenha);

        var usuarioAtualizado = await _usuarioRepository.UpdateAsync(usuario);

        return new UsuarioResponseDto
        {
            Id = usuarioAtualizado.Id,
            NomeCompleto = usuarioAtualizado.NomeCompleto,
            Email = usuarioAtualizado.Email,
            DataNascimento = usuarioAtualizado.DataNascimento,
            Cpf = usuarioAtualizado.Cpf,
            IsGestor = usuarioAtualizado.IsGestor,
            DataCriacao = usuarioAtualizado.DataCriacao,
            DataAtualizacao = usuarioAtualizado.DataAtualizacao,
            Ativo = usuarioAtualizado.Ativo
        };
    }

    public async Task DeleteUsuarioAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado");

        await _usuarioRepository.DeleteAsync(usuario);
    }
}