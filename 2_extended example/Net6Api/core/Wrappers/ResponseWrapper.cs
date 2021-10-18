public static class ResponseWrapper
{
    public static IResult QueryResponseSingle<TQuery>(TQuery? result) where TQuery : IQueryResponse
    {
        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    public static IResult QueryResponseMultiple<TQuery>(TQuery? result) where TQuery : IEnumerable<IQueryResponse>
    {
        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    public static async Task<IResult> CommandHandler<TCommand>(this IMediator _mediator, TCommand command)
    {
        var response = (await _mediator.Send(command)) as CommandResponse;
        return response.HasErrors
            ? Results.BadRequest(response)
            : Results.Accepted(value: response);
    }
}