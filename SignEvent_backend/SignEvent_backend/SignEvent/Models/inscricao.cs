using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent_backend.SignEvent.Models
{
    public class Inscricao
    {
        public int Id { get; set; }
        public int IdParticipante { get; set; }
        public List<int> AtividadesIds { get; set; }
        public DateTime DataInscricao { get; set; }
        public string Status { get; set; } //Confirmada ou Cancelada
        public bool Certificado_Emitido { get; set; }

        public Inscricao()
        {
            AtividadesIds = new List<int>();
            DataInscricao = DateTime.Now;
            Status = "Confirmada";
            Certificado_Emitido = false;
        }
        public override string ToString()
        {
            return $"Inscrição #{Id} - Participante: {IdParticipante} - Status: {Status}";
        }
    }
}
