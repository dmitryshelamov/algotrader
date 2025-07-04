namespace AlgoTrader.Application.Contracts;

public sealed class LadderBotSettingsInternal
{
    public decimal LimitDeposit { get; set; }
    public decimal LimitPerOrder { get; set; }
    public decimal FallPercentage { get; set; }
    public decimal ProfitPercentage { get; set; }
    public decimal Taker { get; set; }
    public decimal Maker { get; set; }
    public bool ReinvestmentProfits { get; set; }
    public decimal TotalIncome { get; set; }
}