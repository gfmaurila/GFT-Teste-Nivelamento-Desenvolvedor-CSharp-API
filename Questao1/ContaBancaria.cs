using Questao1.Contract;

namespace Questao1;
class ContaBancaria
{
    public double Saldo { get; set; }
    private int numero;
    private string titular;

    public ContaBancaria(int numeroConta, string nomeTitular)
    {
        numero = numeroConta;
        titular = nomeTitular;
    }

    public ContaBancaria(int numeroConta, string nomeTitular, IOperacaoBancaria operacaoBancaria) : this(numeroConta, nomeTitular)
    {
        operacaoBancaria.Executar(this);
    }

    public void RealizarOperacao(IOperacaoBancaria operacaoBancaria)
    {
        operacaoBancaria.Executar(this);
    }

    public override string ToString()
    {
        return $"Conta {numero}, Titular: {titular}, Saldo: $ {Saldo.ToString("F2")}";
    }
}