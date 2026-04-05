using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent.SignEvent.Models
{
    public class Participante
    {
        //Identificador do participante
        public int Id {  get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // Profissional ou Aluno

        public DateTime Data_Inscricao { get; set; }
        //Verificador se o participante é ou não surdo
        public bool DeficienteAuditivo { get; set; }

        //define a data de inscricao automaticamente
        public Participante()
        {
            Data_Inscricao = DateTime.Now;
        }
        //metodo para mostrar os dados do participante
        public override string ToString()
        {
            return $"ID: {Id} | {Nome} | {Email} | {Tipo}";
        }
    }
}
