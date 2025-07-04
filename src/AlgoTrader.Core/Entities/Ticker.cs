using AlgoTrader.Core.Entities.Enums;
using AlgoTrader.Core.ValueObjects;

namespace AlgoTrader.Core.Entities;

public sealed class Ticker
{
    public TickerId Id { get; }
    public Symbol Symbol { get; }
    public MarketType MarketType { get; }
    public DateTime TickerStartDate { get; }

    private Ticker()
    {
    }

    public Ticker(TickerId id, Symbol symbol, MarketType marketType, DateTime tickerStartDate)
    {
        Id = id;
        Symbol = symbol;
        MarketType = marketType;
        TickerStartDate = tickerStartDate;
    }

    public static Ticker Create(
        TickerId tickerId,
        Symbol symbol,
        MarketType marketType,
        DateTime tickerStartDate)
    {
        return new Ticker(tickerId, symbol, marketType, tickerStartDate);
    }
}