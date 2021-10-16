public static class LibraryRegistration
{
    public static WebApplicationBuilder AddLibs(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(typeof(CreatePersonCommand).GetTypeInfo().Assembly);
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        builder.Services.AddAutoMapper(typeof(PeopleProfile));
        builder.Services.AddFluentValidation(fv =>
            fv.RegisterValidatorsFromAssemblyContaining<PeopleValidator>());

        //builder.Services.AddHealthCheck(Configuration);

        return builder;
    }
}