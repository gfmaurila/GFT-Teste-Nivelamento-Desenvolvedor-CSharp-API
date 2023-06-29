using Newtonsoft.Json;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5Test.Application.Handlers;

public class CreateIdempotenciaCommandHandlerTest
{
    private readonly IIdempotenciaRepository _idempotenciaRepo;
    private readonly CreateIdempotenciaCommandHandler _handler;

    public CreateIdempotenciaCommandHandlerTest()
    {
        _idempotenciaRepo = Substitute.For<IIdempotenciaRepository>();
        _handler = new CreateIdempotenciaCommandHandler(_idempotenciaRepo);
    }

    [Fact]
    public async Task Handle_ShouldCallAddAsync_WhenChaveIdempotenciaIsNotNullOrEmpty()
    {
        // Arrange
        var command = new CreateIdempotenciaCommand
        {
            ChaveIdempotencia = "TestKey",
            Requisicao = "Requisicao",
            Resultado = "Resultado"
        };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _idempotenciaRepo.Received().AddAsync(Arg.Is<Idempotencia>(i =>
            i.ChaveIdempotencia == command.ChaveIdempotencia &&
            i.Requisicao == JsonConvert.SerializeObject(command.Requisicao) &&
            i.Resultado == command.Resultado
        ));
    }
}
