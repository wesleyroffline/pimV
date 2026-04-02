using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent_backend.SignEvent.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public DateTime DataHora { get; set; }
        public string Palestrante { get; set; }
        public int Vagas { get; set; }
        public string Local { get; set; }
        public bool TemLibras { get; set; }
        public override string ToString()
        {
            string libras = TemLibras ? "[COM LIBRAS]" : "";
            return $"{DataHora:dd/mm/aaaa HH:MM} - {Titulo} - {Palestrante}{libras}";
        }
    }
}
