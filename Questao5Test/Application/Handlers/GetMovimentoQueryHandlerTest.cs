using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Contracts;
using Questao5.Domain.View;

namespace Questao5Test.Application.Handlers;
public class GetMovimentoQueryHandlerTest
{
    private readonly IMovimentoRepository _repo;
    private readonly GetMovimentoQueryHandler _handler;

    public GetMovimentoQueryHandlerTest()
    {
        _repo = Substitute.For<IMovimentoRepository>();
        _handler = new GetMovimentoQueryHandler(_repo);
    }

    /// <summary>
    /// Verifica que, quando o método Handle é chamado com um objeto 
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Handle_WhenCalledWithValidRequest_ShouldReturnCorrectResult()
    {
        // Arrange
        var request = new GetAllSaldoContacorrenteQuery(1234);
        var saldoContaCorrenteView = new SaldoContaCorrenteView
        {
            NumeroConta = 1234,
            NomeTitular = "Test",
            Saldo = 1000,
            Ativo = 1
        };

        _repo.GetSaldoAsync(request.NumeroContaCorrente).Returns(saldoContaCorrenteView);

        // Act
        var result = await _handler.Handle(request, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(saldoContaCorrenteView.NumeroConta, result.NumeroConta);
        Assert.Equal(saldoContaCorrenteView.NomeTitular, result.NomeTitular);
        Assert.Equal(saldoContaCorrenteView.Saldo, result.Saldo);
    }

    /// <summary>
    /// Válido, ele retorna um objeto SaldoMovimentoModel com os valores esperados.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Handle_WhenCalledWithInactiveAccount_ShouldThrowException()
    {
        // Arrange
        var request = new GetAllSaldoContacorrenteQuery(1234);
        var saldoContaCorrenteView = new SaldoContaCorrenteView
        {
            NumeroConta = 1234,
            NomeTitular = "Test",
            Saldo = 1000,
            Ativo = 0
        };

        _repo.GetSaldoAsync(request.NumeroContaCorrente).Returns(saldoContaCorrenteView);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, new CancellationToken()));
    }
}
