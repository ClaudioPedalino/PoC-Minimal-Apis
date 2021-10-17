public static class SwaggerRegistration
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder, AppConfig appConfig)
    {
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc(appConfig.Api.Version, new OpenApiInfo { Title = appConfig.Api.Name, Version = appConfig.Api.Version });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = appConfig.JwtSettings.TokenType
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = appConfig.JwtSettings.TokenType
                                },
                                Scheme = "oauth2",
                                Name = appConfig.JwtSettings.TokenType,
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
            });

        return builder;
    }

    public static WebApplication AddSwagger(this WebApplication app, AppConfig appConfig)
    {
        app.UseSwagger();
        app.UseSwaggerUI(configuration =>
            configuration.SwaggerEndpoint("/swagger/v1/swagger.json", appConfig.Api.Name));
        app.MapFallback(() => Results.Redirect("/swagger"));

        return app;
    }
}