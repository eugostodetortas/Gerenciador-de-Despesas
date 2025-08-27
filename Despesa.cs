using System;

namespace ExpenseTracker
{
    public class Despesa
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime Data { get; set; }

        public Despesa(string descricao, decimal valor, Categoria categoria, DateTime data)
        {
            Descricao = descricao;
            Valor = valor;
            Categoria = categoria;
            Data = data;
        }

        public override string ToString() => $"{Data:dd/MM/yyyy} - {Descricao,-25} | {Categoria,-12} | R$ {Valor,8:F2}";
    }
}