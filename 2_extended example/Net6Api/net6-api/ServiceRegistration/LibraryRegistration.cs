public static class LibraryRegistration
{
    public static WebApplicationBuilder AddLibs(this WebApplicationBuilder builder)
    {
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSignalR();

        builder.Services
            .AddMediatR(typeof(CreatePeopleCommand).GetTypeInfo().Assembly)
            //.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ApiKeyBehaviour<,>))
            .AddAutoMapper(typeof(PeopleProfile))
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateBookValidator>();
            })
            .AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.ColorScheme = ColorScheme.Dark;
            }).AddEntityFramework();

        //builder.Services.AddHealthCheck(Configuration);

        return builder;
    }
}