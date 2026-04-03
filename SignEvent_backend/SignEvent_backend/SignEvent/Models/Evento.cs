using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent.SignEvent.Models
{
    public class Evento
    {
        //Identificador do evento
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Fim { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }

        //Metodo que mostra os dados do evento
        public override string ToString()
        {
            return $"{Nome} - {Data_Inicio:dd/mm/aaaa} a {Data_Fim:dd/mm/aaaa} - {Local}";
        }
    }
}
