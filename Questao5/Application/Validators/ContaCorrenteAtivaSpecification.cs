using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5.Application.Validators;
public class ContaCorrenteAtivaSpecification : ISpecification<Contacorrente>
{
    public bool IsSatisfied(Contacorrente conta) => conta != null && conta.Ativo == 1;
    public string ErrorMessage => "INACTIVE_ACCOUNT: Apenas contas correntes ativas podem receber movimentação.";
}
