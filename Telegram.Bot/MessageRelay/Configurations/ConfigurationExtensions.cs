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
            // .ConfigureHostConfiguration(configHost => configHost.AddEnvironmentVariables())
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.SetBasePath(env.ContentRootPath)
                    .AddEnvironmentVariables()
                    .AddJsonFileConfig(env);
            });
    }

    private static IConfigurationBuilder AddJsonFileConfig(this IConfigurationBuilder builder, IHostEnvironment env)
    {
        return env.IsDevelopment()
            ? builder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            : builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    }
}