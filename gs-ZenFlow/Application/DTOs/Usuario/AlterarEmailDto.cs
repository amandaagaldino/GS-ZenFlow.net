using System.ComponentModel.DataAnnotations;

namespace gs_ZenFlow.Application.DTOs.Usuario;

public class AlterarEmailDto
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;
}
