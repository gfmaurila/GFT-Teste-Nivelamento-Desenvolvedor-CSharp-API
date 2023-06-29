using Questao1.Contract;

namespace Questao1.Service;
class Saque : IOperacaoBancaria
{
    private double valor;
    private const double taxaSaque = 3.50;

    public Saque(double valor)
    {
        this.valor = valor;
    }

    public void Executar(ContaBancaria conta)
    {
        conta.Saldo -= valor + taxaSaque;
    }
}
