using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent.SignEvent.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public string Palestrante { get; set; } = string.Empty;
        public int Vagas { get; set; }
        public int VagasOcupadas { get; set; }
        public string Local { get; set; } = string.Empty;
        public bool TemLibras { get; set; }

        // Verificador se tem vaga disponivel
        public bool TemVagaDisponivel()
        {
            return VagasOcupadas < Vagas;
        }

        //Exibe as informacoes da atividade
        public override string ToString()
        {
            string libras = TemLibras ? "[COM LIBRAS]" : "";
            return $"{DataHora:dd/mm/aaaa HH:MM} - {Titulo} - {Palestrante}{libras} - Vagas: {VagasOcupadas}/{Vagas}";
        }
    }
}
