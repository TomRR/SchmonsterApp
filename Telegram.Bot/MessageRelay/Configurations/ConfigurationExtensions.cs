namespace MessageRelay.Configurations;

internal static class Configurations
{
    internal static IHost ConfigureHost(this HostBuilder host)
    {
        return host
            .AppSettingsConfiguration()
            .ConfigureServices((_, services) =>
            {
                services
                    .RegisterSettings()
                    .RegisterDependencies()
                    .RegisterClients();
            }).UseConsoleLifetime().Build();
    }
    
    private static IServiceCollection RegisterSettings(this IServiceCollection services)
    {
        services.AddOptions<TelegramBotClientSetting>()
            .BindConfiguration(TelegramBotClientSetting.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<GatewaySetting>()
            .BindConfiguration(GatewaySetting.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    private static IHostBuilder AppSettingsConfiguration(this IHostBuilder host)
    {
        return host
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    // .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });
    }
}