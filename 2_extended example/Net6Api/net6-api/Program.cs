using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);
AppConfig appConfig = builder.GetConfiguration();

//builder.AddHealthCheck(appConfig);
builder.AddRateLimit();
builder.AddIdentity(appConfig);
builder.AddDatabase(appConfig);
builder.AddBuiltInLibs();
builder.AddLogging();
builder.AddClients(appConfig);
builder.AddLibs();
builder.AddServices();
builder.AddRepositories();
builder.Services.AddGrpc();
builder.AddSwagger(appConfig);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    //options.ApiVersionReader = new HeaderApiVersionReader("minimal-api-version");
    options.ApiVersionReader = new UrlSegmentApiVersionReader();

    //options.Conventions.Controller<Anycontroller>(configuration);
    options.ReportApiVersions = true;
});

var app = builder.Build();
//app.MapGrpcService<GreeterService>();
//app.MapGet("/grpc", () =>
//    "Communication with gRPC endpoints must be made through a gRPC client");

#region [1] Bloating Endpoints
//#region People Endpoints
//app.MapGet("api/people", async ([FromServices] IMediator _mediator)
//    => Results.Ok(await _mediator.Send(new GetAllPeopleQuery())));

//app.MapGet("api/people/{id}", async ([FromServices] IMediator _mediator, Guid id)
//    => Results.Ok(await _mediator.Send(new GetPeopleByIdQuery(id))));

//app.MapPost("api/people", async ([FromServices] IMediator _mediator, CreatePeopleCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});

//app.MapPut("api/people", async ([FromServices] IMediator _mediator, UpdatePeopleCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});

//app.MapDelete("api/people", async ([FromServices] IMediator _mediator, DeletePeopleCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});
//#endregion

//#region Books Endpoints
//app.MapGet("api/books", async ([FromServices] IMediator _mediator)
//    => Results.Ok(await _mediator.Send(new GetAllBookQuery())));

//app.MapGet("api/books/{id}", async ([FromServices] IMediator _mediator, Guid id)
//    => Results.Ok(await _mediator.Send(new GetBookByIdQuery(id))));

//app.MapPost("api/books", async ([FromServices] IMediator _mediator, CreateBookCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});

//app.MapPut("api/books", async ([FromServices] IMediator _mediator, UpdateBookCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});

//app.MapDelete("api/books", async ([FromServices] IMediator _mediator, DeleteBookCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});
//#endregion
//#region Places Endpoints
//app.MapGet("api/v{version:apiVersion}/places",
//    //[ApiVersion("1.0")]
//    //[ApiVersion("2.0")]
//    [MapToApiVersion("1.0")]
//[ProducesResponseType(typeof(IEnumerable<GetPlaceResponse>), 203)]
//async ([FromServices] IMediator _mediator) =>
//    Results.Ok(await _mediator.Send(new GetAllPlaceQuery())));

//app.MapGet("api/places/{id}", async ([FromServices] IMediator _mediator, Guid id)
//    => Results.Ok(await _mediator.Send(new GetPlaceByIdQuery(id))));

//app.MapPost("api/places", async ([FromServices] IMediator _mediator, CreatePlaceCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});

//app.MapPut("api/places", async ([FromServices] IMediator _mediator, UpdatePlaceCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});

//app.MapDelete("api/places", async ([FromServices] IMediator _mediator, DeletePlaceCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});
//#endregion
#endregion

#region [2] Edpoint definitions
app.AddUserEndpoints();
app.AddPeopleEndpoints();
app.AddBookEndpoints();
app.AddPlaceEndpoints();
#endregion
#region [6] Regisrar todos los endpoints juntos
//app.UseEndpointDefinition();
#endregion


///app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseIpRateLimiting();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseApiVersioning();
app.UseAuthentication();
app.UseAuthorization();
app.UseFluentValidationExceptionHandler();
app.MapHub<CriptoHub>("/live");
app.AddSwagger(appConfig);
//app.UseHealthCheck(appConfig);
app.UseMiniProfiler();

CommandHelper.NotifierHelperConfigure(
    app.Services.GetService<INotifierService>(),
    app.Services.GetService<IDistributedCache>());

app.Run();