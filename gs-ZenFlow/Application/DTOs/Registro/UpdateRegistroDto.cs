using System.ComponentModel.DataAnnotations;

namespace gs_ZenFlow.Application.DTOs.Registro;

public class UpdateRegistroDto
{
    [Required(ErrorMessage = "Nível de estresse é obrigatório")]
    [Range(1, 5, ErrorMessage = "Nível de estresse deve estar entre 1 e 5")]
    public int NivelEstresse { get; set; }

    [StringLength(500, ErrorMessage = "Observações deve ter no máximo 500 caracteres")]
    public string? Observacoes { get; set; }
}

