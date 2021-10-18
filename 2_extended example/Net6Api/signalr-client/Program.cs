using ConsoleTableExt;
using Microsoft.AspNetCore.SignalR.Client;

const string ServerUrl = "https://localhost:7333/live";

await using var connection = new HubConnectionBuilder().WithUrl(ServerUrl).Build();
await connection.StartAsync();

await foreach (var date in connection.StreamAsync<CoinMarketCapResponse>("Streaming"))
{
    PrintGainers(date.Data.GainerList.ConvertAll(coin => new CoinVariance(coin)));
}

static void PrintGainers(List<CoinVariance> gainers)
{
    ConsoleTableBuilder
        .From(gainers)
        .WithTitle("Volatiles al alza", ConsoleColor.Green, TextAligntment.Center)
        .WithFormat(ConsoleTableBuilderFormat.MarkDown)
        .ExportAndWriteLine(TableAligntment.Center);
}