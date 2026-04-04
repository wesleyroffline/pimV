using SignEvent.SignEvent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignEvent_backend.SignEvent.Services
{
    public class ParticipanteService
    {
        private List<Participante> _participantes = new List<Participante>();

        public void Cadastrar(Participante novo)
        {
            // Gera um ID incremental simples
            novo.Id = _participantes.Count + 1;
            _participantes.Add(novo);
        }

        public List<Participante> ObterTodos()
        {
            return _participantes;
        }
    }
}
