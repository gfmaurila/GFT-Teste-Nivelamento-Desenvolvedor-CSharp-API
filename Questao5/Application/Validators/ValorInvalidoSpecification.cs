using Questao5.Application.Commands.Requests;
using Questao5.Domain.Contracts;

namespace Questao5.Application.Validators;
public class ValorInvalidoSpecification : ISpecification<CreateMovimentoCommand>
{
    public bool IsSatisfied(CreateMovimentoCommand movimentacao) => movimentacao != null && movimentacao.Valor > 0;
    public string ErrorMessage => "INVALID_VALUE: Apenas valores positivos podem ser recebidos.";
}
