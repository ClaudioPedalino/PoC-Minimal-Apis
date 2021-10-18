public static class DatabaseRegistration
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder, AppConfig appConfig)
    {
        if (appConfig.DatabaseConfig.UsingLocalDb)
        {
            builder.Services.AddDbContext<DataContext>(opt =>
            {
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
                opt.UseSqlServer(appConfig.DatabaseConfig.SqlLiteDb);
            });
        }
        else
        {
            builder.Services.AddDbContext<DataContext>(opt =>
            {
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
                opt.UseSqlServer(appConfig.DatabaseConfig.SQLServerDb);
            });

            builder.Services.AddDbContext<EventStoreContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                options.UseNpgsql(appConfig.DatabaseConfig.PostgreSqlDb);
            });
        }

        return builder;
    }
}