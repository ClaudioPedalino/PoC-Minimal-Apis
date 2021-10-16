public static class DatabaseRegistration
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        //builder.Services.AddDbContext<DataContext>(opt =>
        //    opt.UseSqlite("Filename=Net6Api_LocalDb.db")); ;

        builder.Services.AddDbContext<DataContext>(opt
            => opt.UseSqlServer("Server=localhost;Database=Net6ApiDb;User Id=sa;Password=Temporal1;"));

        return builder;
    }
}