/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele and Kyle Givler
 * License not yet decided
 */

namespace StockPortfolioSimulator.Performance;

public class PortfolioPerformance
{
    public IReadOnlyList<HoldingPerformance> Holdings { get; }
    public decimal HoldingsValue { get; }
    public decimal TotalPortfolioValue { get; }
    public decimal TotalUnrealizedProfitLoss { get; }
    public decimal TotalProfitLoss { get; }

    public PortfolioPerformance(IReadOnlyList<HoldingPerformance> holdings, decimal holdingsValue, decimal totalPortfolioValue, decimal totalUnrealizedProfitLoss, decimal totalProfitLoss)
    {
        Holdings = holdings;
        HoldingsValue = holdingsValue;
        TotalPortfolioValue = totalPortfolioValue;
        TotalUnrealizedProfitLoss = totalUnrealizedProfitLoss;
        TotalProfitLoss = totalProfitLoss;
    }
}
