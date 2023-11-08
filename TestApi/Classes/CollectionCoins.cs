using System;
using System.Globalization;

namespace TestApi.Classes
{
    public class CollectionCoins : IComparable<CollectionCoins>
    {
        public string symbol { set; get; } //0
        public double priceChange { get; set; } //1
        public double priceChangePercent { get; set; } //2
        public double weightedAvgPrice { get; set; } //3
        public double lastPrice { get; set; } //4
        public double lastQty { get; set; } //5
        public double openPrice { get; set; } //6
        public double highPrice { get; set; } //7
        public double lowPrice { get; set; } //8
        public double volume { get; set; } //9
        public double quoteVolume { get; set; } //10
        public ulong openTime { get; set; }
        public ulong closeTime { get; set; }
        public ulong firstId { get; set; }
        public ulong lastId { get; set; }
        public ulong count { get; set; }


        public CollectionCoins() { }

        public CollectionCoins(string symbol, double lastPrice, double quoteVolume)
        {
            this.symbol = symbol;
            this.lastPrice = lastPrice;
            this.quoteVolume = quoteVolume;
        }

        public CollectionCoins(string symbol, double priceChange, double priceChangePercent, double weightedAvgPrice, double lastPrice, double lastQty, double openPrice,
            double highPrice, double lowPrice, double volume, double quoteVolume, ulong openTime, ulong closeTime, ulong firstId, ulong lastId, ulong count)
        {
            this.symbol = symbol;
            this.priceChange = priceChange;
            this.priceChangePercent = priceChangePercent;
            this.weightedAvgPrice = weightedAvgPrice;
            this.lastPrice = lastPrice;
            this.lastQty = lastQty;
            this.openPrice = openPrice;
            this.highPrice = highPrice;
            this.lowPrice = lowPrice;
            this.volume = volume;
            this.quoteVolume = quoteVolume;
            this.openTime = openTime;
            this.closeTime = closeTime;
            this.firstId = firstId;
            this.lastId = lastId;
            this.count = count;
        }

        // Реализуем интерфейс IComparable<T>
        public int CompareTo(CollectionCoins obj)
        {
            if (this.quoteVolume < obj.quoteVolume)
                return 1;
            if (this.quoteVolume > obj.quoteVolume)
                return -1;
            else
                return 0;
        }

        public override string ToString()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-CA");
            return $"{symbol,-20}{lastPrice,15:C4}{quoteVolume,20:C0}";
        }
    }
}