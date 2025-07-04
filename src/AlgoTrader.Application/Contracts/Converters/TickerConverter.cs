using AlgoTrader.Core.Entities;
using AlgoTrader.Core.ValueObjects;

namespace AlgoTrader.Application.Contracts.Converters;

public static class TickerConverter
{
    public static TickerInternal ToInternal(this Ticker ticker)
    {
        return new TickerInternal
        {
            Id = ticker.Id,
            SymbolLeft = ticker.Symbol.SymbolLeft,
            SymbolRight = ticker.Symbol.SymbolRight,
            MarketType = ticker.MarketType.ToInternal(),
            TickerStartDate = ticker.TickerStartDate
        };
    }

    public static Ticker ToDomain(this TickerInternal ticker)
    {
        return Ticker.Create(
            ticker.Id,
            Symbol.Create(ticker.SymbolLeft, ticker.SymbolRight),
            ticker.MarketType.ToDomain(),
            ticker.TickerStartDate);
    }
}