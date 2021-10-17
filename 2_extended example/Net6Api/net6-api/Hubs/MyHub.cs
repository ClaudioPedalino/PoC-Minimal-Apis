using Microsoft.AspNetCore.SignalR;

public class MyHub : Hub
{
    private readonly CoinMarketCapClient _coinMarketCapClient;

    public MyHub(CoinMarketCapClient coinMarketCapClient)
    {
        _coinMarketCapClient = coinMarketCapClient;

    }


    public async IAsyncEnumerable<CoinMarketCapResponse> Streaming(CancellationToken cancellationToken)
    {

        while (true)
        {
            var response = await _coinMarketCapClient.GetGainersLosers(new CoinMarketCapRequest());
            //await Clients.All.SendAsync("Streaming",response);
            yield return response;
            await Task.Delay(6000, cancellationToken);
        }
    }
}