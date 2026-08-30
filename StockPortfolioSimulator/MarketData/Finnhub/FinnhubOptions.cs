/*
* Stock-Portfolio-Simulator
* Copyright (c) 2026 Kyle Givler
* License not yet decided
*/

namespace StockPortfolioSimulator.MarketData.Finnhub;

public class FinnhubOptions
{
    public const string SectionName = "Finnhub";

    public string ApiKey { get; set; } = "";

    public string BaseUrl { get; set; } = "https://finnhub.io/api/v1/";
}