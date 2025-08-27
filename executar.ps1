# Define o título da janela do console para algo mais descritivo.
$Host.UI.RawUI.WindowTitle = "Gerenciador de Despesas Mensais"

# Limpa a tela para uma apresentação mais limpa.
Clear-Host

Write-Host "=====================================================" -ForegroundColor Cyan
Write-Host " Iniciando o Gerenciador de Despesas Mensais..." -ForegroundColor Cyan
Write-Host "=====================================================" -ForegroundColor Cyan
Write-Host ""

# Garante que o script seja executado a partir do diretório do projeto.
# $PSScriptRoot é uma variável automática que contém o diretório do script.
Set-Location -Path $PSScriptRoot

# O comando 'dotnet run' compila o projeto (se houver alterações) e o executa.
dotnet run --project .

Write-Host ""
Write-Host "O programa foi finalizado." -ForegroundColor Yellow
Pause # Pausa o script e aguarda que o usuário pressione uma tecla para fechar.