using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5.Application.Validators;
public class ContaCorrenteExisteSpecification : ISpecification<Contacorrente>
{
    public bool IsSatisfied(Contacorrente conta) => conta != null;
    public string ErrorMessage => "INVALID_ACCOUNT: Apenas contas correntes cadastradas podem receber movimentação";
}
