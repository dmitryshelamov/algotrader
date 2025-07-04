using AlgoTrader.Core.ValueObjects.Bots;

namespace AlgoTrader.Application.Contracts.Converters;

public static class LadderBotSettingsInternalConverter
{
    public static LadderBotSettings ToDomain(this LadderBotSettingsInternal dto)
    {
        return LadderBotSettings.Create(
            dto.LimitDeposit,
            dto.LimitPerOrder,
            dto.FallPercentage,
            dto.ProfitPercentage,
            dto.Taker,
            dto.Maker,
            dto.ReinvestmentProfits);
    }
}