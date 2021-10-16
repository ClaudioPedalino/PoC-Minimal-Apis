public static class BookEndpointDefinition
{
    public static void AddBookEndpoints(this WebApplication app)
    {
        app.MapGet("api/book", GetAll)
            .Produces<IEnumerable<GetBookResponse>>(200);

        app.MapGet("api/book/{id}", GetById)
            .Produces(200);

        app.MapPost("api/book", Create)
            .Produces(204)
            .Produces<CommandResult>(400);

        app.MapPut("api/book", Update)
            .Produces(204)
            .Produces<CommandResult>(400); ;

        app.MapDelete("api/book", Delete)
            .Produces(204)
            .Produces<CommandResult>(400); ;
    }

    public static async Task<IResult> GetAll([FromServices] IMediator _mediator) =>
        Results.Ok(await _mediator.Send(new GetAllBookQuery()));

    public static async Task<IResult> GetById([FromServices] IMediator _mediator, Guid id) =>
        Results.Ok(await _mediator.Send(new GetBookByIdQuery(id)));

    public static async Task<IResult> Create([FromServices] IMediator _mediator, CreatePersonCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }

    public static async Task<IResult> Update([FromServices] IMediator _mediator, UpdatePersonCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }

    public static async Task<IResult> Delete([FromServices] IMediator _mediator, DeletePersonCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }
}
