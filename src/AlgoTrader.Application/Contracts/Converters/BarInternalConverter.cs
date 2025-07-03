using AlgoTrader.Core.Entites;
using AlgoTrader.Core.ValueObjects;

namespace AlgoTrader.Application.Contracts.Converters;

public static class BarInternalConverter
{
    public static Bar ToDomain(this BarInternal barInternal, TickerId tickerId)
    {
        return new Bar(
            barInternal.Date,
            barInternal.Open,
            barInternal.High,
            barInternal.Low,
            barInternal.Close,
            barInternal.Volume,
            barInternal.Interval.ToDomain(),
            tickerId);
    }

    public static BarInternal ToInternal(this Bar domain)
    {
        return new BarInternal
        {
            Close = domain.Close,
            High = domain.High,
            Low = domain.Low,
            Open = domain.Open,
            Volume = domain.Volume,
            Date = domain.Date,
            Interval = domain.Interval.ToInternal()
        };
    }
}