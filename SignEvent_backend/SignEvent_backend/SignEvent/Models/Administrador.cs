namespace SignEvent_backend.SignEvent.Models
{
    /// <summary>
    /// Usuário administrador com permissões de gestão no sistema.
    /// </summary>
    public class Administrador : UsuarioBase
    {
        public string Senha { get; private set; } = string.Empty;

        public Administrador()
        {
        }

        public Administrador(int id, string nome, string email, string senha)
            : base(id, nome, email)
        {
            Senha = senha;
        }

        public void AtualizarSenha(string novaSenha)
        {
            Senha = novaSenha;
        }

        // Autentica o administrador verificando email e senha
        public bool Autenticar(string email, string senha)
        {
            return Email == email && Senha == senha;
        }

        // Exibe as informações do administrador
        public override string ToString()
        {
            return $"{Id} - {Nome} - {Email}";
        }
    }
}

