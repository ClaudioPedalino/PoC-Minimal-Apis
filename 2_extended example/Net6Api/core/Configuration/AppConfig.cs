public class AppConfig
{
    public DatabaseConfig DatabaseConfig { get; init; }
    public JwtSettings JwtSettings { get; init; }
    public ResilienceConfig ResilienceConfig { get; init; }
    public Api Api { get; init; }
    public CacheSetting CacheSetting { get; init; }
    public string CoinMarketCap { get; init; }
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
}

public class CacheSetting
{
    public int SlidingExpiration { get; init; }
}