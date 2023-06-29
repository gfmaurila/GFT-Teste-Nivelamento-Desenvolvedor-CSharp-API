using Questao5.Domain.Contracts;
using Questao5.Domain.View;

namespace Questao5.Application.Validators;
public class ConsultaContaCorrenteAtivaSpecification : ISpecification<SaldoContaCorrenteView>
{
    public bool IsSatisfied(SaldoContaCorrenteView movimentacao) => movimentacao != null && movimentacao.Ativo == 1;
    public string ErrorMessage => "INACTIVE_ACCOUNT: Conta corrente não está ativa.";
}
