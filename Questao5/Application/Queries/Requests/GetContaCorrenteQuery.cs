using MediatR;
using Questao5.Application.ViewModels;

namespace Questao5.Application.Queries.Requests;
public class GetContaCorrenteQuery : IRequest<ContaCorrenteModel>
{
    public GetContaCorrenteQuery(int numero)
        => NumeroContaCorrente = numero;

    public int NumeroContaCorrente { get; set; }
}
