using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrevisaoTempo.Application;
using PrevisaoTempo.Domain;

namespace PrevisaoTempo.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CidadesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CidadesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Cidade>> CriaCidade([FromBody] CriarCidadeCommand model, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(model, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Cidade>> AlteraCidade([FromBody] AlterarCidadeCommand model, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(model, cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<Cidade>> ExcluiCidade(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ExcluirCidadeCommand() { Id = id }, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IList<Cidade>>> ListaCidades(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RetornaCidadesQuery(), cancellationToken);
        return Ok(response);
    }
}