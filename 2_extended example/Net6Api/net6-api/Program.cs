var builder = WebApplication.CreateBuilder(args);

AppConfig appConfig = builder.GetConfiguration();

//builder.AddHealthCheck(appConfig);
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<CoinMarketCapClient>(client =>
{
    client.BaseAddress = new Uri(appConfig.CoinMarketCap);
    client.DefaultRequestHeaders.Add("CMC_PRO_API_KEY", "ab44c081-2555-4c67-962d-2169b4ccc7d9");
    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36");
});

builder.AddIdentity(appConfig);
builder.AddDatabase(appConfig);
builder.AddLogging();
builder.AddLibs();
builder.AddServices();
builder.AddRepositories();
builder.AddSwagger(appConfig);

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

var app = builder.Build();


#region [1] Bloating Endpoints
//#region People Endpoints
//app.MapGet("api/people", async ([FromServices] IMediator _mediator)
//    => Results.Ok(await _mediator.Send(new GetAllPeopleQuery())));

//app.MapGet("api/people/{id}", async ([FromServices] IMediator _mediator, Guid id)
//    => Results.Ok(await _mediator.Send(new GetPeopleByIdQuery(id))));

//app.MapPost("api/people", async ([FromServices] IMediator _mediator, CreatePersonCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});

//app.MapPut("api/people", async ([FromServices] IMediator _mediator, UpdatePersonCommand command) =>
//{
//    var response = await _mediator.Send(command);
//    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
//});

//app.MapDelete("api/people", async ([FromServices] IMediator _mediator, DeletePersonCommand command) =>
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
//app.MapGet("api/places", async ([FromServices] IMediator _mediator)
//    => Results.Ok(await _mediator.Send(new GetAllPlaceQuery())));

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

#region [2] 
app.AddUserEndpoints();
app.AddPeopleEndpoints();
app.AddBookEndpoints();
app.AddPlaceEndpoints();
#endregion

//[6] app.UseEndpointDefinition();

///app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseApiVersioning();
app.UseAuthentication();
app.UseAuthorization();
app.UseFluentValidationExceptionHandler();
app.MapHub<MyHub>("/live");
app.AddSwagger(appConfig);
//app.UseHealthCheck(appConfig);

NotifierHelper.NotifierHelperConfigure(app.Services.GetService<INotifierService>());
app.UseMiniProfiler();

app.Run();