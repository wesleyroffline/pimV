namespace SignEvent_backend.SignEvent.Models
{

    /// Credenciais fixas para cada perfil de usuário do sistema.
    /// Para alterar, edite os valores diretamente nesta classe.
    public static class CredenciaisFixas
    {
        // ---------- Administrador ----------
        public const string AdminUsername = "admin";
        public const string AdminSenha    = "admin123";
        public const string AdminNome     = "Administrador";
        public const string AdminRole     = "admin";

        // ---------- Professor ----------
        public const string ProfessorUsername = "professor";
        public const string ProfessorSenha    = "prof123";
        public const string ProfessorNome     = "Professor";
        public const string ProfessorRole     = "professor";

        // ---------- Aluno ----------
        public const string AlunoUsername = "aluno";
        public const string AlunoSenha    = "aluno123";
        public const string AlunoNome     = "Aluno";
        public const string AlunoRole     = "aluno";

        /// Autentica um usuário com base nas credenciais fixas.
        /// Retorna o role do usuário autenticado, ou null se inválido.

        public static string? Autenticar(string username, string senha)
        {
            if (username == AdminUsername    && senha == AdminSenha)    return AdminRole;
            if (username == ProfessorUsername && senha == ProfessorSenha) return ProfessorRole;
            if (username == AlunoUsername    && senha == AlunoSenha)    return AlunoRole;
            return null;
        }
    }
}
