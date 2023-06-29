using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.ViewModels;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5Test.Application.Handlers;

public class GetContaCorrenteQueryHandlerTests
{
    private readonly IContacorrenteRepository _repo;
    private readonly GetContaCorrenteQueryHandler _handler;

    public GetContaCorrenteQueryHandlerTests()
    {
        _repo = Substitute.For<IContacorrenteRepository>();
        _handler = new GetContaCorrenteQueryHandler(_repo);
    }

    [Fact]
    public async Task Handle_ShouldReturnContaCorrente_WhenContaExistsAndIsActive()
    {
        // Arrange
        var contaCorrente = new Contacorrente { Ativo = 1 };
        var contaCorrenteModel = new ContaCorrenteModel(contaCorrente.IdContacorrente);

        var query = new GetContaCorrenteQuery(123456);

        _repo.GetByNumeroAsync(query.NumeroContaCorrente).Returns(contaCorrente);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(contaCorrenteModel.IdContacorrente, result.IdContacorrente);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenContaDoesNotExist()
    {
        // Arrange
        var query = new GetContaCorrenteQuery(123456);

        _repo.GetByNumeroAsync(query.NumeroContaCorrente).Returns((Contacorrente)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
        Assert.Equal("INVALID_ACCOUNT: Apenas contas correntes cadastradas podem receber movimentação", ex.Message);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenContaIsInactive()
    {
        // Arrange
        var contaCorrente = new Contacorrente { Ativo = 0 };

        var query = new GetContaCorrenteQuery(123456);

        _repo.GetByNumeroAsync(query.NumeroContaCorrente).Returns(contaCorrente);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
        Assert.Equal("INACTIVE_ACCOUNT: Apenas contas correntes ativas podem receber movimentação.", ex.Message);
    }
}


