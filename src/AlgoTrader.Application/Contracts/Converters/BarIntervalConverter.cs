using System.ComponentModel;

using AlgoTrader.Application.Contracts.Enums;
using AlgoTrader.Core.Entities.Enums;

namespace AlgoTrader.Application.Contracts.Converters;

public static class BarIntervalConverter
{
    public static BarIntervalInternal ToInternal(this BarInterval interval)
    {
        return interval switch
        {
            BarInterval.OneMinute => BarIntervalInternal.OneMinute,
            BarInterval.FiveMinutes => BarIntervalInternal.FiveMinutes,
            BarInterval.OneHour => BarIntervalInternal.OneHour,
            BarInterval.OneDay => BarIntervalInternal.OneDay,
            _ => throw new InvalidEnumArgumentException(nameof(interval), (int)interval, typeof(BarInterval))
        };
    }

    public static BarInterval ToDomain(this BarIntervalInternal interval)
    {
        return interval switch
        {
            BarIntervalInternal.OneMinute => BarInterval.OneMinute,
            BarIntervalInternal.FiveMinutes => BarInterval.FiveMinutes,
            BarIntervalInternal.OneHour => BarInterval.OneHour,
            BarIntervalInternal.OneDay => BarInterval.OneDay,
            _ => throw new InvalidEnumArgumentException(nameof(interval), (int)interval, typeof(BarIntervalInternal))
        };
    }
}