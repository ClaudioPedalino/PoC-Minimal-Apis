public static class PlaceEndpointDefinition
{
    public static void AddPlaceEndpoints(this WebApplication app)
    {
        app.MapGet("api/place", GetAll).AllowAnonymous();
        app.MapGet("api/place/{id}", GetById).AllowAnonymous();

        app.MapPost("api/place", Create).RequireAuthorization();
        app.MapPut("api/place", Update).RequireAuthorization();
        app.MapDelete("api/place", Delete).RequireAuthorization();
    }

    public static async Task<IResult> GetAll([FromServices] IMediator _mediator) =>
        Results.Ok(await _mediator.Send(new GetAllPlaceQuery()));

    public static async Task<IResult> GetById([FromServices] IMediator _mediator, Guid id) =>
        Results.Ok(await _mediator.Send(new GetPlaceByIdQuery(id)));

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