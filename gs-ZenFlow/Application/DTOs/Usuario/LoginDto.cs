using System.ComponentModel.DataAnnotations;

namespace gs_ZenFlow.Application.DTOs.Usuario;

public class LoginDto
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string Senha { get; set; } = string.Empty;
}
