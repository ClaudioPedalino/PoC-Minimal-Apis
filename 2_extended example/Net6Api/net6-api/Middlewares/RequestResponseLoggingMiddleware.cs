public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.ILogger _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, Serilog.ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = SetRequest(context);

        var originalBodyStream = context.Response.Body; ///Copy a pointer to the original response body stream

        using var responseBody = new MemoryStream(); ///Create a new memory stream...

        context.Response.Body = responseBody; ///...and use that for the temporary response body

        await _next(context); ///Continue down the Middleware pipeline, eventually returning to this class

        var response = await SetResponse(context.Response);

        var loggedRequestResponse = new LoggedRequestResponse(request, response);

        _logger.Information(JsonConvert.SerializeObject(
            loggedRequestResponse,
            Newtonsoft.Json.Formatting.Indented));

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private LoggedRequest SetRequest(HttpContext context)
        => new()
        {
            IpRequested = context.Connection.RemoteIpAddress is not null ? context.Connection.RemoteIpAddress.ToString() : default,
            HttpMethod = context.Request.Method,
            RequestTo = $"{context.Request.Scheme} {context.Request.Host}{context.Request.Path} {context.Request.QueryString}",
            Path = context.Request.Path,
            QueryParams = context.Request.QueryString.Value,
        };

    private async Task<LoggedResponse> SetResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin); ///We need to read the response stream from the beginning...

        string bodyString = await new StreamReader(response.Body).ReadToEndAsync(); ///...and copy it into a string

        response.Body.Seek(0, SeekOrigin.Begin); ///We need to reset the reader for the response so that the client can read it.

        return new LoggedResponse()
        {
            StatusCode = response.StatusCode,
            Body = bodyString
        };
    }
}