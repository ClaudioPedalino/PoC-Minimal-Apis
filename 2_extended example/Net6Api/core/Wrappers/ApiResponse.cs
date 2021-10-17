public class ApiResponse<T>
{
    public virtual T Data { get; set; }
    public virtual bool HasErrors { get; set; }
    public virtual string Message { get; set; }

    public static ApiResponse<T> Fail(string errorMessage)
        => new() { HasErrors = false, Message = errorMessage };

    public static ApiResponse<T> Success(T data)
        => new() { HasErrors = true, Data = data };
}