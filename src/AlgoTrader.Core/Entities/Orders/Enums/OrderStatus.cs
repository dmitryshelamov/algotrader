namespace AlgoTrader.Core.Entities.Orders.Enums;

public enum OrderStatus
{
    Draft = 0,
    New = 1,
    Filled = 2,
    PartiallyFilled = 3,
    Canceled = 4,
    PartiallyCanceled = 5,
    PendingCancel = 6,
    Error = 7
}