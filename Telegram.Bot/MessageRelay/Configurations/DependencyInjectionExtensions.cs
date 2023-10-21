namespace MessageRelay.Configurations;

internal static class DependencyInjectionExtensions
{
    internal static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        return services
            // StartUp Init
            .AddSingleton<MessageHandler>()
            // Services
            .AddTransient<IMessageService, MessageService>();
    }
}