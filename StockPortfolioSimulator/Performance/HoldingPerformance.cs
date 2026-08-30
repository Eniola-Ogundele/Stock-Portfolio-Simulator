/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele and Kyle Givler
 * License not yet decided
 */

using StockPortfolioSimulator.Models;
namespace StockPortfolioSimulator.Performance;

public class HoldingPerformance
{
    public Holding Holding { get; }
    public decimal CurrentPrice { get; }
    public decimal CostBasis { get; }
    public decimal CurrentValue { get; }
    public decimal UnrealizedProfitLoss { get; }

    public HoldingPerformance(Holding holding, decimal currentPrice, decimal costBasis, decimal currentValue, decimal unrealizedProfitLoss)
    {
        Holding = holding;
        CurrentPrice = currentPrice;
        CostBasis = costBasis;
        CurrentValue = currentValue;
        UnrealizedProfitLoss = unrealizedProfitLoss;
    }
}