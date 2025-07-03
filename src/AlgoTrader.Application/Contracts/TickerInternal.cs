using AlgoTrader.Application.Contracts.Enums;

namespace AlgoTrader.Application.Contracts;

public sealed class TickerInternal
{
    public Guid Id { get; set; }
    public string SymbolLeft { get; set; }
    public string SymbolRight { get; set; }
    public MarketTypeInternal MarketType { get; set; }
    public DateTime TickerStartDate { get; set; }
    public string FullSymbol => $"{SymbolLeft}{SymbolRight}";
}