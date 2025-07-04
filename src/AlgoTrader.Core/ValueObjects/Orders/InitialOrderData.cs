using AlgoTrader.Core.ValueObjects.Base;

namespace AlgoTrader.Core.ValueObjects.Orders;

public sealed class InitialOrderData : ValueObject
{
    public decimal? LimitQuantity { get; internal set; }

    public decimal? LimitPricePerAsset { get; internal set; }

    public decimal? MarketQuantity { get; internal set; }

    public decimal? MarketFunds { get; internal set; }

    internal InitialOrderData(
        decimal? limitQuantity,
        decimal? limitPricePerAsset,
        decimal? marketQuantity,
        decimal? marketFunds)
    {
        LimitQuantity = limitQuantity;
        LimitPricePerAsset = limitPricePerAsset;
        MarketQuantity = marketQuantity;
        MarketFunds = marketFunds;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return LimitQuantity;
        yield return LimitPricePerAsset;
        yield return MarketQuantity;
        yield return MarketFunds;
    }

    public static InitialOrderData CreateLimit(decimal? limitQuantity, decimal? limitPricePerAsset)
    {
        return new InitialOrderData(
            limitQuantity,
            limitPricePerAsset,
            marketQuantity: null,
            marketFunds: null);
    }

    public static InitialOrderData CreateMarketBuy(decimal marketFunds)
    {
        return new InitialOrderData(
            limitQuantity: null,
            limitPricePerAsset: null,
            marketQuantity: null,
            marketFunds);
    }

    public static InitialOrderData Create(
        decimal? limitQuantity,
        decimal? limitPricePerAsset,
        decimal? marketQuantity,
        decimal? marketFunds)
    {
        return new InitialOrderData(limitQuantity, limitPricePerAsset, marketQuantity, marketFunds);
    }

    public override string ToString()
    {
        return $"LimitQuantity:{LimitQuantity} LimitPricePerAsset:{LimitPricePerAsset} MarketQuantity:{MarketQuantity} MarketFunds:{MarketFunds}";
    }
}