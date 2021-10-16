public class LoggedRequestResponse
{
    public LoggedRequestResponse(LoggedRequest request, LoggedResponse response)
    {
        TraceId = Guid.NewGuid();
        Request = request;
        Response = response;
        Date = DateTime.UtcNow.AddHours(-3);
    }

    public Guid TraceId { get; init; }
    public LoggedRequest Request { get; init; }
    public LoggedResponse Response { get; init; }
    public DateTime Date { get; init; }
}