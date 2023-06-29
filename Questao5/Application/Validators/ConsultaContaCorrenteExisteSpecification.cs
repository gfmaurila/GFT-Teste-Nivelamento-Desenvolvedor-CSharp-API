using Questao5.Domain.Contracts;
using Questao5.Domain.View;

namespace Questao5.Application.Validators;
public class ConsultaContaCorrenteExisteSpecification : ISpecification<SaldoContaCorrenteView>
{
    public bool IsSatisfied(SaldoContaCorrenteView movimentacao) => movimentacao != null;
    public string ErrorMessage => "INVALID_ACCOUNT: Conta corrente não cadastrada.";
}
