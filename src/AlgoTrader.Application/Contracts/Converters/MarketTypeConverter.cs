using System.ComponentModel;

using AlgoTrader.Application.Contracts.Enums;
using AlgoTrader.Core.Entities.Enums;

namespace AlgoTrader.Application.Contracts.Converters;

public static class MarketTypeConverter
{
    public static MarketType ToDomain(this MarketTypeInternal marketType)
    {
        return marketType switch
        {
            MarketTypeInternal.Spot => MarketType.Spot,
            MarketTypeInternal.Futures => MarketType.Futures,
            _ => throw new InvalidEnumArgumentException(nameof(marketType), (int)marketType, typeof(MarketTypeInternal))
        };
    }

    public static MarketTypeInternal ToInternal(this MarketType marketType)
    {
        return marketType switch
        {
            MarketType.Spot => MarketTypeInternal.Spot,
            MarketType.Futures => MarketTypeInternal.Futures,
            _ => throw new InvalidEnumArgumentException(nameof(marketType), (int)marketType, typeof(MarketType))
        };
    }
}