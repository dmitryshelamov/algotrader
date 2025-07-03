using AlgoTrader.Application.Contracts;
using AlgoTrader.Application.Contracts.Enums;
using AlgoTrader.Application.Services;
using AlgoTrader.Infrastructure.CryptoClientsNet.ByBitExchange.Converters;

using Bybit.Net.Clients;
using Bybit.Net.Objects.Models.V5;

using CryptoExchange.Net.Objects;

namespace AlgoTrader.Infrastructure.CryptoClientsNet.ByBitExchange;

internal sealed class ByBitExchangeRestClient : IExchangeRestClient
{
    private readonly BybitRestClient _client;

    public ByBitExchangeRestClient()
    {
        _client = new BybitRestClient();
    }

    public async Task<List<BarInternal>> GetBars(
        string ticker,
        MarketTypeInternal marketType,
        BarIntervalInternal interval,
        DateTime? start,
        DateTime? end,
        int barLimit = 1000,
        CancellationToken ct  = default)
    {
        WebCallResult<BybitResponse<BybitKline>> response = await _client.V5Api.ExchangeData.GetKlinesAsync(
            marketType.ToByBitCategory(),
            ticker,
            interval.ToByBitKLineInterval(),
            start,
            end,
            limit: barLimit,
            ct: ct);

        return response.Data.List.Select(x => x.ToInternal()).ToList();
    }
}