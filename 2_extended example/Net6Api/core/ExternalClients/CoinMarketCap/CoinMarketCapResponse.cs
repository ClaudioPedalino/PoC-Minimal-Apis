public class CoinMarketCapResponse
{
    [JsonProperty("data")]
    public Data Data { get; set; }
}

public class Data
{
    [JsonProperty("gainerList")]
    public List<CoinMarketEntry> GainerList { get; set; }

    [JsonProperty("loserList")]
    public List<CoinMarketEntry> LoserList { get; set; }
}

public class CoinMarketEntry
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("rank")]
    public int Rank { get; set; }

    [JsonProperty("priceChange")]
    public PriceChange PriceChange { get; set; }
}

public class PriceChange
{
    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("priceChange1h")]
    public decimal PriceChange1H { get; set; }

    [JsonProperty("priceChange24h")]
    public decimal PriceChange24H { get; set; }
}