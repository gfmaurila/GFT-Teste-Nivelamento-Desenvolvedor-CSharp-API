using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Validators;
using Questao5.Application.ViewModels;
using Questao5.Domain.Contracts;
using Questao5.Domain.View;

namespace Questao5.Application.Handlers;
public class GetMovimentoQueryHandler : IRequestHandler<GetAllSaldoContacorrenteQuery, SaldoMovimentoModel>
{
    private readonly IMovimentoRepository _repo;

    public GetMovimentoQueryHandler(IMovimentoRepository repo) => _repo = repo;

    public async Task<SaldoMovimentoModel> Handle(GetAllSaldoContacorrenteQuery request, CancellationToken cancellationToken)
    {
        var movimentacao = await _repo.GetSaldoAsync(request.NumeroContaCorrente);

        var specifications = new List<ISpecification<SaldoContaCorrenteView>>
        {
            new ConsultaContaCorrenteExisteSpecification(),
            new ConsultaContaCorrenteAtivaSpecification(),
        };

        foreach (var specification in specifications)
        {
            if (!specification.IsSatisfied(movimentacao))
            {
                throw new Exception(specification.ErrorMessage);
            }
        }

        return new SaldoMovimentoModel(movimentacao.NumeroConta, movimentacao.NomeTitular, DateTime.Now.ToString(), movimentacao.Saldo);
    }
}