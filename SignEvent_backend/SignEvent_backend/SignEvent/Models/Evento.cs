using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent_backend.SignEvent.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Fim { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
        public override string ToString()
        {
            return $"{Nome} - {Data_Inicio:dd/mm/aaaa} a {Data_Fim:dd/mm/aaaa}";
        }
    }
}
