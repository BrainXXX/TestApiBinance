using System.Text.Json.Serialization;

namespace TestApi.Classes
{
    public partial class CoinModel
    {
        [JsonPropertyName("symbol")]
        public string Symbol { set; get; }
        [JsonPropertyName("priceChange")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double PriceChange { get; set; }
        [JsonPropertyName("priceChangePercent")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double PriceChangePercent { get; set; }
        [JsonPropertyName("weightedAvgPrice")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double WeightedAvgPrice { get; set; }
        [JsonPropertyName("prevClosePrice")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? PrevClosePrice { get; set; }
        [JsonPropertyName("lastPrice")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double LastPrice { get; set; }
        [JsonPropertyName("lastQty")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double LastQty { get; set; }
        [JsonPropertyName("openPrice")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double OpenPrice { get; set; }
        [JsonPropertyName("highPrice")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double HighPrice { get; set; }
        [JsonPropertyName("lowPrice")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double LowPrice { get; set; }
        [JsonPropertyName("volume")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double Volume { get; set; }
        [JsonPropertyName("quoteVolume")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double QuoteVolume { get; set; }
        [JsonPropertyName("openTime")]
        public ulong OpenTime { get; set; }
        [JsonPropertyName("closeTime")]
        public ulong CloseTime { get; set; }
        [JsonPropertyName("firstId")]
        public long FirstId { get; set; }
        [JsonPropertyName("lastId")]
        public long LastId { get; set; }
        [JsonPropertyName("count")]
        public ulong Count { get; set; }
    }
}