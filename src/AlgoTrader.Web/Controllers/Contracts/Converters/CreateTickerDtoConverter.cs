using AlgoTrader.Application.Commands.Tickers.CreateTicker;

namespace AlgoTrader.Web.Controllers.Contracts.Converters;

public static class CreateTickerDtoConverter
{
    public static CreateTickerCommand ToCommand(this CreateTickerDto dto)
    {
        return new CreateTickerCommand(dto.Left, dto.Right, dto.MarketType, dto.TickerStartDate);
    }
}