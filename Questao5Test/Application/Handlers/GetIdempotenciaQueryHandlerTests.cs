using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.ViewModels;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5Test.Application.Handlers;
public class GetIdempotenciaQueryHandlerTests
{
    private readonly IIdempotenciaRepository _repo;
    private readonly GetIdempotenciaQueryHandler _handler;

    public GetIdempotenciaQueryHandlerTests()
    {
        _repo = Substitute.For<IIdempotenciaRepository>();
        _handler = new GetIdempotenciaQueryHandler(_repo);
    }

    [Fact]
    public async Task Handle_ShouldReturnIdempotenciaModel_WhenIdempotenciaExists()
    {
        // Arrange
        var idempotenciaEntry = new Idempotencia("TestKey", "{}", "Success");
        var idempotenciaModel = new IdempotenciaModel(idempotenciaEntry.Resultado);

        var query = new GetIdempotenciaQuery("TestKey");

        _repo.GetByChaveIdempotenciaAsync(query.ChaveIdempotencia).Returns(idempotenciaEntry);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(idempotenciaModel.Resultado, result.Resultado);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenIdempotenciaDoesNotExist()
    {
        // Arrange
        var query = new GetIdempotenciaQuery("TestKey");

        _repo.GetByChaveIdempotenciaAsync(query.ChaveIdempotencia).Returns((Idempotencia)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}
