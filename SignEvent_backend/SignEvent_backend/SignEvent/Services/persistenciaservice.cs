using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SignEvent.Models;
using SignEvent.SignEvent.Models;

namespace SignEvent.Services
{
    public class PersistenciaService
    {
        private string caminhoArquivo = "dados_evento.json";

        // Salvar todos os dados em um único arquivo
        public void SalvarDados(List<Participante> participantes, List<Evento> eventos,
                                List<Atividade> atividades, List<Inscricao> inscricoes)
        {
            try
            {
                var dados = new
                {
                    Participantes = participantes,
                    Eventos = eventos,
                    Atividades = atividades,
                    Inscricoes = inscricoes
                };

                string json = JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(caminhoArquivo, json);
                Console.WriteLine("Dados salvos com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar dados: {ex.Message}");
            }
        }

        // Carregar todos os dados do arquivo
        public (List<Participante>, List<Evento>, List<Atividade>, List<Inscricao>) CarregarDados()
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    string json = File.ReadAllText(caminhoArquivo);
                    var dados = JsonSerializer.Deserialize<DadosContainer>(json);

                    if (dados != null)
                    {
                        return (dados.Participantes ?? new List<Participante>(),
                                dados.Eventos ?? new List<Evento>(),
                                dados.Atividades ?? new List<Atividade>(),
                                dados.Inscricoes ?? new List<Inscricao>());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
            }

            return (new List<Participante>(), new List<Evento>(),
                    new List<Atividade>(), new List<Inscricao>());
        }

        // Classe auxiliar para serialização
        private class DadosContainer
        {
            public List<Participante> Participantes { get; set; }
            public List<Evento> Eventos { get; set; }
            public List<Atividade> Atividades { get; set; }
            public List<Inscricao> Inscricoes { get; set; }
        }
    }
}
