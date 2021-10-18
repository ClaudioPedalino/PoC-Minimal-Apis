public static class ServiceRegistration
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IDataContext>(sp => sp.GetRequiredService<DataContext>());
        builder.Services.AddTransient<INotifierService, NotifierService>();

        return builder;
    }
}