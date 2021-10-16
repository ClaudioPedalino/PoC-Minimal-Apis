public static class ServiceRegistration
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenService, TokenService>();

        return builder;
    }
}