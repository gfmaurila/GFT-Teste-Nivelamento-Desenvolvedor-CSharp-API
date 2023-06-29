using MediatR;
using Questao5.Application.ViewModels;

namespace Questao5.Application.Queries.Requests;
public class GetIdempotenciaQuery : IRequest<IdempotenciaModel>
{
    public GetIdempotenciaQuery(string chaveIdempotencia)
        => ChaveIdempotencia = chaveIdempotencia;

    public string ChaveIdempotencia { get; set; }
}
