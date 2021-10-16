var builder = WebApplication.CreateBuilder(args);

builder.AddIdentity(builder.Configuration);
builder.AddDatabase();
builder.AddLogging();
builder.AddLibs();
builder.AddServices();
builder.AddRepositories();
builder.AddSwagger();

var app = builder.Build();

/// START ENDPOINTS
#region People Endpoints
app.MapGet("api/people", async ([FromServices] IMediator _mediator)
    => Results.Ok(await _mediator.Send(new GetAllPeopleQuery())));

app.MapGet("api/people/{id}", async ([FromServices] IMediator _mediator, Guid id)
    => Results.Ok(await _mediator.Send(new GetPeopleByIdQuery(id))));

app.MapPost("api/people", async ([FromServices] IMediator _mediator, CreatePersonCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});

app.MapPut("api/people", async ([FromServices] IMediator _mediator, UpdatePersonCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});

app.MapDelete("api/people", async ([FromServices] IMediator _mediator, DeletePersonCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});
#endregion

#region Books Endpoints
app.MapGet("api/books", async ([FromServices] IMediator _mediator)
    => Results.Ok(await _mediator.Send(new GetAllBookQuery())));

app.MapGet("api/books/{id}", async ([FromServices] IMediator _mediator, Guid id)
    => Results.Ok(await _mediator.Send(new GetBookByIdQuery(id))));

app.MapPost("api/books", async ([FromServices] IMediator _mediator, CreateBookCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});

app.MapPut("api/books", async ([FromServices] IMediator _mediator, UpdateBookCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});

app.MapDelete("api/books", async ([FromServices] IMediator _mediator, DeleteBookCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});
#endregion

#region Places Endpoints
app.MapGet("api/places", async ([FromServices] IMediator _mediator)
    => Results.Ok(await _mediator.Send(new GetAllPlaceQuery())));

app.MapGet("api/places/{id}", async ([FromServices] IMediator _mediator, Guid id)
    => Results.Ok(await _mediator.Send(new GetPlaceByIdQuery(id))));

app.MapPost("api/places", async ([FromServices] IMediator _mediator, CreatePlaceCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});

app.MapPut("api/places", async ([FromServices] IMediator _mediator, UpdatePlaceCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});

app.MapDelete("api/places", async ([FromServices] IMediator _mediator, DeletePlaceCommand command) =>
{
    var response = await _mediator.Send(command);
    return response.HasErrors ? Results.BadRequest() : Results.NoContent();
});
#endregion
/// END ENDPOINTS

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.AddSwagger();
app.UseAuthorization();
app.Run();