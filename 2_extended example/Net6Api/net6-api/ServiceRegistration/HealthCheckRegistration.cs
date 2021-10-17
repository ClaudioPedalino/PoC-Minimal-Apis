public static class HealthCheckRegistration
    {
        public static WebApplicationBuilder AddHealthCheck(this WebApplicationBuilder builder, AppConfig appConfig)
        {
            builder.Services
                .AddHealthChecks()
                //.AddCheck<MemoryHealthCheck>("Memory")
                .AddSqlServer(
                    appConfig.DatabaseConfig.SQLServerDb,
                    name: "SQLServerDb-check",
                    tags: new string[] { "Net6ApiDb" });

            //builder.Services.AddHealthChecksUI().AddInMemoryStorage();
            return builder;
        }

        public static WebApplication UseHealthCheck(this WebApplication app, AppConfig appConfig)
        {
            app.UseRouting();

            //app.UseEndpoints(config =>
            //{
            //    config.MapHealthChecks("/health", new HealthCheckOptions
            //    {
            //        Predicate = _ => true,
            //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //    });

            //    config.MapHealthChecksUI(setup =>
            //    {
            //        setup.UIPath = "/health-ui";
            //        setup.ApiPath = "/health-json";
            //    });
            //});

            return app;
        }
    }