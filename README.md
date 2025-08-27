#  Gerenciador de Despesas Mensais

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

Um aplicativo de console simples, porém funcional, desenvolvido em C# para ajudar no gerenciamento e organização de despesas mensais.

> **Autor:** Este projeto foi desenvolvido por **Chris (@eugostodetortas)**.

---

## 📋 Índice

- [Funcionalidades](#-funcionalidades)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Como Usar](#-como-usar)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Contribuição](#-contribuição)

---

## ✨ Funcionalidades

O Gerenciador de Despesas oferece as seguintes funcionalidades:

- **Registrar Despesas:** Adicione novas despesas com descrição, valor e categoria.
- **Listar Despesas:** Visualize uma lista completa de todas as despesas registradas.
- **Relatório de Totais:** Gere um relatório no console com o total de gastos por categoria e o total geral.
- **Exportar para PDF:** Exporte um relatório detalhado e formatado para um arquivo PDF, incluindo a lista de despesas e o resumo por categoria.
- **Persistência de Dados:** Salve o estado atual das despesas em um arquivo `despesas.json` e carregue-os posteriormente, garantindo que seus dados não sejam perdidos.
- **Aviso de Saída:** O sistema avisa o usuário sobre alterações não salvas antes de fechar o programa.

---

## 🚀 Tecnologias Utilizadas

Este projeto foi construído com as seguintes tecnologias e bibliotecas:

- **.NET / C#:** Plataforma e linguagem principal do projeto.
- **Newtonsoft.Json:** Utilizada para serializar e desserializar os dados das despesas para o formato JSON, permitindo salvar e carregar o estado da aplicação.
- **QuestPDF:** Uma biblioteca moderna para a geração de relatórios em formato PDF de forma programática e fluente.

---

## ⚙️ Como Usar

### Pré-requisitos

- **.NET SDK** (versão 6.0 ou superior)

### Opção 1: Executando via Linha de Comando (Padrão)

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/eugostodetortas/GerenciadorDeDespesas.git
   ```
2. **Navegue até a pasta do projeto:**
   ```bash
   cd GerenciadorDeDespesas/Gerenciador
   ```
3. **Restaure as dependências do projeto:**
   ```bash
   dotnet restore
   ```
4. **Execute a aplicação:**
   ```bash
   dotnet run
   ```

Ao executar, um menu interativo será exibido no console.

### Opção 2: Executando via Scripts (Windows)

Para facilitar a execução no Windows, o projeto inclui scripts que automatizam o processo de compilação e execução.

#### Usando o arquivo `.bat`

1.  Navegue até a pasta do projeto (`Gerenciador`) no seu explorador de arquivos.
2.  Dê um **duplo clique** no arquivo `run.bat`.
3.  O script irá compilar e executar o projeto automaticamente em uma nova janela de console.

#### Usando o arquivo `.ps1` (PowerShell)

1.  Abra um terminal **PowerShell**.
2.  Navegue até a pasta do projeto: `cd GerenciadorDeDespesas/Gerenciador`
3.  Execute o script com o seguinte comando:
    ```powershell
    .\run.ps1
    ```
4.  **Observação:** Caso encontre um erro relacionado à política de execução de scripts, pode ser necessário permitir a execução de scripts locais. Abra o PowerShell **como Administrador** e execute o comando abaixo para permitir scripts assinados para o usuário atual:
    ```powershell
    Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
    ```

---

---

## 📂 Estrutura do Projeto

- `Program.cs`: Arquivo principal que contém a lógica da interface do console (menu, entrada de usuário) e orquestra as operações.
- `Despesa.cs`: (Implícito) Classe que modela uma despesa (descrição, valor, categoria, data).
- `GerenciadorDeDespesas.cs`: (Implícito) Classe responsável pela lógica de negócio (adicionar, listar, calcular totais).
- `despesas.json`: Arquivo gerado para armazenar as despesas salvas.
- `*.pdf`: Arquivos de relatório gerados pela funcionalidade de exportação.

---

## 🤝 Contribuição

Contribuições são bem-vindas! Se você tiver ideias para melhorias ou encontrar algum problema, sinta-se à vontade para abrir uma *issue* ou enviar um *pull request*.

1. Faça um *fork* do projeto.
2. Crie uma nova *branch* (`git checkout -b feature/nova-funcionalidade`).
3. Faça o *commit* de suas alterações (`git commit -m 'Adiciona nova funcionalidade'`).
4. Faça o *push* para a *branch* (`git push origin feature/nova-funcionalidade`).
5. Abra um *Pull Request*.
