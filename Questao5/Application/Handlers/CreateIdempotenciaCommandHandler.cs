using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers;
public class CreateIdempotenciaCommandHandler : IRequestHandler<CreateIdempotenciaCommand, string>
{
    private readonly IIdempotenciaRepository _idempotenciaRepo;

    public CreateIdempotenciaCommandHandler(IIdempotenciaRepository idempotenciaRepo)
    {
        _idempotenciaRepo = idempotenciaRepo;
    }

    public async Task<string> Handle(CreateIdempotenciaCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.ChaveIdempotencia))
            await _idempotenciaRepo.AddAsync(new Idempotencia(request.ChaveIdempotencia, JsonConvert.SerializeObject(request.Requisicao), request.Resultado));

        return null;
    }
}