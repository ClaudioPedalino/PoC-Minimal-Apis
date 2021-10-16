public static class SwaggerRegistration
{

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(configuration
            => configuration.SwaggerDoc("v1", new() { Title = "Net 6 Api", Version = "v1" }));

        return builder;
    }

    public static WebApplication AddSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(configuration =>
            configuration.SwaggerEndpoint("/swagger/v1/swagger.json", $"Net 6 api"));

        return app;
    }
}