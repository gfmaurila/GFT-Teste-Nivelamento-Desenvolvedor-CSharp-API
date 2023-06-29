using Questao1.Contract;
namespace Questao1.Service;
class Deposito : IOperacaoBancaria
{
    private double valor;

    public Deposito(double valor)
    {
        this.valor = valor;
    }

    public void Executar(ContaBancaria conta)
    {
        conta.Saldo += valor;
    }
}
