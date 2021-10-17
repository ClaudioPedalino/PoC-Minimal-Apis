public class ApiKeyException : Exception
{
    public ApiKeyException() : base("Api key was not provided") { }

    public ApiKeyException(string message) : base(message) { }
}