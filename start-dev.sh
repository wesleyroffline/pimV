#!/usr/bin/env bash
set -euo pipefail

# Garante que o dotnet instalado em ~/.dotnet seja encontrado
export DOTNET_ROOT="${HOME}/.dotnet"
export PATH="${DOTNET_ROOT}:${DOTNET_ROOT}/tools:${PATH}"

ROOT_DIR="$(cd "$(dirname "$0")" && pwd)"
BACKEND_PROJECT="${ROOT_DIR}/SignEvent_backend/SignEvent_backend/SignEvent_backend.csproj"
FRONTEND_DIR="${ROOT_DIR}/SignEvent_front-end"
FRONTEND_PORT="${FRONTEND_PORT:-5500}"

if ! command -v dotnet >/dev/null 2>&1; then
  echo "Erro: dotnet nao encontrado no PATH."
  exit 1
fi

if ! command -v python3 >/dev/null 2>&1; then
  echo "Erro: python3 nao encontrado no PATH."
  exit 1
fi

if [ ! -f "$BACKEND_PROJECT" ]; then
  echo "Erro: projeto backend nao encontrado em: $BACKEND_PROJECT"
  exit 1
fi

if [ ! -d "$FRONTEND_DIR" ]; then
  echo "Erro: pasta frontend nao encontrada em: $FRONTEND_DIR"
  exit 1
fi

BACKEND_PID=""
FRONTEND_PID=""

cleanup() {
  echo ""
  echo "Encerrando servicos..."

  if [ -n "$BACKEND_PID" ] && kill -0 "$BACKEND_PID" >/dev/null 2>&1; then
    kill "$BACKEND_PID" >/dev/null 2>&1 || true
  fi

  if [ -n "$FRONTEND_PID" ] && kill -0 "$FRONTEND_PID" >/dev/null 2>&1; then
    kill "$FRONTEND_PID" >/dev/null 2>&1 || true
  fi

  wait >/dev/null 2>&1 || true
  echo "Servicos finalizados."
}

trap cleanup EXIT INT TERM

echo "Iniciando backend..."
dotnet run --project "$BACKEND_PROJECT" &
BACKEND_PID=$!

echo "Iniciando frontend em http://localhost:${FRONTEND_PORT}/login.html ..."
(
  cd "$FRONTEND_DIR"
  python3 -m http.server "$FRONTEND_PORT"
) &
FRONTEND_PID=$!

echo ""
echo "Projeto em execucao"
echo "- Frontend: http://localhost:${FRONTEND_PORT}/login.html"
echo "- Backend: em execucao via dotnet run"
echo ""
echo "Pressione Ctrl + C para encerrar tudo."

wait -n "$BACKEND_PID" "$FRONTEND_PID"
