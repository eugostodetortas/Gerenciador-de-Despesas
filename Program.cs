// /ExpenseTracker/Program.cs

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ExpenseTracker
{
    class Program
    {
        // Gerenciamento de estado
        private static GerenciadorDeDespesas gerenciador = new GerenciadorDeDespesas();
        private const string ArquivoDeDados = "despesas.json";
        private static bool dadosForamAlterados = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Organizador de Despesas Mensais!");
            CarregarDados(); // Carrega os dados automaticamente ao iniciar

            bool sair = false;
            while (!sair)
            {
                MostrarMenu();
                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        RegistrarNovaDespesa();
                        break;
                    case "2":
                        ListarTodasAsDespesas();
                        break;
                    case "3":
                        MostrarRelatorio();
                        break;
                    case "4":
                        ExportarParaPdf();
                        break;
                    case "5":
                        SalvarDados();
                        break;
                    case "6":
                        CarregarDados(isUserAction: true);
                        break;
                    case "7":
                        if (dadosForamAlterados)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Você possui alterações não salvas. Deseja realmente sair? (S/N): ");
                            Console.ResetColor();
                            string? confirmacao = Console.ReadLine();
                            if (confirmacao?.Equals("S", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                sair = true;
                            }
                        }
                        else
                        {
                            sair = true;
                        }
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                if (!sair)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
            Console.WriteLine("Obrigado por usar o organizador. Até mais!");
        }

        private static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("======================================");
            Console.WriteLine("   Gerenciador de Despesas Mensais    ");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Registrar nova despesa");
            Console.WriteLine("2. Listar todas as despesas");
            Console.WriteLine("3. Mostrar relatório de totais");
            Console.WriteLine("4. Exportar para PDF");
            Console.WriteLine("5. Salvar Dados");
            Console.WriteLine("6. Carregar Dados");
            Console.WriteLine("7. Sair");
            Console.Write("\nEscolha uma opção: ");
        }

        private static void RegistrarNovaDespesa()
        {
            try
            {
                Console.Write("Descrição: ");
                string? descricao = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(descricao))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A descrição não pode estar vazia.");
                    Console.ResetColor();
                    return;
                }

                decimal valor = LerValorDecimal("Valor (ex: 45,50): ");
                Categoria categoria = LerCategoria();

                var novaDespesa = new Despesa(descricao, valor, categoria, DateTime.Now);
                gerenciador.AdicionarDespesa(novaDespesa);
                dadosForamAlterados = true; // Marca que houve alteração

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Despesa registrada com sucesso!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nOcorreu um erro inesperado: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static decimal LerValorDecimal(string prompt)
        {
            decimal valor;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out valor) && valor > 0)
                {
                    return valor;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Valor inválido. Por favor, insira um número positivo (ex: 45,50).");
                Console.ResetColor();
            }
        }

        private static Categoria LerCategoria()
        {
            var categorias = Enum.GetValues(typeof(Categoria)).Cast<Categoria>().ToList();
            Console.WriteLine("\nEscolha uma categoria:");
            for (int i = 0; i < categorias.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {GetCategoriaDisplayName(categorias[i])}");
            }

            while (true)
            {
                Console.Write("Opção: ");
                if (int.TryParse(Console.ReadLine(), out int escolha) && escolha > 0 && escolha <= categorias.Count)
                {
                    return categorias[escolha - 1];
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Opção inválida. Por favor, insira um número entre 1 e {categorias.Count}.");
                Console.ResetColor();
            }
        }

        private static string GetCategoriaDisplayName(Categoria categoria)
        {
            // Adicione outros casos aqui se precisar de mais traduções, ex: Alimentacao -> Alimentação
            if (categoria == Categoria.Educacao)
            {
                return "Educação";
            }
            return categoria.ToString();
        }

        private static void ListarTodasAsDespesas()
        {
            Console.WriteLine("\n--- Lista de Todas as Despesas ---");
            var despesas = gerenciador.ListarDespesas();
            if (!despesas.Any())
            {
                Console.WriteLine("Nenhuma despesa registrada ainda.");
                return;
            }

            foreach (var despesa in despesas)
            {
                // Recriamos a string de exibição para usar o nome correto da categoria
                Console.WriteLine($"Descrição: {despesa.Descricao}, Valor: R$ {despesa.Valor:F2}, Categoria: {GetCategoriaDisplayName(despesa.Categoria)}, Data: {despesa.Data:dd/MM/yyyy}");
            }
        }

        private static void MostrarRelatorio()
        {
            Console.WriteLine("\n--- Relatório de Despesas ---");
            var totalPorCategoria = gerenciador.ObterTotalPorCategoria();

            if (!totalPorCategoria.Any())
            {
                Console.WriteLine("Nenhuma despesa para gerar relatório.");
                return;
            }

            Console.WriteLine("Total por Categoria:");
            foreach (var item in totalPorCategoria)
            {
                Console.WriteLine($"- {GetCategoriaDisplayName(item.Key),-12}: R$ {item.Value:F2}");
            }

            Console.WriteLine("------------------------------------");
            decimal totalGeral = gerenciador.CalcularTotalDespesas();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"TOTAL GERAL: R$ {totalGeral:F2}");
            Console.ResetColor();
        }

        private static void ExportarParaPdf()
        {
            var despesas = gerenciador.ListarDespesas().ToList();
            if (!despesas.Any())
            {
                Console.WriteLine("Nenhuma despesa para exportar.");
                return;
            }

            try
            {
                Console.Write("Digite o nome do arquivo para salvar (ex: despesas.pdf): ");
                string? nomeArquivo = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nomeArquivo))
                {
                    nomeArquivo = $"RelatorioDespesas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                    Console.WriteLine($"Nome do arquivo não fornecido. Usando nome padrão: {nomeArquivo}");
                }

                if (!nomeArquivo.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    nomeArquivo += ".pdf";
                }

                // Adicione esta linha para usar a licença comunitária do QuestPDF
                QuestPDF.Settings.License = LicenseType.Community;
                var culturaBrasileira = new CultureInfo("pt-BR");

                QuestPDF.Fluent.Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);

                        page.Header()
                            .Text("Relatório de Despesas")
                            .SemiBold().FontSize(20);

                        page.Content()
                            .Column(col =>
                            {
                                col.Item().PaddingBottom(5).Text("Lista de Despesas").SemiBold().FontSize(16);
                                col.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(3);
                                        columns.RelativeColumn(1.5f);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(2);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Background("#4682B4").Padding(5).Text("Descrição").FontColor("#FFF");
                                        header.Cell().Background("#4682B4").Padding(5).Text("Valor").FontColor("#FFF");
                                        header.Cell().Background("#4682B4").Padding(5).Text("Categoria").FontColor("#FFF");
                                        header.Cell().Background("#4682B4").Padding(5).Text("Data").FontColor("#FFF");
                                    });

                                    foreach (var despesa in despesas)
                                    {
                                        table.Cell().BorderBottom(1).BorderColor("#EEE").Padding(5).Text(despesa.Descricao);
                                        table.Cell().BorderBottom(1).BorderColor("#EEE").Padding(5).Text(despesa.Valor.ToString("C", culturaBrasileira));
                                        table.Cell().BorderBottom(1).BorderColor("#EEE").Padding(5).Text(GetCategoriaDisplayName(despesa.Categoria));
                                        table.Cell().BorderBottom(1).BorderColor("#EEE").Padding(5).Text(despesa.Data.ToString("dd/MM/yyyy HH:mm"));
                                    }
                                });

                                col.Item().PaddingTop(25);

                                var totalPorCategoria = gerenciador.ObterTotalPorCategoria();
                                if (totalPorCategoria.Any())
                                {
                                    col.Item().PaddingBottom(5).Text("Resumo por Categoria").SemiBold().FontSize(16);
                                    foreach (var item in totalPorCategoria)
                                    {
                                        col.Item().Text($"{GetCategoriaDisplayName(item.Key),-12}: {item.Value.ToString("C", culturaBrasileira)}");
                                    }
                                }

                                col.Item().PaddingTop(10);

                                decimal totalGeral = gerenciador.CalcularTotalDespesas();
                                col.Item().AlignRight().Text($"TOTAL GERAL: {totalGeral.ToString("C", culturaBrasileira)}").SemiBold().FontSize(14);
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(x => { x.Span("Página "); x.CurrentPageNumber(); });
                    });
                }).GeneratePdf(nomeArquivo);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nDados exportados com sucesso para: {Path.GetFullPath(nomeArquivo)}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nOcorreu um erro ao exportar para PDF: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static void SalvarDados()
        {
            try
            {
                var despesas = gerenciador.ListarDespesas();
                string json = JsonConvert.SerializeObject(despesas, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(ArquivoDeDados, json);

                dadosForamAlterados = false; // Reseta a flag após salvar
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nDados salvos com sucesso no arquivo: {ArquivoDeDados}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nOcorreu um erro ao salvar os dados: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static void CarregarDados(bool isUserAction = false)
        {
            if (!File.Exists(ArquivoDeDados))
            {
                if (isUserAction)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nArquivo de dados '{ArquivoDeDados}' não encontrado. Nenhum dado foi carregado.");
                    Console.ResetColor();
                }
                // Se não for uma ação do usuário (carga inicial), não mostra mensagem, pois é um estado normal.
                return;
            }

            try
            {
                string json = File.ReadAllText(ArquivoDeDados);
                var despesasCarregadas = JsonConvert.DeserializeObject<List<Despesa>>(json);

                // Reinicia o gerenciador e adiciona as despesas carregadas
                gerenciador = new GerenciadorDeDespesas();
                if (despesasCarregadas != null)
                {
                    foreach (var despesa in despesasCarregadas)
                    {
                        gerenciador.AdicionarDespesa(despesa);
                    }
                }

                dadosForamAlterados = false; // Reseta a flag após carregar
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nDados carregados com sucesso do arquivo: {ArquivoDeDados}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nOcorreu um erro ao carregar os dados: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
