# SignEvent

Documentação oficial do projeto SignEvent.

Este repositório contém:

- Front-end em HTML, CSS e JavaScript puro.
- Back-end em C#.
- Fluxo de autenticação no front desacoplado da regra de login.

## Objetivo do projeto

O front-end apenas coleta credenciais e envia para a API C#.

Não existe validação local de usuário e senha no navegador.

A regra de autenticação e autorização pertence ao back-end.

## Estrutura do repositório

- SignEvent_front-end
  - login.html: tela de login
  - login.js: envio das credenciais para API
  - auth.js: sessão, autenticação e controle de acesso
  - protected.js: proteção das páginas por role
  - admin.html, aluno.html, professor.html: páginas protegidas
  - config.js: URL base da API
  - style.css: estilos
- SignEvent_backend
  - código do projeto C#

## Pré-requisitos

Instale:

1. .NET SDK (recomendado: versão 8 ou superior)
2. Python 3 (para servir o front localmente) ou outro servidor estático

## Como executar o projeto

### Execucao rapida com um comando

Na raiz do repositorio:

./start-dev.sh

O script:

- inicia o back-end com dotnet run
- inicia o front-end em http://localhost:5500/login.html
- encerra os dois servicos ao pressionar Ctrl + C

Opcional:

- para mudar a porta do front-end: FRONTEND_PORT=8080 ./start-dev.sh

### 1. Rodar o back-end

Na raiz do repositório:

dotnet restore

dotnet run --project SignEvent_backend/SignEvent_backend/SignEvent_backend.csproj

Observação:

- O front foi preparado para consumir o endpoint de login em /api/auth/login.
- Se o endpoint estiver em outro projeto ou porta, ajuste a URL em SignEvent_front-end/config.js.

### 2. Configurar URL da API no front

Arquivo:

SignEvent_front-end/config.js

Exemplo:

window.API_BASE_URL = "http://localhost:5000";

### 3. Rodar o front-end

Em outro terminal:

cd SignEvent_front-end

python3 -m http.server 5500

Abra no navegador:

http://localhost:5500/login.html

## Fluxo de autenticação implementado

1. Usuário informa username e password na tela de login.
2. O front envia as credenciais via POST para /api/auth/login.
3. Se a API retornar sucesso:
   - salva user no localStorage
   - salva token no localStorage (quando existir)
   - redireciona por role
4. Se falhar:
   - exibe mensagem de credenciais inválidas

Regras de redirecionamento:

- ADMIN para admin.html
- ALUNO para aluno.html
- PROFESSOR para professor.html

## Sessão e proteção de páginas

- Se não existir usuário em sessão, a navegação é enviada para login.html.
- As páginas protegidas validam role apenas para controle de interface.
- A autorização real sempre deve ser validada no back-end.

## Header Authorization

Quando há token salvo, as chamadas autenticadas podem usar:

Authorization: Bearer TOKEN

No projeto isso está centralizado em auth.js por meio dos helpers de headers e fetch autenticado.

## Contrato esperado do login da API

Requisição:

POST /api/auth/login

Body:

{
  "username": "admin",
  "password": "senha"
}

Resposta de sucesso esperada:

{
  "success": true,
  "user": {
    "username": "admin",
    "role": "ADMIN"
  },
  "token": "jwt-ou-simples"
}

## CORS (se front e API em portas diferentes)

Se estiver rodando front e API em origens diferentes, habilite CORS no back-end para a origem do front.

Exemplo de origem local do front:

http://localhost:5500

## Boas práticas aplicadas

- Sem lista de usuários no front.
- Sem senha fixa em JavaScript.
- Sem duplicar regra de autenticação no cliente.
- Front preparado para JWT ou sessão real.

## Status atual

- Documentação de execução pronta.
- Fluxo de login no front pronto para integração com API C#.
- Projeto pronto para evolução de endpoints protegidos.
