public static class VersioningRegistration
{
    public static WebApplicationBuilder AddApiVersioning(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            //options.ApiVersionReader = new HeaderApiVersionReader("minimal-api-version");
            options.ApiVersionReader = new UrlSegmentApiVersionReader();

            //options.Conventions.Controller<Anycontroller>(configuration);
            options.ReportApiVersions = true;
        });

        return builder;
    }
}