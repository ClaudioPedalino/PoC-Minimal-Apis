public static class ClientsRegistration
{
    public static WebApplicationBuilder AddClients(this WebApplicationBuilder builder, AppConfig appConfig)
    {
        builder.Services
            .AddHttpClient<CoinMarketCapClient>(client =>
            {
                client.BaseAddress = new Uri(appConfig.CoinMarketCap);
                client.DefaultRequestHeaders.Add("CMC_PRO_API_KEY", "ab44c081-2555-4c67-962d-2169b4ccc7d9");
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36");
            })
            .AddTransientHttpErrorPolicy(config =>
                config.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

        return builder;
    }
}