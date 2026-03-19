namespace StockLens.Dtos.QuotesDtos.Analytics.Responses
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

#nullable enable

    // ------------------- Эндпоинт 1: /general_analytics -------------------
    public class StockItemResponse
    {
        [JsonProperty("success", Required = Required.Always)]
        public bool Success { get; set; }

        [JsonProperty("result", Required = Required.Always)]
        public List<Dictionary<string, object>> Result { get; set; } = new List<Dictionary<string, object>>();
    }

    // ------------------- Эндпоинт 2: /anti-crisis-top10 -------------------
    public class AntiCrisisResultItem
    {
        [JsonProperty("ticker", Required = Required.Always)]
        public string Ticker { get; set; } = null!;

        [JsonProperty("relative_strength", Required = Required.Always)]
        public double RelativeStrength { get; set; }

        [JsonProperty("resilient_ratio", Required = Required.Always)]
        public double ResilientRatio { get; set; }

        [JsonProperty("dividend_yield", Required = Required.Always)]
        public double DividendYield { get; set; }

        [JsonProperty("avg_volume", Required = Required.Always)]
        public double AvgVolume { get; set; }

        [JsonProperty("score", Required = Required.Always)]
        public double Score { get; set; }

        [JsonProperty("rank", Required = Required.Always)]
        public int Rank { get; set; }
    }

    public class AntiCrisisResponse
    {
        [JsonProperty("success", Required = Required.Always)]
        public bool Success { get; set; }

        [JsonProperty("data", Required = Required.Always)]
        public List<AntiCrisisResultItem> Data { get; set; } = new List<AntiCrisisResultItem>();
    }

    // ------------------- Эндпоинт 3: /sector-correlations -------------------
    public class SectorCorrelationResponse
    {
        [JsonProperty("sectors", Required = Required.Always)]
        public List<string> Sectors { get; set; } = new List<string>();

        [JsonProperty("matrix", Required = Required.Always)]
        public List<List<decimal>> Matrix { get; set; } = new List<List<decimal>>();

        [JsonProperty("stocks_per_sector", Required = Required.Always)]
        public Dictionary<string, int> StocksPerSector { get; set; } = new Dictionary<string, int>();
    }

    // ------------------- Эндпоинт 4: /portfolio/own-weights -------------------
    public class OwnWeightsResponse
    {
        [JsonProperty("expected_return", Required = Required.Always)]
        public double ExpectedReturn { get; set; }

        [JsonProperty("volatility", Required = Required.Always)]
        public double Volatility { get; set; }

        [JsonProperty("sharpe_ratio", Required = Required.Always)]
        public double SharpeRatio { get; set; }
    }

    // ------------------- Эндпоинт 5: /portfolio/optimize -------------------
    public class OptimizeResponse
    {
        [JsonProperty("weights", Required = Required.Always)]
        public Dictionary<string, double> Weights { get; set; } = new Dictionary<string, double>();

        [JsonProperty("expected_return", Required = Required.Always)]
        public double ExpectedReturn { get; set; }

        [JsonProperty("volatility", Required = Required.Always)]
        public double Volatility { get; set; }

        [JsonProperty("sharpe_ratio", Required = Required.Always)]
        public double SharpeRatio { get; set; }

        [JsonProperty("risk_profile", Required = Required.Always)]
        public string RiskProfile { get; set; } = null!;  // conservative, moderate, aggressive

        [JsonProperty("experience_level", Required = Required.Always)]
        public string ExperienceLevel { get; set; } = null!;  // novice, intermediate, expert
    }
}
