using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent_backend.SignEvent.Models
{
    public class Administrador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
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

