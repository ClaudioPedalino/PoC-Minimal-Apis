public class CoinMarketCapRequest
{
    public int DataType { get; set; } = 2;
    public int Limit { get; set; } = 30;
    public int RankRange { get; set; } = 0;
    public string TimeFrame { get; set; } = "1h";
}