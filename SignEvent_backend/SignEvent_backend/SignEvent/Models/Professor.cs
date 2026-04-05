namespace SignEvent_backend.SignEvent.Models;

/// <summary>
/// Representa o palestrante/professor responsável por atividades acadêmicas.
/// </summary>
public class Professor : UsuarioBase
{
    public string AreaAtuacao { get; private set; } = string.Empty;

    public Professor()
    {
    }

    public Professor(int id, string nome, string email, string areaAtuacao)
        : base(id, nome, email)
    {
        AreaAtuacao = areaAtuacao;
    }

    public void AtualizarAreaAtuacao(string areaAtuacao)
    {
        AreaAtuacao = areaAtuacao;
    }
}
