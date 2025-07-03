using System.ComponentModel;

using AlgoTrader.Application.Contracts.Enums;

using Bybit.Net.Enums;

namespace AlgoTrader.Infrastructure.CryptoClientsNet.ByBitExchange.Converters;

internal static class ByBitKLineIntervalConverter
{
    public static KlineInterval ToByBitKLineInterval(this BarIntervalInternal klineInterval)
    {
        return klineInterval switch
        {
            BarIntervalInternal.OneMinute => KlineInterval.OneMinute,
            BarIntervalInternal.FiveMinutes => KlineInterval.FiveMinutes,
            BarIntervalInternal.OneHour => KlineInterval.OneHour,
            BarIntervalInternal.OneDay => KlineInterval.OneDay,
            _ => throw new InvalidEnumArgumentException(nameof(klineInterval), (int)klineInterval, typeof(BarIntervalInternal))
        };
    }

    public static BarIntervalInternal ToInterval(this KlineInterval klineInterval)
    {
        return klineInterval switch
        {
            KlineInterval.OneMinute => BarIntervalInternal.OneMinute,
            KlineInterval.FiveMinutes => BarIntervalInternal.FiveMinutes,
            KlineInterval.OneHour => BarIntervalInternal.OneHour,
            KlineInterval.OneDay => BarIntervalInternal.OneDay,
            _ => throw new InvalidEnumArgumentException(nameof(klineInterval), (int)klineInterval, typeof(KlineInterval))
        };
    }
}