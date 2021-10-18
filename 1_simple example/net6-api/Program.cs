var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDeveloperService, DeveloperService>();
builder.Services.AddScoped<IDeveloperRepository, DeveloperRepository>();

builder.Services.AddDbContext<DataContext>(opt
    => opt.UseSqlServer(Config.Database));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configuration
    => configuration.SwaggerDoc(Config.Version, new() { Title = Config.Net6, Version = Config.Version }));

var app = builder.Build();

app.MapGet("/api/devs", async ([FromServices] IDeveloperService service)
    => Results.Ok(await service.GetAllAsync()));

app.MapPost("/api/devs", async ([FromServices] IDeveloperService service, DeveloperDto command) =>
{
    var response = await service.CreateAsync(command);

    return response.HasErrors
        ? Results.BadRequest(response)
        : Results.Ok(response);
});

app.UseSwagger();
app.UseSwaggerUI(configuration =>
    configuration.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Config.Net6} {Config.Version}"));

app.Run();
