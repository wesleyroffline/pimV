using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent_backend.SignEvent.Models
{
    public class Participante
    {
        public int Id {  get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Tipo { get; set; } // Profissional ou Aluno

        public DateTime Data_Inscricao { get; set; }
        public bool DeficienteAuditivo { get; set; }

        public Participante()
        {
            Data_Inscricao = DateTime.Now;
        }
        public override string ToString()
        {
            return $"ID: {Id} | {Nome} | {Email} | {Tipo}";
        }
    }
}
