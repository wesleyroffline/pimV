using SignEvent.SignEvent.Models;

namespace SignEvent_backend.SignEvent.Services;

/// <summary>
/// Contrato de persistência em arquivo para armazenamento das entidades acadêmicas.
/// </summary>
public interface IPersistenciaService
{
    void SalvarDados(
        List<Participante> participantes,
        List<Evento> eventos,
        List<Atividade> atividades,
        List<Inscricao> inscricoes);

    (List<Participante> participantes, List<Evento> eventos, List<Atividade> atividades, List<Inscricao> inscricoes)
        CarregarDados();
}
