public static class DatabaseRegistration
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder, AppConfig appConfig)
    {
        if (appConfig.DatabaseConfig.UsingLocalDb)
        {
            builder.Services.AddDbContext<DataContext>(opt
                => opt.UseSqlServer(appConfig.DatabaseConfig.SqlLiteDb));
        }
        else
        {
            builder.Services.AddDbContext<DataContext>(opt
                => opt.UseSqlServer(appConfig.DatabaseConfig.SQLServerDb));
        }

        return builder;
    }
}