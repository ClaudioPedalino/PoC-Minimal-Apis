public class AppConfig
{
    public DatabaseConfig DatabaseConfig { get; init; }
    public JwtSettings JwtSettings { get; init; }
    public ResilienceConfig ResilienceConfig { get; init; }
    public Api Api { get; init; }
    public CacheSetting CacheSetting { get; init; }
    public string CoinMarketCap { get; init; }
    public IpRateLimit IpRateLimit { get; init; }
    //public IpRateLimiting IpRateLimiting { get; set; }
    //public IpRateLimitPolicies IpRateLimitPolicies { get; set; }
}

public class JwtSettings
{
    public string TokenType { get; init; }
    public string Secret { get; init; }
    public ushort ValidHours { get; init; }
}

public class ResilienceConfig
{
    public ushort Retries { get; init; }
    public ushort RetryDelayInMiliseconds { get; init; }
}

public class Api
{
    public string Version { get; init; }
    public string Name { get; init; }
    public string MinimalApiKey { get; init; }
}

public class DatabaseConfig
{
    public bool UsingLocalDb { get; init; }
    public string SqlLiteDb { get; init; }
    public string SQLServerDb { get; init; }
    public string PostgreSqlDb { get; init; }
}

public class CacheSetting
{
    public int SlidingExpiration { get; init; }
}

public class IpRateLimit
{
    public bool EnableEndpointRateLimiting { get; set; }
    public bool StackBlockedRequests { get; set; }
    public string RealIpHeader { get; set; }
    public string ClientIdHeader { get; set; }
    public long HttpStatusCode { get; set; }
    //public List<string> IpWhitelist { get; set; }
    //public List<string> EndpointWhitelist { get; set; }
    //public List<string> ClientWhitelist { get; set; }
    public List<GeneralRule> GeneralRules { get; set; }

}

public partial class GeneralRule
{
    public string Endpoint { get; set; }
    public string Period { get; set; }
    public long Limit { get; set; }
}