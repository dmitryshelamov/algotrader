using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Enums;

using Bybit.Net.Objects.Models.V5;

namespace AlgoTrader.Infrastructure.CryptoClientsNet.ByBitExchange.Converters;

internal static class ByBitKLineConverter
{
    public static BarInternal ToInternal(this BybitKline kline, BarIntervalInternal interval)
    {
        return new BarInternal
        {
            Close = kline.ClosePrice,
            High = kline.HighPrice,
            Low = kline.LowPrice,
            Volume = kline.Volume,
            Date = kline.StartTime,
            Open = kline.OpenPrice,
            Interval = interval,
        };
    }
}