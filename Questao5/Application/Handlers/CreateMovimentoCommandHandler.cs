using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Validators;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers;
public class CreateMovimentoCommandHandler : IRequestHandler<CreateMovimentoCommand, string>
{
    private readonly IMovimentoRepository _movimentacaoRepo;
    private readonly IMediator _mediator;

    public CreateMovimentoCommandHandler(IMediator mediator, IMovimentoRepository movimentacaoRepo)
    {
        _movimentacaoRepo = movimentacaoRepo;
        _mediator = mediator;
    }

    public async Task<string> Handle(CreateMovimentoCommand request, CancellationToken cancellationToken)
    {
        var queryIdempotencia = new GetIdempotenciaQuery(request.ChaveIdempotencia);
        var resultIdempotencia = await _mediator.Send(queryIdempotencia);
        if (resultIdempotencia != null)
            return resultIdempotencia.Resultado;

        var queryContaCorrente = new GetContaCorrenteQuery(request.NumeroContaCorrente);
        var resultContaCorrente = await _mediator.Send(queryContaCorrente);

        var specifications = new List<ISpecification<CreateMovimentoCommand>>
        {
            new ValorInvalidoSpecification(),
            new TipoMovimentoSpecification(),
        };

        foreach (var specification in specifications)
        {
            if (!specification.IsSatisfied(request))
            {
                throw new Exception(specification.ErrorMessage);
            }
        }

        var movimento = await _movimentacaoRepo.AddAsync(new Movimento(resultContaCorrente.IdContacorrente, request.TipoMovimento, request.Valor));

        await _mediator.Send(new CreateIdempotenciaCommand() { ChaveIdempotencia = request.ChaveIdempotencia, Requisicao = JsonConvert.SerializeObject(request), Resultado = movimento });

        return movimento;
    }
}