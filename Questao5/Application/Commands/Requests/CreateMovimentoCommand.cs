using MediatR;

namespace Questao5.Application.Commands.Requests;
public class CreateMovimentoCommand : IRequest<string>
{
    public int NumeroContaCorrente { get; set; }
    public string ChaveIdempotencia { get; set; }
    public string TipoMovimento { get; set; }
    public decimal Valor { get; set; }
}
