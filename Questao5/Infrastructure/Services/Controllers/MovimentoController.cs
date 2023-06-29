using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Application.ViewModels;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("api/movimento")]
public class MovimentoController : ControllerBase
{
    private readonly IMediator _mediator;
    public MovimentoController(IMediator mediator) => _mediator = mediator;

    #region Query
    /// <summary>
    /// Consultar o saldo de uma conta corrente.
    /// </summary>
    /// <param name="numero">Número da conta corrente</param>
    /// <returns>Retorna o saldo da conta corrente ou uma mensagem de erro</returns>
    /// <response code="200">Retorna o saldo da conta corrente</response>
    /// <response code="400">Caso o número da conta seja inválido ou a conta esteja inativa</response>
    [HttpGet("{numero}")]
    [ProducesResponseType(typeof(SaldoMovimentoModel), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> Get(int numero)
    {
        try
        {
            var query = new GetAllSaldoContacorrenteQuery(numero);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion

    #region Command
    /// <summary>
    /// Realiza uma movimentação (crédito ou débito) em uma conta corrente.
    /// </summary>
    /// <param name="request">Requisição com os detalhes da movimentação</param>
    /// <returns>Retorna o ID da movimentação realizada ou uma mensagem de erro</returns>
    /// <response code="200">A movimentação foi realizada com sucesso. O corpo da resposta contém o ID da transação realizada.</response>
    /// <response code="400">Houve um erro na requisição, tal como uma conta inativa, conta não existente, valor inválido ou tipo de movimento inválido.</response>
    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] CreateMovimentoCommand request)
    {
        try
        {
            return Ok(await _mediator.Send(request));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
}
