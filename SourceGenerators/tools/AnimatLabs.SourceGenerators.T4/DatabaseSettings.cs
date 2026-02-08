namespace AnimatLabs.SourceGenerators.T4.Generated;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public int Timeout { get; set; } = 30;
    public RetrySettings Retry { get; set; } = new();
}

public class RetrySettings
{
    public int MaxAttempts { get; set; } = 3;
    public int DelayMs { get; set; } = 1000;
}
