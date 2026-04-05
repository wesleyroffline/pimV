using SignEvent.SignEvent.Models;

namespace SignEvent_backend.SignEvent.Services;

/// <summary>
/// Contrato do módulo administrativo com operações de consulta e organização por LINQ.
/// </summary>
public interface IAdministradorService
{
    IReadOnlyList<Participante> FiltrarInscritos(
        IEnumerable<Participante> participantes,
        string? termoBusca,
        string? tipo,
        bool? deficienteAuditivo);

    IReadOnlyList<Atividade> OrdenarAtividades(
        IEnumerable<Atividade> atividades,
        bool ordenarPorData,
        bool somenteComLibras);

    IReadOnlyList<Atividade> ObterAtividadesDoProfessor(
        IEnumerable<Atividade> atividades,
        string nomeProfessor);

    IReadOnlyList<string> MontarMensagensComunicacao(
        IEnumerable<Atividade> atividades,
        string nomeProfessor,
        DateTime dataReferencia);
}
