using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Enums;

namespace AlgoTrader.Application.Services;

public interface IExchangeRestClient
{
    Task<List<BarInternal>> GetBars(
        string ticker,
        MarketTypeInternal marketType,
        BarIntervalInternal interval,
        DateTime? start = null,
        DateTime? end = null,
        int barLimit = 1000,
        CancellationToken ct  = default);
}