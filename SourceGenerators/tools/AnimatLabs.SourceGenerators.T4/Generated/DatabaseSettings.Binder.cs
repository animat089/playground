namespace AnimatLabs.SourceGenerators.T4.Generated;

public static class DatabaseSettingsBinder
{
    public static DatabaseSettings Bind(global::Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new global::System.ArgumentNullException(nameof(configuration));
        }

        var section = configuration.GetSection("Database");
        return BindSection(section);
    }

    private static DatabaseSettings BindSection(global::Microsoft.Extensions.Configuration.IConfiguration section)
    {
        var result = new DatabaseSettings();

        var valueConnectionString = section["ConnectionString"];
        if (!string.IsNullOrEmpty(valueConnectionString))
        {
            result.ConnectionString = valueConnectionString!;
        }

        if (int.TryParse(section["Timeout"], out var valueTimeout))
        {
            result.Timeout = valueTimeout;
        }

        result.Retry = BindRetry(section.GetSection("Retry"));

        return result;
    }

    private static RetrySettings BindRetry(global::Microsoft.Extensions.Configuration.IConfiguration section)
    {
        var result = new RetrySettings();

        if (int.TryParse(section["MaxAttempts"], out var valueMaxAttempts))
        {
            result.MaxAttempts = valueMaxAttempts;
        }

        if (int.TryParse(section["DelayMs"], out var valueDelayMs))
        {
            result.DelayMs = valueDelayMs;
        }

        return result;
    }
}
