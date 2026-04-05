namespace SignEvent_backend.SignEvent.Models;

/// <summary>
/// Classe base para usuários do sistema. Estrutura preparada para documentação técnica acadêmica.
/// </summary>
public abstract class UsuarioBase
{
    public int Id { get; protected set; }
    public string Nome { get; protected set; } = string.Empty;
    public string Email { get; protected set; } = string.Empty;

    protected UsuarioBase()
    {
    }

    protected UsuarioBase(int id, string nome, string email)
    {
        Id = id;
        Nome = nome;
        Email = email;
    }

    public virtual void AtualizarDadosContato(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}
