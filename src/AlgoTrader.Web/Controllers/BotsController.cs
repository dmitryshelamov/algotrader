using AlgoTrader.Application.Commands.Bots.CreateLadderBot;
using AlgoTrader.Web.Controllers.Contracts;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AlgoTrader.Web.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class BotController : ControllerBase
{
    private readonly IMediator _mediator;

    public BotController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [EndpointSummary("Create new ladder bot")]
    [HttpPut]
    public async Task<ActionResult> Create([FromBody] CreateLadderBotDto dto, CancellationToken ct)
    {
        Guid botId = await _mediator.Send(new CreateLadderBotCommand(dto.TickerId, dto.Settings), ct);

        return Ok(botId);
    }
}