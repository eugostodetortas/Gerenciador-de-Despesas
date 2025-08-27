using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker
{
    public class GerenciadorDeDespesas
    {
        private readonly List<Despesa> _despesas;

        public GerenciadorDeDespesas()
        {
            _despesas = new List<Despesa>();
        }

        public void AdicionarDespesa(Despesa despesa) => _despesas.Add(despesa);

        public IReadOnlyList<Despesa> ListarDespesas() => _despesas.AsReadOnly();

        public decimal CalcularTotalDespesas() => _despesas.Sum(d => d.Valor);

        public Dictionary<Categoria, decimal> ObterTotalPorCategoria()
        {
            return _despesas
                .GroupBy(d => d.Categoria)
                .ToDictionary(g => g.Key, g => g.Sum(d => d.Valor));
        }
    }
}