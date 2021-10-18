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

        return builder;
    }
}