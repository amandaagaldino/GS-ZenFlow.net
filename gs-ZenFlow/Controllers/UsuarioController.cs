using gs_ZenFlow.Application.DTOs.Usuario;
using gs_ZenFlow.Application.UseCase;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace gs_ZenFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("GS-ZenFlow - Endpoints para gerenciamento de usuários (CRUD e Login)")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioUseCase _usuarioUseCase;

    public UsuarioController(IUsuarioUseCase usuarioUseCase)
    {
        _usuarioUseCase = usuarioUseCase;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Criar novo usuário", Description = "Infome o nome completo, email, senha, data nascimento e CPF do usuário")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var usuario = await _usuarioUseCase.CreateUsuarioAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login de usuário", Description = "Autentica um usuário no sistema")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var usuario = await _usuarioUseCase.LoginAsync(dto);
        
        if (usuario == null)
            return Unauthorized(new { message = "Email ou senha incorretos" });

        return Ok(usuario);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar usuário por ID", Description = "Retorna informações de um usuário específico")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(id);
        
        if (usuario == null)
            return NotFound(new { message = "Usuário não encontrado" });

        return Ok(usuario);
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Listar todos os usuários", Description = "Retorna lista de todos os usuários ativos")]
    [ProducesResponseType(typeof(List<UsuarioResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _usuarioUseCase.GetAllUsuariosAsync();
        return Ok(usuarios);
    }
    
    [HttpPatch("{id}/email")]
    [SwaggerOperation(Summary = "Alterar email de um usuário", Description = "Altera o email de um usuário específico")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AlterarEmail(int id, [FromBody] AlterarEmailDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var usuario = await _usuarioUseCase.AlterarEmailUsuarioAsync(id, dto);
            return Ok(usuario);
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message.Contains("não encontrado"))
                return NotFound(new { message = ex.Message });
            
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPatch("{id}/senha")]
    [SwaggerOperation(Summary = "Alterar senha de um usuário", Description = "Altera a senha de um usuário específico")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AlterarSenha(int id, [FromBody] AlterarSenhaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var usuario = await _usuarioUseCase.AlterarSenhaUsuarioAsync(id, dto);
            return Ok(usuario);
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message.Contains("não encontrado"))
                return NotFound(new { message = ex.Message });
            
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remover um usuário", Description = "Remove logicamente um usuário (desativa)")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _usuarioUseCase.DeleteUsuarioAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}