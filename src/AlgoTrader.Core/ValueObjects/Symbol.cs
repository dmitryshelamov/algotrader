using AlgoTrader.Core.ValueObjects.Base;

namespace AlgoTrader.Core.ValueObjects;

public sealed class Symbol : ValueObject
{
    public string SymbolLeft { get; internal set; }
    public string SymbolRight { get; internal set; }
    public string FullSymbol => SymbolLeft + SymbolRight;

    public Symbol(string symbolLeft, string symbolRight)
    {
        if (string.IsNullOrEmpty(symbolLeft) || string.IsNullOrEmpty(symbolRight))
        {
            throw new Exception("SymbolLeft or SymbolRight cannot be null or empty");
        }

        SymbolLeft = symbolLeft;
        SymbolRight = symbolRight;
    }

    public static Symbol Create(string symbolLeft, string symbolRight)
    {
        return new Symbol(symbolLeft, symbolRight);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return SymbolLeft;
        yield return SymbolRight;
    }

    public override string ToString()
    {
        return FullSymbol;
    }
}