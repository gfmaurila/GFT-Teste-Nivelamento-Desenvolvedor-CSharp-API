using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Validators;
using Questao5.Application.ViewModels;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers;
public class GetContaCorrenteQueryHandler : IRequestHandler<GetContaCorrenteQuery, ContaCorrenteModel>
{
    private readonly IContacorrenteRepository _repo;

    public GetContaCorrenteQueryHandler(IContacorrenteRepository repo) => _repo = repo;

    public async Task<ContaCorrenteModel> Handle(GetContaCorrenteQuery request, CancellationToken cancellationToken)
    {
        var conta = await _repo.GetByNumeroAsync(request.NumeroContaCorrente);

        var specifications = new List<ISpecification<Contacorrente>>
        {
            new ContaCorrenteExisteSpecification(),
            new ContaCorrenteAtivaSpecification(),
        };

        foreach (var specification in specifications)
        {
            if (!specification.IsSatisfied(conta))
            {
                throw new Exception(specification.ErrorMessage);
            }
        }
        return new ContaCorrenteModel(conta.IdContacorrente);
    }
}