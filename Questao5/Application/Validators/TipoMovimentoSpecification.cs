using Questao5.Application.Commands.Requests;
using Questao5.Domain.Contracts;

namespace Questao5.Application.Validators;
public class TipoMovimentoSpecification : ISpecification<CreateMovimentoCommand>
{
    public bool IsSatisfied(CreateMovimentoCommand movimentacao) =>
        movimentacao != null &&
        (movimentacao.TipoMovimento == "C" || movimentacao.TipoMovimento == "D");

    public string ErrorMessage => "INVALID_TYPE: Apenas os tipos “débito” ou “crédito” podem ser aceitos.";
}

