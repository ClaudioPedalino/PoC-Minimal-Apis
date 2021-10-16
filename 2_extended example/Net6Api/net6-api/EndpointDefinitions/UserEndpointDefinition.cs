public static class UserEndpointDefinition
{
    public static void AddUserEndpoints(this WebApplication app)
    {
        app.MapGet("api/users", GetAllUsers).RequireAuthorization();
        app.MapPost("api/users/login", Login).AllowAnonymous();
        app.MapPost("api/users/register", Register).AllowAnonymous();
    }

    public static async Task<IResult> GetAllUsers([FromServices] IMediator _mediator) =>
        Results.Ok(await _mediator.Send(new GetAllUserQuery()));

    public static async Task<IResult> Login([FromServices] IMediator _mediator, LoginUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response.ErrorMessages is null
            ? Results.BadRequest(response)
            : Results.Ok(value: response);
    }

    public static async Task<IResult> Register([FromServices] IMediator _mediator, RegisterUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response.ErrorMessages is null
            ? Results.BadRequest(response)
            : Results.Ok(value: response);
    }
}