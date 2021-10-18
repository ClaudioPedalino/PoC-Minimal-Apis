public class CoinMarketCapClient
{
    private readonly HttpClient _client;

    public CoinMarketCapClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<CoinMarketCapResponse> GetGainersLosers(CoinMarketCapRequest request)
    {
        try
        {
            var path = $"/data-api/v3/cryptocurrency/spotlight" +
                $"?dataType={request.DataType}" +
                $"&limit={request.Limit}" +
                $"&rankRange={request.RankRange}" +
                $"&timeframe={request.TimeFrame}";

            using var serviceResult = await _client.GetAsync(_client.BaseAddress + path);

            var jsonResult = await serviceResult.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<CoinMarketCapResponse>(jsonResult);

            return response;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred connecting to {nameof(CoinMarketCapClient)} API {ex}");
            throw;
        }
    }
}