using SignEvent_backend.SignEvent.Models;

var builder = WebApplication.CreateBuilder(args);

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
        user    = new { role, nome }
    });
});

app.Run();

// Modelo da requisição de login
record LoginRequest(string Username, string Password);