public static class LibraryRegistration
{
    public static WebApplicationBuilder AddLibs(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();

        builder.Services
            .AddMediatR(typeof(CreatePeopleCommand).GetTypeInfo().Assembly)
            //.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ApiKeyBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheBehaviour<,>));

        builder.Services.AddAutoMapper(typeof(PeopleProfile));

        builder.Services.AddFluentValidation(fv =>
            fv.RegisterValidatorsFromAssemblyContaining<CreateBookValidator>());

        builder.Services.AddMiniProfiler(options =>
        {
            options.RouteBasePath = "/profiler";
            options.ColorScheme = ColorScheme.Dark;
        }).AddEntityFramework();

        return builder;
    }
}