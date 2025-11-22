using System.ComponentModel.DataAnnotations;

namespace Web_gs_ZenFlow.Application.DTOs.Usuario;

public class AlterarSenhaDto
{
    [Required(ErrorMessage = "Senha atual é obrigatória")]
    public string SenhaAtual { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nova senha é obrigatória")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Nova senha deve ter entre 6 e 50 caracteres")]
    public string NovaSenha { get; set; } = string.Empty;
}

