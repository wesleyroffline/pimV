using System.Globalization;
using SignEvent.SignEvent.Models;

namespace SignEvent_backend.SignEvent.Services;

/// <summary>
/// Serviço administrativo com foco em filtros e ordenações de dados acadêmicos.
/// </summary>
public class AdministradorService : IAdministradorService
{
    public IReadOnlyList<Participante> FiltrarInscritos(
        IEnumerable<Participante> participantes,
        string? termoBusca,
        string? tipo,
        bool? deficienteAuditivo)
    {
        var consulta = participantes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(termoBusca))
        {
            var termoNormalizado = termoBusca.Trim().ToUpperInvariant();
            consulta = consulta.Where(p =>
                p.Nome.ToUpperInvariant().Contains(termoNormalizado) ||
                p.Email.ToUpperInvariant().Contains(termoNormalizado) ||
                p.CPF.ToUpperInvariant().Contains(termoNormalizado));
        }

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            var tipoNormalizado = tipo.Trim().ToUpperInvariant();
            consulta = consulta.Where(p => p.Tipo.ToUpperInvariant() == tipoNormalizado);
        }

        if (deficienteAuditivo.HasValue)
        {
            consulta = consulta.Where(p => p.DeficienteAuditivo == deficienteAuditivo.Value);
        }

        return consulta
            .OrderBy(p => p.Nome, StringComparer.Create(new CultureInfo("pt-BR"), true))
            .ThenBy(p => p.Data_Inscricao)
            .ToList();
    }

    public IReadOnlyList<Atividade> OrdenarAtividades(
        IEnumerable<Atividade> atividades,
        bool ordenarPorData,
        bool somenteComLibras)
    {
        var consulta = atividades.AsQueryable();

        if (somenteComLibras)
        {
            consulta = consulta.Where(a => a.TemLibras);
        }

        if (ordenarPorData)
        {
            return consulta.OrderBy(a => a.DataHora).ThenBy(a => a.Titulo).ToList();
        }

        return consulta.OrderBy(a => a.Titulo).ThenBy(a => a.DataHora).ToList();
    }

    public IReadOnlyList<Atividade> ObterAtividadesDoProfessor(
        IEnumerable<Atividade> atividades,
        string nomeProfessor)
    {
        if (string.IsNullOrWhiteSpace(nomeProfessor))
        {
            return Array.Empty<Atividade>();
        }

        var professorNormalizado = nomeProfessor.Trim().ToUpperInvariant();

        return atividades
            .Where(a => a.Palestrante.Trim().ToUpperInvariant() == professorNormalizado)
            .OrderBy(a => a.DataHora)
            .ToList();
    }

    public IReadOnlyList<string> MontarMensagensComunicacao(
        IEnumerable<Atividade> atividades,
        string nomeProfessor,
        DateTime dataReferencia)
    {
        var atividadesProfessor = ObterAtividadesDoProfessor(atividades, nomeProfessor);

        return atividadesProfessor.Select(atividade =>
        {
            var tipoMensagem = atividade.DataHora.Date == dataReferencia.Date
                ? "Confirmacao"
                : "Aviso de Alteracao";

            return $"[{tipoMensagem}] {atividade.Titulo} em {atividade.DataHora:dd/MM/yyyy HH:mm} ({atividade.Local}).";
        }).ToList();
    }
}
