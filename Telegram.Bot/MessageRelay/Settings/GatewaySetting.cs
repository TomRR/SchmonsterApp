namespace MessageRelay.Settings;

internal sealed class GatewaySetting
{
    public const string ConfigurationSection = "GatewaySetting";
    
    [Required]
    [UrlAttribute]
    public string Uri { get; init; } = default!;
    
    [Required]
    [UrlAttribute]
    public string RequestUri { get; init; } = default!;
    
    public string ApiKey { get; init; } = default!;
}