public static class PeopleEndpointDefinition
{
    public static void AddPeopleEndpoints(this WebApplication app)
    {
        app.MapGet("api/people", GetAll);
        app.MapGet("api/people/{id}", GetById);
        app.MapPost("api/people", Create);
        app.MapPut("api/people", Update);
        app.MapDelete("api/people", Delete);

        app.MapDelete("api/people/with-produces-attribute",
            [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(CommandResult))]
        async ([FromServices] IMediator _mediator, DeletePersonCommand command) =>
        {
            var result = await _mediator.Send(command);

            return result.HasErrors
                ? Results.BadRequest(result)
                : Results.NoContent();
        });
    }

    public static async Task<IResult> GetAll([FromServices] IMediator _mediator) =>
        Results.Ok(await _mediator.Send(new GetAllPeopleQuery()));

    public static async Task<IResult> GetById([FromServices] IMediator _mediator, Guid id) =>
        Results.Ok(await _mediator.Send(new GetPeopleByIdQuery(id)));

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