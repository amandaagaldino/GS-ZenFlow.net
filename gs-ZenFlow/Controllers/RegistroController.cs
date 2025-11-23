using gs_ZenFlow.Application.DTOs.Registro;
using gs_ZenFlow.Application.UseCase;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace gs_ZenFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("GS-ZenFlow - Endpoints para gerenciamento de registros de estresse")]
public class RegistroController : ControllerBase
{
    private readonly IRegistroUseCase _registroUseCase;

    public RegistroController(IRegistroUseCase registroUseCase)
    {
        _registroUseCase = registroUseCase;
    }

    [HttpPost("usuario/{usuarioId}")]
    [SwaggerOperation(Summary = "Criar novo registro de estresse", Description = "Cria um novo registro de estresse para um usuário")]
    [ProducesResponseType(typeof(RegistroResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int usuarioId, [FromBody] CreateRegistroDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var registro = await _registroUseCase.CreateRegistroAsync(usuarioId, dto);
            return CreatedAtAction(nameof(GetById), new { id = registro.Id }, registro);
        }
        catch (InvalidOperationException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Erro ao criar registro"
            );
        }
        catch (ArgumentException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Erro ao criar registro"
            );
        }
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar registro por ID", Description = "Retorna informações de um registro específico")]
    [ProducesResponseType(typeof(RegistroResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var registro = await _registroUseCase.GetRegistroByIdAsync(id);
        
        if (registro == null)
            return Problem(
                detail: "Registro não encontrado",
                statusCode: StatusCodes.Status404NotFound,
                title: "Recurso não encontrado"
            );

        return Ok(registro);
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Listar todos os registros", Description = "Retorna lista de todos os registros ativos")]
    [ProducesResponseType(typeof(List<RegistroResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var registros = await _registroUseCase.GetAllRegistrosAsync();
        return Ok(registros);
    }
    
    [HttpGet("usuario/{usuarioId}")]
    [SwaggerOperation(Summary = "Listar registros de um usuário", Description = "Retorna todos os registros de um usuário específico")]
    [ProducesResponseType(typeof(List<RegistroResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByUsuarioId(int usuarioId)
    {
        try
        {
            var registros = await _registroUseCase.GetRegistrosByUsuarioIdAsync(usuarioId);
            return Ok(registros);
        }
        catch (InvalidOperationException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Erro ao buscar registros"
            );
        }
    }
    
    [HttpPut("{id}/usuario/{usuarioId}")]
    [SwaggerOperation(Summary = "Atualizar registro de estresse", Description = "Atualiza um registro de estresse existente")]
    [ProducesResponseType(typeof(RegistroResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, int usuarioId, [FromBody] UpdateRegistroDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var registro = await _registroUseCase.UpdateRegistroAsync(id, usuarioId, dto);
            return Ok(registro);
        }
        catch (InvalidOperationException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status404NotFound,
                title: "Recurso não encontrado"
            );
        }
        catch (UnauthorizedAccessException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Acesso não autorizado"
            );
        }
        catch (ArgumentException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Erro ao atualizar registro"
            );
        }
    }
    
    [HttpDelete("{id}/usuario/{usuarioId}")]
    [SwaggerOperation(Summary = "Remover registro", Description = "Remove logicamente um registro (desativa)")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, int usuarioId)
    {
        try
        {
            await _registroUseCase.DeleteRegistroAsync(id, usuarioId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status404NotFound,
                title: "Recurso não encontrado"
            );
        }
        catch (UnauthorizedAccessException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Acesso não autorizado"
            );
        }
    }
}