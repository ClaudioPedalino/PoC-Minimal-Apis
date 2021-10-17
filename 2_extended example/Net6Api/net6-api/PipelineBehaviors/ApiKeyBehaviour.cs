public class ApiKeyBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IApiKeyValidation
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Api _api;

    public ApiKeyBehaviour(IHttpContextAccessor httpContextAccessor, IOptionsMonitor<Api> api)
    {
        _httpContextAccessor = httpContextAccessor;
        _api = api.CurrentValue;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var context = _httpContextAccessor.HttpContext;

        var headerApiKeyRequired = context?.Request.Headers
            .FirstOrDefault(x => x.Key == "Minimal-Api-Key").Value;

        if (headerApiKeyRequired is not null
            && headerApiKeyRequired.Value != _api.MinimalApiKey)
        {
            throw new ApiKeyException();
        }

        var response = await next();

        return response;
    }
}