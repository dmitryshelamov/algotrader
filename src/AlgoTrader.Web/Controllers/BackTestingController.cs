using AlgoTrader.Application.Commands.BackTesting.Run;
using AlgoTrader.Application.Contracts.Enums;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AlgoTrader.Web.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class BackTestingController : ControllerBase
{
    private readonly IMediator _mediator;

    public BackTestingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [EndpointSummary("Run BackTesting")]
    [HttpPut]
    public async Task<ActionResult> Run(
        Guid tickerId,
        BarIntervalInternal interval,
        DateTime from,
        DateTime to,
        CancellationToken ct)
    {
        await _mediator.Send(new RunBackTestingCommand(tickerId, interval, from, to), ct);

        return Ok();
    }
}