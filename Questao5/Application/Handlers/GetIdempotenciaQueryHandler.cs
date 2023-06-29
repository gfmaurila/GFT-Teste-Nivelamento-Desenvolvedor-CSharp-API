using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.ViewModels;
using Questao5.Domain.Contracts;

namespace Questao5.Application.Handlers;
public class GetIdempotenciaQueryHandler : IRequestHandler<GetIdempotenciaQuery, IdempotenciaModel>
{
    private readonly IIdempotenciaRepository _repo;

    public GetIdempotenciaQueryHandler(IIdempotenciaRepository repo) => _repo = repo;

    public async Task<IdempotenciaModel> Handle(GetIdempotenciaQuery request, CancellationToken cancellationToken)
    {
        var idempotenciaEntry = await _repo.GetByChaveIdempotenciaAsync(request.ChaveIdempotencia);
        if (idempotenciaEntry != null)
            return new IdempotenciaModel(idempotenciaEntry.Resultado);

        return null;
    }
}