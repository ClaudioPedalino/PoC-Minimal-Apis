public static class PeopleEndpointDefinition
{
    public static void AddPeopleEndpoints(this WebApplication app)
    {
        app.MapGet("api/people", GetAll);
        app.MapGet("api/people/{id}", GetById);
        app.MapPost("api/people", Create);
        app.MapPut("api/people", Update);
        app.MapDelete("api/people", Delete);

        #region [3] [AÑADIR ATRIBUTOS]
        //app.MapDelete("api/people/with-produces-attribute",
        //    [ProducesResponseType(204)]
        //[ProducesResponseType(404, Type = typeof(CommandResult))]
        //async ([FromServices] IMediator _mediator, DeletePersonCommand command) =>
        //{
        //    var result = await _mediator.Send(command);

        //    return result.HasErrors
        //        ? Results.BadRequest(result)
        //        : Results.NoContent();
        //});

        #region [4] [Algunos atirbutos tienen ahora un método fluent para definirse]
        /// Como los Produces y demás métodos que interactuan con OpenApi
        
        //app.MapGet("api/people", GetAll)
        //    .Produces<IEnumerable<GetBookResponse>>(200);

        //app.MapGet("api/people/{id}", GetById)
        //    .Produces<GetBookResponse>(200);

        //app.MapPost("api/people", Create)
        //    .Produces(202)
        //    .Produces<CommandResult>(400);

        //app.MapPut("api/people", Update)
        //    .Produces(202)
        //    .Produces<CommandResult>(400);

        //app.MapDelete("api/people", Delete)
        //    .Produces(202)
        //    .Produces<CommandResult>(400);

        #region [5] Atributos de autenticación también tienen su fluent method
        //app.MapGet("api/people", GetAll)
        //    .AllowAnonymous()
        //    .Produces<IEnumerable<GetBookResponse>>(200);

        //app.MapGet("api/people/{id}", GetById)
        //    .AllowAnonymous()
        //    .Produces<GetBookResponse>(200);

        //app.MapPost("api/people", Create)
        //    .RequireAuthorization()
        //    .Produces(202)
        //    .Produces<CommandResult>(400);

        //app.MapPut("api/people", Update)
        //    .RequireAuthorization()
        //    .Produces(202)
        //    .Produces<CommandResult>(400);

        //app.MapDelete("api/people", Delete)
        //    .RequireAuthorization()
        //    .Produces(202)
        //    .Produces<CommandResult>(400);
        #endregion
        #endregion
        #endregion
    }

    public static async Task<IResult> GetAll([FromServices] IMediator _mediator) =>
        Results.Ok(await _mediator.Send(new GetAllPeopleQuery()));

    public static async Task<IResult> GetById([FromServices] IMediator _mediator, Guid id) =>
        Results.Ok(await _mediator.Send(new GetPeopleByIdQuery(id)));

    public static async Task<IResult> Create([FromServices] IMediator _mediator, CreatePeopleCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }

    public static async Task<IResult> Update([FromServices] IMediator _mediator, UpdatePeopleCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }

    public static async Task<IResult> Delete([FromServices] IMediator _mediator, DeletePeopleCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }
}