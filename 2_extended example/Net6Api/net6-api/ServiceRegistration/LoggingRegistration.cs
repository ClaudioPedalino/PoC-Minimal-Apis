public static class LoggingRegistration
{
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ILogger>(_ =>
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        });

        //builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
        //{
        //    var settings = config.Build();
        //    Log.Logger = new LoggerConfiguration()
        //        .Enrich.FromLogContext()
        //        //.WriteTo.SQLServer(
        //        //    connectionString: "<connectionString>",
        //        //    tableName: "Logging",
        //        //    columnOptions: columnWriters,
        //        //    needAutoCreateTable: true)
        //        .WriteTo.Console()
        //        .CreateLogger();
        //}).UseSerilog();

        return builder;
    }
}