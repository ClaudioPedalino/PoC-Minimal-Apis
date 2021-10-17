public class CoinVariance
{
    public CoinVariance(CoinMarketEntry x)
    {
        Id = x.Id;
        Symbol = x.Symbol;
        Name = x.Name;
        Rank = x.Rank;
        Price = $"$ {x.PriceChange.Price:N6}";
        Price1h = $"% {x.PriceChange.PriceChange1H:N2}";
        Price24h = $"% {x.PriceChange.PriceChange24H:N2}";
    }

    public int Id { get; set; }
    public int Rank { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    public string Price1h { get; set; }
    public string Price24h { get; set; }
}