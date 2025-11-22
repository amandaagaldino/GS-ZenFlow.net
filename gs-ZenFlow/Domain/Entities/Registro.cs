namespace gs_ZenFlow.Domain.Entities;

public class Registro
{
    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public int NivelEstresse { get; private set; } // 1-5
    public string? Observacoes { get; private set; }
    public DateTime Data { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAtualizacao { get; private set; }
    public bool Ativo { get; private set; }

    // Navegação
    public Usuario Usuario { get; private set; } = null!;

    private Registro() { }

    public Registro(int usuarioId, int nivelEstresse, string? observacoes = null)
    {
        if (nivelEstresse < 1 || nivelEstresse > 5)
            throw new ArgumentException("Nível de estresse deve estar entre 1 e 5", nameof(nivelEstresse));

        UsuarioId = usuarioId;
        NivelEstresse = nivelEstresse;
        Observacoes = observacoes;
        Data = DateTime.UtcNow;
        DataCriacao = DateTime.UtcNow;
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
        DataAtualizacao = DateTime.UtcNow;
    }
}