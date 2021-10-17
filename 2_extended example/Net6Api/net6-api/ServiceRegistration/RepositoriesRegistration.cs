public static class RepositoriesRegistration
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddTransient<IPeopleRepository, PeopleRepository>()
            .AddTransient<IPlaceRepository, PlaceRepository>()
            .AddTransient<IBookRepository, BookRepository>()
            .AddTransient<IUserRepository, UserRepository>();
        
        return builder;
    }
}