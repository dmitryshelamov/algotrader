using AlgoTrader.Core.Entites.Enums;
using AlgoTrader.Core.ValueObjects;

namespace AlgoTrader.Core.Entites;

public sealed class Bar
{
    public DateTime Date { get; }
    public decimal Open { get; }
    public decimal High { get; }
    public decimal Low { get; }
    public decimal Close { get; }
    public decimal Volume { get; }
    public BarInterval Interval { get; }
    public TickerId TickerId { get; }
    public Ticker Ticker { get; }

    public Bar(
        DateTime date,
        decimal open,
        decimal high,
        decimal low,
        decimal close,
        decimal volume,
        BarInterval interval,
        TickerId tickerId)
    {
        Date = date;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
        Interval = interval;
        TickerId = tickerId;
    }
}