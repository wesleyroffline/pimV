using SignEvent.Models;
using SignEvent.SignEvent.Models;

namespace SignEvent_backend.SignEvent.Services;

/// <summary>
/// Contrato para operações críticas otimizadas de inscrição e emissão de certificado.
/// </summary>
public interface IInscricaoCertificadoService
{
    Inscricao RegistrarInscricaoRapida(
        Participante participante,
        IEnumerable<int> atividadesIds,
        IList<Inscricao> inscricoes);

    Certificado GerarCertificadoRapido(
        Participante participante,
        int cargaHoraria,
        IDictionary<int, Certificado> certificadosPorParticipante);
}
