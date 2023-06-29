using MediatR;
using Questao5.Application.ViewModels;

namespace Questao5.Application.Queries.Requests;
public class GetAllSaldoContacorrenteQuery : IRequest<SaldoMovimentoModel>
{
    public GetAllSaldoContacorrenteQuery(int numero)
        => NumeroContaCorrente = numero;

    public int NumeroContaCorrente { get; set; }
}
