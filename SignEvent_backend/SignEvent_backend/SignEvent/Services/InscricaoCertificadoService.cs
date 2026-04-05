using SignEvent.Models;
using SignEvent.SignEvent.Models;

namespace SignEvent_backend.SignEvent.Services;

/// <summary>
/// Serviço para reduzir tempo de resposta em operações críticas do sistema.
/// </summary>
public class InscricaoCertificadoService : IInscricaoCertificadoService
{
    public Inscricao RegistrarInscricaoRapida(
        Participante participante,
        IEnumerable<int> atividadesIds,
        IList<Inscricao> inscricoes)
    {
        var novaInscricao = new Inscricao
        {
            Id = inscricoes.Count == 0 ? 1 : inscricoes.Max(i => i.Id) + 1,
            IdParticipante = participante.Id,
            AtividadesIds = atividadesIds.Distinct().ToList(),
            DataInscricao = DateTime.Now,
            Status = "Confirmada",
            Certificado_Emitido = false
        };

        inscricoes.Add(novaInscricao);
        return novaInscricao;
    }

    public Certificado GerarCertificadoRapido(
        Participante participante,
        int cargaHoraria,
        IDictionary<int, Certificado> certificadosPorParticipante)
    {
        if (certificadosPorParticipante.TryGetValue(participante.Id, out var existente))
        {
            return existente;
        }

        var novoCertificado = new Certificado
        {
            Id = certificadosPorParticipante.Count + 1,
            IdParticipante = participante.Id,
            CargaHoraria = cargaHoraria,
            CaminhoArquivo = $"certificados/{participante.Id}-{DateTime.Now:yyyyMMddHHmm}.pdf"
        };

        certificadosPorParticipante[participante.Id] = novoCertificado;
        return novoCertificado;
    }
}
