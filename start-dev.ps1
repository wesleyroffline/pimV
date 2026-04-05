Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$rootDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$backendProject = Join-Path $rootDir "SignEvent_backend/SignEvent_backend/SignEvent_backend.csproj"
$frontendDir = Join-Path $rootDir "SignEvent_front-end"
$frontendPort = if ($env:FRONTEND_PORT) { $env:FRONTEND_PORT } else { "5500" }

function Get-PythonCommand {
    if (Get-Command py -ErrorAction SilentlyContinue) {
        return @{ FileName = "py"; Arguments = @("-3", "-m", "http.server", $frontendPort) }
    }

    if (Get-Command python -ErrorAction SilentlyContinue) {
        return @{ FileName = "python"; Arguments = @("-m", "http.server", $frontendPort) }
    }

    throw "Erro: Python não encontrado no PATH. Instale Python 3 para iniciar o frontend."
}

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw "Erro: dotnet não encontrado no PATH."
}

if (-not (Test-Path $backendProject)) {
    throw "Erro: projeto backend não encontrado em: $backendProject"
}

if (-not (Test-Path $frontendDir)) {
    throw "Erro: pasta frontend não encontrada em: $frontendDir"
}

$pythonCommand = Get-PythonCommand
$backendProcess = $null
$frontendProcess = $null

try {
    Write-Host "Iniciando backend..."
    $backendProcess = Start-Process -FilePath "dotnet" -ArgumentList @("run", "--project", $backendProject) -WorkingDirectory $rootDir -PassThru

    Write-Host "Iniciando frontend em http://localhost:$frontendPort/login.html ..."
    $frontendProcess = Start-Process -FilePath $pythonCommand.FileName -ArgumentList $pythonCommand.Arguments -WorkingDirectory $frontendDir -PassThru

    Write-Host ""
    Write-Host "Projeto em execução"
    Write-Host "- Frontend: http://localhost:$frontendPort/login.html"
    Write-Host "- Backend: em execução via dotnet run"
    Write-Host ""
    Write-Host "Pressione Ctrl + C para encerrar tudo."

    while ($true) {
        if ($backendProcess.HasExited -or $frontendProcess.HasExited) {
            break
        }

        Start-Sleep -Seconds 1
    }
}
finally {
    Write-Host ""
    Write-Host "Encerrando serviços..."

    if ($backendProcess -and -not $backendProcess.HasExited) {
        Stop-Process -Id $backendProcess.Id -Force -ErrorAction SilentlyContinue
    }

    if ($frontendProcess -and -not $frontendProcess.HasExited) {
        Stop-Process -Id $frontendProcess.Id -Force -ErrorAction SilentlyContinue
    }

    Write-Host "Serviços finalizados."
}