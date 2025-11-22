namespace Web_gs_ZenFlow.Application.DTOs.Registro;

public class RegistroResponseDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioNome { get; set; } = string.Empty;
    public int NivelEstresse { get; set; }
    public string? Observacoes { get; set; }
    public DateTime Data { get; set; }
}

