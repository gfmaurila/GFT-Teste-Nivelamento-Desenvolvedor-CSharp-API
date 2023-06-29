using MediatR;

namespace Questao5.Application.Commands.Requests;
public class CreateIdempotenciaCommand : IRequest<string>
{
    public string ChaveIdempotencia { get; set; }
    public string Requisicao { get; set; }
    public string Resultado { get; set; }
}
