using AlgoTrader.Application.Contracts.Enums;

namespace AlgoTrader.Application.Contracts;

public sealed class BarInternal
{
    public DateTime Date { get; set; }
    public decimal Open { get;set; }
    public decimal High { get;set; }
    public decimal Low { get;set; }
    public decimal Close { get; set;}
    public decimal Volume { get;set; }
    public BarIntervalInternal Interval { get; set; }
}