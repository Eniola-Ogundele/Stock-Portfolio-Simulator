/*
 * Stock-Portfolio-Simulator
 * Copyright (c) 2026 Eniola Ogundele and Kyle Givler
 * License not yet decided
 */

namespace StockPortfolioSimulator.Models;

public enum TradeResult
{
    Success,
    InvalidQuantity,
    InvalidPrice,
    InsufficientCash,
    AssetNotFound,
    InsufficientShares
}
