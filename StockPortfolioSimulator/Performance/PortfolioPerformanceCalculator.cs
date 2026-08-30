/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele and Kyle Givler
 * License not yet decided
 */

using StockPortfolioSimulator.MarketData;
using StockPortfolioSimulator.Models;

namespace StockPortfolioSimulator.Performance;

public class PortfolioPerformanceCalculator
{
    private readonly IMarketPriceProvider _marketPriceProvider;

    public PortfolioPerformanceCalculator(IMarketPriceProvider marketPriceProvider)
    {
        _marketPriceProvider = marketPriceProvider;
    }

    public async Task<PortfolioPerformance> CalculateAsync(Portfolio portfolio)
    {
        List<HoldingPerformance> holdingPerformances = new();

        foreach (Holding holding in portfolio.Holdings)
        {
            decimal currentPrice = await _marketPriceProvider.GetCurrentPrice(holding.Asset);
            decimal costBasis = holding.Quantity * holding.AveragePurchasePrice;
            decimal currentValue = holding.Quantity * currentPrice;
            decimal unrealizedProfitLoss = currentValue - costBasis;

            HoldingPerformance holdingPerformance = new HoldingPerformance(holding, currentPrice, costBasis, currentValue, unrealizedProfitLoss);

            holdingPerformances.Add(holdingPerformance);
        }

        decimal holdingsValue = holdingPerformances.Sum(performance => performance.CurrentValue);
        decimal totalPortfolioValue = portfolio.CashBalance + holdingsValue;
        decimal totalUnrealizedProfitLoss = holdingPerformances.Sum(performance => performance.UnrealizedProfitLoss);
        decimal totalBuyValue = portfolio.Transactions.Where(transaction => transaction.Type == TransactionType.Buy).Sum(transaction => transaction.Total);
        decimal totalSellValue = portfolio.Transactions.Where(transaction => transaction.Type == TransactionType.Sell).Sum(transaction => transaction.Total);
        decimal startingCash = portfolio.CashBalance + totalBuyValue - totalSellValue;
        decimal totalProfitLoss = totalPortfolioValue - startingCash;

        return new PortfolioPerformance(holdingPerformances, holdingsValue, totalPortfolioValue, totalUnrealizedProfitLoss, totalProfitLoss);
    }
}