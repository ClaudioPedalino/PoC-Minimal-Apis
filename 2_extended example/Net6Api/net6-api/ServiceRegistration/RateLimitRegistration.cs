public static class RateLimitRegistration
{
    public static WebApplicationBuilder AddRateLimit(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        builder.Services.Configure<IpRateLimitOptions>(
            builder.Configuration.GetSection($"{nameof(AppConfig)}:{nameof(AppConfig.IpRateLimit)}"));

        builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        return builder;
    }
}
