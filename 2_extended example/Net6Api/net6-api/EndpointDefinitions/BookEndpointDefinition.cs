public static class BookEndpointDefinition
{
    public static void AddBookEndpoints(this WebApplication app)
    {
        app.MapGet("api/books", GetAll)
            .AllowAnonymous()
            .Produces<IEnumerable<GetBookResponse>>(200);

        #region [7] Attributos custom
        //app.MapGet("api/books", [ApiKey] async ([FromServices] IMediator _mediator)
        //    => Results.Ok(await _mediator.Send(new GetAllBookQuery())));
        #endregion
        #region [8] Crear Custom Fluent Config
        //app.MapGet("api/books", GetAll)
        //    .AllowAnonymous()
        //    .RequireCustomApiKey_Intento1()
        //    .RequireCustomApiKey_Intento2()
        //    .Produces<IEnumerable<GetBookResponse>>(200);
        #endregion

        app.MapGet("api/books/{id}", GetById)
            .AllowAnonymous()
            .Produces<GetBookResponse>(200);

        app.MapPost("api/books", Create)
            .RequireAuthorization()
            .Produces(202)
            .Produces<CommandResponse>(400);

        app.MapPut("api/books", Update)
            .RequireAuthorization()
            .Produces(202)
            .Produces<CommandResponse>(400);

        app.MapDelete("api/books", Delete)
            .RequireAuthorization()
            .Produces(202)
            .Produces<CommandResponse>(400);
    }

    //[ApiKey]
    public static async Task<IResult> GetAll([FromServices] IMediator _mediator) =>
        Results.Ok(await _mediator.Send(new GetAllBookQuery()));

    //[ApiKey]
    public static async Task<IResult> GetById([FromServices] IMediator _mediator, Guid id) =>
        Results.Ok(await _mediator.Send(new GetBookByIdQuery(id)));

    public static async Task<IResult> Create([FromServices] IMediator _mediator, CreateBookCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }

    public static async Task<IResult> Update([FromServices] IMediator _mediator, UpdateBookCommand command)
    {

        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }

    public static async Task<IResult> Delete([FromServices] IMediator _mediator, DeleteBookCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }
}

#region [8] Crear Fluent Custom Config
public static class CustomFluentEndpointConfiguration
{
    public static TBuilder RequireCustomApiKey_Intento1<TBuilder>(this TBuilder builder)
    {
        /// No me salió hacer andar ésto
        return builder;
    }

    public static DelegateEndpointConventionBuilder RequireCustomApiKey_Intento2(this DelegateEndpointConventionBuilder builder)
    {
        /// No me salió hacer andar ésto
        return builder;
    }
}
#endregion