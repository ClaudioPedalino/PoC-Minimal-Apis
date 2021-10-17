﻿public static class PlaceEndpointDefinition
{
    public static void AddPlaceEndpoints(this WebApplication app)
    {
        app.MapGet("api/places", GetAll)
            .Produces<IEnumerable<GetPlaceResponse>>(200)
            .AllowAnonymous();

        app.MapGet("api/places/{id}", GetById)
            .Produces<GetPlaceResponse>(200)
            .AllowAnonymous();

        app.MapPost("api/places", Create)
            .RequireAuthorization()
            .Produces(202)
            .Produces<CommandResponse>(400);

        app.MapPut("api/places", Update)
            .RequireAuthorization()
            .Produces(202)
            .Produces<CommandResponse>(400);

        app.MapDelete("api/places", Delete)
            .RequireAuthorization()
            .Produces(202)
            .Produces<CommandResponse>(400);
    }

    public static async Task<IResult> GetAll([FromServices] IMediator _mediator) =>
        Results.Ok(await _mediator.Send(new GetAllPlaceQuery()));

    public static async Task<IResult> GetById([FromServices] IMediator _mediator, Guid id) =>
        Results.Ok(await _mediator.Send(new GetPlaceByIdQuery(id)));

    public static async Task<IResult> Create([FromServices] IMediator _mediator, CreatePlaceCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }

    public static async Task<IResult> Update([FromServices] IMediator _mediator, UpdatePlaceCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }

    public static async Task<IResult> Delete([FromServices] IMediator _mediator, DeletePlaceCommand command)
    {
        var response = await _mediator.Send(command);
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }
}