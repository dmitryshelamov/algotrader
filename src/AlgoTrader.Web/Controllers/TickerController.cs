using AlgoTrader.Web.Controllers.Contracts;
using AlgoTrader.Web.Controllers.Contracts.Converters;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AlgoTrader.Web.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TickerController : ControllerBase
{
    private readonly IMediator _mediator;

    public TickerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [EndpointSummary("Create new Ticker")]
    [HttpPut]
    public async Task<ActionResult> Create([FromBody] CreateTickerDto dto, CancellationToken ct)
    {
        return Ok(await _mediator.Send(dto.ToCommand(), ct));
    }
}