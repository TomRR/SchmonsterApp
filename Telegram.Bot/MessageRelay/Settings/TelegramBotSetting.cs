namespace MessageRelay.Settings;

internal sealed class TelegramBotClientSetting
{
    public const string ConfigurationSection = "TelegramBotClientSetting";
    
    [Required]
    public string Token { get; init; } = default!;

    [Required] 
    public long ChatId { get; init; } = default!;
}