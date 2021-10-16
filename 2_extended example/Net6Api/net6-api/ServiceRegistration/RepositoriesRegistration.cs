public static class RepositoriesRegistration
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddTransient<IPeopleRepository, PeopleRepository>();
        builder.Services.AddTransient<IPlaceRepository, PlaceRepository>();
        builder.Services.AddTransient<IBookRepository, BookRepository>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        return builder;
    }
}