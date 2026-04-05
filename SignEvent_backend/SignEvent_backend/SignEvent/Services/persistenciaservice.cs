using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SignEvent.Models;
using SignEvent.SignEvent.Models;
using SignEvent_backend.SignEvent.Services;

namespace SignEvent.Services
{
    /// <summary>
    /// Serviço de persistência de dados com logs para auditoria e apoio documental.
    /// </summary>
    public class PersistenciaService : IPersistenciaService
    {
        private readonly string caminhoArquivo = "dados_evento.json";
        private readonly string caminhoLogErro = Path.Combine("SignEvent", "Data", "logs-erros.txt");
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        private void RegistrarErro(string operacao, Exception ex)
        {
            try
            {
                var pastaLog = Path.GetDirectoryName(caminhoLogErro);
                if (!string.IsNullOrWhiteSpace(pastaLog))
                {
                    Directory.CreateDirectory(pastaLog);
                }

                var linha = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {operacao}: {ex.Message}{Environment.NewLine}";
                File.AppendAllText(caminhoLogErro, linha);
            }
            catch
            {
                // Falha no log não deve interromper o fluxo principal.
            }
        }

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

                string json = JsonSerializer.Serialize(dados, jsonOptions);
                File.WriteAllText(caminhoArquivo, json);
                Console.WriteLine("Dados salvos com sucesso");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Erro de permissão ao salvar os dados.");
                RegistrarErro("SalvarDados - Permissao", ex);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erro de I/O ao salvar os dados.");
                RegistrarErro("SalvarDados - IO", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar dados: {ex.Message}");
                RegistrarErro("SalvarDados - Geral", ex);
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
            catch (JsonException ex)
            {
                Console.WriteLine("Arquivo de dados em formato inválido.");
                RegistrarErro("CarregarDados - JSON", ex);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erro de I/O ao carregar os dados.");
                RegistrarErro("CarregarDados - IO", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
                RegistrarErro("CarregarDados - Geral", ex);
            }

            return (new List<Participante>(), new List<Evento>(),
                    new List<Atividade>(), new List<Inscricao>());
        }

        // Classe auxiliar para serialização
        private class DadosContainer
        {
            public List<Participante> Participantes { get; set; } = new List<Participante>();
            public List<Evento> Eventos { get; set; } = new List<Evento>();
            public List<Atividade> Atividades { get; set; } = new List<Atividade>();
            public List<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
        }
    }
}
