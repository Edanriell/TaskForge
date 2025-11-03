namespace TaskForge.Api.Settings;

public sealed class CorsOptions
{
    public const string PolicyName = "TaskForgeCorsPolicy";
    public const string SectionName = "Cors";

    public required string[] AllowedOrigins { get; init; }
}