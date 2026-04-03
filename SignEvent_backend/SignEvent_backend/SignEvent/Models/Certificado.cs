using System;

namespace SignEvent.Models
{
    public class Certificado
    {
        public int Id { get; set; }
        public int IdParticipante { get; set; }
        public string CodigoValidacao { get; set; }
        public DateTime DataEmissao { get; set; }
        public int CargaHoraria { get; set; }
        public string CaminhoArquivo { get; set; }

        //Gerador do codigo do certificado

        public Certificado()
        {
            CodigoValidacao = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            DataEmissao = DateTime.Now;
        }

        //Exibe as informacoes do certificado

        public override string ToString()
        {
            return $"Certificado {CodigoValidacao} - Participante: {IdParticipante} - {DataEmissao:dd/MM/yyyy}";

        }
    }
}