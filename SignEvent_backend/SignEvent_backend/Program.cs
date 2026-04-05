using SignEvent_backend.SignEvent.Models;
using SignEvent_backend.SignEvent.Services;
using SignEvent.Models;
using SignEvent.SignEvent.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAdministradorService, AdministradorService>();
builder.Services.AddSingleton<IInscricaoCertificadoService, InscricaoCertificadoService>();

// Libera o front-end local (CORS)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();

var participantes = new List<Participante>();
var atividades = new List<Atividade>();
var inscricoes = new List<Inscricao>();
var certificados = new Dictionary<int, Certificado>();

// POST /api/auth/login
// Body: { "username": "...", "password": "..." }
// Resposta: { "success": true, "user": { "role": "admin|professor|aluno" } }
app.MapPost("/api/auth/login", (LoginRequest req) =>
{
    var role = CredenciaisFixas.Autenticar(req.Username, req.Password);

    if (role is null)
        return Results.Unauthorized();

    string nome = role switch
    {
        CredenciaisFixas.AdminRole     => CredenciaisFixas.AdminNome,
        CredenciaisFixas.ProfessorRole => CredenciaisFixas.ProfessorNome,
        _                              => CredenciaisFixas.AlunoNome
    };

    return Results.Ok(new
    {
        success = true,
        user    = new { role, nome, username = req.Username }
    });
});

app.MapGet("/api/admin/inscritos", (
    IAdministradorService adminService,
    string? termo,
    string? tipo,
    bool? deficienteAuditivo) =>
{
    var resultado = adminService.FiltrarInscritos(participantes, termo, tipo, deficienteAuditivo);
    return Results.Ok(resultado);
});

app.MapGet("/api/admin/atividades", (
    IAdministradorService adminService,
    bool ordenarPorData = true,
    bool somenteComLibras = false) =>
{
    var resultado = adminService.OrdenarAtividades(atividades, ordenarPorData, somenteComLibras);
    return Results.Ok(resultado);
});

app.MapGet("/api/professor/{nomeProfessor}/atividades", (
    IAdministradorService adminService,
    string nomeProfessor) =>
{
    var resultado = adminService.ObterAtividadesDoProfessor(atividades, nomeProfessor);
    return Results.Ok(resultado);
});

app.MapGet("/api/professor/{nomeProfessor}/mensagens", (
    IAdministradorService adminService,
    string nomeProfessor) =>
{
    var mensagens = adminService.MontarMensagensComunicacao(atividades, nomeProfessor, DateTime.Today);
    return Results.Ok(mensagens);
});

app.MapPost("/api/aluno/inscricao", (
    IInscricaoCertificadoService inscricaoService,
    InscricaoRapidaRequest request) =>
{
    var participante = participantes.FirstOrDefault(p => p.Id == request.IdParticipante);
    if (participante is null)
    {
        return Results.NotFound(new { message = "Participante não encontrado." });
    }

    var inscricao = inscricaoService.RegistrarInscricaoRapida(participante, request.AtividadesIds, inscricoes);
    return Results.Ok(inscricao);
});

app.MapPost("/api/aluno/certificado", (
    IInscricaoCertificadoService inscricaoService,
    CertificadoRapidoRequest request) =>
{
    var participante = participantes.FirstOrDefault(p => p.Id == request.IdParticipante);
    if (participante is null)
    {
        return Results.NotFound(new { message = "Participante não encontrado." });
    }

    var certificado = inscricaoService.GerarCertificadoRapido(participante, request.CargaHoraria, certificados);
    return Results.Ok(certificado);
});

app.Run();

// Modelo da requisição de login
record LoginRequest(string Username, string Password);

// Modelo da operação crítica de inscrição
record InscricaoRapidaRequest(int IdParticipante, List<int> AtividadesIds);

// Modelo da operação crítica de certificado
record CertificadoRapidoRequest(int IdParticipante, int CargaHoraria);