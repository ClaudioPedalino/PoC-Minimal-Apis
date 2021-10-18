public class CommandResponse
{
    public CommandResponse()
    {
        HasErrors = false;
        Message = string.Empty;
    }

    public virtual bool HasErrors { get; set; }
    public virtual string Message { get; set; }

    public static CommandResponse Success(string message = "")
    {
        if (!string.IsNullOrWhiteSpace(message))
            CommandHelper.Notify(message);

        CommandHelper.Clean("GetAllPeopleQuery");
        CommandHelper.Clean("GetAllBookQuery");
        CommandHelper.Clean("GetAllPlaceQuery");


        return new() { HasErrors = false, Message = message };
    }

    public static CommandResponse Fail(string message)
        => new() { HasErrors = true, Message = message };

    public static CommandResponse NotFound()
        => new() { HasErrors = true, Message = "No se encontró un registro con la información enviada" };
}