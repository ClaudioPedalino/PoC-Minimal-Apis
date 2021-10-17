public static class ConfigurationRegistration
{
    public static AppConfig GetConfiguration(this WebApplicationBuilder builder)
    {
        var appConfig = new AppConfig();

        builder.Services.Configure<CacheSetting>(
            builder.Configuration.GetSection(
                $"{nameof(AppConfig)}:{nameof(AppConfig.CacheSetting)}"));

        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection(
                $"{nameof(AppConfig)}:{nameof(AppConfig.JwtSettings)}"));

        builder.Services.Configure<Api>(
            builder.Configuration.GetSection(
                $"{nameof(AppConfig)}:{nameof(AppConfig.Api)}"));

        builder.Services.Configure<DatabaseConfig>(
            builder.Configuration.GetSection(
                $"{nameof(AppConfig)}:{nameof(AppConfig.DatabaseConfig)}"));

        builder.Configuration.Bind(nameof(AppConfig), appConfig);
        return appConfig;
    }
}