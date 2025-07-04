using AlgoTrader.Core.ValueObjects.Base;

namespace AlgoTrader.Core.ValueObjects.Bots;

public sealed class LadderBotSettings : ValueObject
{
    public const decimal MinimumLimitPerOrder = 1.5m;
    public decimal LimitDeposit { get; internal set; }
    public decimal LimitPerOrder { get; internal set; }
    public decimal FallPercentage { get; internal set; }
    public decimal ProfitPercentage { get; internal set; }
    public decimal Taker { get; internal set; }
    public decimal Maker { get; internal set; }
    public bool ReinvestmentProfits { get; internal set; }
    public decimal TotalIncome { get; internal set; } = new decimal(0);

    public decimal CalculatedLimitDeposit
    {
        get
        {
            if (ReinvestmentProfits)
            {
                return LimitDeposit + TotalIncome;
            }

            return LimitDeposit;
        }
    }

    public decimal CalculatedLimitPerOrder
    {
        get
        {
            if (ReinvestmentProfits)
            {
                decimal percentChange = decimal.Round(TotalIncome / LimitDeposit * 100, GlobalSettings.Round);
                decimal limitPerOrder = decimal.Round(LimitPerOrder * (1 + percentChange / 100), GlobalSettings.Round);

                return limitPerOrder;
            }

            return LimitPerOrder;
        }
    }

    public LadderBotSettings(
        decimal limitDeposit,
        decimal limitPerOrder,
        decimal fallPercentage,
        decimal profitPercentage,
        decimal taker,
        decimal maker,
        bool reinvestmentProfits)
    {
        LimitDeposit = limitDeposit;
        LimitPerOrder = limitPerOrder;
        FallPercentage = fallPercentage;
        ProfitPercentage = profitPercentage;
        Taker = taker;
        Maker = maker;
        ReinvestmentProfits = reinvestmentProfits;
    }

    public static LadderBotSettings Create(
        decimal limitDeposit,
        decimal limitPerOrder,
        decimal fallPercentage,
        decimal profitPercentage,
        decimal taker,
        decimal maker,
        bool reinvestmentProfits)
    {
        return new LadderBotSettings(
            limitDeposit,
            limitPerOrder,
            fallPercentage,
            profitPercentage,
            taker,
            maker,
            reinvestmentProfits);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return LimitPerOrder;
        yield return LimitDeposit;
        yield return ProfitPercentage;
        yield return FallPercentage;
        yield return Taker;
        yield return Maker;
        yield return ReinvestmentProfits;
    }

    public override string ToString()
    {
        return
            $"LimitPerOrder:{LimitPerOrder} LimitDeposit:{LimitDeposit} FallPercentage:{FallPercentage} ProfitPercentage:{ProfitPercentage} Taker:{Taker} Maker:{Maker} ReinvestmentProfits:{ReinvestmentProfits}";
    }

    public void UpdateTotalIncome(decimal tradeIncome)
    {
        TotalIncome += tradeIncome;
    }
}