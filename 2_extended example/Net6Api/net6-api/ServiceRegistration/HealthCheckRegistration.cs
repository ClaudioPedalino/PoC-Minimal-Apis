public static class HealthCheckRegistration
{
    public static WebApplicationBuilder AddHealthCheck(this WebApplicationBuilder builder, AppConfig appConfig)
    {
        builder.Services
            .AddHealthChecks()
            .AddCheck<MemoryHealthCheck>("Memory")
            .AddCheck("Mock Service 1", () => HealthCheckResult.Healthy("Service 1 ok"))
            .AddUrlGroup(
                uri: new Uri("https://google.com"),
                name: "CoinMarketCap WebSite",
                failureStatus: HealthStatus.Unhealthy,
                tags: new string[] { "client", "http", "external" })
            .AddSqlServer(
                connectionString: appConfig.DatabaseConfig.SQLServerDb,
                healthQuery: "SELECT 1;",
                name: "SQLServerDb-check",
                failureStatus: HealthStatus.Degraded,
                tags: new string[] { "Net6ApiDb" });

        builder.Services.AddHealthChecksUI()
            .AddSqlServerStorage(appConfig.DatabaseConfig.SQLServerDb);

        return builder;
    }

    public static WebApplication UseHealthCheck(this WebApplication app, AppConfig appConfig)
    {
        app.UseRouting();

        app.UseEndpoints(config =>
        {
            config.MapHealthChecksUI();

            config.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            //config.MapHealthChecksUI(setup =>
            //{
            //    setup.UIPath = "/hc-ui";
            //    setup.ApiPath = "/hc-json";
            //});
        });

        return app;
    }
}