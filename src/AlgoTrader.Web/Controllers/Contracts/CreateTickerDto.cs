using AlgoTrader.Application.Contracts.Enums;

namespace AlgoTrader.Web.Controllers.Contracts;

public sealed class CreateTickerDto
{
    public string Left { get; set; } = null!;
    public string Right { get; set; } = null!;
    public MarketTypeInternal MarketType { get; set; }
    public DateTime TickerStartDate { get; set; }
}