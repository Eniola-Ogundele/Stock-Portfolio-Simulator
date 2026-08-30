/*
* Stock-Portfolio-Simulator
* Copyright (c) 2026 Kyle Givler
* License not yet decided
*/

// Note: IOptions<FinnhubOptions> has not been wired up yet.

namespace StockPortfolioSimulator.MarketData.Finnhub;

public class FinnhubOptions
{
    public const string SectionName = "Finnhub";

    public string ApiKey { get; set; } = "";

    public string BaseUrl { get; set; } = "https://finnhub.io/api/v1/";
}