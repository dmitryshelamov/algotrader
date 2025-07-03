using System.ComponentModel;

using AlgoTrader.Application.Contracts.Enums;

using Bybit.Net.Enums;

namespace AlgoTrader.Infrastructure.CryptoClientsNet.ByBitExchange.Converters;

internal static class ByBitCategoryConverter
{
    public static Category ToByBitCategory(this MarketTypeInternal marketType)
    {
        return marketType switch
        {
            MarketTypeInternal.Spot => Category.Spot,
            MarketTypeInternal.Futures => Category.Linear,
            _ => throw new InvalidEnumArgumentException(nameof(marketType), (int)marketType, typeof(MarketTypeInternal))
        };
    }

    public static MarketTypeInternal ToInternal(this Category marketType)
    {
        return marketType switch
        {
            Category.Spot => MarketTypeInternal.Spot,
            Category.Linear => MarketTypeInternal.Futures,
            _ => throw new InvalidEnumArgumentException(nameof(marketType), (int)marketType, typeof(MarketTypeInternal))
        };
    }
}