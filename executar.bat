@echo off
REM Define o título da janela do console para algo mais descritivo.
title Gerenciador de Despesas Mensais

echo =====================================================
echo  Iniciando o Gerenciador de Despesas Mensais...
echo =====================================================
echo.

REM Garante que os comandos sejam executados a partir do diretório onde o script está localizado.
cd /d "%~dp0"

REM O comando 'dotnet run' compila o projeto (se houver alterações) e o executa em seguida.
dotnet run --project .

echo.
echo O programa foi finalizado. Pressione qualquer tecla para fechar esta janela.
pause > nul