using AlgoTrader.Application.Contracts.Enums;

namespace AlgoTrader.Application.Contracts;

public sealed class TickerInternal
{
    public Guid Id { get; }
    public string SymbolLeft { get; }
    public string SymbolRight { get; }
    public MarketTypeInternal MarketType { get; }
    public DateTime TickerStartDate { get; }
    public string FullSymbol => $"{SymbolLeft}{SymbolRight}";
}