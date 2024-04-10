using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrevisaoTempo.Application;
using PrevisaoTempo.Application.Exceptions;
using PrevisaoTempo.Application.Models;
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


    [HttpPost()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cidade))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Cidade>> CriaCidade([FromBody] CriarCidadeCommand model, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(model, cancellationToken);
            return Ok(response);
        } 
        catch (PrevisaoTempoException ex) 
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
 
   }

    [HttpPost()]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cidade))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Cidade>> AlteraCidade([FromBody] AlterarCidadeCommand model, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(model, cancellationToken);
            return Ok(response);
        } 
        catch (PrevisaoTempoException ex) 
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> ExcluiCidade(int id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(new ExcluirCidadeCommand() { Id = id }, cancellationToken);
            return Ok(response);
        } 
        catch (PrevisaoTempoException ex) 
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Cidade>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<Cidade>>> ListaCidades(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(new RetornaCidadesQuery(), cancellationToken);
            return Ok(response);
        } 
        catch (PrevisaoTempoException ex) 
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CidadeComPrevisaoTempo))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CidadeComPrevisaoTempo>> ConsultaPrevisaoTempo(int id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(new ConsultaPrevisaoTempoQuery() { IdCidade = id }, cancellationToken);
            return Ok(response);
        } 
        catch (PrevisaoTempoException ex) 
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
   }
}