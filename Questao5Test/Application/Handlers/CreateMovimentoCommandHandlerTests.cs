using MediatR;
using Newtonsoft.Json;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.ViewModels;
using Questao5.Domain.Contracts;
using Questao5.Domain.Entities;

namespace Questao5Test.Application.Handlers;

public class CreateMovimentoCommandHandlerTests
{
    private readonly IMovimentoRepository _movimentoRepo;
    private readonly IMediator _mediator;
    private readonly CreateMovimentoCommandHandler _handler;

    public CreateMovimentoCommandHandlerTests()
    {
        _movimentoRepo = Substitute.For<IMovimentoRepository>();
        _mediator = Substitute.For<IMediator>();
        _handler = new CreateMovimentoCommandHandler(_mediator, _movimentoRepo);
    }

    [Fact]
    public async Task Handle_ShouldHandleSuccessfully_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateMovimentoCommand
        {
            ChaveIdempotencia = "TestKey",
            NumeroContaCorrente = 123,
            TipoMovimento = "D",
            Valor = 100.00M
        };

        var contaCorrente = new ContaCorrenteModel("123");

        var movimento = new Movimento(contaCorrente.IdContacorrente, command.TipoMovimento, command.Valor);

        _mediator.Send(Arg.Any<GetIdempotenciaQuery>()).Returns((IdempotenciaModel)null);
        _mediator.Send(Arg.Any<GetContaCorrenteQuery>()).Returns(contaCorrente);
        _movimentoRepo.AddAsync(Arg.Any<Movimento>()).Returns(movimento.IdMovimento);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _movimentoRepo.Received().AddAsync(Arg.Is<Movimento>(m =>
            m.IdContaCorrente == contaCorrente.IdContacorrente &&
            m.TipoMovimento == command.TipoMovimento &&
            m.Valor == command.Valor
        ));

        await _mediator.Received().Send(Arg.Is<CreateIdempotenciaCommand>(i =>
            i.ChaveIdempotencia == command.ChaveIdempotencia &&
            i.Requisicao == JsonConvert.SerializeObject(command) &&
            i.Resultado == result
        ));

        Assert.Equal(result, movimento.IdMovimento);
    }
}

